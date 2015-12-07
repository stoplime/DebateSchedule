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
    public class FooterData
    {
        private DatabaseLayer dl;

        public FooterData()
        {
            dl = new DatabaseLayer();
        }

        public bool getSuperList(out List<string> names, out List<string> emails)
        {
            dl.SqlConnection.Open();
            string getSuperName = "SELECT pers_firstname, pers_lastname, pers_usersid FROM Person WHERE pers_type='SuperReferee'";
            SqlCommand sqlcSuperName = new SqlCommand(getSuperName, dl.SqlConnection);
            SqlDataReader readerSuperName = sqlcSuperName.ExecuteReader();
            names = new List<string>();
            emails = new List<string>();
            List<int> ids = new List<int>();
            if (readerSuperName.HasRows)
            {
                while (readerSuperName.Read())
                {
                    names.Add(readerSuperName.GetString(0)+" "+readerSuperName.GetString(1));
                    ids.Add(readerSuperName.GetInt32(2));
                }
            }
            readerSuperName.Close();
            string getEmails = "SELECT users_id, users_email FROM Users";
            SqlCommand sqlcEmails = new SqlCommand(getEmails, dl.SqlConnection);
            SqlDataReader readerEmails = sqlcEmails.ExecuteReader();
            if (readerEmails.HasRows)
            {
                while (readerEmails.Read())
                {
                    for (int i = 0; i < ids.Count; i++)
                    {
                        if (ids[i] == readerEmails.GetInt32(0))
                        {
                            emails.Add(readerEmails.GetString(1));
                        }
                    }
                }
            }
            readerEmails.Close();
            dl.SqlConnection.Close();
            return true;
        }

    }
}
