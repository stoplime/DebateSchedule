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
    public class EditProfileData
    {
        private DatabaseLayer dl;
        
        public EditProfileData()
        {
            dl = new DatabaseLayer();
        }

        public bool GetData(int userID, out string username, out string email, out string firstname, out string lastname)
        {
            dl.SqlConnection.Open();
            string getDataString = "SELECT users_login, users_email FROM Users WHERE users_id='" + userID + "'";
            SqlCommand sqlc = new SqlCommand(getDataString, dl.SqlConnection);
            SqlDataReader reader = sqlc.ExecuteReader();
            username = "";
            email = "";
            firstname = "";
            lastname = "";
            if (reader.HasRows)
            {
                if (reader.Read())
                {
                    username = reader.GetString(0);
                    email = reader.GetString(1);
                }
            }
            reader.Close();
            string getNamesString = "SELECT pers_firstname, pers_lastname FROM Person WHERE pers_usersid='" + userID + "'";
            SqlCommand sqlcNames = new SqlCommand(getNamesString, dl.SqlConnection);
            SqlDataReader readerNames = sqlcNames.ExecuteReader();
            if (readerNames.HasRows)
            {
                if (readerNames.Read())
                {
                    firstname = readerNames.GetString(0);
                    lastname = readerNames.GetString(1);
                }
            }
            dl.SqlConnection.Close();
            return true;
        }

        public bool UpdateProfile(int userID, string firstName, string lastName, string username, string email, string password)
        {
            dl.SqlConnection.Open();
            string updateUsersString = "UPDATE Users SET users_login=@users_login, users_email=@users_email, users_password=@users_password WHERE users_id='" + userID + "'";
            SqlCommand sqlc = new SqlCommand(updateUsersString, dl.SqlConnection);
            sqlc.Parameters.AddWithValue("users_login", username);
            sqlc.Parameters.AddWithValue("users_email", email);
            sqlc.Parameters.AddWithValue("users_password", password);
            string updatePersonString = "UPDATE Person SET pers_firstname=@pers_firstname, pers_lastname=@pers_lastname WHERE pers_usersid='" + userID + "'";
            SqlCommand sqlcPers = new SqlCommand(updatePersonString, dl.SqlConnection);
            sqlcPers.Parameters.AddWithValue("pers_firstname", firstName);
            sqlcPers.Parameters.AddWithValue("pers_lastname", lastName);
            sqlc.ExecuteNonQuery();
            sqlcPers.ExecuteNonQuery();
            dl.SqlConnection.Close();
            return true;
        }
    }
}
