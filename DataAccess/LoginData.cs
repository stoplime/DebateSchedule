using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class LoginData
    {

        public LoginData()
        {
            DatabaseLayer dl = new DatabaseLayer();

        }

        public bool validUsername(string userName)
        {
            //returns true if username exists in database
            return true;
        }

        public bool validEmail(string email)
        {
            //returns true if email exists in database
            return true;
        }

        public bool validPass(string username, string pass, bool isEmail)
        {
            //returns true if login sequence is correct
            if (isEmail)
            {
                //check the pass from username as email
                return true;
            }
            else
            {
                //check the pass from username as username
                return true;
            }
        }

    }
}
