using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MoveFileToFolderWithPrefixs
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string selectedPath = "";
            string showText = "";
            if(string.IsNullOrEmpty( textBox1.Text) )
            {
                var fbd = this.folderBrowserDialog1;
                DialogResult result = fbd.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    selectedPath = fbd.SelectedPath;
                    textBox1.Text = fbd.SelectedPath;
                }
            }
            else
            {
                selectedPath = textBox1.Text;
            }
            var ext = new List<string> { ".jpg", ".gif", ".png", ".jpeg" };
            var files = Directory.GetFiles(selectedPath, "*.*", SearchOption.AllDirectories)
                .Where(s => ext.Contains(Path.GetExtension(s))).ToArray();
            foreach (var fileItem in files)
            {
                MoveFileToFolder(fileItem);
                showText += $"{fileItem}\r\n";
            }
            this.textBox2.Text = showText;
        }

        private string MoveFileToFolder(string s)
        {
            //C:\Users\vhhiep\Desktop\test pdf\maximumPc\Maximum PC - May 2017_Page_003.jpg
            var fileName = s.Split('\\').Last();
            var urlPath = s.Substring(0, s.Length - fileName.Length);
            var folderName = fileName.Substring(0, fileName.IndexOf("_Page_"));
            var folderUrl = $"{urlPath}{folderName}";
            System.IO.Directory.CreateDirectory(folderUrl);
            System.IO.File.Move(s, $"{folderUrl}\\{fileName}");
            return s;
        }
    }
}
