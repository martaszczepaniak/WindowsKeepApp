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
        ContextMenu ColorChangeContextMenu;


        public NoteContainer(Note note, Point location, KeepIt.DeleteNoteDelegate DeleteNote)
        {
            m_note = note;
            m_location = location;
            m_deleteNote = DeleteNote;

            SetDefaultAttributes();
            this.Controls.Add(CreateTitleTextBox());
            this.Controls.Add(CreateTextTextBox());
            this.Controls.Add(CreateDeleteButton());
            this.Controls.Add(CreateColorChangeButton());
            this.Controls.Add(CreateSendButton());
            
        }

        private void SetDefaultAttributes()
        {
            this.Location = m_location;
            this.Name = "NoteContainer" + m_note.m_id.ToString();
            this.Size = new Size(200, 200);
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
            return noteContainerTitle;
        }

        private MetroFramework.Controls.MetroButton CreateDeleteButton()
        {
            noteContainerDeleteButton.Location = new Point(140, 180);
            noteContainerDeleteButton.Name = "noteContainerDeleteButton" + m_note.m_id.ToString();
            noteContainerDeleteButton.Size = new Size(20, 20);
            noteContainerDeleteButton.Click += new EventHandler(noteDeleteButton_Click);
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
            MenuItem yellow = new MenuItem("yellow");
            yellow.Click += Yellow_Click;
            MenuItem peachPuff = new MenuItem("PeachPuff");
            peachPuff.Click += Red_Click;
            ColorChangeContextMenu.MenuItems.Add(yellow);
            ColorChangeContextMenu.MenuItems.Add(peachPuff);
            ColorChangeContextMenu.Show(noteContainerColorChangeButton, new Point(0,20));
        }

        private void Red_Click(object sender, EventArgs e)
        {
            noteContainerTitle.UseCustomBackColor = true;
            noteContainerTitle.BackColor = Color.PeachPuff;
            noteContainerColorChangeButton.UseCustomBackColor = true;
            noteContainerColorChangeButton.BackColor = Color.PeachPuff;
            noteContainerText.UseCustomBackColor = true;
            noteContainerText.BackColor = Color.PeachPuff;
            BackColor = Color.PeachPuff;
            noteContainerDeleteButton.UseCustomBackColor = true;
            noteContainerDeleteButton.BackColor = Color.PeachPuff;
        }

        private void Yellow_Click(object sender, EventArgs e)
        {
            noteContainerTitle.UseCustomBackColor = true;
            noteContainerTitle.BackColor = Color.Yellow;
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();

                message.From = new MailAddress("winkeepappfrom@gmail.com");
                message.To.Add(new MailAddress("mart.szcz@gmail.com"));
                message.Subject = noteContainerTitle.Text;
                message.Body = noteContainerText.Text;

                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("winkeepappfrom@gmail.com", "najlepszehaslo");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("err: " + ex.Message);
            }
        }
    }
}
