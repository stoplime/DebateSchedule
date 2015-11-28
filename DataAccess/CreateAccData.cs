using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public class CreateAccData
    {
        private DatabaseLayer dl;

        public CreateAccData()
        {
            dl = new DatabaseLayer();
        }

        public List<string> GetSchools()
        {
            //returns an array of all the available school names
            dl.SqlConnection.Open();
            string getSchoolString = "SELECT scho_name FROM School";
            SqlCommand sqlc = new SqlCommand(getSchoolString, dl.SqlConnection);
            SqlDataReader reader = sqlc.ExecuteReader();
            List<string> schools = new List<string>();
            while (reader.HasRows)
            {
                while (reader.Read())
                {
                    schools.Add(reader.GetString(0));
                }
            }
            dl.SqlConnection.Close();
            return schools;
        }

        public int GetSchoolID(string schoolName)
        {
            //returns an array of all the available school names
            dl.SqlConnection.Open();
            string getSchoolString = "SELECT scho_id FROM School WHERE scho_name='" + schoolName + "'";
            SqlCommand sqlc = new SqlCommand(getSchoolString, dl.SqlConnection);
            SqlDataReader reader = sqlc.ExecuteReader();
            int id = -1;
            while (reader.HasRows)
            {
                while (reader.Read())
                {
                    id = reader.GetInt32(0);
                }
            }
            dl.SqlConnection.Close();

            return id;
        }

        public List<string> GetTeams(string selectedSchool)
        {
            //returns an array of all the available team names from the selected school
            dl.SqlConnection.Open();
            string getTeamString = "SELECT team_name FROM Teams WHERE team_school='" + selectedSchool + "'";
            SqlCommand sqlc = new SqlCommand(getTeamString, dl.SqlConnection);
            SqlDataReader reader = sqlc.ExecuteReader();
            List<string> teams = new List<string>();
            while (reader.HasRows)
            {
                while (reader.Read())
                {
                    teams.Add(reader.GetString(0));
                }
            }
            dl.SqlConnection.Close();
            return teams;
        }

        public int GetTeamID(string teamName)
        {
            //returns an array of all the available school names
            dl.SqlConnection.Open();
            string getTeamString = "SELECT team_id FROM School WHERE team_name='" + teamName + "'";
            SqlCommand sqlc = new SqlCommand(getTeamString, dl.SqlConnection);
            SqlDataReader reader = sqlc.ExecuteReader();
            int id = -1;
            while (reader.HasRows)
            {
                while (reader.Read())
                {
                    id = reader.GetInt32(0);
                }
            }
            dl.SqlConnection.Close();

            return id;
        }

        public bool validUsername(string userName)
        {
            //returns true if username exists in database
            dl.SqlConnection.Open();
            string getUsername = "SELECT * FROM Users WHERE users_login='" + userName + "'";
            SqlCommand sqlc = new SqlCommand(getUsername, dl.SqlConnection);
            int names = sqlc.ExecuteNonQuery();
            
            if (names > 0)
            {
                dl.SqlConnection.Close();
                return true;
            }
            dl.SqlConnection.Close();
            return false;
        }

        public bool validEmail(string email)
        {
            //returns true if email exists in database
            dl.SqlConnection.Open();
            string getEmail = "SELECT * FROM Users WHERE users_email='" + email + "'";
            SqlCommand sqlc = new SqlCommand(getEmail, dl.SqlConnection);
            int emails = sqlc.ExecuteNonQuery();
            if (emails > 0)
            {
                dl.SqlConnection.Close();
                return true;
            }
            dl.SqlConnection.Close();
            return false;
        }

        public bool validSchool(string school)
        {
            //returns true if school name already exists in the data base
            dl.SqlConnection.Open();
            string schoolExists = "SELECT * FROM School WHERE scho_name='" + school + "'";
            SqlCommand sqlc = new SqlCommand(schoolExists, dl.SqlConnection);
            if (sqlc.ExecuteNonQuery() > 0)
            {
                dl.SqlConnection.Close();
                return false;
            }
            dl.SqlConnection.Close();
            return true;
        }

        public bool validRefCode(string refCode)
        {
            //returns true if referee code is valid
            
            return true;
        }

        public string AddUser(string firstName, string lastName, string userName, int encriptPass, string email, int userType, string schoolSelect, string teamSelect, string schoolInput)
        {
            string errorMsgs = "";
            //creates an entry for new user into the database
            dl.SqlConnection.Open();

            try
            {
                string usersTable = "INSERT INTO Users(users_login, users_password, users_email, users_createddate) " + "VALUES(@users_login, @users_password, @users_email, @users_createddate)";
                SqlCommand command = new SqlCommand(usersTable, dl.SqlConnection);
                command.Parameters.AddWithValue("users_login", userName);
                command.Parameters.AddWithValue("users_password", encriptPass);
                command.Parameters.AddWithValue("users_email", email);
                command.Parameters.AddWithValue("users_createddate", DateTime.Now);
                command.ExecuteNonQuery();
                string personTable;
                string schoolTable;

                switch (userType)
                {
                    case 1:
                        //Team member
                        personTable = "INSERT INTO Person(pers_type, pers_firstname, pers_secondname, pers_schoolid, pers_teamid) " + "VALUES(@pers_type, @pers_firstname, @pers_secondname, @pers_schoolid, @pers_teamid)";
                        SqlCommand sqlcTeam = new SqlCommand(personTable, dl.SqlConnection);
                        sqlcTeam.Parameters.AddWithValue("pers_type", userType);
                        sqlcTeam.Parameters.AddWithValue("pers_firstname", firstName);
                        sqlcTeam.Parameters.AddWithValue("pers_secondname", lastName);
                        int schoolId = GetSchoolID(schoolSelect);
                        if (schoolId >= 0)
                        {
                            sqlcTeam.Parameters.AddWithValue("pers_schoolid", schoolId);
                        }
                        else
                        {
                            errorMsgs += "Invalid school selected from to the system";
                        }
                        int teamId = GetTeamID(teamSelect);
                        if (teamId >= 0)
                        {
                            sqlcTeam.Parameters.AddWithValue("pers_teamid", teamId);
                        }
                        else
                        {
                            errorMsgs += "Invalid team selected from to the system";
                        }
                        sqlcTeam.ExecuteNonQuery();
                        break;
                    case 2:
                        //School Rep
                        personTable = "INSERT INTO Person(pers_type, pers_firstname, pers_secondname, pers_schoolid) " + "VALUES(@pers_type, @pers_firstname, @pers_secondname, @pers_schoolid)";
                        schoolTable = "INSERT INTO School(scho_name)" + "VALUES(@scho_name)";
                        SqlCommand sqlcNewSchool = new SqlCommand(schoolTable, dl.SqlConnection);
                        sqlcNewSchool.Parameters.AddWithValue("scho_name", schoolInput);
                        sqlcNewSchool.ExecuteNonQuery();
                        SqlCommand sqlcSchRep = new SqlCommand(personTable, dl.SqlConnection);
                        sqlcSchRep.Parameters.AddWithValue("pers_type", userType);
                        sqlcSchRep.Parameters.AddWithValue("pers_firstname", firstName);
                        sqlcSchRep.Parameters.AddWithValue("pers_secondname", lastName);
                        int newSchoolId = GetSchoolID(schoolInput);
                        sqlcSchRep.Parameters.AddWithValue("pers_schoolid", newSchoolId);
                        sqlcNewSchool.ExecuteNonQuery();
                        sqlcSchRep.ExecuteNonQuery();
                        break;
                    case 3:
                        //referee
                        personTable = "INSERT INTO Person(pers_type, pers_firstname, pers_secondname) " + "VALUES(@pers_type, @pers_firstname, @pers_secondname)";
                        SqlCommand sqlcRef = new SqlCommand(personTable, dl.SqlConnection);
                        sqlcRef.Parameters.AddWithValue("pers_type", userType);
                        sqlcRef.Parameters.AddWithValue("pers_firstname", firstName);
                        sqlcRef.Parameters.AddWithValue("pers_secondname", lastName);
                        sqlcRef.ExecuteNonQuery();
                        break;
                    default:
                        break;
                }
            }
            catch (SqlException sqle)
            {
                throw sqle;
            }
            finally
            {
                if (dl.SqlConnection.State != ConnectionState.Closed)
                {
                    dl.SqlConnection.Close();
                }
            }
            return errorMsgs;
        }
    }
}
