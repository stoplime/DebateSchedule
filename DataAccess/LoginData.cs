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
    public class LoginData
    {
        private DatabaseLayer dl;
        public LoginData()
        {
            dl = new DatabaseLayer();
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

        public bool validPass(string username, string pass, bool isEmail, out int userID)
        {
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
                userID = reader.GetInt32(0);
                dl.SqlConnection.Close();
                return true;
            }
            userID = -1;
            dl.SqlConnection.Close();
            return false;
        }

    }
}
