using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class CreateAccData
    {

        public CreateAccData()
        {
            DatabaseLayer dl = new DatabaseLayer();
        }

        public string[] GetSchools()
        {
            //returns an array of all the available school names
            return new string[1];
        }

        public string[] GetTeams(string selectedSchool)
        {
            //returns an array of all the available team names from the selected school
            return new string[1];
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

        public bool validSchool(string school)
        {
            //returns true if school name already exists in the data base
            return true;
        }

        public bool validRefCode(string refCode)
        {
            //returns true if referee code is valid
            return true;
        }

        public string AddUser(string firstName, string lastName, string userName, string encriptPass, string email, int userType, string schoolSelect, string teamSelect, string schoolInput)
        {
            string errorMsgs = "";
            //creates an entry for new user into the database



            return errorMsgs;
        }
    }
}
