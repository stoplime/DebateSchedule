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
    public class DatabaseLayer
    {
        private SqlConnection sqlConnection;
        public SqlConnection SqlConnection
        {
            get { return sqlConnection; }
            set { sqlConnection = value; }
        }
        

        public DatabaseLayer()
        {
            string conn = ConfigurationManager.ConnectionStrings["scheduler_database"].ToString();
            sqlConnection = new SqlConnection(conn);
        }

        //add user from sql execute command
        /*
        public string newUser(CreateAccount new_user)
        {
            int insertid = 0;

            try
            {
                sqlConnection.Open();
                string newUser_sql_string = "INSERT INTO Users(user_logon, user_password, user_email) " + "VALUES(@user_logon, @user_password, @user_email)";
                SqlCommand command = new SqlCommand(newUser_sql_string, sqlConnection);
                command.Parameters.AddWithValue("user_logon", new_user.UserName);
                command.Parameters.AddWithValue("user_password", new_user.EncriptPass);
                command.Parameters.AddWithValue("user_email", new_user.Email);

                insertid = command.ExecuteNonQuery();

                if (insertid > 0)
                {
                    return "success";
                }
                else
                {
                    return "Error in trying to add new user: " + command.ToString();
                }
            }
            catch (SqlException sqle)
            {
                throw sqle;
            }
            finally
            {
                if (sqlConnection.State != ConnectionState.Closed)
                {
                    sqlConnection.Close();
                }
            }
        }
        */

    } 
}
