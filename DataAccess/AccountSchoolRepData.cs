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
    public class AccountSchoolRepData
    {
        DatabaseLayer dl;

        public AccountSchoolRepData()
        {
            dl = new DatabaseLayer();
        }

        public int getSchoolID(int schoolRepID)
        {
            bool preOpened = true;
            if (dl.SqlConnection.State == ConnectionState.Closed)
            {
                dl.SqlConnection.Open();
                preOpened = false;
            }
            string getSchoolIDString = "SELECT pers_schoolid FROM Person WHERE pers_usersid='" + schoolRepID + "'";
            SqlCommand sqlc = new SqlCommand(getSchoolIDString, dl.SqlConnection);
            SqlDataReader reader = sqlc.ExecuteReader();
            int schoolID = 0;
            if (reader.HasRows)
            {
                if (reader.Read())
                {
                    if (!reader.IsDBNull(0))
                    {
                        schoolID = reader.GetInt32(0);
                    }
                }
            }
            reader.Close();
            if (!preOpened)
            {
                dl.SqlConnection.Close();
            }
            return schoolID;
        }

        public bool getRepresentative(int schoolRepID, out string myName, out string schoolName)
        {
            dl.SqlConnection.Open();
            string getRepresentativeString = "SELECT pers_firstname, pers_lastname, pers_schoolid FROM Person WHERE pers_usersid='" + schoolRepID + "'";
            SqlCommand sqlc = new SqlCommand(getRepresentativeString, dl.SqlConnection);
            SqlDataReader reader = sqlc.ExecuteReader();
            myName = "";
            int schoolID = 0;
            if (reader.HasRows)
            {
                if (reader.Read())
                {
                    myName = reader.GetString(0) + " " + reader.GetString(1);
                    schoolID = reader.GetInt32(2);
                }
            }
            string getSchoolName = "SELECT scho_name FROM School WHERE scho_id='" + schoolID + "'";
            SqlCommand sqlcSchool = new SqlCommand(getSchoolName, dl.SqlConnection);
            reader.Close();
            SqlDataReader readerSchool = sqlcSchool.ExecuteReader();
            schoolName = "";
            if (readerSchool.HasRows)
            {
                if (readerSchool.Read())
                {
                    schoolName = readerSchool.GetString(0);
                }
            }
            dl.SqlConnection.Close();
            return true;
        }

        public List<int> getTeamMembers(int teamID)
        {
            bool preOpened = true;
            if (dl.SqlConnection.State == ConnectionState.Closed)
            {
                dl.SqlConnection.Open();
                preOpened = false;
            }
            string getTeamMembersString = "SELECT pers_id FROM Person WHERE pers_teamid='" + teamID + "'";
            SqlCommand sqlc = new SqlCommand(getTeamMembersString, dl.SqlConnection);
            SqlDataReader reader = sqlc.ExecuteReader();
            List<int> members = new List<int>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    members.Add(reader.GetInt32(0));
                }
            }
            if (!preOpened)
            {
                reader.Close();
                dl.SqlConnection.Close();
            }
            return members;
        }

        public string getTeamMemberName(int memberID)
        {
            dl.SqlConnection.Open();
            string getNameString = "SELECT pers_firstname, pers_lastname FROM Person WHERE pers_id='" + memberID + "'";
            SqlCommand sqlc = new SqlCommand(getNameString, dl.SqlConnection);
            SqlDataReader reader = sqlc.ExecuteReader();
            string name = "";
            if (reader.HasRows)
            {
                if (reader.Read())
                {
                    name = reader.GetString(0) + " " + reader.GetString(1);
                }
            }
            dl.SqlConnection.Close();
            return name;
        }

        public bool getTeams(int schoolRepID, out List<int> teamIDs, out List<string> teamNames, out List<List<int>> teamMembers)
        {
            dl.SqlConnection.Open();
            int schoolID = getSchoolID(schoolRepID);
            string getTeamsString = "SELECT team_id, team_name FROM Team WHERE team_deleted IS NULL AND team_schoolid='" + schoolID + "'";
            SqlCommand sqlc = new SqlCommand(getTeamsString, dl.SqlConnection);
            SqlDataReader reader = sqlc.ExecuteReader();
            teamIDs = new List<int>();
            teamNames = new List<string>();
            teamMembers = new List<List<int>>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    teamIDs.Add(reader.GetInt32(0));
                    teamNames.Add(reader.GetString(1));
                }
            }
            reader.Close();
            for (int i = 0; i < teamIDs.Count; i++)
            {

                teamMembers.Add(getTeamMembers(teamIDs[i]));
            }
            dl.SqlConnection.Close();
            return true;
        }

        public bool addTeam(string teamName, int schoolID)
        {
            dl.SqlConnection.Open();
            string addTeamString = "INSERT INTO Team(team_name, team_schoolid) "+"VALUES(@team_name, @team_schoolid)";
            SqlCommand sqlc = new SqlCommand(addTeamString, dl.SqlConnection);
            sqlc.Parameters.AddWithValue("team_name", teamName);
            sqlc.Parameters.AddWithValue("team_schoolid", schoolID);
            sqlc.ExecuteNonQuery();
            dl.SqlConnection.Close();
            return true;
        }

    }
}
