using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace WindowsKeepApplication
{
    public class DatabaseConnection
    {
        public DatabaseConnection()
        {
            setupDatabase();
        }
      
        public SQLiteConnection m_dbConnection;

        void createNewDatabase()
        {
            SQLiteConnection.CreateFile("Notes.sqlite");
        }

        public void connectToDatabase()
        {
            m_dbConnection = new SQLiteConnection("Data Source=Notes.sqlite;Version=3;");
            m_dbConnection.Open();
        }

        void createTables()
        {
            string sql = "create table if not exists notes(id integer PRIMARY KEY autoincrement, category text, title text, color text)";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            string sql2 = "create table if not exists items(id string PRIMARY KEY, text text, note_id string, FOREIGN KEY(note_id) REFERENCES notes(id) )";
            SQLiteCommand command2 = new SQLiteCommand(sql2, m_dbConnection);
            command2.ExecuteNonQuery();
        }

        void setupDatabase()
        {
            if (!File.Exists("Notes.sqlite"))
            {
                createNewDatabase();
            }
            connectToDatabase();

            createTables();
        }

    }

}
