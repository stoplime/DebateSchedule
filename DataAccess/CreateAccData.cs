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
    public class CreateAccData
    {
        private DatabaseLayer dl;

        public CreateAccData()
        {
            dl = new DatabaseLayer();
        }
        #region get list
        /*public List<string> GetSchools()
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
        }*/

        public string getUserType(int userID)
        {
            dl.SqlConnection.Open();
            string getUserTypeString = "SELECT pers_type FROM Person WHERE pers_usersid='" + userID + "'";
            SqlCommand sqlc = new SqlCommand(getUserTypeString, dl.SqlConnection);
            SqlDataReader reader = sqlc.ExecuteReader();
            SqlString type = "";
            if (reader.HasRows)
            {
                if (reader.Read())
                {
                    type = reader.GetSqlString(0);
                }
            }
            dl.SqlConnection.Close();
            return type.ToString();
        }

        public List<string> GetTeams()
        {
            //returns an array of all the available team names
            dl.SqlConnection.Open();
            string getTeamString = "SELECT team_name FROM Team";
            SqlCommand sqlc = new SqlCommand(getTeamString, dl.SqlConnection);
            SqlDataReader reader = sqlc.ExecuteReader();
            List<string> teams = new List<string>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string temp = reader.GetString(0);
                    teams.Add(temp);
                }
            }
            dl.SqlConnection.Close();
            return teams;
        }
        #endregion

        #region get ids
        private int GetUserID(string userName)
        {
            int id;
            //returns an array of all the available school names
            //dl.SqlConnection.Open();
            string getUserString = "SELECT users_id FROM Users WHERE users_login='" + userName + "'";
            SqlCommand sqlc = new SqlCommand(getUserString, dl.SqlConnection);
            id = (int)sqlc.ExecuteScalar();
            return id;
        }

        private int GetSchoolIDFromName(string schoolName)
        {
            //returns an array of all the available school names
            //dl.SqlConnection.Open();
            string getSchoolString = "SELECT scho_id FROM School WHERE scho_name='" + schoolName + "'";
            SqlCommand sqlc = new SqlCommand(getSchoolString, dl.SqlConnection);
            int id = (int)sqlc.ExecuteScalar();
            //dl.SqlConnection.Close();
            return id;
        }
        
        public int GetSchoolID(string teamName)
        {
            //returns an array of all the available school names
            bool sqlConnected = (dl.SqlConnection.State != ConnectionState.Closed) ? true : false;
            if (!sqlConnected)
            {
                dl.SqlConnection.Open();
            }
            string getSchoolString = "SELECT team_schoolid FROM Team WHERE team_name='" + teamName + "'";
            SqlCommand sqlc = new SqlCommand(getSchoolString, dl.SqlConnection);
            int id = (int)sqlc.ExecuteScalar();
            if (!sqlConnected)
            {
                dl.SqlConnection.Close();
            }
            return id;
        }

        public int GetTeamID(string teamName)
        {
            //returns an array of all the available school names
            bool sqlConnected = (dl.SqlConnection.State != ConnectionState.Closed) ? true : false;
            if (!sqlConnected)
            {
                dl.SqlConnection.Open();
            }
            string getTeamString = "SELECT team_id FROM Team WHERE team_name='" + teamName + "'";
            SqlCommand sqlc = new SqlCommand(getTeamString, dl.SqlConnection);
            int id = (int)sqlc.ExecuteScalar();
            if (!sqlConnected)
            {
                dl.SqlConnection.Close();
            }
            return id;
        }
        #endregion

        #region Validation code
        public bool validUsername(string userName)
        {
            //returns true if username exists in database
            dl.SqlConnection.Open();
            string getUsername = "SELECT users_id FROM Users WHERE users_login='" + userName + "'";
            SqlCommand sqlc = new SqlCommand(getUsername, dl.SqlConnection);
            int names = 0;
            if (sqlc.ExecuteScalar() != null)
            {
                names = (int)sqlc.ExecuteScalar();
            }
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
            int emails = 0;
            if (sqlc.ExecuteScalar() != null)
            {
                emails = (int)sqlc.ExecuteScalar();
            }
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
            string schoolExists = "SELECT scho_id FROM School WHERE scho_name='" + school + "'";
            SqlCommand sqlc = new SqlCommand(schoolExists, dl.SqlConnection);
            int schools = 0;
            if (sqlc.ExecuteScalar() != null)
            {
                schools = (int)sqlc.ExecuteScalar();
            }
            //Debug.WriteLine("found number of schools: " + schools);
            if (schools > 0)
            {
                dl.SqlConnection.Close();
                return true;
            }
            dl.SqlConnection.Close();
            return false;
        }

        public bool validRefCode(string refCode)
        {
            //returns true if referee code is valid

            return true;
        }
        #endregion

        #region add new user
        public string AddUser(string firstName, string lastName, string userName, string encriptPass, string email, int userType, string teamSelect, string schoolInput, out int userID)
        {
            string errorMsgs = "";
            //creates an entry for new user into the database
            dl.SqlConnection.Open();
            Debug.WriteLine("~~~~~~~~ pre try block");
            try
            {
                Debug.WriteLine("pre try statement");
                string usersTable = "INSERT INTO Users(users_login, users_password, users_email) " + "VALUES(@users_login, @users_password, @users_email)";
                Debug.WriteLine("created usersTable string");
                SqlCommand command = new SqlCommand(usersTable, dl.SqlConnection);
                Debug.WriteLine("set command");
                command.Parameters.AddWithValue("users_login", userName);
                Debug.WriteLine("added users_login");
                command.Parameters.AddWithValue("users_password", encriptPass);
                Debug.WriteLine("added users_password");
                command.Parameters.AddWithValue("users_email", email);
                Debug.WriteLine("added users_email");

                Debug.WriteLine("Created users: "+command.ExecuteNonQuery());
                userID = GetUserID(userName);

                
                string personTable;
                string schoolTable;

                switch (userType)
                {
                    case 1:
                        //Team member
                        personTable = "INSERT INTO Person(pers_type, pers_firstname, pers_lastname, pers_schoolid, pers_teamid, pers_usersid) " + "VALUES(@pers_type, @pers_firstname, @pers_lastname, @pers_schoolid, @pers_teamid, @pers_usersid)";
                        SqlCommand sqlcTeam = new SqlCommand(personTable, dl.SqlConnection);
                        sqlcTeam.Parameters.AddWithValue("pers_type", "TeamMember");
                        sqlcTeam.Parameters.AddWithValue("pers_firstname", firstName);
                        sqlcTeam.Parameters.AddWithValue("pers_lastname", lastName);
                        int schoolId = GetSchoolID(teamSelect);
                        if (schoolId >= 0)
                        {
                            sqlcTeam.Parameters.AddWithValue("pers_schoolid", schoolId);
                        }
                        else
                        {
                            errorMsgs += "Invalid school selected from the system";
                        }
                        int teamId = GetTeamID(teamSelect);
                        if (teamId >= 0)
                        {
                            sqlcTeam.Parameters.AddWithValue("pers_teamid", teamId);
                        }
                        else
                        {
                            errorMsgs += "Invalid team selected from the system";
                        }
                        sqlcTeam.Parameters.AddWithValue("pers_usersid", userID);

                        sqlcTeam.ExecuteNonQuery();
                        break;
                    case 2:
                        //School Rep
                        personTable = "INSERT INTO Person(pers_type, pers_firstname, pers_lastname, pers_schoolid, pers_usersid) " + "VALUES(@pers_type, @pers_firstname, @pers_lastname, @pers_schoolid, @pers_usersid)";
                        schoolTable = "INSERT INTO School(scho_name)" + "VALUES(@scho_name)";
                        SqlCommand sqlcNewSchool = new SqlCommand(schoolTable, dl.SqlConnection);
                        sqlcNewSchool.Parameters.AddWithValue("scho_name", schoolInput);
                        sqlcNewSchool.ExecuteNonQuery();
                        SqlCommand sqlcSchRep = new SqlCommand(personTable, dl.SqlConnection);
                        sqlcSchRep.Parameters.AddWithValue("pers_type", "SchoolRep");
                        sqlcSchRep.Parameters.AddWithValue("pers_firstname", firstName);
                        sqlcSchRep.Parameters.AddWithValue("pers_lastname", lastName);
                        int newSchoolId = GetSchoolIDFromName(schoolInput);
                        sqlcSchRep.Parameters.AddWithValue("pers_schoolid", newSchoolId);
                        sqlcSchRep.Parameters.AddWithValue("pers_usersid", userID);
                        sqlcSchRep.ExecuteNonQuery();
                        break;
                    case 3:
                        //referee
                        personTable = "INSERT INTO Person(pers_type, pers_firstname, pers_lastname, pers_usersid) " + "VALUES(@pers_type, @pers_firstname, @pers_lastname, @pers_usersid)";
                        SqlCommand sqlcRef = new SqlCommand(personTable, dl.SqlConnection);
                        sqlcRef.Parameters.AddWithValue("pers_type", "Referee");
                        sqlcRef.Parameters.AddWithValue("pers_firstname", firstName);
                        sqlcRef.Parameters.AddWithValue("pers_lastname", lastName);
                        sqlcRef.Parameters.AddWithValue("pers_usersid", userID);
                        sqlcRef.ExecuteNonQuery();
                        break;
                    default:
                        break;
                }//*/
            }
            catch (SqlException sqle)
            {
                Debug.WriteLine("This is where the exception is :" + sqle.ToString());
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
        #endregion

    }
}
