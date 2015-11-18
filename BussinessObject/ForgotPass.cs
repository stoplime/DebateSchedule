using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace BussinessObject
{
    class ForgotPass
    {
        private string username;
        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public ForgotPass(string username)
        {
            this.username = username;
        }

        public bool Validate(out string errorMsgs)
        {
            LoginData dataLogin = new LoginData();

            errorMsgs = "";

            //validate the username
            bool enteredUsername = false;
            bool enteredEmail = false;

            //***Check if username exists***
            if (dataLogin.validUsername(username))//returns true if correct
            {
                //user entered a username
                enteredUsername = true;
            }
            //***Check if Email exists***
            if (dataLogin.validEmail(username))//returns true if correct
            {
                //user entered an email
                enteredEmail = true;
            }
            if (!enteredEmail && !enteredUsername)
            {
                //ERROR: user entered an invalid Username or Email
                errorMsgs = "Invalid Username or Email";
                return false;
            }

            //finish Validation
            //*********** Send Email to user ************


            return true;
        }

    }
}
