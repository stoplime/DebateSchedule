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
        private string conn = ConfigurationManager.ConnectionStrings["scheduler_database"].ToString();
        private SqlConnection objsqlconn;
        public DatabaseLayer()
        {
            objsqlconn = new SqlConnection(conn);
            objsqlconn.Open();
        }

        public void InsertUpdateDeleteSQLString(string sqlstring)
        {
            
            SqlCommand objcmd = new SqlCommand(sqlstring, objsqlconn);
            objcmd.ExecuteNonQuery();
        }

        public DataTable ToDataTable(string sqlcommand)
        {
            SqlDataAdapter da = new SqlDataAdapter(sqlcommand, objsqlconn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }
}
