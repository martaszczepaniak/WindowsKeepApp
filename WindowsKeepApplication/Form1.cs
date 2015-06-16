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

        public delegate void CreateNoteDelegate(string title, string body, string color);
        public delegate void DeleteNoteDelegate(int noteId);
        public delegate void EditNoteDelegate(string title, string body, int id);
        public delegate void ColorDelegate(string color, int id);
        public delegate void ReloadDelegate();
        private ReloadDelegate reloadDelegate;
        private ColorDelegate colorDelegate;
        private DeleteNoteDelegate deleteNoteDelegate;
        private EditNoteDelegate editNoteDelegate;

        NoteCreator m_noteCreator;
        NoteList m_noteList = new NoteList();
        Dictionary<int, NoteContainer> m_noteContainerList = new Dictionary<int, NoteContainer>();

        public MetroFramework.Controls.MetroTextBox noteFormOpener;

        public KeepIt(DatabaseConnection db)
        {
            m_db = db;
            SetDefaultAttributes();

            CreateNoteDelegate createNoteDelegate = CreateNote;
            deleteNoteDelegate = DeleteNote;
            editNoteDelegate = EditNote;
            colorDelegate = UpdateColors;
            reloadDelegate = Reload;

            m_noteCreator = new NoteCreator(createNoteDelegate);
            Controls.Add(m_noteCreator);

            Controls.Add(CreateNoteFormOpener());

            GetNotes();
            RenderNoteContainers();
        }

        private void SetDefaultAttributes()
        {
            ClientSize = new Size(700, 450);
            Name = "KeepIt";
            Text = "KeepIt";
            this.AutoScroll = true;
        }

        // --------------------------- DELEGATED METHODS --------------------------

        public void CreateNote(string title, string body, string color)
        {
            SQLiteCommand insertSQL = new SQLiteCommand("INSERT INTO notes (title, body, color) VALUES (@title, @body, @color)", m_db.m_dbConnection);
            insertSQL.Parameters.AddWithValue("title", title);
            insertSQL.Parameters.AddWithValue("body", body);
            insertSQL.Parameters.AddWithValue("color", color);
            insertSQL.ExecuteNonQuery();
            GetNotes();
            RenderNoteContainers();
            CloseForm();
        }

        public void EditNote(string title, string body, int id)
        {
            SQLiteCommand insertSQL = new SQLiteCommand("UPDATE notes SET title = @title, body = @body WHERE id = @id", m_db.m_dbConnection);
            insertSQL.Parameters.AddWithValue("title", title);
            insertSQL.Parameters.AddWithValue("body", body);
            insertSQL.Parameters.AddWithValue("id", id);
            insertSQL.ExecuteNonQuery();
        }

        private void GetNotes()
        {
            SQLiteCommand selectSQL = new SQLiteCommand("SELECT * FROM notes", m_db.m_dbConnection);
            SQLiteDataReader reader = selectSQL.ExecuteReader();
            
            m_noteList.Clear();

            while (reader.Read())
            {

                Note note = new Note(reader.GetInt32(0), reader["body"].ToString(), reader["title"].ToString(), reader["color"].ToString());

                m_noteList.Add(reader.GetInt32(0), note);
            }
        }

        private void DeleteNote(int noteId)
        {
            SQLiteCommand deleteSQL = new SQLiteCommand("DELETE FROM notes WHERE id = @id", m_db.m_dbConnection);
            deleteSQL.Parameters.AddWithValue("id", noteId);
            deleteSQL.ExecuteNonQuery();
            GetNotes();
            RenderNoteContainers();
        }

        private void UpdateColors(string color, int id)
        {
            SQLiteCommand insertSQL = new SQLiteCommand("UPDATE notes SET color = @color WHERE id = @id", m_db.m_dbConnection);
            insertSQL.Parameters.AddWithValue("color", color);
            insertSQL.Parameters.AddWithValue("id", id);
            insertSQL.ExecuteNonQuery();
        }

        private void Reload()
        {
            GetNotes();
            RenderNoteContainers();
            CloseForm();
        }

        //------------------------------------- RERENDERING NOTES -------------------------------------

        private void RenderNoteContainers()
        {
            int x = 0;
            int y = 0;
            int i = 0;
            int noteContainerY = 135;

            foreach (KeyValuePair<int, NoteContainer> entry in m_noteContainerList)
            {
                Controls.Remove(entry.Value);
            }

            m_noteContainerList.Clear();

            foreach (KeyValuePair<int, Note> entry in m_noteList)
            {

                int usedSpace = i * 223 + 23;
                int freeSpace = Size.Width - usedSpace;
                if (freeSpace > 223)
                {
                    x = i * 223;
                    i++;
                }
                else
                {
                    x = 0;
                    y += 220;
                    i = 1;
                }

                NoteContainer noteContainer = new NoteContainer(entry.Value, new Point(23 + x, noteContainerY + y), deleteNoteDelegate, editNoteDelegate, colorDelegate, reloadDelegate);
                Controls.Add(noteContainer);
                m_noteContainerList.Add(entry.Value.m_id, noteContainer);
            }
        }

        // ----------------------------- NOTE OPENER STUFF...:hamburger: ---------------------------------------

        public MetroFramework.Controls.MetroTextBox CreateNoteFormOpener()
        {
            noteFormOpener = new MetroFramework.Controls.MetroTextBox();
            noteFormOpener.Location = new Point(23, 63);
            noteFormOpener.Name = "noteFormOpener";
            noteFormOpener.Size = new Size(640, 23);
            noteFormOpener.Click += new EventHandler(noteFormOpener_Click);

            noteFormOpener.Text = "Add a note...";
            return noteFormOpener;
        }

        private void noteFormOpener_Click(object sender, EventArgs e)
        {
            openForm();
        }

        private void openForm()
        {
            m_noteCreator.Visible = true;
            noteFormOpener.Visible = false;
            m_noteCreator.noteTitle.Focus();
        }

        private void CloseForm()
        {
            m_noteCreator.Visible = false;
            noteFormOpener.Visible = true;
            m_noteCreator.noteTitle.Text = "";
        }
    }
}
