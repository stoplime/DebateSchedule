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
    public class ScheduleData
    {
        private DatabaseLayer dl;

        private string userID;
        public string UserID
        {
            set {
                userID = value;
                setUserType(userID);
            }
        }

        private string userType;
        public string UserType
        {
            get { return userType; }
        }
        
        public ScheduleData()
        {
            dl = new DatabaseLayer();
            
        }

        private void setUserType(string userID)
        {
            dl.SqlConnection.Open();
            string getTypeString = "SELECT pers_type FROM Person WHERE pers_userid='" + userID + "'";
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

        public bool UpdateSchedule()
        {

            return true;
        }

    }
}
