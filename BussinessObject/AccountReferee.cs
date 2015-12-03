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

        // The unhosted matches
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

        private List<int> refIDs;
        public List<int> RefIDs
        {
            get { return refIDs; }
        }
        // The matches that are considered the referee's hosting
        private List<int> myMatchIDs;
        public List<int> MyMatchIDs
        {
            get { return myMatchIDs; }
        }

        private List<DateTime> myMatchTimes;
        public List<DateTime> MyMatchTimes
        {
            get { return myMatchTimes; }
        }

        private List<string> myMatchTeam1Names;
        public List<string> MyMatchTeam1Names
        {
            get { return myMatchTeam1Names; }
        }

        private List<string> myMatchTeam2Names;
        public List<string> MyMatchTeam2Names
        {
            get { return myMatchTeam2Names; }
        }

        private List<int> myMatchTeam1Score;
        public List<int> MyMatchTeam1Score
        {
            get { return myMatchTeam1Score; }
        }

        private List<int> myMatchTeam2Score;
        public List<int> MyMatchTeam2Score
        {
            get { return myMatchTeam2Score; }
        }

        #region constructor
        public AccountReferee()
        {
            accessData = new AccountRefereeData();
            getUnhostedMatches();
            myMatchIDs = new List<int>();
            myMatchTimes = new List<DateTime>();
            myMatchTeam1Names = new List<string>();
            myMatchTeam2Names = new List<string>();
        }
        #endregion

        private void getUnhostedMatches()
        {
            List<int> team1IDs;
            List<int> team2IDs;
            matchTeam1Names = new List<string>();
            matchTeam2Names = new List<string>();
            accessData.GetMatches(out matchIDs, out matchTimes, out team1IDs, out team2IDs, out refIDs);
            for (int i = 0; i < team1IDs.Count; i++)
            {
                matchTeam1Names.Add(accessData.getTeamName(team1IDs[i]));
                matchTeam2Names.Add(accessData.getTeamName(team2IDs[i]));
            }
        }

        public void getMyMatches(int refereeID)
        {
            List<int> team1IDs;
            List<int> team2IDs;
            myMatchTeam1Names = new List<string>();
            myMatchTeam2Names = new List<string>();
            accessData.GetMyMatches(refereeID, out myMatchIDs, out myMatchTimes, out team1IDs, out team2IDs, out myMatchTeam1Score, out myMatchTeam2Score);
            for (int i = 0; i < team1IDs.Count; i++)
            {
                myMatchTeam1Names.Add(accessData.getTeamName(team1IDs[i]));
                myMatchTeam2Names.Add(accessData.getTeamName(team2IDs[i]));
            }
        }

        public void setHosting(int refereeID, int matchID)
        {
            accessData.AddHost(refereeID, matchID);
        }

        public void removeHosting(int matchID)
        {
            accessData.AddHost(0, matchID);
        }
    }
}
