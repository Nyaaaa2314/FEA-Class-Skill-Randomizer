using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace ClassSkillRandomizer
{
    public partial class Form1 : Form
    {
        public string selectedFileName = "";
        public string selectedFileName2 = "";
        public string curDir = Directory.GetCurrentDirectory();
        public Form1()
        {
            InitializeComponent();
            
        }

        public void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = "c:\\",
                Filter = "bin files (*.bin*)|*.bin*",
                FilterIndex = 0,
                RestoreDirectory = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                selectedFileName = openFileDialog1.FileName;
                richTextBox1.AppendText("\n GameData.bin selected successfully");
            }
        }

        public void button2_Click(object sender, EventArgs e)
        {
            RandomizerFunctions Func1 = new RandomizerFunctions();

            StreamWriter unpack = new StreamWriter("unpack.bat");
            unpack.WriteLine("asset_pack.rb -u " + selectedFileName);
            unpack.Close();
            FileInfo fi = new FileInfo(selectedFileName);
            string txtdir = fi.DirectoryName;
            Process.Start("unpack.bat");
            FileInfo GameDataTxt = new FileInfo(txtdir + @"\GameData.bin_data.txt");
            for (; ; )
            {
                
                if (File.Exists(txtdir + @"\GameData.bin_data.txt") == true)
                {
                   GameDataTxt.Refresh();
                   if(GameDataTxt.Length > 1500000)
                    {
                        if (IsFileLocked(GameDataTxt) == false)
                        {
                            break;
                        }
                        
                    }
                }
                continue;
            }

            //System.Threading.Thread.Sleep(5000);
            //richTextBox1.AppendText(txtdir + @"\GameData.bin_data.txt");
            //string line = File.ReadLines(txtdir + @"\GameData.bin_data.txt").Skip(14).Take(1).First();
            //richTextBox1.AppendText(line);
            //System.IO.File.Copy(selectedFileName, unpack, true);
            string[] Skills = {"01", "02", "03", "04", "05", "06", "07", "08", "09", "0A", "0B", "0C", "0D", "0E", "0F", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "1A", "1B", "1C", "1D", "1E", "1F", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "2A", "2B", "2C", "2D", "2E", "2F", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "3A", "3B", "3C", "3D", "3E", "3F", "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "4A", "4B", "4C", "4D", "4E", "4F", "50", "51", "52", "53", "54", "55", "56", "57"};
            string[] SkillsD = {"01", "02", "03", "04", "05", "06", "07", "08", "09", "0A", "0B", "0C", "0D", "0E", "0F", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "1A", "1B", "1C", "1D", "1E", "1F", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "2A", "2B", "2C", "2D", "2E", "2F", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "3A", "3B", "3C", "3D", "3E", "3F", "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "4A", "4B", "4C", "4D", "4E", "4F", "50", "51", "52", "53", "54", "55", "56", "57", "63", "64", "65", "66"};
            Encoding utf8WithoutBom = new UTF8Encoding(false);
            string[] GameData = File.ReadAllLines(txtdir + @"\GameData.bin_data.txt", utf8WithoutBom);
            string[] Classes = { "Lord", "Great Lord", "Tactician", "Grandmaster", "Cavalier", "Knight", "Paladin", "Great Knight", "General", "Barbarian", "Fighter", "Mercenary", "Archer", "Berserker", "Warrior", "Hero", "Bow Knight", "Sniper", "Myrmidon", "Thief", "Swordmaster", "Assassin", "Trickster", "Pegasus Knight", "Falcon Knight", "Dark Flier", "Wyvern Rider", "Wyvern Lord", "Griffon Rider", "Troubadour", "Priest/Cleric", "Mage", "Dark Mage", "Valkyrie", "War Monk/ War Cleric", "Sage", "Dark Knight", "Sorcerer", "Dancer", "Manakete", "Taguel", "Villager" };
            string[] ClassesD = { "Lord", "Great Lord", "Tactician", "Grandmaster", "Cavalier", "Knight", "Paladin", "Great Knight", "General", "Barbarian", "Fighter", "Mercenary", "Archer", "Berserker", "Warrior", "Hero", "Bow Knight", "Sniper", "Myrmidon", "Thief", "Swordmaster", "Assassin", "Trickster", "Pegasus Knight", "Falcon Knight", "Dark Flier", "Wyvern Rider", "Wyvern Lord", "Griffon Rider", "Troubadour", "Priest/Cleric", "Mage", "Dark Mage", "Valkyrie", "War Monk/ War Cleric", "Sage", "Dark Knight", "Sorcerer", "Dancer", "Manakete", "Taguel", "Villager", "Dread Fighter", "Bride"};
            if (checkBox1.Checked)
            {
                Random rnd = new Random();
                string[] SkillsR = SkillsD.OrderBy(x => rnd.Next()).ToArray();
                string[] SkillLog = new string[SkillsR.Length];
                SkillLog = Func1.SkillLogOutputRandomizerDLC(SkillsR);
                GameData = Func1.SkillRandomizerDLC(GameData, SkillsR);
                

                File.WriteAllLines(txtdir + @"\GameData.bin_data.txt", GameData, utf8WithoutBom);
                StreamWriter log = new StreamWriter("log.txt");
                for (int i = 0; i < ClassesD.Length * 2; i += 2)
                {
                    if (i == (ClassesD.Length * 2) - 1)
                    {
                        log.WriteLine("Leftover" + "\n" + SkillLog[i] + " & " + SkillLog[i + 1] + SkillLog[i + 2] + "\n");
                    }
                    else
                    {
                        log.WriteLine(ClassesD[i / 2] + "\n" + SkillLog[i] + " & " + SkillLog[i + 1] + "\n");
                    }
                }
                log.Close();
            }
            else
            {
                Random rnd = new Random();
                string[] SkillsR = Skills.OrderBy(x => rnd.Next()).ToArray();
                string[] SkillLog = new string[SkillsR.Length];
                SkillLog = Func1.SkillLogOutputRandomizer(SkillsR);
                GameData = Func1.SkillRandomizer(GameData, SkillsR);


                File.WriteAllLines(txtdir + @"\GameData.bin_data.txt", GameData, utf8WithoutBom);
                StreamWriter log = new StreamWriter("log.txt");
                for (int i = 0; i < Classes.Length * 2; i+=2)
                {
                    if (i == (Classes.Length * 2) - 1)
                    {
                        log.WriteLine("Leftover" + "\n" + SkillLog[i] + " & " + SkillLog[i + 1] + SkillLog[i + 2] + "\n");
                    }
                    else
                    {
                        log.WriteLine(Classes[i / 2] + "\n" + SkillLog[i] + " & " + SkillLog[i + 1] + "\n");
                    }
                }
                log.Close();
            }
            StreamWriter repack = new StreamWriter("repack.bat");
            repack.WriteLine("asset_pack.rb -p " + txtdir + @"\GameData.bin_data.txt" + " " + txtdir + @"\GameData.bin_data");
            repack.Close();
            Process.Start("repack.bat");
            richTextBox1.AppendText("\n Cleaning up...");
            FileInfo GameBinData = new FileInfo(txtdir + @"\GameData.bin_data");
            for (; ; )
            {
                if (File.Exists(txtdir + @"\GameData.bin_data") == true)
                {
                    GameBinData.Refresh();
                    if (GameBinData.Length > 370000)
                    {
                        if(IsFileLocked(GameBinData) == false && IsFileLocked(GameDataTxt) == false)
                        {
                            break;
                        }
                        
                    }
                }
                continue;
            }
            File.Delete(txtdir + @"\GameData.bin_data.txt");
            File.Delete(txtdir + @"\GameData.bin");
            File.Delete(curDir + @"\unpack.bat");
            File.Delete(curDir + @"\repack.bat");
            File.Move(txtdir + @"\GameData.bin_data", txtdir + @"\GameData.bin");
            richTextBox1.AppendText("\n Done! Class Skills successfully randomized!");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            StreamWriter unpack = new StreamWriter("unpack.bat");
            unpack.WriteLine("asset_pack.rb -u " + selectedFileName2);
            unpack.Close();
            Process.Start("unpack.bat");
            FileInfo fi = new FileInfo(selectedFileName2);
            string txtdir = fi.DirectoryName;
            Encoding utf8WithoutBom = new UTF8Encoding(false);
            FileInfo StaticBinTxt = new FileInfo(txtdir + @"\static.bin_data.txt");
            for (; ; )
            {
                if (File.Exists(txtdir + @"\static.bin_data.txt") == true)
                {
                    StaticBinTxt.Refresh();
                    if (StaticBinTxt.Length > 130000)
                    {
                        if(IsFileLocked(StaticBinTxt) == false)
                        {
                            break;
                        }
                    }
                }
                continue;
            }
            string[] Static = File.ReadAllLines(txtdir + @"\static.bin_data.txt", utf8WithoutBom);
            string[] LOOKUP_TABLE = { "59", "89", "D2", "D1", "DE", "C6", "47", "21", "BA", "DB", "C5", "EC", "35", "BD", "9F", "9B", "2D", "7B", "B2", "09", "F7", "53", "99", "8F", "C4", "90", "FA", "34", "F8", "19", "94", "02", "ED", "56", "40", "6C", "F4", "88", "4F", "2B", "B4", "BB", "EB", "74", "B7", "0D", "C2", "A4", "EE", "93", "CF", "42", "F1", "17", "BF", "F0", "A5", "BC", "0F", "6E", "1B", "73", "8D", "A6", "3B", "50", "33", "E0", "AF", "9D", "DD", "FF", "FE", "AA", "CE", "12", "62", "E2", "FB", "C1", "23", "49", "D6", "CD", "04", "2F", "41", "15", "1A", "32", "03", "8A", "14", "58", "0A", "A3", "D0", "71", "7D", "D3", "A0", "52", "BE", "D7", "8B", "48", "37", "13", "A8", "44", "08", "3C", "E3", "63", "F6", "DF", "16", "7C", "46", "F3", "07", "CC", "79", "C3", "6B", "3F", "81", "00", "20", "28", "AE", "EF", "6D", "8E", "0E", "1D", "4B", "95", "A1", "B6", "D4", "C7", "3E", "E5", "D8", "5A", "43", "26", "7A", "E4", "4E", "9C", "30", "4C", "C8", "97", "FD", "54", "68", "C0", "FC", "36", "1C", "75", "01", "96", "E9", "1F", "45", "06", "70", "2C", "29", "67", "2E", "F5", "9E", "92", "60", "3D", "E8", "E7", "66", "2A", "91", "EA", "57", "A9", "1E", "5F", "27", "51", "C9", "65", "18", "AB", "83", "D5", "85", "61", "0C", "77", "7E", "F9", "7F", "5E", "DC", "84", "5C", "6A", "39", "4D", "87", "5B", "DA", "69", "E6", "5D", "11", "82", "10", "55", "D9", "CB", "8C", "72", "86", "6F", "64", "80", "CA", "A2", "05", "AC", "4A", "B1", "0B", "38", "E1", "AD", "31", "B3", "98", "78", "B8", "22", "76", "9A", "24", "A7", "25", "B5", "F2", "B9", "B0", "3A" };
            //LOOKUP_TABLE is a Hex array of values that is used for encrypting the randomized values since Intelligent Systems didnt want stats to be datamined.
            Random rnd = new Random();
            int seed = rnd.Next(0, 263263263);
            int[] Growths = new int[8];
            string[] HexG = new string[8];
            int[] GRlog = new int[8];
            StreamWriter Growthlog = new StreamWriter(curDir + @"\Growthlog.txt");
            for (int i = 0; i < 52; i++)
            {
                int seed2 = seed;
                while (seed2 == seed)
                {
                    seed = rnd.Next(0, 263263263);
                }

                int ID = i;
                int GR = 0;
                string GRHex = "";
                int index = 0;
                for (int j = 0; j < 8; j++)
                {
                    Random rng = new Random(i + j + seed); //This keeps the randomization from being the same accross the stats by forcing a new seed for every character and stat.
                    if (i == 1 || i == 2)
                    {
                        if (j == 0)
                        {
                            GR = rng.Next(35, 50);
                            GRlog[j] = GR;
                            GRHex = GR.ToString("X");
                            if (GRHex.Length == 1)
                            {
                                GRHex = "0" + GRHex;
                            }
                            index = Array.IndexOf(LOOKUP_TABLE, GRHex);
                            Growths[j] = (index + (99 * (((ID - 1) ^ 167) - (33 * j)) ^ 217)) & 0xFF;
                        }
                        else if (j == 3 || j == 4)
                        {
                            GR = rng.Next(30, 50);
                            GRlog[j] = GR;
                            GRHex = GR.ToString("X");
                            if (GRHex.Length == 1)
                            {
                                GRHex = "0" + GRHex;
                            }
                            index = Array.IndexOf(LOOKUP_TABLE, GRHex);
                            Growths[j] = (index + (99 * (((ID - 1) ^ 167) - (33 * j)) ^ 217)) & 0xFF;
                        }
                        else if (j == 5)
                        {
                            GR = rng.Next(30, 70);
                            GRlog[j] = GR;
                            GRHex = GR.ToString("X");
                            if (GRHex.Length == 1)
                            {
                                GRHex = "0" + GRHex;
                            }
                            index = Array.IndexOf(LOOKUP_TABLE, GRHex);
                            Growths[j] = (index + (99 * (((ID - 1) ^ 167) - (33 * j)) ^ 217)) & 0xFF;
                        }
                        else
                        {
                            GR = rng.Next(10, 50);
                            GRlog[j] = GR;
                            GRHex = GR.ToString("X");
                            if (GRHex.Length == 1)
                            {
                                GRHex = "0" + GRHex;
                            }
                            index = Array.IndexOf(LOOKUP_TABLE, GRHex);
                            Growths[j] = (index + (99 * (((ID - 1) ^ 167) - (33 * j)) ^ 217)) & 0xFF;
                        }
                        HexG[j] = Growths[j].ToString("X");
                        if (HexG[j].Length == 1)
                        {
                            HexG[j] = "0" + HexG[j];
                        }
                    }
                    else
                    {
                        if (j == 0)
                        {
                            GR = rng.Next(35, 50);
                            GRlog[j] = GR;
                            GRHex = GR.ToString("X");
                            if (GRHex.Length == 1)
                            {
                                GRHex = "0" + GRHex;
                            }
                            index = Array.IndexOf(LOOKUP_TABLE, GRHex);
                            Growths[j] = (index + (99 * (((ID) ^ 167) - (33 * j)) ^ 217)) & 0xFF;
                        }
                        else if (j == 3 || j == 4)
                        {
                            GR = rng.Next(30, 50);
                            GRlog[j] = GR;
                            GRHex = GR.ToString("X");
                            if (GRHex.Length == 1)
                            {
                                GRHex = "0" + GRHex;
                            }
                            index = Array.IndexOf(LOOKUP_TABLE, GRHex);
                            Growths[j] = (index + (99 * (((ID) ^ 167) - (33 * j)) ^ 217)) & 0xFF;
                        }
                        else if (j == 5)
                        {
                            GR = rng.Next(30, 70);
                            GRlog[j] = GR;
                            GRHex = GR.ToString("X");
                            if (GRHex.Length == 1)
                            {
                                GRHex = "0" + GRHex;
                            }
                            index = Array.IndexOf(LOOKUP_TABLE, GRHex);
                            Growths[j] = (index + (99 * (((ID) ^ 167) - (33 * j)) ^ 217)) & 0xFF;
                        }
                        else
                        {
                            GR = rng.Next(10, 50);
                            GRlog[j] = GR;
                            GRHex = GR.ToString("X");
                            if (GRHex.Length == 1)
                            {
                                GRHex = "0" + GRHex;
                            }
                            index = Array.IndexOf(LOOKUP_TABLE, GRHex);
                            Growths[j] = (index + (99 * (((ID) ^ 167) - (33 * j)) ^ 217)) & 0xFF;
                        }
                        HexG[j] = Growths[j].ToString("X");
                        if (HexG[j].Length == 1)
                        {
                            HexG[j] = "0" + HexG[j];
                        }
                    }
                }
                switch (i)
                {
                    case 0:
                        break;
                    case 1:
                        Static[12] = "0x" + HexG[0] + HexG[1] + HexG[2] + HexG[3];
                        Static[13] = "0x" + HexG[4] + HexG[5] + HexG[6] + HexG[7];
                        Growthlog.WriteLine(" M!Robin");
                        for (int k = 0; k < GRlog.Length; k++)
                        {
                            Growthlog.WriteLine(GRlog[k].ToString() + " ");
                        }
                        break;
                    case 2:
                        Static[155] = "0x" + HexG[0] + HexG[1] + HexG[2] + HexG[3];
                        Static[156] = "0x" + HexG[4] + HexG[5] + HexG[6] + HexG[7];
                        Growthlog.WriteLine("\n F!Robin");
                        for (int k = 0; k < GRlog.Length; k++)
                        {
                            Growthlog.WriteLine(GRlog[k].ToString() + " ");
                        }
                        break;
                    case 3:
                        Static[441] = "0x" + HexG[0] + HexG[1] + HexG[2] + HexG[3];
                        Static[442] = "0x" + HexG[4] + HexG[5] + HexG[6] + HexG[7];
                        Growthlog.WriteLine("\n Chrom");
                        for (int k = 0; k < GRlog.Length; k++)
                        {
                            Growthlog.WriteLine(GRlog[k].ToString() + " ");
                        }
                        break;
                    case 4:
                        Static[584] = "0x" + HexG[0] + HexG[1] + HexG[2] + HexG[3];
                        Static[585] = "0x" + HexG[4] + HexG[5] + HexG[6] + HexG[7];
                        Growthlog.WriteLine("\n Lissa");
                        for (int k = 0; k < GRlog.Length; k++)
                        {
                            Growthlog.WriteLine(GRlog[k].ToString() + " ");
                        }
                        break;
                    case 5:
                        Static[727] = "0x" + HexG[0] + HexG[1] + HexG[2] + HexG[3];
                        Static[728] = "0x" + HexG[4] + HexG[5] + HexG[6] + HexG[7];
                        Growthlog.WriteLine("\n Frederick");
                        for (int k = 0; k < GRlog.Length; k++)
                        {
                            Growthlog.WriteLine(GRlog[k].ToString() + " ");
                        }
                        break;
                    case 6:
                        Static[870] = "0x" + HexG[0] + HexG[1] + HexG[2] + HexG[3];
                        Static[871] = "0x" + HexG[4] + HexG[5] + HexG[6] + HexG[7];
                        Growthlog.WriteLine("\n Virion");
                        for (int k = 0; k < GRlog.Length; k++)
                        {
                            Growthlog.WriteLine(GRlog[k].ToString() + " ");
                        }
                        break;
                    case 7:
                        Static[1013] = "0x" + HexG[0] + HexG[1] + HexG[2] + HexG[3];
                        Static[1014] = "0x" + HexG[4] + HexG[5] + HexG[6] + HexG[7];
                        Growthlog.WriteLine("\n Sully");
                        for (int k = 0; k < GRlog.Length; k++)
                        {
                            Growthlog.WriteLine(GRlog[k].ToString() + " ");
                        }
                        break;
                    case 8:
                        Static[1156] = "0x" + HexG[0] + HexG[1] + HexG[2] + HexG[3];
                        Static[1157] = "0x" + HexG[4] + HexG[5] + HexG[6] + HexG[7];
                        Growthlog.WriteLine("\n Vaike ");
                        for (int k = 0; k < GRlog.Length; k++)
                        {
                            Growthlog.WriteLine(GRlog[k].ToString() + " ");
                        }
                        break;
                    case 9:
                        Static[1299] = "0x" + HexG[0] + HexG[1] + HexG[2] + HexG[3];
                        Static[1300] = "0x" + HexG[4] + HexG[5] + HexG[6] + HexG[7];
                        Growthlog.WriteLine("\n Stahl ");
                        for (int k = 0; k < GRlog.Length; k++)
                        {
                            Growthlog.WriteLine(GRlog[k].ToString() + " ");
                        }
                        break;
                    case 10:
                        Static[1442] = "0x" + HexG[0] + HexG[1] + HexG[2] + HexG[3];
                        Static[1443] = "0x" + HexG[4] + HexG[5] + HexG[6] + HexG[7];
                        Growthlog.WriteLine("\n Miriel ");
                        for (int k = 0; k < GRlog.Length; k++)
                        {
                            Growthlog.WriteLine(GRlog[k].ToString() + " ");
                        }
                        break;
                    case 11:
                        Static[1585] = "0x" + HexG[0] + HexG[1] + HexG[2] + HexG[3];
                        Static[1586] = "0x" + HexG[4] + HexG[5] + HexG[6] + HexG[7];
                        Growthlog.WriteLine("\n Kellam ");
                        for (int k = 0; k < GRlog.Length; k++)
                        {
                            Growthlog.WriteLine(GRlog[k].ToString() + " ");
                        }
                        break;
                    case 12:
                        Static[1728] = "0x" + HexG[0] + HexG[1] + HexG[2] + HexG[3];
                        Static[1729] = "0x" + HexG[4] + HexG[5] + HexG[6] + HexG[7];
                        Growthlog.WriteLine("\n Sumia ");
                        for (int k = 0; k < GRlog.Length; k++)
                        {
                            Growthlog.WriteLine(GRlog[k].ToString() + " ");
                        }
                        break;
                    case 13:
                        Static[1871] = "0x" + HexG[0] + HexG[1] + HexG[2] + HexG[3];
                        Static[1872] = "0x" + HexG[4] + HexG[5] + HexG[6] + HexG[7];
                        Growthlog.WriteLine("\n Lon'qu ");
                        for (int k = 0; k < GRlog.Length; k++)
                        {
                            Growthlog.WriteLine(GRlog[k].ToString() + " ");
                        }
                        break;
                    case 14:
                        Static[2014] = "0x" + HexG[0] + HexG[1] + HexG[2] + HexG[3];
                        Static[2015] = "0x" + HexG[4] + HexG[5] + HexG[6] + HexG[7];
                        Growthlog.WriteLine("\n Ricken ");
                        for (int k = 0; k < GRlog.Length; k++)
                        {
                            Growthlog.WriteLine(GRlog[k].ToString() + " ");
                        }
                        break;
                    case 15:
                        Static[2157] = "0x" + HexG[0] + HexG[1] + HexG[2] + HexG[3];
                        Static[2158] = "0x" + HexG[4] + HexG[5] + HexG[6] + HexG[7];
                        Growthlog.WriteLine("\n Maribelle ");
                        for (int k = 0; k < GRlog.Length; k++)
                        {
                            Growthlog.WriteLine(GRlog[k].ToString() + " ");
                        }
                        break;
                    case 16:
                        Static[2300] = "0x" + HexG[0] + HexG[1] + HexG[2] + HexG[3];
                        Static[2301] = "0x" + HexG[4] + HexG[5] + HexG[6] + HexG[7];
                        Growthlog.WriteLine("\n Panne ");
                        for (int k = 0; k < GRlog.Length; k++)
                        {
                            Growthlog.WriteLine(GRlog[k].ToString() + " ");
                        }
                        break;
                    case 17:
                        Static[2443] = "0x" + HexG[0] + HexG[1] + HexG[2] + HexG[3];
                        Static[2444] = "0x" + HexG[4] + HexG[5] + HexG[6] + HexG[7];
                        Growthlog.WriteLine("\n Gaius ");
                        for (int k = 0; k < GRlog.Length; k++)
                        {
                            Growthlog.WriteLine(GRlog[k].ToString() + " ");
                        }
                        break;
                    case 18:
                        Static[2586] = "0x" + HexG[0] + HexG[1] + HexG[2] + HexG[3];
                        Static[2587] = "0x" + HexG[4] + HexG[5] + HexG[6] + HexG[7];
                        Growthlog.WriteLine("\n Cordelia ");
                        for (int k = 0; k < GRlog.Length; k++)
                        {
                            Growthlog.WriteLine(GRlog[k].ToString() + " ");
                        }
                        break;
                    case 19:
                        Static[2729] = "0x" + HexG[0] + HexG[1] + HexG[2] + HexG[3];
                        Static[2730] = "0x" + HexG[4] + HexG[5] + HexG[6] + HexG[7];
                        Growthlog.WriteLine("\n Gregor ");
                        for (int k = 0; k < GRlog.Length; k++)
                        {
                            Growthlog.WriteLine(GRlog[k].ToString() + " ");
                        }
                        break;
                    case 20:
                        Static[2872] = "0x" + HexG[0] + HexG[1] + HexG[2] + HexG[3];
                        Static[2873] = "0x" + HexG[4] + HexG[5] + HexG[6] + HexG[7];
                        Growthlog.WriteLine("\n Nowi ");
                        for (int k = 0; k < GRlog.Length; k++)
                        {
                            Growthlog.WriteLine(GRlog[k].ToString() + " ");
                        }
                        break;
                    case 21:
                        Static[3015] = "0x" + HexG[0] + HexG[1] + HexG[2] + HexG[3];
                        Static[3016] = "0x" + HexG[4] + HexG[5] + HexG[6] + HexG[7];
                        Growthlog.WriteLine("\n Libra ");
                        for (int k = 0; k < GRlog.Length; k++)
                        {
                            Growthlog.WriteLine(GRlog[k].ToString() + " ");
                        }
                        break;
                    case 22:
                        Static[3158] = "0x" + HexG[0] + HexG[1] + HexG[2] + HexG[3];
                        Static[3159] = "0x" + HexG[4] + HexG[5] + HexG[6] + HexG[7];
                        Growthlog.WriteLine("\n Tharja ");
                        for (int k = 0; k < GRlog.Length; k++)
                        {
                            Growthlog.WriteLine(GRlog[k].ToString() + " ");
                        }
                        break;
                    case 23:
                        Static[3301] = "0x" + HexG[0] + HexG[1] + HexG[2] + HexG[3];
                        Static[3302] = "0x" + HexG[4] + HexG[5] + HexG[6] + HexG[7];
                        Growthlog.WriteLine("\n Olivia ");
                        for (int k = 0; k < GRlog.Length; k++)
                        {
                            Growthlog.WriteLine(GRlog[k].ToString() + " ");
                        }
                        break;
                    case 24:
                        Static[3444] = "0x" + HexG[0] + HexG[1] + HexG[2] + HexG[3];
                        Static[3445] = "0x" + HexG[4] + HexG[5] + HexG[6] + HexG[7];
                        Growthlog.WriteLine("\n Cherche ");
                        for (int k = 0; k < GRlog.Length; k++)
                        {
                            Growthlog.WriteLine(GRlog[k].ToString() + " ");
                        }
                        break;
                    case 25:
                        Static[3587] = "0x" + HexG[0] + HexG[1] + HexG[2] + HexG[3];
                        Static[3588] = "0x" + HexG[4] + HexG[5] + HexG[6] + HexG[7];
                        Growthlog.WriteLine("\n Henry ");
                        for (int k = 0; k < GRlog.Length; k++)
                        {
                            Growthlog.WriteLine(GRlog[k].ToString() + " ");
                        }
                        break;
                    case 26:
                        Static[3730] = "0x" + HexG[0] + HexG[1] + HexG[2] + HexG[3];
                        Static[3731] = "0x" + HexG[4] + HexG[5] + HexG[6] + HexG[7];
                        Growthlog.WriteLine("\n Lucina ");
                        for (int k = 0; k < GRlog.Length; k++)
                        {
                            Growthlog.WriteLine(GRlog[k].ToString() + " ");
                        }
                        break;
                    case 27:
                        Static[3873] = "0x" + HexG[0] + HexG[1] + HexG[2] + HexG[3];
                        Static[3874] = "0x" + HexG[4] + HexG[5] + HexG[6] + HexG[7];
                        Growthlog.WriteLine("\n Say'ri ");
                        for (int k = 0; k < GRlog.Length; k++)
                        {
                            Growthlog.WriteLine(GRlog[k].ToString() + " ");
                        }
                        break;
                    case 28:
                        Static[4016] = "0x" + HexG[0] + HexG[1] + HexG[2] + HexG[3];
                        Static[4017] = "0x" + HexG[4] + HexG[5] + HexG[6] + HexG[7];
                        Growthlog.WriteLine("\n Basilio ");
                        for (int k = 0; k < GRlog.Length; k++)
                        {
                            Growthlog.WriteLine(GRlog[k].ToString() + " ");
                        }
                        break;
                    case 29:
                        Static[4159] = "0x" + HexG[0] + HexG[1] + HexG[2] + HexG[3];
                        Static[4160] = "0x" + HexG[4] + HexG[5] + HexG[6] + HexG[7];
                        Growthlog.WriteLine("\n Flavia ");
                        for (int k = 0; k < GRlog.Length; k++)
                        {
                            Growthlog.WriteLine(GRlog[k].ToString() + " ");
                        }
                        break;
                    case 30:
                        Static[4302] = "0x" + HexG[0] + HexG[1] + HexG[2] + HexG[3];
                        Static[4303] = "0x" + HexG[4] + HexG[5] + HexG[6] + HexG[7];
                        Growthlog.WriteLine("\n Donnel ");
                        for (int k = 0; k < GRlog.Length; k++)
                        {
                            Growthlog.WriteLine(GRlog[k].ToString() + " ");
                        }
                        break;
                    case 31:
                        Static[4445] = "0x" + HexG[0] + HexG[1] + HexG[2] + HexG[3];
                        Static[4446] = "0x" + HexG[4] + HexG[5] + HexG[6] + HexG[7];
                        Growthlog.WriteLine("\n Anna ");
                        for (int k = 0; k < GRlog.Length; k++)
                        {
                            Growthlog.WriteLine(GRlog[k].ToString() + " ");
                        }
                        break;
                    case 32:
                        Static[4588] = "0x" + HexG[0] + HexG[1] + HexG[2] + HexG[3];
                        Static[4589] = "0x" + HexG[4] + HexG[5] + HexG[6] + HexG[7];
                        Growthlog.WriteLine("\n Owain ");
                        for (int k = 0; k < GRlog.Length; k++)
                        {
                            Growthlog.WriteLine(GRlog[k].ToString() + " ");
                        }
                        break;
                    case 33:
                        Static[4731] = "0x" + HexG[0] + HexG[1] + HexG[2] + HexG[3];
                        Static[4732] = "0x" + HexG[4] + HexG[5] + HexG[6] + HexG[7];
                        Growthlog.WriteLine("\n Inigo ");
                        for (int k = 0; k < GRlog.Length; k++)
                        {
                            Growthlog.WriteLine(GRlog[k].ToString() + " ");
                        }
                        break;
                    case 34:
                        Static[4874] = "0x" + HexG[0] + HexG[1] + HexG[2] + HexG[3];
                        Static[4875] = "0x" + HexG[4] + HexG[5] + HexG[6] + HexG[7];
                        Growthlog.WriteLine("\n Brady ");
                        for (int k = 0; k < GRlog.Length; k++)
                        {
                            Growthlog.WriteLine(GRlog[k].ToString() + " ");
                        }
                        break;
                    case 35:
                        Static[5017] = "0x" + HexG[0] + HexG[1] + HexG[2] + HexG[3];
                        Static[5018] = "0x" + HexG[4] + HexG[5] + HexG[6] + HexG[7];
                        Growthlog.WriteLine("\n Kjelle ");
                        for (int k = 0; k < GRlog.Length; k++)
                        {
                            Growthlog.WriteLine(GRlog[k].ToString() + " ");
                        }
                        break;
                    case 36:
                        Static[5160] = "0x" + HexG[0] + HexG[1] + HexG[2] + HexG[3];
                        Static[5161] = "0x" + HexG[4] + HexG[5] + HexG[6] + HexG[7];
                        Growthlog.WriteLine("\n Cynthia ");
                        for (int k = 0; k < GRlog.Length; k++)
                        {
                            Growthlog.WriteLine(GRlog[k].ToString() + " ");
                        }
                        break;
                    case 37:
                        Static[5303] = "0x" + HexG[0] + HexG[1] + HexG[2] + HexG[3];
                        Static[5304] = "0x" + HexG[4] + HexG[5] + HexG[6] + HexG[7];
                        Growthlog.WriteLine("\n Severa ");
                        for (int k = 0; k < GRlog.Length; k++)
                        {
                            Growthlog.WriteLine(GRlog[k].ToString() + " ");
                        }
                        break;
                    case 38:
                        Static[5446] = "0x" + HexG[0] + HexG[1] + HexG[2] + HexG[3];
                        Static[5447] = "0x" + HexG[4] + HexG[5] + HexG[6] + HexG[7];
                        Growthlog.WriteLine("\n Gerome ");
                        for (int k = 0; k < GRlog.Length; k++)
                        {
                            Growthlog.WriteLine(GRlog[k].ToString() + " ");
                        }
                        break;
                    case 39:
                        Static[5589] = "0x" + HexG[0] + HexG[1] + HexG[2] + HexG[3];
                        Static[5590] = "0x" + HexG[4] + HexG[5] + HexG[6] + HexG[7];
                        Growthlog.WriteLine("\n M!Morgan ");
                        for (int k = 0; k < GRlog.Length; k++)
                        {
                            Growthlog.WriteLine(GRlog[k].ToString() + " ");
                        }
                        break;
                    case 40:
                        Static[5732] = "0x" + HexG[0] + HexG[1] + HexG[2] + HexG[3];
                        Static[5733] = "0x" + HexG[4] + HexG[5] + HexG[6] + HexG[7];
                        Growthlog.WriteLine("\n F!Morgan ");
                        for (int k = 0; k < GRlog.Length; k++)
                        {
                            Growthlog.WriteLine(GRlog[k].ToString() + " ");
                        }
                        break;
                    case 41:
                        Static[5875] = "0x" + HexG[0] + HexG[1] + HexG[2] + HexG[3];
                        Static[5876] = "0x" + HexG[4] + HexG[5] + HexG[6] + HexG[7];
                        Growthlog.WriteLine("\n Yarne ");
                        for (int k = 0; k < GRlog.Length; k++)
                        {
                            Growthlog.WriteLine(GRlog[k].ToString() + " ");
                        }
                        break;
                    case 42:
                        Static[6018] = "0x" + HexG[0] + HexG[1] + HexG[2] + HexG[3];
                        Static[6019] = "0x" + HexG[4] + HexG[5] + HexG[6] + HexG[7];
                        Growthlog.WriteLine("\n Laurent ");
                        for (int k = 0; k < GRlog.Length; k++)
                        {
                            Growthlog.WriteLine(GRlog[k].ToString() + " ");
                        }
                        break;
                    case 43:
                        Static[6161] = "0x" + HexG[0] + HexG[1] + HexG[2] + HexG[3];
                        Static[6162] = "0x" + HexG[4] + HexG[5] + HexG[6] + HexG[7];
                        Growthlog.WriteLine("\n Noire ");
                        for (int k = 0; k < GRlog.Length; k++)
                        {
                            Growthlog.WriteLine(GRlog[k].ToString() + " ");
                        }
                        break;
                    case 44:
                        Static[6304] = "0x" + HexG[0] + HexG[1] + HexG[2] + HexG[3];
                        Static[6305] = "0x" + HexG[4] + HexG[5] + HexG[6] + HexG[7];
                        Growthlog.WriteLine("\n Nah ");
                        for (int k = 0; k < GRlog.Length; k++)
                        {
                            Growthlog.WriteLine(GRlog[k].ToString() + " ");
                        }
                        break;
                    case 45:
                        Static[6447] = "0x" + HexG[0] + HexG[1] + HexG[2] + HexG[3];
                        Static[6448] = "0x" + HexG[4] + HexG[5] + HexG[6] + HexG[7];
                        Growthlog.WriteLine("\n Tiki ");
                        for (int k = 0; k < GRlog.Length; k++)
                        {
                            Growthlog.WriteLine(GRlog[k].ToString() + " ");
                        }
                        break;
                    case 46:
                        Static[6590] = "0x" + HexG[0] + HexG[1] + HexG[2] + HexG[3];
                        Static[6591] = "0x" + HexG[4] + HexG[5] + HexG[6] + HexG[7];
                        Growthlog.WriteLine("\n Gangrel ");
                        for (int k = 0; k < GRlog.Length; k++)
                        {
                            Growthlog.WriteLine(GRlog[k].ToString() + " ");
                        }
                        break;
                    case 47:
                        Static[6733] = "0x" + HexG[0] + HexG[1] + HexG[2] + HexG[3];
                        Static[6734] = "0x" + HexG[4] + HexG[5] + HexG[6] + HexG[7];
                        Growthlog.WriteLine("\n Walhart ");
                        for (int k = 0; k < GRlog.Length; k++)
                        {
                            Growthlog.WriteLine(GRlog[k].ToString() + " ");
                        }
                        break;
                    case 48:
                        Static[6876] = "0x" + HexG[0] + HexG[1] + HexG[2] + HexG[3];
                        Static[6877] = "0x" + HexG[4] + HexG[5] + HexG[6] + HexG[7];
                        Growthlog.WriteLine("\n Emmeryn ");
                        for (int k = 0; k < GRlog.Length; k++)
                        {
                            Growthlog.WriteLine(GRlog[k].ToString() + " ");
                        }
                        break;
                    case 49:
                        Static[7019] = "0x" + HexG[0] + HexG[1] + HexG[2] + HexG[3];
                        Static[7020] = "0x" + HexG[4] + HexG[5] + HexG[6] + HexG[7];
                        Growthlog.WriteLine("\n Yen'fay ");
                        for (int k = 0; k < GRlog.Length; k++)
                        {
                            Growthlog.WriteLine(GRlog[k].ToString() + " ");
                        }
                        break;
                    case 50:
                        Static[7162] = "0x" + HexG[0] + HexG[1] + HexG[2] + HexG[3];
                        Static[7163] = "0x" + HexG[4] + HexG[5] + HexG[6] + HexG[7];
                        Growthlog.WriteLine("\n Aversa ");
                        for (int k = 0; k < GRlog.Length; k++)
                        {
                            Growthlog.WriteLine(GRlog[k].ToString() + " ");
                        }
                        break;
                    case 51:
                        Static[7305] = "0x" + HexG[0] + HexG[1] + HexG[2] + HexG[3];
                        Static[7306] = "0x" + HexG[4] + HexG[5] + HexG[6] + HexG[7];
                        Growthlog.WriteLine("\n Priam ");
                        for (int k = 0; k < GRlog.Length; k++)
                        {
                            Growthlog.WriteLine(GRlog[k].ToString() + " ");
                        }
                        Growthlog.WriteLine("");
                        break;
                }
            }
            Growthlog.Close();
            if (checkBox2.Checked)
            {
                string[] Modifiers = new string[7];
                string[] GrowthLog = File.ReadAllLines(curDir + @"\Growthlog.txt");
                for (int j =0; j < 52; j++)
                {
                    for (int i = 0; i < Modifiers.Length; i++)
                    {
                        int seed2 = seed;
                        while (seed2 == seed)
                        {
                            seed = rnd.Next(0, 263263263);
                        }
                        Random rng = new Random(i + seed);
                        if (i == 0 || i == 1)
                        {
                            int MT = rng.Next(-3, 0);
                            switch (MT)
                            {
                                case -3:
                                    Modifiers[i] = "FD";
                                    break;
                                case -2:
                                    Modifiers[i] = "FE";
                                    break;
                                case -1:
                                    Modifiers[i] = "FF";
                                    break;
                                case 0:
                                    Modifiers[i] = "00";
                                    break;
                            }
                        }
                        else
                        {
                            int MT = rng.Next(0, 3);
                            switch (MT)
                            {
                                case 0:
                                    Modifiers[i] = "00";
                                    break;
                                case 1:
                                    Modifiers[i] = "01";
                                    break;
                                case 2:
                                    Modifiers[i] = "02";
                                    break;
                                case 3:
                                    Modifiers[i] = "03";
                                    break;
                            }
                        }
                    }
                    Random rnj = new Random(j + seed);
                    string[] ModifiersR = Modifiers.OrderBy(x => rnj.Next()).ToArray();
                    string[] ModifierLog = new string[ModifiersR.Length];
                    for (int i = 0; i < ModifierLog.Length; i++)
                    {
                        switch (ModifiersR[i])
                        {
                            case "FD":
                                ModifierLog[i] = "-3";
                                break;
                            case "FE":
                                ModifierLog[i] = "-2";
                                break;
                            case "FF":
                                ModifierLog[i] = "-1";
                                break;
                            case "00":
                                ModifierLog[i] = "0";
                                break;
                            case "01":
                                ModifierLog[i] = "1";
                                break;
                            case "02":
                                ModifierLog[i] = "2";
                                break;
                            case "03":
                                ModifierLog[i] = "3";
                                break;
                        }
                    }
                    switch (j)
                    {
                        case 0:
                            break;
                        case 1:
                            Static[12 + 4] = "0x00" + ModifiersR[0] + ModifiersR[1] + ModifiersR[2];
                            Static[13 + 4] = "0x" + ModifiersR[3] + ModifiersR[4] + ModifiersR[5] + ModifiersR[6];
                            GrowthLog[(j * 10) - 1] ="Stat Cap Modifiers: " +  ModifierLog[0] + " " + ModifierLog[1] + " " + ModifierLog[2] + " " + ModifierLog[3] + " " + ModifierLog[4] + " " + ModifierLog[5] + " " + ModifierLog[6];
                            break;
                        case 2:
                            Static[155 + 4] = "0x00" + ModifiersR[0] + ModifiersR[1] + ModifiersR[2];
                            Static[156 + 4] = "0x" + ModifiersR[3] + ModifiersR[4] + ModifiersR[5] + ModifiersR[6];
                            GrowthLog[(j * 10) - 1] ="Stat Cap Modifiers: " +  ModifierLog[0] + " " + ModifierLog[1] + " " + ModifierLog[2] + " " + ModifierLog[3] + " " + ModifierLog[4] + " " + ModifierLog[5] + " " + ModifierLog[6];
                            break;
                        case 3:
                            Static[441 + 4] = "0x00" + ModifiersR[0] + ModifiersR[1] + ModifiersR[2];
                            Static[442 + 4] = "0x" + ModifiersR[3] + ModifiersR[4] + ModifiersR[5] + ModifiersR[6];
                            GrowthLog[(j * 10) - 1] ="Stat Cap Modifiers: " +  ModifierLog[0] + " " + ModifierLog[1] + " " + ModifierLog[2] + " " + ModifierLog[3] + " " + ModifierLog[4] + " " + ModifierLog[5] + " " + ModifierLog[6];
                            break;
                        case 4:
                            Static[584 + 4] = "0x00" + ModifiersR[0] + ModifiersR[1] + ModifiersR[2];
                            Static[585 + 4] = "0x" + ModifiersR[3] + ModifiersR[4] + ModifiersR[5] + ModifiersR[6];
                            GrowthLog[(j * 10) - 1] ="Stat Cap Modifiers: " +  ModifierLog[0] + " " + ModifierLog[1] + " " + ModifierLog[2] + " " + ModifierLog[3] + " " + ModifierLog[4] + " " + ModifierLog[5] + " " + ModifierLog[6];
                            break;
                        case 5:
                            Static[727 + 4] = "0x00" + ModifiersR[0] + ModifiersR[1] + ModifiersR[2];
                            Static[728 + 4] = "0x" + ModifiersR[3] + ModifiersR[4] + ModifiersR[5] + ModifiersR[6];
                            GrowthLog[(j * 10) - 1] ="Stat Cap Modifiers: " +  ModifierLog[0] + " " + ModifierLog[1] + " " + ModifierLog[2] + " " + ModifierLog[3] + " " + ModifierLog[4] + " " + ModifierLog[5] + " " + ModifierLog[6];
                            break;
                        case 6:
                            Static[870 + 4] = "0x00" + ModifiersR[0] + ModifiersR[1] + ModifiersR[2];
                            Static[871 + 4] = "0x" + ModifiersR[3] + ModifiersR[4] + ModifiersR[5] + ModifiersR[6];
                            GrowthLog[(j * 10) - 1] ="Stat Cap Modifiers: " +  ModifierLog[0] + " " + ModifierLog[1] + " " + ModifierLog[2] + " " + ModifierLog[3] + " " + ModifierLog[4] + " " + ModifierLog[5] + " " + ModifierLog[6];
                            break;
                        case 7:
                            Static[1013 + 4] = "0x00" + ModifiersR[0] + ModifiersR[1] + ModifiersR[2];
                            Static[1014 + 4] = "0x" + ModifiersR[3] + ModifiersR[4] + ModifiersR[5] + ModifiersR[6];
                            GrowthLog[(j * 10) - 1] = "Stat Cap Modifiers: " + ModifierLog[0] + " " + ModifierLog[1] + " " + ModifierLog[2] + " " + ModifierLog[3] + " " + ModifierLog[4] + " " + ModifierLog[5] + " " + ModifierLog[6];
                            break;
                        case 8:
                            Static[1156 + 4] = "0x00" + ModifiersR[0] + ModifiersR[1] + ModifiersR[2];
                            Static[1157 + 4] = "0x" + ModifiersR[3] + ModifiersR[4] + ModifiersR[5] + ModifiersR[6];
                            GrowthLog[(j * 10) - 1] ="Stat Cap Modifiers: " +  ModifierLog[0] + " " + ModifierLog[1] + " " + ModifierLog[2] + " " + ModifierLog[3] + " " + ModifierLog[4] + " " + ModifierLog[5] + " " + ModifierLog[6];
                            break;
                        case 9:
                            Static[1299 + 4] = "0x00" + ModifiersR[0] + ModifiersR[1] + ModifiersR[2];
                            Static[1300 + 4] = "0x" + ModifiersR[3] + ModifiersR[4] + ModifiersR[5] + ModifiersR[6];
                            GrowthLog[(j * 10) - 1] ="Stat Cap Modifiers: " +  ModifierLog[0] + " " + ModifierLog[1] + " " + ModifierLog[2] + " " + ModifierLog[3] + " " + ModifierLog[4] + " " + ModifierLog[5] + " " + ModifierLog[6];
                            break;
                        case 10:
                            Static[1442 + 4] = "0x00" + ModifiersR[0] + ModifiersR[1] + ModifiersR[2];
                            Static[1443 + 4] = "0x" + ModifiersR[3] + ModifiersR[4] + ModifiersR[5] + ModifiersR[6];
                            GrowthLog[(j * 10) - 1] ="Stat Cap Modifiers: " +  ModifierLog[0] + " " + ModifierLog[1] + " " + ModifierLog[2] + " " + ModifierLog[3] + " " + ModifierLog[4] + " " + ModifierLog[5] + " " + ModifierLog[6];
                            break;
                        case 11:
                            Static[1585 + 4] = "0x00" + ModifiersR[0] + ModifiersR[1] + ModifiersR[2];
                            Static[1586 + 4] = "0x" + ModifiersR[3] + ModifiersR[4] + ModifiersR[5] + ModifiersR[6];
                            GrowthLog[(j * 10) - 1] ="Stat Cap Modifiers: " +  ModifierLog[0] + " " + ModifierLog[1] + " " + ModifierLog[2] + " " + ModifierLog[3] + " " + ModifierLog[4] + " " + ModifierLog[5] + " " + ModifierLog[6];
                            break;
                        case 12:
                            Static[1728 + 4] = "0x00" + ModifiersR[0] + ModifiersR[1] + ModifiersR[2];
                            Static[1729 + 4] = "0x" + ModifiersR[3] + ModifiersR[4] + ModifiersR[5] + ModifiersR[6];
                            GrowthLog[(j * 10) - 1] ="Stat Cap Modifiers: " +  ModifierLog[0] + " " + ModifierLog[1] + " " + ModifierLog[2] + " " + ModifierLog[3] + " " + ModifierLog[4] + " " + ModifierLog[5] + " " + ModifierLog[6];
                            break;
                        case 13:
                            Static[1871 + 4] = "0x00" + ModifiersR[0] + ModifiersR[1] + ModifiersR[2];
                            Static[1872 + 4] = "0x" + ModifiersR[3] + ModifiersR[4] + ModifiersR[5] + ModifiersR[6];
                            GrowthLog[(j * 10) - 1] ="Stat Cap Modifiers: " +  ModifierLog[0] + " " + ModifierLog[1] + " " + ModifierLog[2] + " " + ModifierLog[3] + " " + ModifierLog[4] + " " + ModifierLog[5] + " " + ModifierLog[6];
                            break;
                        case 14:
                            Static[2014 + 4] = "0x00" + ModifiersR[0] + ModifiersR[1] + ModifiersR[2];
                            Static[2015 + 4] = "0x" + ModifiersR[3] + ModifiersR[4] + ModifiersR[5] + ModifiersR[6];
                            GrowthLog[(j * 10) - 1] ="Stat Cap Modifiers: " +  ModifierLog[0] + " " + ModifierLog[1] + " " + ModifierLog[2] + " " + ModifierLog[3] + " " + ModifierLog[4] + " " + ModifierLog[5] + " " + ModifierLog[6];
                            break;
                        case 15:
                            Static[2157 + 4] = "0x00" + ModifiersR[0] + ModifiersR[1] + ModifiersR[2];
                            Static[2158 + 4] = "0x" + ModifiersR[3] + ModifiersR[4] + ModifiersR[5] + ModifiersR[6];
                            GrowthLog[(j * 10) - 1] ="Stat Cap Modifiers: " +  ModifierLog[0] + " " + ModifierLog[1] + " " + ModifierLog[2] + " " + ModifierLog[3] + " " + ModifierLog[4] + " " + ModifierLog[5] + " " + ModifierLog[6];
                            break;
                        case 16:
                            Static[2300 + 4] = "0x00" + ModifiersR[0] + ModifiersR[1] + ModifiersR[2];
                            Static[2301 + 4] = "0x" + ModifiersR[3] + ModifiersR[4] + ModifiersR[5] + ModifiersR[6];
                            GrowthLog[(j * 10) - 1] ="Stat Cap Modifiers: " +  ModifierLog[0] + " " + ModifierLog[1] + " " + ModifierLog[2] + " " + ModifierLog[3] + " " + ModifierLog[4] + " " + ModifierLog[5] + " " + ModifierLog[6];
                            break;
                        case 17:
                            Static[2443 + 4] = "0x00" + ModifiersR[0] + ModifiersR[1] + ModifiersR[2];
                            Static[2444 + 4] = "0x" + ModifiersR[3] + ModifiersR[4] + ModifiersR[5] + ModifiersR[6];
                            GrowthLog[(j * 10) - 1] ="Stat Cap Modifiers: " +  ModifierLog[0] + " " + ModifierLog[1] + " " + ModifierLog[2] + " " + ModifierLog[3] + " " + ModifierLog[4] + " " + ModifierLog[5] + " " + ModifierLog[6];
                            break;
                        case 18:
                            Static[2586 + 4] = "0x00" + ModifiersR[0] + ModifiersR[1] + ModifiersR[2];
                            Static[2587 + 4] = "0x" + ModifiersR[3] + ModifiersR[4] + ModifiersR[5] + ModifiersR[6];
                            GrowthLog[(j * 10) - 1] ="Stat Cap Modifiers: " +  ModifierLog[0] + " " + ModifierLog[1] + " " + ModifierLog[2] + " " + ModifierLog[3] + " " + ModifierLog[4] + " " + ModifierLog[5] + " " + ModifierLog[6];
                            break;
                        case 19:
                            Static[2729 + 4] = "0x00" + ModifiersR[0] + ModifiersR[1] + ModifiersR[2];
                            Static[2730 + 4] = "0x" + ModifiersR[3] + ModifiersR[4] + ModifiersR[5] + ModifiersR[6];
                            GrowthLog[(j * 10) - 1] ="Stat Cap Modifiers: " +  ModifierLog[0] + " " + ModifierLog[1] + " " + ModifierLog[2] + " " + ModifierLog[3] + " " + ModifierLog[4] + " " + ModifierLog[5] + " " + ModifierLog[6];
                            break;
                        case 20:
                            Static[2872 + 4] = "0x00" + ModifiersR[0] + ModifiersR[1] + ModifiersR[2];
                            Static[2873 + 4] = "0x" + ModifiersR[3] + ModifiersR[4] + ModifiersR[5] + ModifiersR[6];
                            GrowthLog[(j * 10) - 1] ="Stat Cap Modifiers: " +  ModifierLog[0] + " " + ModifierLog[1] + " " + ModifierLog[2] + " " + ModifierLog[3] + " " + ModifierLog[4] + " " + ModifierLog[5] + " " + ModifierLog[6];
                            break;
                        case 21:
                            Static[3015 + 4] = "0x00" + ModifiersR[0] + ModifiersR[1] + ModifiersR[2];
                            Static[3016 + 4] = "0x" + ModifiersR[3] + ModifiersR[4] + ModifiersR[5] + ModifiersR[6];
                            GrowthLog[(j * 10) - 1] ="Stat Cap Modifiers: " +  ModifierLog[0] + " " + ModifierLog[1] + " " + ModifierLog[2] + " " + ModifierLog[3] + " " + ModifierLog[4] + " " + ModifierLog[5] + " " + ModifierLog[6];
                            break;
                        case 22:
                            Static[3158 + 4] = "0x00" + ModifiersR[0] + ModifiersR[1] + ModifiersR[2];
                            Static[3159 + 4] = "0x" + ModifiersR[3] + ModifiersR[4] + ModifiersR[5] + ModifiersR[6];
                            GrowthLog[(j * 10) - 1] ="Stat Cap Modifiers: " +  ModifierLog[0] + " " + ModifierLog[1] + " " + ModifierLog[2] + " " + ModifierLog[3] + " " + ModifierLog[4] + " " + ModifierLog[5] + " " + ModifierLog[6];
                            break;
                        case 23:
                            Static[3301 + 4] = "0x00" + ModifiersR[0] + ModifiersR[1] + ModifiersR[2];
                            Static[3302 + 4] = "0x" + ModifiersR[3] + ModifiersR[4] + ModifiersR[5] + ModifiersR[6];
                            GrowthLog[(j * 10) - 1] ="Stat Cap Modifiers: " +  ModifierLog[0] + " " + ModifierLog[1] + " " + ModifierLog[2] + " " + ModifierLog[3] + " " + ModifierLog[4] + " " + ModifierLog[5] + " " + ModifierLog[6];
                            break;
                        case 24:
                            Static[3444 + 4] = "0x00" + ModifiersR[0] + ModifiersR[1] + ModifiersR[2];
                            Static[3445 + 4] = "0x" + ModifiersR[3] + ModifiersR[4] + ModifiersR[5] + ModifiersR[6];
                            GrowthLog[(j * 10) - 1] ="Stat Cap Modifiers: " +  ModifierLog[0] + " " + ModifierLog[1] + " " + ModifierLog[2] + " " + ModifierLog[3] + " " + ModifierLog[4] + " " + ModifierLog[5] + " " + ModifierLog[6];
                            break;
                        case 25:
                            Static[3587 + 4] = "0x00" + ModifiersR[0] + ModifiersR[1] + ModifiersR[2];
                            Static[3588 + 4] = "0x" + ModifiersR[3] + ModifiersR[4] + ModifiersR[5] + ModifiersR[6];
                            GrowthLog[(j * 10) - 1] = "Stat Cap Modifiers: " + ModifierLog[0] + " " + ModifierLog[1] + " " + ModifierLog[2] + " " + ModifierLog[3] + " " + ModifierLog[4] + " " + ModifierLog[5] + " " + ModifierLog[6];
                            break;
                        case 26:
                            break;
                        case 27:
                            Static[3873 + 4] = "0x00" + ModifiersR[0] + ModifiersR[1] + ModifiersR[2];
                            Static[3874 + 4] = "0x" + ModifiersR[3] + ModifiersR[4] + ModifiersR[5] + ModifiersR[6];
                            GrowthLog[(j * 10) - 1] ="Stat Cap Modifiers: " +  ModifierLog[0] + " " + ModifierLog[1] + " " + ModifierLog[2] + " " + ModifierLog[3] + " " + ModifierLog[4] + " " + ModifierLog[5] + " " + ModifierLog[6];
                            break;
                        case 28:
                            Static[4016 + 4] = "0x00" + ModifiersR[0] + ModifiersR[1] + ModifiersR[2];
                            Static[4017 + 4] = "0x" + ModifiersR[3] + ModifiersR[4] + ModifiersR[5] + ModifiersR[6];
                            GrowthLog[(j * 10) - 1] ="Stat Cap Modifiers: " +  ModifierLog[0] + " " + ModifierLog[1] + " " + ModifierLog[2] + " " + ModifierLog[3] + " " + ModifierLog[4] + " " + ModifierLog[5] + " " + ModifierLog[6];
                            break;
                        case 29:
                            Static[4159 + 4] = "0x00" + ModifiersR[0] + ModifiersR[1] + ModifiersR[2];
                            Static[4160 + 4] = "0x" + ModifiersR[3] + ModifiersR[4] + ModifiersR[5] + ModifiersR[6];
                            GrowthLog[(j * 10) - 1] ="Stat Cap Modifiers: " +  ModifierLog[0] + " " + ModifierLog[1] + " " + ModifierLog[2] + " " + ModifierLog[3] + " " + ModifierLog[4] + " " + ModifierLog[5] + " " + ModifierLog[6];
                            break;
                        case 30:
                            Static[4302 + 4] = "0x00" + ModifiersR[0] + ModifiersR[1] + ModifiersR[2];
                            Static[4303 + 4] = "0x" + ModifiersR[3] + ModifiersR[4] + ModifiersR[5] + ModifiersR[6];
                            GrowthLog[(j * 10) - 1] ="Stat Cap Modifiers: " +  ModifierLog[0] + " " + ModifierLog[1] + " " + ModifierLog[2] + " " + ModifierLog[3] + " " + ModifierLog[4] + " " + ModifierLog[5] + " " + ModifierLog[6];
                            break;
                        case 31:
                            Static[4445 + 4] = "0x00" + ModifiersR[0] + ModifiersR[1] + ModifiersR[2];
                            Static[4446 + 4] = "0x" + ModifiersR[3] + ModifiersR[4] + ModifiersR[5] + ModifiersR[6];
                            GrowthLog[(j * 10) - 1] ="Stat Cap Modifiers: " +  ModifierLog[0] + " " + ModifierLog[1] + " " + ModifierLog[2] + " " + ModifierLog[3] + " " + ModifierLog[4] + " " + ModifierLog[5] + " " + ModifierLog[6];
                            break;
                        case 45:
                            Static[6447 + 4] = "0x00" + ModifiersR[0] + ModifiersR[1] + ModifiersR[2];
                            Static[6448 + 4] = "0x" + ModifiersR[3] + ModifiersR[4] + ModifiersR[5] + ModifiersR[6];
                            GrowthLog[(j * 10) - 1] ="Stat Cap Modifiers: " +  ModifierLog[0] + " " + ModifierLog[1] + " " + ModifierLog[2] + " " + ModifierLog[3] + " " + ModifierLog[4] + " " + ModifierLog[5] + " " + ModifierLog[6];
                            break;
                        case 46:
                            Static[6590 + 4] = "0x00" + ModifiersR[0] + ModifiersR[1] + ModifiersR[2];
                            Static[6591 + 4] = "0x" + ModifiersR[3] + ModifiersR[4] + ModifiersR[5] + ModifiersR[6];
                            GrowthLog[(j * 10) - 1] ="Stat Cap Modifiers: " +  ModifierLog[0] + " " + ModifierLog[1] + " " + ModifierLog[2] + " " + ModifierLog[3] + " " + ModifierLog[4] + " " + ModifierLog[5] + " " + ModifierLog[6];
                            break;
                        case 47:
                            Static[6733 + 4] = "0x00" + ModifiersR[0] + ModifiersR[1] + ModifiersR[2];
                            Static[6734 + 4] = "0x" + ModifiersR[3] + ModifiersR[4] + ModifiersR[5] + ModifiersR[6];
                            GrowthLog[(j * 10) - 1] ="Stat Cap Modifiers: " +  ModifierLog[0] + " " + ModifierLog[1] + " " + ModifierLog[2] + " " + ModifierLog[3] + " " + ModifierLog[4] + " " + ModifierLog[5] + " " + ModifierLog[6];
                            break;
                        case 48:
                            Static[6876 + 4] = "0x00" + ModifiersR[0] + ModifiersR[1] + ModifiersR[2];
                            Static[6877 + 4] = "0x" + ModifiersR[3] + ModifiersR[4] + ModifiersR[5] + ModifiersR[6];
                            GrowthLog[(j * 10) - 1] ="Stat Cap Modifiers: " +  ModifierLog[0] + " " + ModifierLog[1] + " " + ModifierLog[2] + " " + ModifierLog[3] + " " + ModifierLog[4] + " " + ModifierLog[5] + " " + ModifierLog[6];
                            break;
                        case 49:
                            Static[7019 + 4] = "0x00" + ModifiersR[0] + ModifiersR[1] + ModifiersR[2];
                            Static[7020 + 4] = "0x" + ModifiersR[3] + ModifiersR[4] + ModifiersR[5] + ModifiersR[6];
                            GrowthLog[(j * 10) - 1] ="Stat Cap Modifiers: " +  ModifierLog[0] + " " + ModifierLog[1] + " " + ModifierLog[2] + " " + ModifierLog[3] + " " + ModifierLog[4] + " " + ModifierLog[5] + " " + ModifierLog[6];
                            break;
                        case 50:
                            Static[7162 + 4] = "0x00" + ModifiersR[0] + ModifiersR[1] + ModifiersR[2];
                            Static[7163 + 4] = "0x" + ModifiersR[3] + ModifiersR[4] + ModifiersR[5] + ModifiersR[6];
                            GrowthLog[(j * 10) - 1] ="Stat Cap Modifiers: " +  ModifierLog[0] + " " + ModifierLog[1] + " " + ModifierLog[2] + " " + ModifierLog[3] + " " + ModifierLog[4] + " " + ModifierLog[5] + " " + ModifierLog[6];
                            break;
                        case 51:
                            Static[7305 + 4] = "0x00" + ModifiersR[0] + ModifiersR[1] + ModifiersR[2];
                            Static[7306 + 4] = "0x" + ModifiersR[3] + ModifiersR[4] + ModifiersR[5] + ModifiersR[6];
                            GrowthLog[(j * 10) - 1] ="Stat Cap Modifiers: " +  ModifierLog[0] + " " + ModifierLog[1] + " " + ModifierLog[2] + " " + ModifierLog[3] + " " + ModifierLog[4] + " " + ModifierLog[5] + " " + ModifierLog[6];
                            break;
                    }
                }
                File.WriteAllLines(curDir + @"\Growthlog.txt", GrowthLog);
            }
            File.WriteAllLines(txtdir + @"\static.bin_data.txt", Static, utf8WithoutBom);
            StreamWriter repack = new StreamWriter("repack.bat");
            repack.WriteLine("asset_pack.rb -p " + txtdir + @"\static.bin_data.txt" + " " + txtdir + @"\static.bin_data");
            repack.Close();
            Process.Start("repack.bat");
            //System.Threading.Thread.Sleep(5000);
            richTextBox1.AppendText("\n Cleaning up...");
            FileInfo StaticBinData = new FileInfo(txtdir + @"\static.bin_data");
            for (; ; )
            {
                if (File.Exists(txtdir + @"\static.bin_data") == true)
                {
                    StaticBinData.Refresh();
                    if (StaticBinData.Length > 50000)
                    {
                        if (IsFileLocked(StaticBinData) == false && IsFileLocked(StaticBinTxt) == false)
                        {
                            break;
                        }
                        
                    }
                }
                continue;
            }
            File.Delete(txtdir + @"\static.bin_data.txt");
            File.Delete(txtdir + @"\static.bin");
            File.Delete(curDir + @"\unpack.bat");
            File.Delete(curDir + @"\repack.bat");
            File.Move(txtdir + @"\static.bin_data", txtdir + @"\static.bin");
            richTextBox1.AppendText("\n Done! Character Growths successfully randomized!");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog2 = new OpenFileDialog
            {
                InitialDirectory = "c:\\",
                Filter = "bin files (*.bin*)|*.bin*",
                FilterIndex = 0,
                RestoreDirectory = true
            };

            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                selectedFileName2 = openFileDialog2.FileName;
                richTextBox1.AppendText("\n Static.bin selected successfully");
            }
        }
        protected virtual bool IsFileLocked(FileInfo file)
        {
            try
            {
                using (FileStream stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    stream.Close();
                }
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }

            //file is not locked
            return false;
        }
    }
}
