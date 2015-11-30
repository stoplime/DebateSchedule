using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace DataAccess
{
    public class ScoresData
    {
        private DatabaseLayer dl;

        public ScoresData()
        {
            dl = new DatabaseLayer();
        }

        public bool GetSchedule(out List<int> matchID, out List<DateTime> dateTime, out List<int> team1, out List<int> team1Score, out List<int> team2, out List<int> team2Score)
        {
            dl.SqlConnection.Open();
            string getScheduleString = "SELECT * FROM Schedule WHERE sche_deleted IS NULL";
            SqlCommand sqlc = new SqlCommand(getScheduleString, dl.SqlConnection);
            SqlDataReader reader = sqlc.ExecuteReader();
            matchID = new List<int>();
            dateTime = new List<DateTime>();
            team1 = new List<int>();
            team1Score = new List<int>();
            team2 = new List<int>();
            team2Score = new List<int>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    matchID.Add(reader.GetInt32(0));
                    dateTime.Add(reader.GetDateTime(1));
                    team1.Add(reader.GetInt32(2));
                    team2.Add(reader.GetInt32(3));
                    if (reader.GetValue(4) != DBNull.Value)
                    {
                        team1Score.Add(reader.GetInt32(4));
                    }
                    else
                    {
                        team1Score.Add(0);
                    }
                    if (reader.GetValue(5) != DBNull.Value)
                    {
                        team2Score.Add(reader.GetInt32(5));
                    }
                    else
                    {
                        team2Score.Add(0);
                    }
                }
            }
            dl.SqlConnection.Close();
            return true;
        }

        public string GetTeamName(int teamID)
        {
            dl.SqlConnection.Open();
            string getTeamNameString = "SELECT team_name FROM Team WHERE team_id='" + teamID + "'";
            SqlCommand sqlc = new SqlCommand(getTeamNameString, dl.SqlConnection);
            string name = (string)sqlc.ExecuteScalar();
            dl.SqlConnection.Close();
            return name;
        }
    }
}
