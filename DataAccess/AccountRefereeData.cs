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

        public bool GetMatches(out List<int> matchIDs, out List<DateTime> dateTimes, out List<int> team1s, out List<int> team2s)
        {
            dl.SqlConnection.Open();
            string getScheduleString = "SELECT * FROM Schedule WHERE sche_deleted IS NULL AND sche_refereeid IS NULL";
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

        public string getTeamName(int teamID)
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
