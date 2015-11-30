using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataAccess;

namespace BussinessObject
{
    public class AccountReferee
    {
        private AccountRefereeData accessData;

        private List<int> matchIDs;
        public List<int> MatchIDs
        {
            get { return matchIDs; }
        }

        private List<DateTime> matchTimes;
        public List<DateTime> MatchTimes
        {
            get { return matchTimes; }
        }

        private List<string> matchTeam1Names;
        public List<string> MatchTeam1Names
        {
            get { return matchTeam1Names; }
        }

        private List<string> matchTeam2Names;
        public List<string> MatchTeam2Names
        {
            get { return matchTeam2Names; }
        }

        public AccountReferee()
        {
            accessData = new AccountRefereeData();
            getMatches();
            
        }

        private void getMatches()
        {
            List<int> team1IDs;
            List<int> team2IDs;
            matchTeam1Names = new List<string>();
            matchTeam2Names = new List<string>();
            accessData.GetMatches(out matchIDs, out matchTimes, out team1IDs, out team2IDs);
            for (int i = 0; i < team1IDs.Count; i++)
            {
                matchTeam1Names.Add(accessData.getTeamName(team1IDs[i]));
                matchTeam2Names.Add(accessData.getTeamName(team2IDs[i]));
            }
        }

    }
}
