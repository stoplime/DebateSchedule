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
    public class AccountMember
    {
        private string username;
        public string Username
        {
            get { return username; }
        }
        private string teamname;
        public string Teamname
        {
            get { return teamname; }
        }
        private string school;
        public string School
        {
            get { return school; }
        }
        private string email;
        public string Email
        {
            get { return email; }
        }
        private string score;
        public string Score
        {
            get { return score; }
        }
        private int teamSize;
        public int TeamSize
        {
            get { return teamSize; }
        }

        private DatabaseLayer dl;
        private string userID;

        public AccountMember()
        {
            userID = "Error: cannot find userID";
            username = "Error: cannot find user name, user id was not selected. Please login and try again.";
            teamname = "Error: cannot find team name, user id was not selected. Please login and try again.";
            school = "Error: cannot find school name, user id was not selected. Please login and try again.";
            email = "Error: cannot find email, user id was not selected. Please login and try again.";
            score = "Error: cannot find score, user id was not selected. Please login and try again.";
            teamSize = -1;
        }

        public AccountMember (string userid) : this()
        {
            this.userID = userid;
            
            dl = new DatabaseLayer();
            username = getUserName();
            teamname = getTeam();
            school = getSchool();
            score = getScore();
            teamSize = getTeamSize();
        }

        private string getUserName()
        {
            dl.SqlConnection.Open();
            string getUserNameString = "SELECT users_login FROM Users WHERE user_id='" + userID + "'";
            SqlCommand sqlc = new SqlCommand(getUserNameString, dl.SqlConnection);
            SqlDataReader reader = sqlc.ExecuteReader();
            SqlString user = "";
            while (reader.HasRows)
            {
                user = reader.GetSqlString(0);
            }
            dl.SqlConnection.Close();
            return user.ToString();
        }

        private string getTeam()
        {
            dl.SqlConnection.Open();
            string getTeamString = "SELECT pers_teamid FROM Person WHERE user_id='" + userID + "'";
            SqlCommand sqlc = new SqlCommand(getTeamString, dl.SqlConnection);
            SqlDataReader reader = sqlc.ExecuteReader();
            SqlString returnTeam = "";
            while (reader.HasRows)
            {
                 returnTeam = reader.GetSqlString(0);
            }
            string getTeamNameString = "SELECT team_name FROM Teams WHERE team_id='" + returnTeam.ToString() + "'";
            SqlCommand sqlcName = new SqlCommand(getTeamNameString, dl.SqlConnection);
            SqlDataReader readerName = sqlcName.ExecuteReader();
            SqlString teamName = "";
            while (readerName.HasRows)
            {
                teamName = readerName.GetSqlString(0);
            }
            dl.SqlConnection.Close();
            return teamName.ToString();
        }

        private string getSchool()
        {
            dl.SqlConnection.Open();
            string getSchoolIDString = "SELECT pers_schoolid FROM Person WHERE user_id='" + userID + "'";
            SqlCommand sqlc = new SqlCommand(getSchoolIDString, dl.SqlConnection);
            SqlDataReader reader = sqlc.ExecuteReader();
            SqlString schoolID = "";
            while (reader.HasRows)
            {
                schoolID = reader.GetSqlString(0);
            }
            string getSchoolNameString = "SELECT scho_name FROM School WHERE scho_id='" + schoolID.ToString() + "'";
            SqlCommand sqlcName = new SqlCommand(getSchoolIDString, dl.SqlConnection);
            SqlDataReader readerName = sqlcName.ExecuteReader();
            SqlString schoolName = "";
            while (readerName.HasRows)
            {
                schoolName = readerName.GetSqlString(0);
            }
            dl.SqlConnection.Close();
            return schoolName.ToString();
        }

        private string getEmail()
        {
            dl.SqlConnection.Open();
            string getEmailString = "SELECT users_email FROM Users WHERE user_id='" + userID + "'";
            SqlCommand sqlc = new SqlCommand(getEmailString, dl.SqlConnection);
            SqlDataReader reader = sqlc.ExecuteReader();
            SqlString email = "";
            while (reader.HasRows)
            {
                email = reader.GetSqlString(0);
            }
            dl.SqlConnection.Close();
            return email.ToString(); 
        }

        private string getScore()
        {
            dl.SqlConnection.Open();
            string getTeamString = "SELECT pers_teamid FROM Person WHERE user_id='" + userID + "'";
            SqlCommand sqlc = new SqlCommand(getTeamString, dl.SqlConnection);
            SqlDataReader reader = sqlc.ExecuteReader();
            SqlString returnTeam = "";
            while (reader.HasRows)
            {
                returnTeam = reader.GetSqlString(0);
            }
            string getTeamNameString = "SELECT team_score FROM Teams WHERE team_id='" + returnTeam.ToString() + "'";
            SqlCommand sqlcScore = new SqlCommand(getTeamNameString, dl.SqlConnection);
            SqlDataReader readerScore = sqlcScore.ExecuteReader();
            SqlString teamScore = "";
            while (readerScore.HasRows)
            {
                teamScore = readerScore.GetSqlString(0);
            }
            dl.SqlConnection.Close();
            return teamScore.ToString();
        }

        private int getTeamSize()
        {
            dl.SqlConnection.Open();
            string getTeamString = "SELECT pers_teamid FROM Person WHERE user_id='" + userID + "'";
            SqlCommand sqlc = new SqlCommand(getTeamString, dl.SqlConnection);
            SqlDataReader reader = sqlc.ExecuteReader();
            SqlString returnTeam = "";
            while (reader.HasRows)
            {
                returnTeam = reader.GetSqlString(0);
            }
            string getTeamNameString = "SELECT team_score FROM Teams WHERE team_id='" + returnTeam.ToString() + "'";
            SqlCommand sqlcSize = new SqlCommand(getTeamNameString, dl.SqlConnection);
            SqlDataReader readerSize = sqlcSize.ExecuteReader();
            int teamSize = -1;
            while (readerSize.HasRows)
            {
                teamSize = readerSize.GetInt32(0);
            }
            dl.SqlConnection.Close();
            return teamSize;
        }
    }
}
