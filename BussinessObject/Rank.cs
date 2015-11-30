using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataAccess;

namespace BussinessObject
{
    public class Rank
    {
        private RankData rankData;

        private List<string> teamNames;
        public List<string> TeamNames
        {
            get { return teamNames; }
        }

        private List<int> teamScores;
        public List<int> TeamScores
        {
            get { return teamScores; }
        }

        private List<int> teamWins;
        public List<int> TeamWins
        {
            get { return teamWins; }
        }

        private List<int> teamTotalMatches;
        public List<int> TeamTotalMatches
        {
            get { return teamTotalMatches; }
        }

        public Rank()
        {
            rankData = new RankData();
            rankData.GetRanks(out teamNames, out teamScores, out teamWins, out teamTotalMatches);
        }
    }
}
