using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataAccess;

namespace BussinessObject
{
    public class Scheduler
    {
        public List<Round> RoundTest;

        private ScheduleData accessToData;

        public Scheduler()
        {
            accessToData = new ScheduleData();

            List<Team> test = new List<Team>();
            
            for (int i = 1; i <= 10; i++)
            {
                test.Add(new Team("A"+i, i));
            }

            RoundTest = ReSchedule(test);
        }

        public void setUserID(string userID)
        {
            accessToData.UserID = userID;
        }

        public bool isUserSuper()
        {
            if (accessToData.UserType == "SuperReferee")
            {
                return true;
            }
            return false;
        }

        public static List<Round> ReSchedule(List<Team> listTeam)
        {
            if (listTeam.Count % 2 != 0)
            {
                listTeam.Add(new Team("~bye~",0));
            }
            List<Round> result = new List<Round>();
            int numberOfRounds = (listTeam.Count - 1);
            int numberOfMatchesInARound = listTeam.Count / 2;

            List<Team> teams = new List<Team>();

            teams.AddRange(listTeam.Skip(numberOfMatchesInARound).Take(numberOfMatchesInARound));
            teams.AddRange(listTeam.Skip(1).Take(numberOfMatchesInARound - 1).ToArray().Reverse());

            int numberOfTeams = teams.Count;

            for (int roundNumber = 0; roundNumber < numberOfRounds; roundNumber++)
            {
                Round round = new Round();
                int teamIdx = roundNumber % numberOfRounds;
                round.Matches.Add(new Match(teams[teamIdx], listTeam[0]));

                for (int idx = 1; idx < numberOfMatchesInARound; idx++)
                {
                    int firstTeamIndex = (roundNumber + idx) % numberOfTeams;
                    int secondTeamIndex = (roundNumber + numberOfTeams - idx) % numberOfTeams;

                    round.Matches.Add(new Match(teams[firstTeamIndex], teams[secondTeamIndex]));
                }
                result.Add(round);
            }
            return result;
        }

    }
}
