using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminPage.Models
{
    public class Category
    {
        int id;
        public int Id {
            get { return id; }
            set { id = value; }
        }

        string name;
        public string Name {
            get { return name; }
            set { name = value; }
        }

        int coiceCounter;
        public int CoiceCounter {
            get { return coiceCounter; }
            set { coiceCounter = value; }
        }

        public Category(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}