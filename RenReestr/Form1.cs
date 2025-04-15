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
using System.IO.Compression;
using System.Security;
using System.Diagnostics;

namespace RenReestr
{
    public partial class RenameReestr : Form
    {
        public string sourceFolder = "C://prr/";
        public string targetFolder = "C://prr/";
        //public string tempFolder = "C://prr//tf/";
        public string tempFolder = "C:\\prr\\tf\\";
        public string iniFile = "c:/Renreestr/cr.ini";
        public string zipFile, zipFile1, zipFile2;
        public string[] ends = { "20", "21", "22", "23", "24", "25", "26", "27", "28", "29" };
        public string selEnd;
        public Button selectButton;
        public string cLaunch;

        public RenameReestr()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            openFileDialog1 = new OpenFileDialog()
            {
                InitialDirectory = sourceFolder,
                FileName = "Select a zip file",
                Filter = "Zip files (*.zip)|*.zip",
                Title = "Open zip file"
            };       
  
            ClientSize = new Size(330, 360);
            Controls.Add(selectButton);        //Controls.Add(textBox1);
            iniFileWork(0, 0, 0);
            comboBox1.Items.AddRange(ends);
            comboBox1.MaxDropDownItems = 10;
            comboBox1.Top = 30;
            comboBox1.Left = 10;
            comboBox1.Height = 15;
            comboBox1.Width = 120;
            comboBox1.SelectedItem = "20";
            Process.Start("explorer.exe", "c:\\prr\\");
        }

        public void iniFileWork(byte FileClose, byte countLaunch, byte countRen)
        {
            //string iniPath = "c:/Renreestr/cr.ini"; // файл настроек
            //StreamReader reader = new StreamReader(iniPath);
            int cL, cR;
            string cLaunch, cRen;
            string[] iniStrings = File.ReadAllLines(iniFile);            
            switch (FileClose)
            {
                case 0: iniStrings[2] = "NormalExit=0";
                break;
                case 1: iniStrings[2] = "NormalExit=1";
                break;
                default: MessageBox.Show("iniFileWork first parameter not 0 and not 1" );
                break;
            }
            if (Int32.TryParse(iniStrings[0].Substring(iniStrings[0].IndexOf("=") + 1, iniStrings[0].Length - iniStrings[0].IndexOf("=") - 1), out cL))
            {                
                //MessageBox.Show("iniFileWork first line " + cL + " " + countRen);
            }            
            if (countLaunch == 1)
            {                
                cL++;                
                cLaunch = String.Concat("TotalLaunch=", cL.ToString());
                iniStrings[0] = cLaunch;
                //iniStrings[0].Replace("mountains", "peaks");                
            }
            if (Int32.TryParse(iniStrings[1].Substring(iniStrings[1].IndexOf("=") + 1, iniStrings[1].Length - iniStrings[1].IndexOf("=") - 1), out cR))
            {
                //MessageBox.Show("iniFileWork first line " + cL + " " + countRen);
            }     
            if (countRen == 1)
            {
                cR++;
                cRen = String.Concat("CountRename=", cR.ToString());
                iniStrings[1] = cRen;
            }            
            File.WriteAllLines(iniFile, iniStrings);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selEnd = comboBox1.SelectedItem.ToString();
            //zipFile2 = zipFile1 + selEnd;            
            //MessageBox.Show(comboBox1.SelectedItem.ToString());
        }

        /*public void SetText(string text)
        {
            textBox1.Text = text;
        }*/

        public void renReestr(string pathFolder, string zipFile1, string endName)
        {
            string txtName1, iniName1, txtName2, iniName2, replStr;
            DirectoryInfo dirInfo = new DirectoryInfo(pathFolder);
            if (dirInfo.Exists)
            {
                //zipFile2 = "C://prr//F007710276486408500023.ZIP";
                //txtName1 = String.Concat("c://prr//tf//", zipFile1.Substring(7, 22), ".txt");
                string[] allfiles = Directory.GetFiles(pathFolder);
                //MessageBox.Show("RenReestr from list txt " + allfiles[1]);
                txtName1 = allfiles[0];
                iniName1 = allfiles[1];                 
                txtName2 = String.Concat(txtName1.Substring(0, 29), endName, ".txt");
                iniName2 = String.Concat(iniName1.Substring(0, 29), endName, "h.ini");
                //MessageBox.Show(txtName1 + "RenReestr txt file name" + txtName2);
                // Переименование файла
                FileInfo fi = new System.IO.FileInfo(txtName1);
                if (fi.Exists)
                {
                    fi.MoveTo(txtName2);
                }
                else
                {
                    MessageBox.Show("RenReestr txt file not found" );
                }
                FileInfo fi_ = new System.IO.FileInfo(iniName1);
                if (fi_.Exists)
                {
                    fi_.MoveTo(iniName2);
                }
                else
                {
                    MessageBox.Show("RenReestr ini file not found");
                }
                //Изменение ини файла                
                Encoding win1251 = Encoding.GetEncoding( "windows-1251" );                
                string[] readIni = File.ReadAllLines(iniName2, win1251);
                //MessageBox.Show("RenReestr repl str " + readIni[0]);
                replStr = String.Concat(readIni[7].Substring(0, 8), endName);
                readIni[7] = replStr;
                File.WriteAllLines(iniName2, readIni, win1251);
                //MessageBox.Show("RenReestr repl str " + replStr);                

                zipFile2 = String.Concat(zipFile1.Substring(7, 20), endName, ".ZIP");
                
                //ZipFile.CreateFromDirectory(pathFolder, zipFile2);
            }
            else
            {
                MessageBox.Show("RenReestr Каталог не существует");
            }
        }

        public void createFolder(string pathFolder)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(pathFolder);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
        }


        public void clearFolder(string pathFolder)
        {
            if (Directory.Exists(pathFolder))
            {
                Directory.Delete(pathFolder, true);            
            }
            else
            {
                MessageBox.Show("Каталог не существует");
            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            createFolder(tempFolder);
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var filePath = openFileDialog1.FileName;
                    using (Stream str = openFileDialog1.OpenFile())
                    {
                        ZipFile.ExtractToDirectory(filePath, tempFolder);                        
                        MessageBox.Show("OFD OK");                        
                    }
                              
                }
                catch (SecurityException ex)
                {
                    MessageBox.Show(ex.Message);
                    MessageBox.Show(ex.StackTrace);                                        
                }
            }
            clearFolder(tempFolder);
            //MessageBox.Show("Реестр перименован");
        }

        private void button1_Click(object sender, EventArgs e)
        {            
            createFolder(tempFolder);
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var filePath = openFileDialog1.FileName;
                    using (Stream str = openFileDialog1.OpenFile())
                    {
                        ZipFile.ExtractToDirectory(filePath, tempFolder);
                        //MessageBox.Show(filePath);
                        renReestr(tempFolder, filePath, selEnd);                        
                        ZipFile.CreateFromDirectory(tempFolder, targetFolder + zipFile2);
                        iniFileWork(0, 0, 1);
                    }
                        
                }
                catch (SecurityException ex)
                {
                    MessageBox.Show(ex.Message);
                    MessageBox.Show(ex.StackTrace);                                        
                }
            }
            clearFolder(tempFolder);
            //MessageBox.Show("Реестр перименован");
            //clearFolder(sourceFolder);
            iniFileWork(1, 1, 0);
        }
    }
}
