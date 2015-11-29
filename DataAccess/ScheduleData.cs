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
    public class ScheduleData
    {
        private DatabaseLayer dl;

        private string userID;
        public string UserID
        {
            set {
                userID = value;
                setUserType(userID);
            }
        }

        private string userType;
        public string UserType
        {
            get { return userType; }
        }

        #region constructor
        public ScheduleData()
        {
            dl = new DatabaseLayer();
            
        }
        #endregion

        #region Methods
        //private method to set the usertype variable from user id
        private void setUserType(string userID)
        {
            dl.SqlConnection.Open();
            string getTypeString = "SELECT pers_type FROM Person WHERE pers_userid='" + userID + "'";
            SqlCommand sqlc = new SqlCommand(getTypeString, dl.SqlConnection);
            SqlDataReader reader = sqlc.ExecuteReader();
            SqlString type = "";
            while (reader.HasRows)
            {
                type = reader.GetSqlString(0);
            }
            dl.SqlConnection.Close();
            userType = type.ToString();
        }

        public DateTime GetMatchTime(int matchID)
        {
            dl.SqlConnection.Open();
            string getMatchTimeString = "SELECT sche_datetime FROM Schedule WHERE sche_id='" + matchID + "'";
            SqlCommand sqlc = new SqlCommand(getMatchTimeString, dl.SqlConnection);
            SqlDataReader reader = sqlc.ExecuteReader();
            SqlDateTime time = SqlDateTime.MinValue;
            if (reader.HasRows)
            {
                if (reader.Read())
                {
                    time = reader.GetDateTime(0);
                }
            }
            dl.SqlConnection.Close();
            return (DateTime)time;
        }

        public int GetTeam1ID(int matchID)
        {
            dl.SqlConnection.Open();
            string getTeam1String = "SELECT sche_team1 FROM Schedule WHERE sche_id='" + matchID + "'";
            SqlCommand sqlc = new SqlCommand(getTeam1String, dl.SqlConnection);
            SqlDataReader reader = sqlc.ExecuteReader();
            int team = (int)sqlc.ExecuteScalar();
            return team;
        }

        public int GetTeam2ID(int matchID)
        {
            dl.SqlConnection.Open();
            string getTeam2String = "SELECT sche_team2 FROM Schedule WHERE sche_id='" + matchID + "'";
            SqlCommand sqlc = new SqlCommand(getTeam2String, dl.SqlConnection);
            SqlDataReader reader = sqlc.ExecuteReader();
            int team = (int)sqlc.ExecuteScalar();
            return team;
        }

        public int GetTeam1Score(int matchID)
        {
            dl.SqlConnection.Open();
            string getTeam1String = "SELECT sche_team1score FROM Schedule WHERE sche_id='" + matchID + "'";
            SqlCommand sqlc = new SqlCommand(getTeam1String, dl.SqlConnection);
            SqlDataReader reader = sqlc.ExecuteReader();
            int score = (int)sqlc.ExecuteScalar();
            return score;
        }

        public int GetTeam2Score(int matchID)
        {
            dl.SqlConnection.Open();
            string getTeam2String = "SELECT sche_team2score FROM Schedule WHERE sche_id='" + matchID + "'";
            SqlCommand sqlc = new SqlCommand(getTeam2String, dl.SqlConnection);
            SqlDataReader reader = sqlc.ExecuteReader();
            int score = (int)sqlc.ExecuteScalar();
            return score;
        }

        public void GetTeams(out List<int> teamIDs, out List<string> teamNames)
        {
            dl.SqlConnection.Open();
            string getTeamIDString = "SELECT team_id, team_name FROM Team";
            SqlCommand sqlc = new SqlCommand(getTeamIDString, dl.SqlConnection);
            SqlDataReader reader = sqlc.ExecuteReader();
            teamIDs = new List<int>();
            teamNames = new List<string>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    teamIDs.Add(reader.GetInt32(0));
                    teamNames.Add(reader.GetString(1));
                }
            }
            dl.SqlConnection.Close();
        }

        #endregion


        public bool UpdateSchedule()
        {
            dl.SqlConnection.Open();


            dl.SqlConnection.Close();
            return true;
        }

    }
}
