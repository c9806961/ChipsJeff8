namespace ChipsJeff8
{
    partial class GameScreen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameScreen));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.romMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.romOpenItem = new System.Windows.Forms.ToolStripMenuItem();
            this.romResetItem = new System.Windows.Forms.ToolStripMenuItem();
            this.romExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.screenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.screen1xItem = new System.Windows.Forms.ToolStripMenuItem();
            this.screen2xItem = new System.Windows.Forms.ToolStripMenuItem();
            this.screen4xItem = new System.Windows.Forms.ToolStripMenuItem();
            this.screenPixelColourItem = new System.Windows.Forms.ToolStripMenuItem();
            this.speedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.speedPoint25 = new System.Windows.Forms.ToolStripMenuItem();
            this.speedPoint5 = new System.Windows.Forms.ToolStripMenuItem();
            this.speed1 = new System.Windows.Forms.ToolStripMenuItem();
            this.speed2 = new System.Windows.Forms.ToolStripMenuItem();
            this.speed4 = new System.Windows.Forms.ToolStripMenuItem();
            this.speedPauseItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveStateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveStateSaveItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveStateLoadItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.helpGuide = new System.Windows.Forms.ToolStripMenuItem();
            this.helpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusSpeed = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusSize = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.romMenu,
            this.screenToolStripMenuItem,
            this.speedToolStripMenuItem,
            this.saveStateToolStripMenuItem,
            this.helpMenu});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // romMenu
            // 
            this.romMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.romOpenItem,
            this.romResetItem,
            this.romExitMenuItem});
            this.romMenu.Name = "romMenu";
            this.romMenu.Size = new System.Drawing.Size(44, 20);
            this.romMenu.Text = "Rom";
            // 
            // romOpenItem
            // 
            this.romOpenItem.Name = "romOpenItem";
            this.romOpenItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.romOpenItem.Size = new System.Drawing.Size(152, 22);
            this.romOpenItem.Text = "Open..";
            // 
            // romResetItem
            // 
            this.romResetItem.Name = "romResetItem";
            this.romResetItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.romResetItem.Size = new System.Drawing.Size(152, 22);
            this.romResetItem.Text = "Reset";
            // 
            // romExitMenuItem
            // 
            this.romExitMenuItem.Name = "romExitMenuItem";
            this.romExitMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.romExitMenuItem.Size = new System.Drawing.Size(152, 22);
            this.romExitMenuItem.Text = "Quit";
            // 
            // screenToolStripMenuItem
            // 
            this.screenToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.screen1xItem,
            this.screen2xItem,
            this.screen4xItem,
            this.screenPixelColourItem});
            this.screenToolStripMenuItem.Name = "screenToolStripMenuItem";
            this.screenToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.screenToolStripMenuItem.Text = "Screen";
            // 
            // screen1xItem
            // 
            this.screen1xItem.Name = "screen1xItem";
            this.screen1xItem.Size = new System.Drawing.Size(138, 22);
            this.screen1xItem.Text = "0.5x Size";
            // 
            // screen2xItem
            // 
            this.screen2xItem.Name = "screen2xItem";
            this.screen2xItem.Size = new System.Drawing.Size(138, 22);
            this.screen2xItem.Text = "1x Size";
            // 
            // screen4xItem
            // 
            this.screen4xItem.Name = "screen4xItem";
            this.screen4xItem.Size = new System.Drawing.Size(138, 22);
            this.screen4xItem.Text = "2x Size";
            // 
            // screenPixelColourItem
            // 
            this.screenPixelColourItem.Name = "screenPixelColourItem";
            this.screenPixelColourItem.Size = new System.Drawing.Size(138, 22);
            this.screenPixelColourItem.Text = "Pixel Colour";
            // 
            // speedToolStripMenuItem
            // 
            this.speedToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.speedPoint25,
            this.speedPoint5,
            this.speed1,
            this.speed2,
            this.speed4,
            this.speedPauseItem});
            this.speedToolStripMenuItem.Name = "speedToolStripMenuItem";
            this.speedToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.speedToolStripMenuItem.Text = "Speed";
            // 
            // speedPoint25
            // 
            this.speedPoint25.Name = "speedPoint25";
            this.speedPoint25.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.speedPoint25.Size = new System.Drawing.Size(155, 22);
            this.speedPoint25.Text = "0.25x Speed";
            // 
            // speedPoint5
            // 
            this.speedPoint5.Name = "speedPoint5";
            this.speedPoint5.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.speedPoint5.Size = new System.Drawing.Size(155, 22);
            this.speedPoint5.Text = "0.5x Speed";
            // 
            // speed1
            // 
            this.speed1.Name = "speed1";
            this.speed1.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.speed1.Size = new System.Drawing.Size(155, 22);
            this.speed1.Text = "1x Speed";
            // 
            // speed2
            // 
            this.speed2.Name = "speed2";
            this.speed2.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.speed2.Size = new System.Drawing.Size(155, 22);
            this.speed2.Text = "2x Speed";
            // 
            // speed4
            // 
            this.speed4.Name = "speed4";
            this.speed4.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.speed4.Size = new System.Drawing.Size(155, 22);
            this.speed4.Text = "4x Speed";
            // 
            // speedPauseItem
            // 
            this.speedPauseItem.Name = "speedPauseItem";
            this.speedPauseItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.speedPauseItem.Size = new System.Drawing.Size(155, 22);
            this.speedPauseItem.Text = "Pause";
            // 
            // saveStateToolStripMenuItem
            // 
            this.saveStateToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveStateSaveItem,
            this.saveStateLoadItem});
            this.saveStateToolStripMenuItem.Name = "saveStateToolStripMenuItem";
            this.saveStateToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.saveStateToolStripMenuItem.Text = "Save state";
            // 
            // saveStateSaveItem
            // 
            this.saveStateSaveItem.Name = "saveStateSaveItem";
            this.saveStateSaveItem.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.saveStateSaveItem.Size = new System.Drawing.Size(119, 22);
            this.saveStateSaveItem.Text = "Save";
            // 
            // saveStateLoadItem
            // 
            this.saveStateLoadItem.Name = "saveStateLoadItem";
            this.saveStateLoadItem.ShortcutKeys = System.Windows.Forms.Keys.F9;
            this.saveStateLoadItem.Size = new System.Drawing.Size(119, 22);
            this.saveStateLoadItem.Text = "Load";
            // 
            // helpMenu
            // 
            this.helpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpGuide,
            this.helpAbout});
            this.helpMenu.Name = "helpMenu";
            this.helpMenu.Size = new System.Drawing.Size(44, 20);
            this.helpMenu.Text = "Help";
            // 
            // helpGuide
            // 
            this.helpGuide.Name = "helpGuide";
            this.helpGuide.Size = new System.Drawing.Size(127, 22);
            this.helpGuide.Text = "Key Guide";
            // 
            // helpAbout
            // 
            this.helpAbout.Name = "helpAbout";
            this.helpAbout.Size = new System.Drawing.Size(127, 22);
            this.helpAbout.Text = "About";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusSpeed,
            this.statusSize});
            this.statusStrip1.Location = new System.Drawing.Point(0, 426);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 24);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusSpeed
            // 
            this.statusSpeed.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.statusSpeed.Name = "statusSpeed";
            this.statusSpeed.Size = new System.Drawing.Size(54, 19);
            this.statusSpeed.Text = "1x Speed";
            this.statusSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // statusSize
            // 
            this.statusSize.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.statusSize.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.statusSize.Name = "statusSize";
            this.statusSize.Size = new System.Drawing.Size(46, 19);
            this.statusSize.Text = "1x Size";
            this.statusSize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // GameScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "GameScreen";
            this.Text = "ChipsJeff8";
            this.Load += new System.EventHandler(this.GameScreen_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem romMenu;
        private System.Windows.Forms.ToolStripMenuItem romExitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem romOpenItem;
        private System.Windows.Forms.ToolStripMenuItem romResetItem;
        private System.Windows.Forms.ToolStripMenuItem screenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem screen1xItem;
        private System.Windows.Forms.ToolStripMenuItem screen2xItem;
        private System.Windows.Forms.ToolStripMenuItem screen4xItem;
        private System.Windows.Forms.ToolStripMenuItem screenPixelColourItem;
        private System.Windows.Forms.ToolStripMenuItem saveStateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveStateSaveItem;
        private System.Windows.Forms.ToolStripMenuItem saveStateLoadItem;
        private System.Windows.Forms.ToolStripMenuItem speedPauseItem;
        private System.Windows.Forms.ToolStripMenuItem speedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem speedPoint25;
        private System.Windows.Forms.ToolStripMenuItem speedPoint5;
        private System.Windows.Forms.ToolStripMenuItem speed1;
        private System.Windows.Forms.ToolStripMenuItem speed2;
        private System.Windows.Forms.ToolStripMenuItem speed4;
        private System.Windows.Forms.ToolStripMenuItem helpMenu;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusSpeed;
        private System.Windows.Forms.ToolStripStatusLabel statusSize;
        private System.Windows.Forms.ToolStripMenuItem helpGuide;
        private System.Windows.Forms.ToolStripMenuItem helpAbout;
    }
}

