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

        public bool getTypeList(out List<int> IDs, out List<string> names, out List<string> emails, string type)
        {
            dl.SqlConnection.Open();
            string getRefListString = "SELECT pers_id, pers_firstname, pers_lastname FROM Person WHERE pers_type='" + type + "'";
            SqlCommand sqlc = new SqlCommand(getRefListString, dl.SqlConnection);
            SqlDataReader reader = sqlc.ExecuteReader();
            IDs = new List<int>();
            names = new List<string>();
            emails = new List<string>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    IDs.Add(reader.GetInt32(0));
                    names.Add(reader.GetString(1) + " " + reader.GetString(2));
                }
            }
            reader.Close();
            for (int i = 0; i < IDs.Count; i++)
            {
                Debug.WriteLine("reader value: "+reader.IsClosed);
                emails.Add(GetEmailFromPersID(IDs[i]));
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

        public bool updateRefereeToSuper(int refereeID)
        {
            dl.SqlConnection.Open();
            string updateRefString = "UPDATE Person SET pers_type='SuperReferee' WHERE pers_id='" + refereeID + "'";
            SqlCommand sqlc = new SqlCommand(updateRefString, dl.SqlConnection);
            sqlc.ExecuteNonQuery();
            dl.SqlConnection.Close();
            return true;
        }

        public string GetEmailFromPersID(int persID)
        {
            bool wasClosed = false;
            if (dl.SqlConnection.State == ConnectionState.Closed)
            {
                wasClosed = true;
                dl.SqlConnection.Open();
            }
            string getUsersIDString = "SELECT pers_usersid FROM Person WHERE pers_id='" + persID + "'";
            SqlCommand sqlc = new SqlCommand(getUsersIDString, dl.SqlConnection);
            SqlDataReader reader = sqlc.ExecuteReader();
            int userID = 0;
            if (reader.HasRows)
            {
                if (reader.Read())
                {
                    userID = reader.GetInt32(0);
                }
            }
            reader.Close();
            string getEmailString = "SELECT users_email FROM Users WHERE users_id='" + userID + "'";
            SqlCommand sqlcEmail = new SqlCommand(getEmailString, dl.SqlConnection);
            SqlDataReader readerEmail = sqlcEmail.ExecuteReader();
            string email = "";
            if (readerEmail.HasRows)
            {
                if (readerEmail.Read())
                {
                    email = readerEmail.GetString(0);
                }
            }
            readerEmail.Close();
            if (wasClosed)
            {
                dl.SqlConnection.Close();
            }
            return email;
        }
        
        public string getMyEmail(int myID)
        {
            dl.SqlConnection.Open();
            string getMyEmailString = "SELECT users_email FROM Users WHERE users_id='" + myID + "'";
            SqlCommand sqlc = new SqlCommand(getMyEmailString, dl.SqlConnection);
            SqlDataReader reader = sqlc.ExecuteReader();
            string email = "";
            if (reader.HasRows)
            {
                if (reader.Read())
                {
                    email = reader.GetString(0);
                }
            }
            dl.SqlConnection.Close();
            return email;
        }
    }
}
