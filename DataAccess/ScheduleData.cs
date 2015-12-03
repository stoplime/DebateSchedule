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
    public class ScheduleData
    {
        private DatabaseLayer dl;

        private string userID;
        public string UserID
        {
            set {
                userID = value;
                getUserType(userID);
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
        private void getUserType(string userID)
        {
            dl.SqlConnection.Open();
            string getTypeString = "SELECT pers_type FROM Person WHERE pers_usersid='" + userID + "'";
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

        public int GetScheduleRows(out List<int> matchIDs)
        {
            dl.SqlConnection.Open();
            string getRowsString = "SELECT sche_id FROM Schedule WHERE sche_deleted IS NULL";
            SqlCommand sqlc = new SqlCommand(getRowsString, dl.SqlConnection);
            SqlDataReader reader = sqlc.ExecuteReader();
            matchIDs = new List<int>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    matchIDs.Add(reader.GetInt32(0));
                }
            }
            dl.SqlConnection.Close();
            return matchIDs.Count;
        }

        public void GetScheduleData(int matchID, out DateTime matchTime, out int team1ID, out int team2ID)
        {
            dl.SqlConnection.Open();
            string getMatchTimeString = "SELECT sche_datetime, sche_team1, sche_team2 FROM Schedule WHERE sche_id='" + matchID + "'";
            SqlCommand sqlc = new SqlCommand(getMatchTimeString, dl.SqlConnection);
            SqlDataReader reader = sqlc.ExecuteReader();
            SqlDateTime time = SqlDateTime.MinValue;
            matchTime = DateTime.MinValue;
            team1ID = 0;
            team2ID = 0;
            if (reader.HasRows)
            {
                if (reader.Read())
                {
                    time = reader.GetDateTime(0);
                    matchTime = (DateTime)time;
                    team1ID = reader.GetInt32(1);
                    team2ID = reader.GetInt32(2);
                }
            }
            dl.SqlConnection.Close();
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

        /*public DateTime GetMatchTime(int matchID)
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
        }*/
        /*public int GetTeam2Score(int matchID)
        {
            dl.SqlConnection.Open();
            string getTeam2String = "SELECT sche_team2score FROM Schedule WHERE sche_id='" + matchID + "'";
            SqlCommand sqlc = new SqlCommand(getTeam2String, dl.SqlConnection);
            SqlDataReader reader = sqlc.ExecuteReader();
            int score = (int)sqlc.ExecuteScalar();
            return score;
        }*/

        public void GetTeamScore(int matchID, out int team1Score, out int team2Score)
        {
            dl.SqlConnection.Open();
            string getTeam1String = "SELECT sche_team1score, sche_team2score FROM Schedule WHERE sche_id='" + matchID + "'";
            SqlCommand sqlc = new SqlCommand(getTeam1String, dl.SqlConnection);
            SqlDataReader reader = sqlc.ExecuteReader();
            team1Score = 0;
            team2Score = 0;
            if (reader.HasRows)
            {
                if (reader.Read())
                {
                    team1Score = reader.GetInt32(0);
                    team2Score = reader.GetInt32(1);
                }
            }
        }

        public void GetTeams(out List<int> teamIDs, out List<string> teamNames)
        {
            dl.SqlConnection.Open();
            string getTeamIDString = "SELECT team_id, team_name FROM Team WHERE team_deleted IS NULL";
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

        public bool UpdateMatch(int matchID, DateTime matchTime, int team1, int team2)
        {
            dl.SqlConnection.Open();
            /*
            string getTeam1IDString = "SELECT team_id FROM Team WHERE team_name='" + team1 + "'";
            string getTeam2IDString = "SELECT team_id FROM Team WHERE team_name='" + team2 + "'";
            SqlCommand sqlcTeam1 = new SqlCommand(getTeam1IDString, dl.SqlConnection);
            SqlCommand sqlcTeam2 = new SqlCommand(getTeam2IDString, dl.SqlConnection);
            int team1ID = 0;
            if (sqlcTeam1.ExecuteScalar() != null)
            {
                team1ID = (int)sqlcTeam1.ExecuteScalar();
            }
            int team2ID = 0;
            if (sqlcTeam2.ExecuteScalar() != null)
            {
                team2ID = (int)sqlcTeam2.ExecuteScalar();
            }
            */
            string updateMatchTable = "UPDATE Schedule SET sche_datetime=@sche_datetime, sche_team1=@sche_team1, sche_team2=@sche_team2 WHERE sche_id=@sche_id";
            SqlCommand sqlcUpdate = new SqlCommand(updateMatchTable, dl.SqlConnection);
            sqlcUpdate.Parameters.AddWithValue("sche_datetime", matchTime);
            sqlcUpdate.Parameters.AddWithValue("sche_team1", team1);
            sqlcUpdate.Parameters.AddWithValue("sche_team2", team2);
            sqlcUpdate.Parameters.AddWithValue("sche_id", matchID);
            sqlcUpdate.ExecuteNonQuery();
            dl.SqlConnection.Close();
            return true;
        }

        public bool DeleteMatch(int matchID)
        {
            dl.SqlConnection.Open();
            string deleteMatchTable = "UPDATE Schedule SET sche_deleted=@sche_deleted WHERE sche_id=@sche_id";
            SqlCommand sqlcUpdate = new SqlCommand(deleteMatchTable, dl.SqlConnection);
            sqlcUpdate.Parameters.AddWithValue("sche_deleted", 'X');
            sqlcUpdate.Parameters.AddWithValue("sche_id", matchID);
            sqlcUpdate.ExecuteNonQuery();
            dl.SqlConnection.Close();
            return true;
        }
        
        public bool autoUpdateSchedule(List<DateTime> times, List<int> team1ID, List<int> team2ID)
        {
            dl.SqlConnection.Open();
            string scheduleTable = "INSERT INTO Schedule(sche_datetime, sche_team1, sche_team2) " + "VALUES(@sche_datetime, @sche_team1, @sche_team2)";
            SqlCommand command = new SqlCommand(scheduleTable, dl.SqlConnection);
            for (int i = 0; i < times.Count; i++)
            {
                command.Parameters.AddWithValue("sche_datetime", (SqlDateTime)times[i]);
                command.Parameters.AddWithValue("sche_team1", team1ID[i]);
                command.Parameters.AddWithValue("sche_team2", team2ID[i]);

                Debug.WriteLine("Created schedule " + i + ": " + command.ExecuteNonQuery());
                command.Parameters.Clear();
            }

            dl.SqlConnection.Close();
            return true;
        }

        public bool addNewRow(DateTime dateTime, int team1, int team2)
        {
            dl.SqlConnection.Open();
            string addRowString = "INSERT INTO Schedule(sche_datetime, sche_team1, sche_team2) " + "VALUES(@sche_datetime, @sche_team1, @sche_team2)";
            SqlCommand command = new SqlCommand(addRowString, dl.SqlConnection);
            command.Parameters.AddWithValue("sche_datetime", (SqlDateTime)dateTime);
            command.Parameters.AddWithValue("sche_team1", team1);
            command.Parameters.AddWithValue("sche_team2", team2);
            command.ExecuteNonQuery();
            dl.SqlConnection.Close();
            return true;
        }

        public bool getTeamsFromDate(DateTime date, out List<int> participatingTeams)
        {
            dl.SqlConnection.Open();
            string getTeamsString = "SELECT sche_team1, sche_team2 FROM Schedule WHERE sche_datetime='" + date+"'";
            SqlCommand sqlc = new SqlCommand(getTeamsString, dl.SqlConnection);
            SqlDataReader reader = sqlc.ExecuteReader();
            participatingTeams = new List<int>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    participatingTeams.Add(reader.GetInt32(0));
                    participatingTeams.Add(reader.GetInt32(1));
                }
            }
            dl.SqlConnection.Close();
            return true;
        }
        #endregion
    }
}
