using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using System.Data.SQLite;
using System.Drawing;
using System.ComponentModel;
using System.Net;
using System.Net.Mail;

namespace WindowsKeepApplication
{
    class NoteContainer : GroupBox
    {
        Note m_note;
        Point m_location;
        public MetroFramework.Controls.MetroButton noteContainerDeleteButton = new MetroFramework.Controls.MetroButton();
        public MetroFramework.Controls.MetroButton noteContainerColorChangeButton = new MetroFramework.Controls.MetroButton();
        public MetroFramework.Controls.MetroButton sendButton = new MetroFramework.Controls.MetroButton();
        MetroFramework.Controls.MetroTextBox noteContainerText;
        MetroFramework.Controls.MetroTextBox noteContainerTitle;
        public KeepIt.DeleteNoteDelegate m_deleteNote;
        public KeepIt.EditNoteDelegate m_editNote;
        public KeepIt.ColorDelegate m_colors;
        public KeepIt.ReloadDelegate m_reload;

        public delegate string SendTitleDelegate();
        private SendTitleDelegate sendTitleDelegate;

        public delegate string SendTextDelegate();
        private SendTextDelegate sendTextDelegate;

        ContextMenu ColorChangeContextMenu;



        public NoteContainer(Note note, Point location, KeepIt.DeleteNoteDelegate DeleteNote, KeepIt.EditNoteDelegate EditNote, KeepIt.ColorDelegate NoteColor, KeepIt.ReloadDelegate Reload)
        {
            m_note = note;
            m_location = location;
            m_deleteNote = DeleteNote;
            m_editNote = EditNote;
            m_colors = NoteColor;
            m_reload = Reload;

            SetDefaultAttributes();
            this.Controls.Add(CreateTitleTextBox());
            this.Controls.Add(CreateTextTextBox());
            this.Controls.Add(CreateDeleteButton());
            this.Controls.Add(CreateColorChangeButton());
            this.Controls.Add(CreateSendButton());

            sendTitleDelegate = SendMessageTitle;
            sendTextDelegate = SendMessageText;

        }

        private void SetDefaultAttributes()
        {
            this.Location = m_location;
            this.Name = "NoteContainer" + m_note.m_id.ToString();
            this.Size = new Size(200, 200);
            BackColor = Color.FromName(m_note.m_color);
        }

        //-----------------------------------DELEGATE METHODS----------------------
        private string SendMessageTitle()
        {
            return noteContainerTitle.Text;
        }

        private string SendMessageText()
        {
            return noteContainerText.Text;
        }

        private MetroFramework.Controls.MetroTextBox CreateTextTextBox()
        {
            noteContainerText = new MetroFramework.Controls.MetroTextBox();
            noteContainerText.Location = new Point(0, 30);
            noteContainerText.Name = "NoteContainerText" + m_note.m_id.ToString();
            noteContainerText.Size = new Size(200, 150);
            noteContainerText.Multiline = true;
            Console.WriteLine(m_note.m_text);
            noteContainerText.Text = m_note.m_text ;
            noteContainerText.UseCustomBackColor = true;
            noteContainerText.BackColor = Color.FromName(m_note.m_color);
            noteContainerText.Leave += new EventHandler(noteContainerText_Leave);
            return noteContainerText;
        }

        private MetroFramework.Controls.MetroTextBox CreateTitleTextBox()
        {
            noteContainerTitle = new MetroFramework.Controls.MetroTextBox();
            noteContainerTitle.Location = new Point(0, 0);
            noteContainerTitle.Name = "noteContainerTitle" + m_note.m_id.ToString();
            noteContainerTitle.Size = new Size(200, 30);
            noteContainerTitle.Multiline = true;
            noteContainerTitle.Text = m_note.m_title;
            noteContainerTitle.UseCustomBackColor = true;
            noteContainerTitle.BackColor = Color.FromName(m_note.m_color);
            noteContainerTitle.Leave += new EventHandler(noteContainerTitle_Leave);
            return noteContainerTitle;
        }

        private MetroFramework.Controls.MetroButton CreateDeleteButton()
        {
            noteContainerDeleteButton.Location = new Point(140, 180);
            noteContainerDeleteButton.Name = "noteContainerDeleteButton" + m_note.m_id.ToString();
            noteContainerDeleteButton.Size = new Size(60, 20);
            noteContainerDeleteButton.Click += new EventHandler(noteDeleteButton_Click);
            noteContainerDeleteButton.Text = "X";
            return noteContainerDeleteButton;
        }

        private MetroFramework.Controls.MetroButton CreateColorChangeButton()
        {
            noteContainerColorChangeButton.Location = new Point(60, 180);
            noteContainerColorChangeButton.Name = "noteContainerColorChangeButton" + m_note.m_id.ToString();
            noteContainerColorChangeButton.Size = new Size(80, 20);
            noteContainerColorChangeButton.Text = "Change color";
            noteContainerColorChangeButton.Click += new EventHandler(noteContainerColorChangeButton_Click);
            return noteContainerColorChangeButton;
        }

        private MetroFramework.Controls.MetroButton CreateSendButton()
        {
            sendButton.Location = new Point(0, 180);
            sendButton.Name = "noteContainerColorChangeButton" + m_note.m_id.ToString();
            sendButton.Size = new Size(60, 20);
            sendButton.Text = "Send";
            sendButton.Click += new EventHandler(sendButton_Click);
            return sendButton;
        }

        private void noteDeleteButton_Click(object sender, EventArgs e)
        {
            m_deleteNote(m_note.m_id);
        }

        private void noteContainerColorChangeButton_Click(object sender, EventArgs e)
        {
            ColorChangeContextMenu = new ContextMenu();
            MenuItem Aquamarine = new MenuItem("Aquamarine");
            Aquamarine.Click += Aquamarine_Click;
            MenuItem GreenYellow = new MenuItem("GreenYellow");
            GreenYellow.Click += GreenYellow_Click;
            MenuItem Yellow = new MenuItem("Yellow");
            Yellow.Click += Yellow_Click;
            ColorChangeContextMenu.MenuItems.Add(Aquamarine);
            ColorChangeContextMenu.MenuItems.Add(GreenYellow);
            ColorChangeContextMenu.MenuItems.Add(Yellow);
            ColorChangeContextMenu.Show(noteContainerColorChangeButton, new Point(0,20));
            noteContainerColorChangeButton.UseCustomBackColor = true;
            noteContainerColorChangeButton.BackColor = Color.FromName(m_note.m_color);
        }

        private void GreenYellow_Click(object sender, EventArgs e)
        {
            this.m_colors("GreenYellow", m_note.m_id);
            this.m_reload();
        }

        private void Yellow_Click(object sender, EventArgs e)
        {
            this.m_colors("Yellow", m_note.m_id);
            this.m_reload();
        }

        private void Aquamarine_Click(object sender, EventArgs e)
        {
            this.m_colors("Aquamarine", m_note.m_id);
            this.m_reload();
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            sendingForm sForm = new sendingForm(sendTitleDelegate, sendTextDelegate);
            sForm.Show();
        }

        private void noteContainerText_Leave(object sender, EventArgs e)
        {
            this.m_editNote(noteContainerTitle.Text, noteContainerText.Text, m_note.m_id);
        }

        private void noteContainerTitle_Leave(object sender, EventArgs e)
        {
            this.m_editNote(noteContainerTitle.Text, noteContainerText.Text, m_note.m_id);
        }
    }
}
