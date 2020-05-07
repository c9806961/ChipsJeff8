using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace ChipsJeff8
{
    class Chip8CPU
    {
        const ushort STARTINGPC = 512; // program code starts at 512
        public byte[] ram;
        //registers
        public ushort I;
        public byte[] V;
        public byte DT;
        public byte ST;
        public ushort PC;
        public ushort[] stack;
        public byte SP;
        Random rand;
        //graphics
        const int screenWidth = 64;
        const int screenHeight = 32;
        public bool[,] screenBuff = new bool[screenHeight, screenWidth];
        public bool needsRedraw;
        public List<byte> keysPressed;


        public Chip8CPU()
        {
            ram = new byte[4096];
            PC = STARTINGPC;
            I = 0;
            V = new byte[16]; // 16 V registers
            DT = 0;
            ST = 0;
            stack = new ushort[16]; // 16 value stack
            SP = 0;
            rand = new Random();
            LoadFonts();
            needsRedraw = true;
            keysPressed = new List<byte>();
        }
        public bool LoadRom(string fileName)
        {
            try
            {
                using (BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open)))
                {
                    while (reader.BaseStream.Position < reader.BaseStream.Length)
                    {
                        ram[PC] = reader.ReadByte();
                        PC++;
                    }
                    PC = STARTINGPC; // reset PC
                }
            }
            catch (IOException)
            {
                return false;
            }
            return true;
        }
        public bool LoadSaveState(string fileName)
        {
            try
            {
                using (BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open)))
                {
                    while (reader.BaseStream.Position < reader.BaseStream.Length)
                    {
                        I = reader.ReadUInt16();
                        for (int b = 0; b < V.Length; b++)
                            V[b] = reader.ReadByte();
                        DT = reader.ReadByte();
                        ST = reader.ReadByte();
                        PC = reader.ReadUInt16();
                        for (int b = 0; b < stack.Length; b++)
                            stack[b] = reader.ReadUInt16();
                        SP = reader.ReadByte();
                        for (int b = 0; b < ram.Length; b++)
                            ram[b] = reader.ReadByte();
                        for (int i = 0; i < screenHeight; i++)
                            for (int j = 0; j < screenWidth; j++)
                                screenBuff[i, j] = reader.ReadBoolean();
                    }
                }
            }
            catch (IOException)
            {
                return false;
            }
            return true;
        }
        public bool WriteSaveSate(string fileName)
        {
 
            try
            {
                using (BinaryWriter writer = new BinaryWriter(File.Open(fileName, FileMode.Create)))
                {
                    writer.Write((ushort)I);
                    Console.WriteLine("I " + I);
                    for (int b = 0; b < V.Length; b++)
                        writer.Write(V[b]);
                    writer.Write(DT);
                    writer.Write(ST);
                    writer.Write((ushort)PC);
                    for (int b = 0; b < stack.Length; b++)
                        writer.Write((ushort)stack[b]);
                    writer.Write(SP);
                    for (int b = 0; b < ram.Length; b++)
                        writer.Write(ram[b]);
                    for (int i = 0; i < screenHeight; i++)
                        for (int j = 0; j < screenWidth; j++)
                            writer.Write(screenBuff[i, j]);
                    writer.Close();
                }
            }
            catch (IOException)
            {
                return false;
            }
            return true;
        }

        public void ExecuteInstruction()
        {
            // get the next opcode and components of it
            ushort opcode = (ushort)(ram[PC] << 8 | ram[PC + 1]);
            byte firstFourBits = (byte)(opcode >> 12);
            byte  lastFourBits = (byte)(opcode >> 0 & 0X000F);
            ushort nnn = (ushort)(opcode >> 0 & 0X0FFF);
            byte n = (byte)(opcode >> 0 & 0X000F);
            byte x = (byte)(opcode >> 8 & 0X000F);
            byte y = (byte)(opcode >> 4 & 0X000F);
            byte kk = (byte)(opcode >> 0 & 0X00FF);
            if (opcode == 0x00E0)  //CLS - 00E0
            {
                // clear screen
                for (int i = 0; i < screenHeight; i++)
                    for (int j = 0; j < screenWidth; j++)
                        screenBuff[i, j] = false;
                needsRedraw = true;
                PC += 2;
            }
            else if (opcode == 0x00EE) //RET - 00EE
            {
                SP--;
                PC = stack[SP];
                PC += 2;
            }
            else if (firstFourBits == 1) //JP addr - 1nnn
            {
                // jump to nnn
                PC = nnn;
            }
            else if (firstFourBits == 2) //CALL addr - 2nnn
            {
                // goto sub
                stack[SP] = PC;
                SP++;
                PC = nnn;

            }
            else if (firstFourBits == 3) // SE vx, byte - 3xkk
            {
                // if Vx == kk skip
                if (V[x] == kk)
                {
                    PC += 2;
                }
                PC += 2;
            }
            else if (firstFourBits == 4) // SNE vx, byte - 4xkk
            {
                // if Vx != kk skip
                if (V[x] != kk)
                {
                    PC += 2;
                }
                PC += 2;
            }
            else if (firstFourBits == 5) // SE Vx, Vy - 5xy0
            {
                // if Vx == Vy skip
                if (V[x] == V[y])
                {
                    PC += 2;
                }
                PC += 2;
            }
            else if (firstFourBits == 6) // LD vx, byte- 6xkk
            {
                // store kk in Vx
                V[x] = kk;
                PC += 2;
            }
            else if (firstFourBits == 7) // ADD Vx, byte- 7xkk
            {
                // add kk to Vx, store in Vx
                V[x] = (byte)(V[x] + kk);
                PC += 2;
            }
            else if (firstFourBits == 8) // Arithmatic
            {
                if (lastFourBits == 0) // LD Vx, Vy - 8xy0
                {
                    // Store Vy in Vx
                    V[x] = V[y];
                }
                else if (lastFourBits == 1) // OR Vx, Vy - 8xy1
                {
                    // Bitwise VX OR Vy , store in  Vx
                    V[x] = (byte)(V[x] | V[y]);
                }
                else if (lastFourBits == 2) // AND Vx, Vy - 8xy2
                {
                    // Bitwise VX AND Vy , store in  Vx
                    V[x] = (byte)(V[x] & V[y]);
                }
                else if (lastFourBits == 3) // XOR Vx, Vy - 8xy3
                {
                    // Bitwise VX XOR Vy , store in  Vx
                    V[x] = (byte)(V[x] ^ V[y]);
                }
                else if (lastFourBits == 4) // ADD Vx, Vy - 8xy4
                {
                    // The values of Vx and Vy are added together. If the result is greater than 8 bits (i.e., > 255,) VF is set to 1, otherwise 0. Only the lowest 8 bits of the result are kept, and stored in Vx.
                    V[0xF] = (byte)(V[x] + V[y] > 0xFF ? 1 : 0);
                    V[x] += V[y];
                }
                else if (lastFourBits == 5) // SUB Vx, Vy - 8xy5
                {
                    //  If Vx > Vy, then VF is set to 1, otherwise 0.Then Vy is subtracted from Vx, and the results stored in Vx.
                    V[0xF] = (byte)(V[x] > V[y] ? 1 : 0);
                    V[x] -= V[y];
                }
                else if (lastFourBits == 6) // SHR Vx{, Vy} - 8xy6
                {
                    //If the least - significant bit of Vx is 1, then VF is set to 1, otherwise 0.Then Vx is divided by 2. 0x1 = 1 = 00000001
                    V[0xF] = (byte)(((V[x] & 0x1) == 0x1) ? 1 : 0);
                    V[x] = (byte)(V[x] >> 1);
                }
                else if (lastFourBits == 7) // SUBN Vx, Vy - 8xy7
                {
                    //If Vy > Vx, then VF is set to 1, otherwise 0.Then Vx is subtracted from Vy, and the results stored in Vx.
                    V[0xF] = (byte)(V[y] > V[x] ? 1 : 0);
                    V[x] = (byte)(V[y] - V[x]);
                }
                else if (lastFourBits == 0xE) // SHL Vx{, Vy} - 8xyE
                {
                    //If the most - significant bit of Vx is 1, then VF is set to 1, otherwise to 0.Then Vx is multiplied by 2. 0x80 + 128 = 10000000
                    V[0xF] = (byte)(((V[x] & 0x80) == 0x80) ? 1 : 0);
                    V[x] = (byte)(V[x] << 1);
                }
                PC += 2;
            }
            else if (firstFourBits == 9) // SNE Vx, Vy - 9xy0
            {
                //The values of Vx and Vy are compared, and if they are not equal, the program counter is increased by 2.
                if (V[x] != V[y])
                    PC += 2;
                PC += 2;
            }
            else if (firstFourBits == 0xA) // LD I, addr - Annn
            {
                // The value of register I is set to nnn.
                I = nnn;
                PC += 2;
            }
            else if (firstFourBits == 0xB) // JMP V0, addr - Bnnn
            {
                //The program counter is set to nnn plus the value of V0.
                PC = (ushort)(nnn + V[0]);
            }
            else if (firstFourBits == 0xC) // RND Vx, byte - Cxkk
            {
                //The interpreter generates a random number from 0 to 255, which is then ANDed with the value kk. The results are stored in Vx.See instruction 8xy2 for more information on AND.
                V[x] = (byte)(rand.Next(256) & kk);
                PC += 2;
            }
            else if (firstFourBits == 0xD) // DRW Vx, Vy, nibble - Dxyn
            {

                //The interpreter reads n bytes from memory, starting at the address stored in I. These bytes are then displayed as sprites on screen at coordinates (Vx, Vy). Sprites are XORed onto the existing screen. If this causes any pixels to be erased, VF is set to 1, otherwise it is set to 0. If the sprite is positioned so part of it is outside the coordinates of the display, it wraps around to the opposite side of the screen. See instruction 8xy3 for more information on XOR, and section 2.4, Display, for more information on the Chip-8 screen and sprites.                
                V[0xF] = 0;
                byte[] sprite = new byte[n];
                Array.Copy(ram, I, sprite, 0, n);
                byte Y;
                byte X;
                for (int line = 0; line < sprite.Length; line++)
                {
                    for (int pixel = 0; pixel < 8; pixel++)
                    {

                        Y = (byte)((V[y]  + line) % screenHeight);
                        X = (byte)((V[x]  + pixel) % screenWidth);
                        bool oldPixel = screenBuff[Y, X];
                        if (((byte)((sprite[line] >> 7 - pixel) & 1)) == 1)
                        {
                            screenBuff[Y, X] = screenBuff[Y, X] ^ true;
                        }
                        else
                        {
                            screenBuff[Y, X] = screenBuff[Y, X] ^ false;
                        }
                        bool newPixel = screenBuff[Y, X];
                        if (oldPixel & !newPixel)
                            V[0xF] = 1;

                    }
                }
                needsRedraw = true;
                PC += 2;
        }
            else if (firstFourBits == 0xE) // input 
            {
                if (kk == 0x9E)  // SKP Vx - Ex9E
                {
                    // Checks the keyboard, and if the key corresponding to the value of Vx is currently in the down position, PC is increased by 2.
                    if (keysPressed.Contains(V[x]))
                        PC += 2;
                }
                else if (kk == 0xA1) // SKNP Vx - ExA1
                {
                    // Checks the keyboard, and if the key corresponding to the value of Vx is currently in the up position, PC is increased by 2.
                    if (!keysPressed.Contains(V[x]))
                        PC += 2;
                }
                PC += 2;
            }
            else if (firstFourBits == 0xF) // F
            {
                if (kk == 0x07) // LD Vx, DT - Fx07
                {
                    //The value of DT is placed into Vx.
                    V[x] = DT;
                    PC += 2;
                }
                else if (kk == 0x0A) // LD Vx, K - Fx0A
                {
                    //All execution stops until a key is pressed, then the value of that key is stored in Vx.
                    if (keysPressed.Count >= 1)
                    {
                        V[x] = keysPressed[0];
                        PC += 2;
                    }
                }
                else if (kk == 0x15) // LD DT, Vx - Fx15
                {
                    //DT is set equal to the value of Vx.
                    DT = V[x];
                    PC += 2;
                }
                else if (kk == 0x18) // LD ST, Vx - Fx18
                {
                    //ST is set equal to the value of Vx.
                    ST = V[x];
                    PC += 2;
                }
                else if (kk == 0x1E) // ADD I, Vx - Fx1E
                {
                    //The values of I and Vx are added, and the results are stored in I.
                    I += V[x];
                    PC += 2;
                }
                else if (kk == 0x29) // LD F, Vx - Fx29
                {
                    //The value of I is set to the location for the hexadecimal sprite corresponding to the value of Vx.See section 2.4, Display, for more information on the Chip - 8 hexadecimal font.
                    // Fonts are in 1 = 0x0000 , 2 = 0x0005 ... F = 0x004B ie location of n = n*5
                    I = (ushort)(V[x] * 5);
                    PC += 2;
                }
                else if (kk == 0x33) //LD B, Vx - Fx33
                {
                    //The interpreter takes the decimal value of Vx, and places the hundreds digit in memory at location in I, the tens digit at location I+1, and the ones digit at location I + 2.
                    ram[I] = (byte)((V[x] / 100) % 10);
                    ram[I + 1] = (byte)((V[x] / 10) % 10);
                    ram[I + 2] = (byte)(V[x] % 10);
                    PC += 2;
                }
                else if (kk == 0x55) //LD[I], Vx - Fx55
                {
                    //The interpreter copies the values of registers V0 through Vx into memory, starting at the address in I.
                    for (int count = 0; count <= x; count++)
                    {
                        ram[I + count] = V[count];
                    }
                    PC += 2;
                }
                else if (kk == 0x65) // LD Vx, [I] - Fx65
                {
                    //The interpreter reads values from memory starting at location I into registers V0 through Vx.
                    for (int count = 0; count <= x; count++)
                    {
                        V[count] = ram[I + count];
                    }
                    PC += 2;
                }
            }
        }

        public void CheckTimers()
        {
            if (DT > 0)
                DT--;
            if (ST > 0)
                ST--;
        }

        private void LoadFonts()
        {
            byte[] fonts = 
            { 0xF0, 0x90, 0x90, 0x90, 0xF0 // 0
             ,0x20, 0x60, 0x20, 0x20, 0x70 // 1
             ,0xF0, 0x10 ,0xF0, 0x80, 0xF0 // 2
             ,0xF0, 0x10, 0xF0, 0x10, 0xF0 // 3
             ,0x90, 0x90 ,0xF0, 0x10, 0x10 // 4
             ,0xF0, 0x80, 0xF0, 0x10, 0xF0 // 5
             ,0xF0, 0x80, 0xF0, 0x90, 0xF0 // 6
             ,0xF0, 0x10, 0x20, 0x40, 0x40 // 7
             ,0xF0, 0x90, 0xF0, 0x90, 0xF0 // 8
             ,0xF0, 0x90, 0xF0, 0x10, 0xF0 // 9
             ,0xF0, 0x90, 0xF0 ,0x90, 0x90 // A
             ,0xE0, 0x90, 0xE0, 0x90, 0xE0 // B
             ,0xF0, 0x80, 0x80, 0x80, 0xF0 // C
             ,0xE0, 0x90, 0x90, 0x90, 0xE0 // D
             ,0xF0, 0x80, 0xF0, 0x80, 0xF0 // E
             ,0xF0, 0x80, 0xF0, 0x80, 0x80 // F
            };
            Array.Copy(fonts, ram, fonts.Length);
        }
    }
}