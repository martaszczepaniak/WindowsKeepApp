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

namespace WindowsKeepApplication
{
    public partial class KeepIt : MetroForm
    {
        DatabaseConnection m_db;
        NoteCreator m_noteCreator = new NoteCreator();
        public int noteContainerY = 135;
        Stack<NoteContainer> noteContainerStack = new Stack<NoteContainer>();
        public MetroFramework.Controls.MetroButton noteDeleteButton;

        public KeepIt(DatabaseConnection db)
        {
            m_db = db;
            this.ClientSize = new Size(600, 450);

            this.Name = "KeepIt";
            this.Text = "KeepIt";

            this.Controls.Add(m_noteCreator);
            m_noteCreator.CreateNoteFormOpener();
            this.Controls.Add(m_noteCreator.noteFormOpener);
            this.Controls.Add(CreateNoteDeleteButton());

            m_noteCreator.noteSubmitButton.Click += new EventHandler(noteSubmitButton_Click);
            m_noteCreator.noteFormOpener.Click += new EventHandler(noteFormOpener_Click);
            this.noteDeleteButton.Click += new EventHandler(noteDeleteButton_Click);
            retrieveNotes();
        }

        private void noteFormOpener_Click(object sender, EventArgs e)
        {
            openForm();
        }

        private void noteSubmitButton_Click(object sender, EventArgs e)
        {
            createNote();
            deleteNotes();
            retrieveNotes();
            closeForm();
        }

        public MetroFramework.Controls.MetroButton CreateNoteDeleteButton()
        {
            noteDeleteButton = new MetroFramework.Controls.MetroButton();
            noteDeleteButton.Location = new Point(554, 23);
            noteDeleteButton.Name = "noteDeleteButton";
            noteDeleteButton.Size = new Size(40, 38);
            noteDeleteButton.UseCustomBackColor = true;
            noteDeleteButton.BackColor = Color.Honeydew;
            noteDeleteButton.Text = "delete";
            return noteDeleteButton;
        }

        private void noteDeleteButton_Click(object sender, EventArgs e)
        {
            deleteNote();
        }

        private void openForm()
        {
            m_noteCreator.Visible = true;
            m_noteCreator.noteFormOpener.Visible = false;
            m_noteCreator.noteTitle.Focus();
        }

        private void closeForm()
        {
            m_noteCreator.Visible = false;
            m_noteCreator.noteFormOpener.Visible = true;
            m_noteCreator.noteTitle.Text = "";
        }

        private void createNote()
        {
            SQLiteCommand insertSQL = new SQLiteCommand("INSERT INTO notes (title) VALUES (?)", m_db.m_dbConnection);
            insertSQL.Parameters.AddWithValue("title", m_noteCreator.noteTitle.Text);
            insertSQL.ExecuteNonQuery();
        }

        private void retrieveNotes()
        {
            SQLiteCommand selectSQL = new SQLiteCommand("SELECT * FROM notes", m_db.m_dbConnection);
            SQLiteDataReader reader = selectSQL.ExecuteReader();
            int x = 0;
            int y = 0;
            int i = 0;
            
            while (reader.Read())
            {
                int usedSpace = i * 183 + 23;
                int freeSpace = Size.Width - usedSpace;
                Note note = new Note(reader["id"].ToString(), reader["category"].ToString(), reader["title"].ToString(), reader["color"].ToString());
                string noteItems = note.Items(m_db.m_dbConnection);

                if (freeSpace > 183)
                {
                    x = i * 183;
                    i++;
                }
                else
                {
                    x = 0;
                    y += 220;
                    i = 1;
                }

                NoteContainer noteContainer = new NoteContainer(note, new Point(23 + x, noteContainerY + y), noteItems);
                this.Controls.Add(noteContainer);

                noteContainerStack.Push(noteContainer);
            }
        }

        private void deleteNotes()
        {
            foreach(NoteContainer noteContainer in noteContainerStack)
            {
                this.Controls.Remove(noteContainer);
            }
            noteContainerStack.Clear();
        }

        public void deleteNote()
        {
            NoteContainer noteContainer = noteContainerStack.Peek();
            this.Controls.Remove(noteContainer);
            noteContainerStack.Pop();
            SQLiteCommand insertSQL = new SQLiteCommand("DELETE FROM notes WHERE id = @noteId", m_db.m_dbConnection);
            insertSQL.Parameters.Add(new SQLiteParameter("@noteId", "1"));
            insertSQL.ExecuteNonQuery();
        }

    }
}
