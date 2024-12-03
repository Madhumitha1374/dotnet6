using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ProgressBarComplete
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int value = 0;
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //for (int i = 1; i< 100; i++)
            //{
            //    Thread.Sleep(10000);
            //    backgroundWorker1.ReportProgress(0);
            //}
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //progressBar1.Value += 1;
            //label1.Text = "Progress Bar Loading...." + progressBar1.Value;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Completed");
        }

        // SECOND PROGRESS BAR
        private async void button1_Click(object sender, EventArgs e)
        {
            progressBar2.Maximum = 100;
            progressBar2.Minimum = 0;
            //backgroundWorker1.RunWorkerAsync();

            string notepadFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "WordDocumentsList.txt");

            using (StreamWriter writer = new StreamWriter(notepadFilePath, false))
            {
                for(int i =1; i <= 100; i++)
                {
                    value += 1;
                    progressBar2.Value = value;
                    await writer.WriteLineAsync(value.ToString());
                    if(value == 100)
                    {
                        label2.Text = "Data copied Completed";
                    }
                    //else
                    //{
                    //    label2.Text = "Progressing......" + value.ToString();
                    //}
                    await Task.Delay(100);
                }
            }

            // Optionally, open the Notepad file after writing
            //System.Diagnostics.Process.Start("notepad.exe", notepadFilePath);
        }


        // FIRST PROGRESS BAR
        private async void btnBrowse_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedPath = folderDialog.SelectedPath;
                    await FindWordDocumentsAsync(selectedPath);
                }
            }
        }
        private async Task FindWordDocumentsAsync(string folderPath)
        {
            try
            {
                lstWordDocs.Items.Clear();
                progressBar1.Value = 0;

                var allFiles = Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories)
                                        .Where(file => file.EndsWith(".doc", StringComparison.OrdinalIgnoreCase) ||
                                                       file.EndsWith(".docx", StringComparison.OrdinalIgnoreCase))
                                        .ToArray();

                progressBar1.Maximum = allFiles.Length;

                foreach (var file in allFiles)
                {
                    lstWordDocs.Items.Add(file);
                    progressBar1.Value++;

                    await Task.Delay(100); // Simulate some delay for the sake of demonstration
                }
                await WriteToNotepadAsync(allFiles);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private async Task WriteToNotepadAsync(string[] filePaths)
        {
            try
            {
                string notepadFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "WordDocumentsList.txt");

                using (StreamWriter writer = new StreamWriter(notepadFilePath, false))
                {
                    foreach (var file in filePaths)
                    {
                        await writer.WriteLineAsync(file);
                    }
                }

                // Optionally, open the Notepad file after writing
                System.Diagnostics.Process.Start("notepad.exe", notepadFilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while writing to Notepad: " + ex.Message);
            }
        }
    }
}
