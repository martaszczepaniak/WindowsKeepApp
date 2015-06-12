using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace WindowsKeepApplication
{
    public class Note
    {
        public int m_id;
        public string m_category;
        public string m_title;
        public string m_color;

        public Note(int id, string category, string title, string color)
        {
            m_id = id;
            m_category = category;
            m_title = title;
            m_color = color;
        }

        public string Items(SQLiteConnection db)
        {
            string sql = "select * from items where items.note_id = 1";
            SQLiteCommand command = new SQLiteCommand(sql, db);
            command.ExecuteNonQuery();
            SQLiteDataReader reader = command.ExecuteReader();
            string items = "";
            while (reader.Read())
                items += reader["id"].ToString();
            return items;
        }
    }
}
