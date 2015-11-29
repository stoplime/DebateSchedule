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

        private int team1Score;
        public int Team1Score
        {
            get { return team1Score; }
            set { team1Score = value; }
        }

        private int team2Score;
        public int Team2Score
        {
            get { return team2Score; }
            set { team2Score = value; }
        }

        public Match()
        {

        }

        public Match(Team team1, Team team2)
        {
            this.team1 = team1;
            this.team2 = team2;
        }

        public Match(Team team1, Team team2, int matchID, DateTime time) : this(team1, team2)
        {
            this.matchID = matchID;
            this.time = time;
        }

        public Match(Team team1, Team team2, int matchID, DateTime time, int team1Score, int team2Score) : this(team1, team2, matchID, time)
        {
            this.team1Score = team1Score;
            this.team2Score = team2Score;
        }

        public override string ToString()
        {
            return string.Format("{0} vs {1}", Team1.Name, Team2.Name);
        }
    }
}
