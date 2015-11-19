using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject
{
    public class Team
    {
        private string name;
        private int id;

        public Team(string name, int id)
        {
            this.name = name;
            this.id = id;
        }

        public Team()
        { }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public int ID
        {
            get { return id; }
            set { id = value; }
        }
    }
}
