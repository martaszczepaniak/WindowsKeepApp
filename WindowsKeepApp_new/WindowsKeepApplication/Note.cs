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
        public string m_id;
        public string m_category;
        public string m_title;
        public string m_color;

        public Note(string id, string category, string title, string color)
        {
            m_id = id;
            m_category = category;
            m_title = title;
            m_color = color;
        }
    }
}
