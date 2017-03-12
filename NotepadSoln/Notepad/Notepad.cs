﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notepad
{
    public partial class Notepad : Form
    {
        private bool isFileAlreadySave;
        private bool isFileDirty;
        private string CurrOpenFile;
        public Notepad()
        {
            InitializeComponent();
        }
        private void aboutNotepadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("All right Reserved with the Author", "About Notepad",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainRichTextBox.Clear();
        }
        private void exitApllicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //open a file 
            OpenFileDialog openFileDialog = new OpenFileDialog();
            //Filter For Spcefic File type
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|Rich Text Format (*.rtf)|*.rtf";
            DialogResult result = openFileDialog.ShowDialog();
            if(result == DialogResult.OK)
            {
                if(Path.GetExtension(openFileDialog.FileName) == ".txt")
                    MainRichTextBox.LoadFile(openFileDialog.FileName, RichTextBoxStreamType.PlainText);
                if(Path.GetExtension(openFileDialog.FileName) == ".rtf")
                    MainRichTextBox.LoadFile(openFileDialog.FileName, RichTextBoxStreamType.PlainText);
                this.Text = Path.GetFileName(openFileDialog.FileName) + " - Notepad";

                isFileAlreadySave = true;
                isFileDirty = false;
                CurrOpenFile = openFileDialog.FileName;
            }
        }
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAs();
        }
        private void SaveAs()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|Rich Text Format (*.rtf)|*.rtf";

            DialogResult result = saveFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (Path.GetExtension(saveFileDialog.FileName) == ".txt")
                    MainRichTextBox.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.PlainText);
                if (Path.GetExtension(saveFileDialog.FileName) == ".rtf")
                    MainRichTextBox.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.PlainText);

                this.Text = Path.GetFileName(saveFileDialog.FileName) + " - Notepad";

                isFileAlreadySave = true;
                isFileDirty = false;
                CurrOpenFile = saveFileDialog.FileName;
            }
        }
        private void Notepad_Load(object sender, EventArgs e)
        {
            isFileAlreadySave = false;
            isFileDirty = false;
            CurrOpenFile = "";
        }
        private void MainRichTextBox_TextChanged(object sender, EventArgs e)
        {
            isFileDirty = true;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(isFileAlreadySave)
            {
                if (Path.GetExtension(CurrOpenFile) == ".txt")
                    MainRichTextBox.SaveFile(CurrOpenFile, RichTextBoxStreamType.PlainText);
                if (Path.GetExtension(CurrOpenFile) == ".rtf")
                    MainRichTextBox.SaveFile(CurrOpenFile, RichTextBoxStreamType.PlainText);
                isFileDirty = false;
            }
            else
            {
                if(isFileDirty)
                {
                    SaveAs();
                }
                else
                {
                    MainRichTextBox.Clear();
                    this.Text = "Untitle - Notepad";
                }
            }
        }
    }
}