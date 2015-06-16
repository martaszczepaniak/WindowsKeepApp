using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using System.Data.SQLite;
using System.Net;
using System.Net.Mail;

namespace WindowsKeepApplication
{
    class sendingForm : Form
    {
        TextBox To = new TextBox();
        public NoteContainer.SendTitleDelegate m_sendTitle;
        public NoteContainer.SendTextDelegate m_sendText;

        public sendingForm(NoteContainer.SendTitleDelegate sendTitle, NoteContainer.SendTextDelegate sendText)
        {
            m_sendText = sendText;
            m_sendTitle = sendTitle;
            this.Controls.Add(To);
            Label SendTo = new Label();
            this.Controls.Add(SendTo);
            To.Show();
            To.Text = "";
            SendTo.Show();
            To.Location = new Point(70, 50);
            To.Size = new Size(150, 20);
            SendTo.Location = new Point(70, 30);
            SendTo.Size = new Size(150, 20);
            SendTo.Text = "Send To:";
            Button sendButton = new Button();
            sendButton.Show();
            this.Controls.Add(sendButton);
            sendButton.Location = new Point(95, 70);
            sendButton.Size = new Size(100, 20);
            sendButton.Text = "Send";
            sendButton.Click += new EventHandler(sendButton_Click);
        }

        public void SendMessageTo()
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();

                message.From = new MailAddress("winkeepappfrom@gmail.com");
                message.To.Add(new MailAddress(To.Text));
                message.Subject = this.m_sendTitle();
                message.Body = this.m_sendText();

                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("winkeepappfrom@gmail.com", "");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("err: " + ex.Message);
            }
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            this.SendMessageTo();
        }

    }
}
