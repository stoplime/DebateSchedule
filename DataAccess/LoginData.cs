using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Diagnostics;

namespace DataAccess
{
    public class LoginData
    {
        private DatabaseLayer dl;
        public LoginData()
        {
            dl = new DatabaseLayer();
        }

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
            int emails = (int)sqlc.ExecuteScalar();
            if (emails > 0)
            {
                dl.SqlConnection.Close();
                return true;
            }
            dl.SqlConnection.Close();
            return false;
        }

        public bool resetPass(string email, string pass)
        {
            dl.SqlConnection.Open();
            string resetPassString = "UPDATE Users SET users_password='" + pass + "' WHERE users_email='" + email + "'";
            SqlCommand sqlc = new SqlCommand(resetPassString, dl.SqlConnection);
            sqlc.ExecuteNonQuery();
            dl.SqlConnection.Close();
            return true;
        }

        public bool validPass(string username, string pass, bool isEmail, out int userID)
        {
            userID = -1;
            //returns true if login sequence is correct
            dl.SqlConnection.Open();
            string getPass = "SELECT users_id FROM Users WHERE ";
            if (isEmail)
            {
                getPass += "users_email";
            }
            else
            {
                getPass += "users_login";
            }

            getPass += "='" + username + "' AND users_password='" + pass + "'";

            SqlCommand sqlc = new SqlCommand(getPass, dl.SqlConnection);
            SqlDataReader reader = sqlc.ExecuteReader();
            if (reader.HasRows)
            {
                if (reader.Read())
                {
                    userID = reader.GetInt32(0);
                }
                dl.SqlConnection.Close();
                return true;
            }
            dl.SqlConnection.Close();
            return false;
        }

    }
}
