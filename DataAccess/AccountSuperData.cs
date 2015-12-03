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
    public class AccountSuperData
    {
        private DatabaseLayer dl;

        public AccountSuperData()
        {
            dl = new DatabaseLayer();
        }

        public bool getTypeList(out List<int> refereeIDs, out List<string> refereeNames, string type)
        {
            dl.SqlConnection.Open();
            string getRefListString = "SELECT pers_id, pers_firstname, pers_lastname FROM Person WHERE pers_type='" + type + "'";
            SqlCommand sqlc = new SqlCommand(getRefListString, dl.SqlConnection);
            SqlDataReader reader = sqlc.ExecuteReader();
            refereeIDs = new List<int>();
            refereeNames = new List<string>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    refereeIDs.Add(reader.GetInt32(0));
                    refereeNames.Add(reader.GetString(1) + " " + reader.GetString(2));
                }
            }
            dl.SqlConnection.Close();
            return true;
        }

        public bool GetMatches(out List<int> matchIDs, out List<DateTime> dateTimes, out List<int> team1s, out List<int> team2s, out List<int> team1Scores, out List<int> team2Scores)
        {
            dl.SqlConnection.Open();
            string getScheduleString = "SELECT * FROM Schedule WHERE sche_deleted IS NULL";
            SqlCommand sqlc = new SqlCommand(getScheduleString, dl.SqlConnection);
            SqlDataReader reader = sqlc.ExecuteReader();
            matchIDs = new List<int>();
            dateTimes = new List<DateTime>();
            team1s = new List<int>();
            team2s = new List<int>();
            team1Scores = new List<int>();
            team2Scores = new List<int>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    matchIDs.Add(reader.GetInt32(0));
                    dateTimes.Add(reader.GetDateTime(1));
                    team1s.Add(reader.GetInt32(2));
                    team2s.Add(reader.GetInt32(3));
                    team1Scores.Add(reader.GetInt32(4));
                    team2Scores.Add(reader.GetInt32(5));
                }
            }
            dl.SqlConnection.Close();
            return true;
        }
    }
}
