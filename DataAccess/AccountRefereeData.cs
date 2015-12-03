using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace DataAccess
{
    public class AccountRefereeData
    {
        private DatabaseLayer dl;

        public AccountRefereeData()
        {
            dl = new DatabaseLayer();
        }

        public bool GetMatches(out List<int> matchIDs, out List<DateTime> dateTimes, out List<int> team1s, out List<int> team2s, out List<int> refIDs)
        {
            dl.SqlConnection.Open();
            string getScheduleString = "SELECT * FROM Schedule WHERE sche_deleted IS NULL";
            SqlCommand sqlc = new SqlCommand(getScheduleString, dl.SqlConnection);
            SqlDataReader reader = sqlc.ExecuteReader();
            matchIDs = new List<int>();
            dateTimes = new List<DateTime>();
            team1s = new List<int>();
            team2s = new List<int>();
            refIDs = new List<int>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    matchIDs.Add(reader.GetInt32(0));
                    dateTimes.Add(reader.GetDateTime(1));
                    team1s.Add(reader.GetInt32(2));
                    team2s.Add(reader.GetInt32(3));
                    if (reader.GetValue(6) != DBNull.Value)
                    {
                        refIDs.Add(reader.GetInt32(6));
                    }
                    else
                    {
                        refIDs.Add(0);
                    }
                }
            }
            dl.SqlConnection.Close();
            return true;
        }

        public bool GetMyMatches(int refereeID, out List<int> matchIDs, out List<DateTime> dateTimes, out List<int> team1s, out List<int> team2s)
        {
            dl.SqlConnection.Open();
            string getScheduleString = "SELECT * FROM Schedule WHERE sche_deleted IS NULL AND sche_refereeid='"+ refereeID + "'";
            SqlCommand sqlc = new SqlCommand(getScheduleString, dl.SqlConnection);
            SqlDataReader reader = sqlc.ExecuteReader();
            matchIDs = new List<int>();
            dateTimes = new List<DateTime>();
            team1s = new List<int>();
            team2s = new List<int>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    matchIDs.Add(reader.GetInt32(0));
                    dateTimes.Add(reader.GetDateTime(1));
                    team1s.Add(reader.GetInt32(2));
                    team2s.Add(reader.GetInt32(3));
                }
            }
            dl.SqlConnection.Close();
            return true;
        }

        public bool AddHost(int refereeID, int matchID)
        {
            dl.SqlConnection.Open();
            string addHostString = "UPDATE Schedule SET sche_refereeid='" + refereeID + "' WHERE sche_id='" + matchID + "'";
            SqlCommand sqlc = new SqlCommand(addHostString, dl.SqlConnection);
            sqlc.ExecuteNonQuery();
            dl.SqlConnection.Close();
            return true;
        }

        public string getTeamName(int teamID)
        {
            dl.SqlConnection.Open();
            string getTeamNameString = "SELECT team_name FROM Team WHERE team_id='" + teamID + "'";
            SqlCommand sqlc = new SqlCommand(getTeamNameString, dl.SqlConnection);
            string name = (string)sqlc.ExecuteScalar();
            dl.SqlConnection.Close();
            return name;
        }

        private int getNumMatches(string teamName)
        {
            string getString = "SELECT team_matches FROM Team WHERE team_name='" + teamName + "'";
            SqlCommand sqlc = new SqlCommand(getString, dl.SqlConnection);
            SqlDataReader reader = sqlc.ExecuteReader();
            int matches = 0;
            if (reader.HasRows)
            {
                if (reader.Read())
                {
                    if (!reader.IsDBNull(0))
                    {
                        matches = reader.GetInt32(0);
                    }
                }
            }

            reader.Close();
            return matches;
        }

        private int getNumWins(string teamName)
        {
            string getString = "SELECT team_wins FROM Team WHERE team_name='" + teamName + "'";
            SqlCommand sqlc = new SqlCommand(getString, dl.SqlConnection);
            SqlDataReader reader = sqlc.ExecuteReader();
            int wins = 0;
            if (reader.HasRows)
            {
                if (reader.Read())
                {
                    if (!reader.IsDBNull(0))
                    {
                        wins = reader.GetInt32(0);
                    }
                }
            }

            reader.Close();
            return wins;
        }

        public bool setScores(int matchID, int team1Score, int team2Score, string team1Name, string team2Name)
        {
            dl.SqlConnection.Open();
            string setScoresString = "UPDATE Schedule SET sche_team1score='" + team1Score + "', sche_team2score='" + team2Score + "' WHERE sche_id='" + matchID + "'";
            SqlCommand sqlc = new SqlCommand(setScoresString, dl.SqlConnection);
            sqlc.ExecuteNonQuery();
            string setScoresTeam1String = "UPDATE Team SET team_score='" + team1Score + "', team_wins=@teamWins1, team_matches=@teamMatches1 WHERE team_name='" + team1Name + "'";
            SqlCommand sqlcTeam1 = new SqlCommand(setScoresTeam1String, dl.SqlConnection);
            string setScoresTeam2String = "UPDATE Team SET team_score='" + team2Score + "', team_wins=@teamWins2, team_matches=@teamMatches2 WHERE team_name='" + team2Name + "'";
            SqlCommand sqlcTeam2 = new SqlCommand(setScoresTeam2String, dl.SqlConnection);
            int matches1 = getNumMatches(team1Name);
            int matches2 = getNumMatches(team2Name);
            int wins1 = getNumWins(team1Name);
            int wins2 = getNumWins(team2Name);
            if (team1Score == team2Score)
            {
                sqlcTeam1.Parameters.AddWithValue("teamWins1", (++wins1));
                sqlcTeam2.Parameters.AddWithValue("teamWins2", (++wins2));
            }
            else if (team1Score > team2Score)
            {
                sqlcTeam1.Parameters.AddWithValue("teamWins1", (++wins1));
                sqlcTeam2.Parameters.AddWithValue("teamWins2", (wins2));
            }
            else
            {
                sqlcTeam1.Parameters.AddWithValue("teamWins1", (wins1));
                sqlcTeam2.Parameters.AddWithValue("teamWins2", (++wins2));
            }
            sqlcTeam1.Parameters.AddWithValue("teamMatches1", ++matches1);
            sqlcTeam2.Parameters.AddWithValue("teamMatches2", ++matches2);
            sqlcTeam1.ExecuteNonQuery();
            sqlcTeam2.ExecuteNonQuery();
            dl.SqlConnection.Close();
            return true;
        }
    }
}
