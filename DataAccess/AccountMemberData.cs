using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using System.Web;

namespace DataAccess
{
    public class AccountMemberData
    {
        private DatabaseLayer dl;

        public AccountMemberData()
        {
            dl = new DatabaseLayer();
        }

        public string getUserName(int userID)
        {
            dl.SqlConnection.Open();
            string getUserNameString = "SELECT users_login FROM Users WHERE users_id='" + userID + "'";
            SqlCommand sqlc = new SqlCommand(getUserNameString, dl.SqlConnection);
            SqlDataReader reader = sqlc.ExecuteReader();
            string user = "";
            if (reader.HasRows)
            {
                if (reader.Read())
                {
                    user = reader.GetString(0);
                }
            }
            dl.SqlConnection.Close();
            return user;
        }

        public string getTeam(int userID)
        {
            dl.SqlConnection.Open();
            string getTeamString = "SELECT pers_teamid FROM Person WHERE pers_usersid='" + userID + "'";
            SqlCommand sqlc = new SqlCommand(getTeamString, dl.SqlConnection);
            SqlDataReader reader = sqlc.ExecuteReader();
            int returnTeam = 0;
            if (reader.HasRows)
            {
                if (reader.Read())
                {
                    returnTeam = reader.GetInt32(0);
                }
            }
            string getTeamNameString = "SELECT team_name FROM Team WHERE team_id='" + returnTeam + "'";
            SqlCommand sqlcName = new SqlCommand(getTeamNameString, dl.SqlConnection);
            reader.Close();
            SqlDataReader readerName = sqlcName.ExecuteReader();
            string teamName = "";
            if (readerName.HasRows)
            {
                if (readerName.Read())
                {
                    teamName = readerName.GetString(0);
                }
            }
            dl.SqlConnection.Close();
            return teamName.ToString();
        }

        public string getSchool(int userID)
        {
            dl.SqlConnection.Open();
            string getSchoolIDString = "SELECT pers_schoolid FROM Person WHERE pers_usersid='" + userID + "'";
            SqlCommand sqlc = new SqlCommand(getSchoolIDString, dl.SqlConnection);
            SqlDataReader reader = sqlc.ExecuteReader();
            int schoolID = 0;
            if (reader.HasRows)
            {
                if (reader.Read())
                {
                    schoolID = reader.GetInt32(0);
                }
            }
            reader.Close();
            string getSchoolNameString = "SELECT scho_name FROM School WHERE scho_id='" + schoolID+ "'";
            SqlCommand sqlcName = new SqlCommand(getSchoolNameString, dl.SqlConnection);
            SqlDataReader readerName = sqlcName.ExecuteReader();
            string schoolName = "";
            if (readerName.HasRows)
            {
                if (readerName.Read())
                {
                    schoolName = readerName.GetString(0);
                }
            }
            dl.SqlConnection.Close();
            return schoolName;
        }

        public string getEmail(int userID)
        {
            dl.SqlConnection.Open();
            string getEmailString = "SELECT users_email FROM Users WHERE users_id='" + userID + "'";
            SqlCommand sqlc = new SqlCommand(getEmailString, dl.SqlConnection);
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

        public int getScore(int userID)
        {
            dl.SqlConnection.Open();
            string getTeamString = "SELECT pers_teamid FROM Person WHERE pers_usersid='" + userID + "'";
            SqlCommand sqlc = new SqlCommand(getTeamString, dl.SqlConnection);
            SqlDataReader reader = sqlc.ExecuteReader();
            int returnTeam = 0;
            if (reader.HasRows)
            {
                if (reader.Read())
                {
                    returnTeam = reader.GetInt32(0);
                }
            }
            string getTeamNameString = "SELECT team_score FROM Team WHERE team_id='" + returnTeam + "'";
            SqlCommand sqlcScore = new SqlCommand(getTeamNameString, dl.SqlConnection);
            reader.Close();
            SqlDataReader readerScore = sqlcScore.ExecuteReader();
            int teamScore = 0;
            if (readerScore.HasRows)
            {
                if (readerScore.Read())
                {
                    if (!readerScore.IsDBNull(0))
                    {
                        teamScore = readerScore.GetInt32(0);
                    }
                }
            }
            dl.SqlConnection.Close();
            return teamScore;
        }
    }
}
