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
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private int id;
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        private int score;
        public int Score
        {
            get { return score; }
            set { score = value; }
        }

        public Team()
        {

        }

        public Team(string name, int id)
        {
            this.name = name;
            this.id = id;
        }

        public Team(string name, int id, int score) : this(name, id)
        {
            this.score = score;
        }
        
    }
}
