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
            //string getDataString = "SELECT users_login, users_email FROM Users WHERE users_id='" + userID + "'";
            //SqlCommand sqlc = new SqlCommand(getDataString, dl.SqlConnection);
            //SqlDataReader reader = sqlc.ExecuteReader();
            dl.SqlConnection.Close();
            return true;
        }

    }
}
