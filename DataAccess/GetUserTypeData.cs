﻿using System;
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
    public class GetUserTypeData
    {
        private DatabaseLayer dl;
        private string userID;
        public string UserID
        {
            set
            {
                userID = value;
                getUserType(userID);
            }
        }

        private string userType;
        public string UserType
        {
            get { return userType; }
        }

        public GetUserTypeData()
        {
            dl = new DatabaseLayer();
        }

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
    }
}
