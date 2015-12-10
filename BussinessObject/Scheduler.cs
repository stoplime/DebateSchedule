using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics; 

using DataAccess;

namespace BussinessObject
{
    public class Scheduler
    {
        //public List<Round> RoundTest;
        private ScheduleData accessToData;

        private int numberOfRows;
        public int NumberOfRows
        {
            get { return numberOfRows; }
        }

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

        public Scheduler()
        {
            accessToData = new ScheduleData();

            matchIDs = new List<int>();
            matchTeam1Names = new List<string>();
            matchTeam2Names = new List<string>();
            List<int> team1IdTemp;
            List<int> team2IdTemp;
            accessToData.GetScheduleData(out matchIDs, out matchTimes, out team1IdTemp, out team2IdTemp);
            for (int i = 0; i < matchIDs.Count; i++)
            {
                matchTeam1Names.Add(accessToData.GetTeamName(team1IdTemp[i]));
                matchTeam2Names.Add(accessToData.GetTeamName(team2IdTemp[i]));
            }
            
        }

        public bool autoReschedule(DateTime start)
        {
            //delete previous schedule
            List<int> previousScheduleIDs;
            accessToData.GetScheduleRows(out previousScheduleIDs);
            for (int i = 0; i < previousScheduleIDs.Count; i++)
            {
                accessToData.DeleteMatch(previousScheduleIDs[i]);
            }
            //create schedule
            List<int> teamIDs;
            List<string> teamNames;
            List<Team> teams = new List<Team>();
            accessToData.GetTeams(out teamIDs, out teamNames);
            for (int i = 0; i < teamIDs.Count; i++)
            {
                teams.Add(new Team(teamNames[i], teamIDs[i]));
            }
            List<Round> rounds = ReSchedule(teams, start);
            List<DateTime> times = new List<DateTime>();
            List<int> team1ID = new List<int>();
            List<int> team2ID = new List<int>();

            numberOfRows = 0;
            for (int i = 0; i < rounds.Count; i++)
            {
                for (int j = 0; j < rounds[i].Matches.Count; j++)
                {
                    if (rounds[i].Matches[j].Team1.ID > 0 && rounds[i].Matches[j].Team2.ID > 0)
                    {
                        times.Add(rounds[i].Matches[j].Time);
                        team1ID.Add(rounds[i].Matches[j].Team1.ID);
                        team2ID.Add(rounds[i].Matches[j].Team2.ID);
                        numberOfRows++;
                    }
                }
            }
            return accessToData.autoUpdateSchedule(times, team1ID, team2ID);
        }

        private static List<DateTime> generateDateTimes(DateTime start, int rounds)
        {
            while (start.DayOfWeek != DayOfWeek.Saturday)
            {
                start = start.AddDays(1);
                //Debug.WriteLine("The day of week: "+start.DayOfWeek);
                //Debug.WriteLine("The date: " + start.Date);
            }
            List<DateTime> times = new List<DateTime>();
            for (int i = 1; i <= rounds; i++)
            {
                if (i%2 != 0)
                {
                    DateTime temp = start.Date;
                    times.Add(temp.AddHours(9));
                    //Debug.WriteLine("added 9 oclock debate");
                }
                else
                {
                    DateTime temp = start.Date;
                    times.Add(temp.AddHours(14));
                    //Debug.WriteLine("added 2 oclock debate");
                    start = start.AddDays(7);
                    //Debug.WriteLine("added 7 days");
                }
            }
            return times;
        }

        private static List<Round> ReSchedule(List<Team> listTeam, DateTime start)
        {
            if (listTeam.Count % 2 != 0)
            {
                listTeam.Add(new Team("~PlaceHolder~", 0));
            }
            List<Round> result = new List<Round>();
            int numberOfRounds = (listTeam.Count - 1);
            int numberOfMatchesInARound = listTeam.Count / 2;
            //Debug.WriteLine("started getting times");
            List<DateTime> times = generateDateTimes(start, numberOfRounds);
            //Debug.WriteLine("finnished getting times");
            List<Team> teams = new List<Team>();

            teams.AddRange(listTeam.Skip(numberOfMatchesInARound).Take(numberOfMatchesInARound));
            teams.AddRange(listTeam.Skip(1).Take(numberOfMatchesInARound - 1).ToArray().Reverse());

            int numberOfTeams = teams.Count;

            int counter = 1;

            for (int roundNumber = 0; roundNumber < numberOfRounds; roundNumber++)
            {
                Round round = new Round();
                int teamIdx = roundNumber % numberOfRounds;
                round.Matches.Add(new Match(teams[teamIdx], listTeam[0], counter, times[roundNumber]));
                counter++;
                for (int idx = 1; idx < numberOfMatchesInARound; idx++)
                {
                    int firstTeamIndex = (roundNumber + idx) % numberOfTeams;
                    int secondTeamIndex = (roundNumber + numberOfTeams - idx) % numberOfTeams;
                    //Debug.WriteLine("pre-added matchID and time");
                    round.Matches.Add(new Match(teams[firstTeamIndex], teams[secondTeamIndex], counter, times[roundNumber]));
                    //Debug.WriteLine("post-added matchID and time");
                    counter++;
                }
                result.Add(round);
            }
            return result;
        }

    }
}
