using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using System.Data.SQLite;
using System.Drawing;

namespace WindowsKeepApplication
{
    class NoteCreator : GroupBox
    {

        public NoteCreator()
        {
            SetDefaultAttributes();
            this.Controls.Add(CreateNoteTitleTextBox());
            this.Controls.Add(CreateNoteTextTextBox());
            this.Controls.Add(CreateNoteSubmitButton());
           
        }

        public MetroFramework.Controls.MetroTextBox noteTitle;
        public MetroFramework.Controls.MetroTextBox noteText;
        public MetroFramework.Controls.MetroButton noteSubmitButton;
        public MetroFramework.Controls.MetroTextBox noteFormOpener;

        private void SetDefaultAttributes()
        {
           
            this.Location = new Point(23, 63);
            this.Name = "NoteCreatorGroup";
            this.Size = new Size(554, 61);
            this.Visible = false;
        }

        public MetroFramework.Controls.MetroTextBox CreateNoteTitleTextBox()
        {
            noteTitle = new MetroFramework.Controls.MetroTextBox();
            noteTitle.Location = new Point(0, 0);
            noteTitle.Name = "noteTitle";
            noteTitle.Size = new Size(554, 23);
            return noteTitle;
        }
        public MetroFramework.Controls.MetroTextBox CreateNoteTextTextBox()
        {
            noteText = new MetroFramework.Controls.MetroTextBox();
            noteText.Location = new Point(0, 23);
            noteText.Name = "noteText";
            noteText.Size = new Size(414, 38);
            return noteText;
        }

        public MetroFramework.Controls.MetroButton CreateNoteSubmitButton()
        {
            noteSubmitButton = new MetroFramework.Controls.MetroButton();
            noteSubmitButton.Location = new Point(414, 23);
            noteSubmitButton.Name = "noteSubmitButton";
            noteSubmitButton.Size = new Size(140, 38);
           // noteSubmitButton.BackColor = Color.SkyBlue;
            noteSubmitButton.Text = "READY";
            return noteSubmitButton;
        }

        public MetroFramework.Controls.MetroTextBox CreateNoteFormOpener()
        {
            noteFormOpener = new MetroFramework.Controls.MetroTextBox();
            noteFormOpener.Location = new Point(23, 63);
            noteFormOpener.Name = "noteFormOpener";
            noteFormOpener.Size = new Size(554, 23);
            
        
            noteFormOpener.Text = "Add a note...";
            return noteFormOpener;
        }
    }
}
