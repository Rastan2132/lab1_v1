using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace lab1_v1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void wybur_button_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.Description = "Wybierz katalog z plikami JPG";
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                string selectedFolderPath = folderBrowserDialog1.SelectedPath;
                textBox1.Text = selectedFolderPath;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.Description = "Wybierz katalog docelowy dla kopii plików JPG";
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                string destinationFolderPath = folderBrowserDialog1.SelectedPath;
                textBox2.Text = destinationFolderPath;
            }
            button2.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sourceFolderPath = textBox1.Text;
            string destinationFolderPath = textBox2.Text;

            CopyImageFiles(sourceFolderPath, destinationFolderPath);

            MessageBox.Show("Pliki JPG zostały skopiowane do katalogu docelowego.");
        }

        private void CopyImageFiles(string sourceFolderPath, string destinationFolderPath)
        {
            string[] supportedExtensions = { ".jpg", ".jpeg", ".png", ".ico", ".gif", ".tiff", ".webp", ".eps", ".svg" };

            foreach (string sourceFilePath in Directory.GetFiles(sourceFolderPath, "*.*", SearchOption.AllDirectories))
            {
                string sourceExtension = Path.GetExtension(sourceFilePath).ToLower();

                if (Array.Exists(supportedExtensions, ext => ext == sourceExtension))
                {
                    DateTime creationTime = File.GetCreationTime(sourceFilePath);
                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(sourceFilePath);
                    string destinationFileName = $"{creationTime:yyyyMMddHHmmss}_{Path.Combine(Path.GetDirectoryName(sourceFilePath),
                        fileNameWithoutExtension).Replace(sourceFolderPath, "").TrimStart('\\').Replace("\\", "_")}" +
                        $"{sourceExtension}";
                    string destinationFilePath = Path.Combine(destinationFolderPath, destinationFileName);

                    File.Copy(sourceFilePath, destinationFilePath);
                }
            }
        }

    }
}
