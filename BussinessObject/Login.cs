using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace BussinessObject
{
    public class Login
    {
        // properties
        private string username;
        private LoginData dataLogin;

        private string encriptPass;
        //for debug purposes only
        public string EncriptPass
        {
            get { return encriptPass; }
        }

        //Constructor
        public Login(string username, string password)
        {
            this.username = username;
            //this.encriptPass = _PassEncription.Encript(password);
            this.encriptPass = password;

            dataLogin = new LoginData();
        }

        //Methods
        public string GetUserType(int userID)
        {
            return dataLogin.getUserType(userID);
        }

        public bool Validate(out string errorMsgs, out int userID)
        {
            

            //stoplime's validation code
            // Assuming dataLogin object
            errorMsgs = "";
            bool enteredEmail = false;
            Debug.WriteLine("username: " + username);
            Debug.WriteLine("username exists: "+dataLogin.validUsername(username));
            //***Check if username exists***
            /*if (dataLogin.validUsername(username))//returns true if correct
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
                userID = -1;
                return false;
            }*/
            if (username.Contains('@'))
            {
                enteredEmail = true;
            }

            //***Check if password is correct***
            if (dataLogin.validPass(username, encriptPass, enteredEmail, out userID))// accepts three input(string username, string pass, bool isEmail) returns true if correct
            {
                // user enterd a correct username and pass
                return true;
            }
            else
            {
                // user entered an incorrect username and pass
                errorMsgs = "Invalid Password";
                return false;
            }
            
        }

    }
}
