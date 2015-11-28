using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject
{
    public class Match
    {
        private int matchID;
        public int MatchID
        {
            get { return matchID; }
            set { matchID = value; }
        }

        private DateTime time;
        public DateTime Time
        {
            get { return time; }
            set { time = value; }
        }

        private Team team1;
        public Team Team1
        {
            get { return team1; }
            set { team1 = value; }
        }

        private Team team2;
        public Team Team2
        {
            get { return team2; }
            set { team2 = value; }
        }


        public Match(Team team1, Team team2)
        {
            this.team1 = team1;
            this.team2 = team2;
        }

        public override string ToString()
        {
            return string.Format("{0} vs {1}", Team1.Name, Team2.Name);
        }
    }
}
