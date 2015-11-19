using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject
{
    public class Round
    {
        public List<Match> Matches { get; set; }

        public Round()
        {
            Matches = new List<Match>();
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, Matches) + Environment.NewLine;
        }

    }
}
