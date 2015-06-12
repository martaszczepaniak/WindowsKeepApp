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
    class NoteContainer : GroupBox
    {
        Note m_note;
        Point m_location;
        public int m_noteContainerId;
        string m_noteItems;
        public MetroFramework.Controls.MetroButton noteContainerDeleteButton = new MetroFramework.Controls.MetroButton();


        public NoteContainer(Note note, Point location, string noteItems, int noteContainerId)
        {
            m_note = note;
            m_location = location;
            m_noteItems = noteItems;
            m_noteContainerId = noteContainerId;
            //m_noteContainerId = m_note.m_id;

            SetDefaultAttributes();
            this.Controls.Add(CreateTitleTextBox());
            this.Controls.Add(CreateTextTextBox());
            this.Controls.Add(CreateDeleteButton());
        }

        private void SetDefaultAttributes()
        {
            this.Location = m_location;
            this.Name = "NoteContainer" + m_note.m_id.ToString();
            this.Size = new Size(160, 200);
        }

        private MetroFramework.Controls.MetroTextBox CreateTextTextBox()
        {
            MetroFramework.Controls.MetroTextBox noteContainerText = new MetroFramework.Controls.MetroTextBox();
            noteContainerText.Location = new Point(0, 30);
            noteContainerText.Name = "NoteContainerText" + m_note.m_id.ToString();
            noteContainerText.Size = new Size(160, 150);
            noteContainerText.Multiline = true;
            noteContainerText.Text = m_noteItems;
            return noteContainerText;
        }

        private MetroFramework.Controls.MetroTextBox CreateTitleTextBox()
        {
            MetroFramework.Controls.MetroTextBox noteContainerTitle = new MetroFramework.Controls.MetroTextBox();
            noteContainerTitle.Location = new Point(0, 0);
            noteContainerTitle.Name = "noteContainerTitle" + m_note.m_id.ToString();
            noteContainerTitle.Size = new Size(160, 30);
            noteContainerTitle.Multiline = true;
            noteContainerTitle.Text = m_note.m_title;
            return noteContainerTitle;
        }

        private MetroFramework.Controls.MetroButton CreateDeleteButton()
        {
            noteContainerDeleteButton.Location = new Point(140, 180);
            noteContainerDeleteButton.Name = "noteContainerDeleteButton" + m_note.m_id.ToString();
            noteContainerDeleteButton.Size = new Size(20, 20);
            return noteContainerDeleteButton;
        }
    }

}
