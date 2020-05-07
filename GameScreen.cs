using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChipsJeff8
{
    public partial class GameScreen : Form
    {
        Chip8CPU cpu;
        // ui stuff
        int scale;
        Color pixelColour;
        private Panel gamePanel;
        int cycleFreq = 500;
        float speedMultiplyer = 1;
        bool paused;
        bool running;
        bool loadStatePending;
        bool saveStatePending;
        Task gameTask;
        string fileName;
        string saveStateFilename;
        Dictionary<Keys, byte> keyMap = new Dictionary<Keys, byte>()
                                            {
                                                {Keys.D1, 0x1},
                                                {Keys.D2, 0x2},
                                                {Keys.D3, 0x3},
                                                {Keys.D4, 0xC},
                                                {Keys.Q, 0x4},
                                                {Keys.W, 0x5},
                                                {Keys.E, 0x6},
                                                {Keys.R, 0xD},
                                                {Keys.A, 0x7},
                                                {Keys.S, 0x8},
                                                {Keys.D, 0x9},
                                                {Keys.F, 0xE},
                                                {Keys.Z, 0xA},
                                                {Keys.X, 0x0},
                                                {Keys.C, 0xB},
                                                {Keys.V, 0xF}
                                            };
        public GameScreen()
        {
            InitializeComponent();
            //default colour
            scale = 16;
            pixelColour = Color.Green;
            gamePanel = new Panel();
            this.Controls.Add(gamePanel);
            // window settings
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            ScaleUI();
            this.KeyDown += SetKeyDown;
            this.KeyUp += SetKeyUp;
            paused = false;
            running = false;
            loadStatePending = false;
            saveStatePending = false;
            // menu actions
            // rom menu
            romExitMenuItem.Click += ExitGame;
            romResetItem.Click += ResetRom;
            romOpenItem.Click += LoadRom;
            //screen menu
            screen1xItem.Click += ChangeScale;
            screen2xItem.Click += ChangeScale;
            screen4xItem.Click += ChangeScale;
            screenPixelColourItem.Click += ChangePixelColour;
            //speed menu
            speedPauseItem.Enabled = false;
            speedPauseItem.Click += PauseGame;
            speedPoint25.Click += ChangeSpeed;
            speedPoint5.Click += ChangeSpeed;
            speed1.Click += ChangeSpeed;
            speed2.Click += ChangeSpeed;
            speed4.Click += ChangeSpeed;
            // save state menu
            saveStateSaveItem.Click += SaveSaveState;
            saveStateLoadItem.Click += LoadSaveState;
            saveStateSaveItem.Enabled = false;
            saveStateLoadItem.Enabled = false;
            // help menu
            helpAbout.Click += ShowAbout;
            helpGuide.Click += ShowGuide;
        }

        void ScaleUI()
        {
            gamePanel.Location = new Point(0, menuStrip1.Height);
            gamePanel.Width = 64 * scale;
            gamePanel.Height = 32 * scale;
            this.ClientSize = new Size(gamePanel.Width, gamePanel.Height + menuStrip1.Height + statusStrip1.Height);
        }
        protected override void OnLoad(EventArgs e)
        {
            //StartGame();
        }

        void StartGame()
        {
            running = false;
            if (gameTask != null)
            {
                while (!gameTask.IsCompleted)
                {
                    // requires to ensure old game is dead in the case of a reset
                }
                gameTask.Dispose();
            }
            saveStateSaveItem.Enabled = true;
            if (File.Exists(saveStateFilename))
                saveStateLoadItem.Enabled = true;
            // create CPU
            paused = false;
            speedPauseItem.Text = "Pause";
            saveStateSaveItem.Enabled = true;
            cpu = new Chip8CPU();
            Console.WriteLine("Emulator started.");
            // Loadrom and run CPU
            if (loadStatePending)
            {
                loadStatePending = false;
                if (cpu.LoadSaveState(saveStateFilename))
                {
                    gameTask = Task.Run(GameLoop);
                }
            }
            else
            {
                if (cpu.LoadRom(fileName))
                {
                    gameTask = Task.Run(GameLoop);
                }
            }
        }
        void SetKeyDown(object sender, KeyEventArgs e)
        {
            if (keyMap.ContainsKey(e.KeyCode))
            {
                if (!cpu.keysPressed.Contains(keyMap[e.KeyCode]))
                    cpu.keysPressed.Add(keyMap[e.KeyCode]);
            }
            if (e.KeyCode == Keys.Space)
            {
                PauseGame(null, null);
            }
        }
        void SetKeyUp(object sender, KeyEventArgs e)
        {
            if (keyMap.ContainsKey(e.KeyCode))
            {
                if (cpu.keysPressed.Contains(keyMap[e.KeyCode]))
                    cpu.keysPressed.Remove(keyMap[e.KeyCode]);
            }
        }
        private void GameScreen_Load(object sender, EventArgs e)
        {
        }
        public void Draw(bool[,] buff)
        {

            SolidBrush myBrush = new SolidBrush(pixelColour);
            Font myFont = new Font("Arial", 2 * scale);
            BufferedGraphicsContext currentContext;
            BufferedGraphics myBuffer;
            currentContext = BufferedGraphicsManager.Current;
            myBuffer = currentContext.Allocate(gamePanel.CreateGraphics(), gamePanel.DisplayRectangle);
            myBuffer.Graphics.Clear(Color.Black);
            Graphics g = myBuffer.Graphics;
            if (paused)
            {
                Rectangle rect1 = new Rectangle(0, 0, gamePanel.Width, gamePanel.Height);
                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Center;
                stringFormat.LineAlignment = StringAlignment.Center;
                g.DrawString("PAUSED", myFont, myBrush, rect1, stringFormat);
                myBrush.Color = Color.FromArgb(25, pixelColour);
            }
            for (int y = 0; y < buff.GetLength(0) & y < gamePanel.Width; y++)
                for (int x = 0; x < buff.GetLength(1) & x < gamePanel.Height; x++)
                    if (buff[y, x])
                        g.FillRectangle(myBrush, new Rectangle(x * scale + 1, y * scale + 1, scale - scale / 8, scale - scale / 8));
            myBuffer.Render();
            myBrush.Dispose();
            myBuffer.Dispose();
            cpu.needsRedraw = false;
        }
        //private delegate void SafeCallDelegate();
        private void GameLoop()
        {
            running = true;
            Stopwatch tickTimer = new Stopwatch();
            Stopwatch tick60Timer = new Stopwatch();
            tickTimer.Start();
            tick60Timer.Start();
            while (running)
            {
                if (!paused)
                {
                    if (saveStatePending)
                    {
                        cpu.WriteSaveSate(saveStateFilename);
                        saveStatePending = false;
                    }
                    tickTimer.Stop();
                    if (tickTimer.Elapsed.Ticks >= 10000000 / cycleFreq / speedMultiplyer) // maybe move this to ticks
                    {
                        Task.Run(cpu.ExecuteInstruction);
                        tickTimer.Reset();
                    }
                    tickTimer.Start();
                    tick60Timer.Stop();
                    if (tick60Timer.Elapsed.Ticks >= 10000000 / 60 / speedMultiplyer) // maybe move this to ticks?
                    {
                        Task.Run(cpu.CheckTimers);
                        tick60Timer.Reset();
                    }
                    tick60Timer.Start();
                    //draw graphics
                    if (cpu.needsRedraw)
                        Draw(cpu.screenBuff);
                    //do sound
                    if (cpu.ST > 0)
                    {
                        Console.Beep(200,cpu.ST*16); // consider a task for long beeps
                        cpu.ST = 0;
                    }
                }
            }
        }
        void ExitGame(object sender, System.EventArgs e)
        {
            Application.Exit();
        }

        void ChangeScale(Object sender, System.EventArgs e)
        {
            if (sender == screen1xItem)
                scale = 8;
            else if (sender == screen2xItem)
                scale = 16;
            else if (sender == screen4xItem)
                scale = 32;
            statusSize.Text = ((ToolStripMenuItem)sender).Text;
            ScaleUI();
            if (cpu != null)
                Draw(cpu.screenBuff);
        }

        void ResetRom(Object sender, System.EventArgs e)
        {
            if (fileName != null)
              StartGame();
        }

        void LoadRom(Object sender, System.EventArgs e)
        {
            // enable menu items that were disbaled
            speedPauseItem.Enabled = true;
            bool wasPaused = paused;
            if (!wasPaused)
                PauseGame(null, null);
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (fileName == null)
                openFileDialog.InitialDirectory = "c:\\";
            else
                openFileDialog.InitialDirectory = fileName.Substring(0, fileName.LastIndexOf('\\'));
            openFileDialog.Filter = "chip 8 roms (*.ch8)|*.ch8";
            openFileDialog.FilterIndex = 0;
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                fileName = openFileDialog.FileName;
                saveStateFilename = fileName + ".save";
                StartGame();
            }
            else
            {
                if (!wasPaused)
                    PauseGame(null, null);
            }
            Text = "ChipsJeff8 - " + fileName;
        }
        void ChangePixelColour(Object sender, System.EventArgs e)
        {
            bool wasPaused = paused;
            if (!wasPaused)
                PauseGame(null, null);
            ColorDialog MyDialog = new ColorDialog();
            MyDialog.AllowFullOpen = false;
            MyDialog.Color = pixelColour;
            if (MyDialog.ShowDialog() == DialogResult.OK)
            {
                pixelColour = MyDialog.Color;
                Draw(cpu.screenBuff);
            }
            if (!wasPaused)
                PauseGame(null, null);
        }
        void PauseGame(Object sender, System.EventArgs e)
        {
            paused = !paused;
            if (paused)
            {
                speedPauseItem.Text = "Resume";
            }
            else
            {
                speedPauseItem.Text = "Pause";
            }
            if (cpu != null)
                Draw(cpu.screenBuff);
        }

        void ChangeSpeed(Object sender, System.EventArgs e)
        {
            if (sender == speedPoint25)
                speedMultiplyer = 0.25f;
            else if (sender == speedPoint5)
                speedMultiplyer = 0.5f;
            else if (sender == speed1)
                speedMultiplyer = 1;
            else if (sender == speed2)
                speedMultiplyer = 2;
            else if (sender == speed4)
                speedMultiplyer = 4;
            statusSpeed.Text = ((ToolStripMenuItem)sender).Text;
        }

        void LoadSaveState(Object sender, System.EventArgs e)
        {
            loadStatePending = true;
            StartGame();
        }
        void SaveSaveState(Object sender, System.EventArgs e)
        {
            saveStatePending = true;
            saveStateLoadItem.Enabled = true;
        }
        void ShowAbout(Object sender, System.EventArgs e)
        {
            About a = new About();
            a.FormBorderStyle = FormBorderStyle.FixedSingle;
            a.MaximizeBox = false;
            a.Show();
        }
        void ShowGuide(Object sender, System.EventArgs e)
        {
            Guide g = new Guide();
            g.FormBorderStyle = FormBorderStyle.FixedSingle;
            g.MaximizeBox = false;
            g.Show();
        }
    }
}
