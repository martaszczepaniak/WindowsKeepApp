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
        public string m_text;
        public string m_title;
        public string m_color;

        public Note(int id, string text, string title, string color)
        {
            m_id = id;
            m_text = text;
            m_title = title;
            m_color = color;
        }


    }
}
