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

        public bool validPass(string username, int pass, bool isEmail)
        {
            //returns true if login sequence is correct
            dl.SqlConnection.Open();
            if (isEmail)
            {
                //check the pass from username as email
                string getPassFromEmail = "SELECT users_password FROM Users WHERE users_email='" + username + "'";
                SqlCommand sqlc = new SqlCommand(getPassFromEmail, dl.SqlConnection);
                int emails = sqlc.ExecuteNonQuery();
                if (emails > 0)
                {
                    dl.SqlConnection.Close();
                    if (true)
                    {

                    }
                    
                }

                dl.SqlConnection.Close();
                return false;
            }
            else
            {
                //check the pass from username as username
                string getPassFromName = "SELECT users_password FROM Users WHERE users_login='" + username + "'";
                SqlCommand sqlc = new SqlCommand(getPassFromName, dl.SqlConnection);
                int names = sqlc.ExecuteNonQuery();
                if (names > 0)
                {
                    dl.SqlConnection.Close();
                    return true;
                }
                dl.SqlConnection.Close();
                return false;
            }
        }

    }
}
