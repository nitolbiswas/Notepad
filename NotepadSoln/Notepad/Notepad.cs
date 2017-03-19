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
            if (isFileDirty)
            {
                DialogResult result = MessageBox.Show("Do You want to Save this file?", "File Save",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                switch (result)
                {
                    case DialogResult.Yes:
                        SaveFileMenu();
                        ClearScreen();
                        break;
                    case DialogResult.No:
                        ClearScreen();
                        break;
                }
            }
            else
            {
                ClearScreen();
            }

            UndoRedoOnOf(false);
        }

        private void UndoRedoOnOf(bool enable)
        {
            undoToolStripMenuItem.Enabled = enable;
            redoToolStripMenuItem.Enabled = enable;
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
            UndoRedoOnOf(false);
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
            undoToolStripMenuItem.Enabled = true;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileMenu();
        }

        private void SaveFileMenu()
        {
            if (isFileAlreadySave)
            {
                if (Path.GetExtension(CurrOpenFile) == ".txt")
                    MainRichTextBox.SaveFile(CurrOpenFile, RichTextBoxStreamType.PlainText);
                if (Path.GetExtension(CurrOpenFile) == ".rtf")
                    MainRichTextBox.SaveFile(CurrOpenFile, RichTextBoxStreamType.PlainText);
                isFileDirty = false;
            }
            else
            {
                if (isFileDirty)
                {
                    SaveAs();
                }
                else
                {
                    ClearScreen();
                }
            }
        }

        private void ClearScreen()
        {
            MainRichTextBox.Clear();
            this.Text = "Untitle - Notepad";
            isFileDirty = false;
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainRichTextBox.Undo();
            redoToolStripMenuItem.Enabled = true;
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainRichTextBox.Redo();
            redoToolStripMenuItem.Enabled = false;
            undoToolStripMenuItem.Enabled = true;
        }

        private void selectAllToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MainRichTextBox.SelectAll();
        }

        private void dateTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainRichTextBox.SelectedText += DateTime.Now.ToString();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            FormatText(FontStyle.Bold);
        }

        private void FormatText(FontStyle fontStyle)
        {
            MainRichTextBox.SelectionFont = new Font(MainRichTextBox.Font, fontStyle);
        }

        private void italicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormatText(FontStyle.Italic);
        }

        private void underlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormatText(FontStyle.Underline);
        }

        private void strickThrowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormatText(FontStyle.Strikeout);
        }

        private void formatFontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog = new FontDialog();
            fontDialog.ShowColor = true;

            DialogResult result = fontDialog.ShowDialog();
            if(result == DialogResult.OK)
            {
                MainRichTextBox.SelectionFont = fontDialog.Font;
                MainRichTextBox.SelectionColor = fontDialog.Color;
            }
        }
    }
}