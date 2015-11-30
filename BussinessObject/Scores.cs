using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataAccess;

namespace BussinessObject
{
    public class Scores
    {
        private ScoresData scoresData;

        private List<int> matchIDs;
        public List<int> MatchIDs
        {
            get { return matchIDs; }
        }

        private List<DateTime> dateTimes;
        public List<DateTime> DateTimes
        {
            get { return dateTimes; }
        }

        private List<string> team1s;
        public List<string> Team1s
        {
            get { return team1s; }
        }

        private List<int> team1Scores;
        public List<int> Team1Scores
        {
            get { return team1Scores; }
        }

        private List<string> team2s;
        public List<string> Team2s
        {
            get { return team2s; }
        }

        private List<int> team2Scores;
        public List<int> Team2Scores
        {
            get { return team2Scores; }
        }

        public Scores()
        {
            scoresData = new ScoresData();
            team1s = new List<string>();
            team2s = new List<string>();
            getScores();
        }

        private void getScores()
        {
            List<int> team1IDs;
            List<int> team2IDs;
            scoresData.GetSchedule(out matchIDs, out dateTimes, out team1IDs, out team1Scores, out team2IDs, out team2Scores);
            for (int i = 0; i < team1IDs.Count; i++)
            {
                team1s.Add(scoresData.GetTeamName(team1IDs[i]));
                team2s.Add(scoresData.GetTeamName(team2IDs[i]));
            }
        }
    }
}
