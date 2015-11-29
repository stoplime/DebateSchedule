using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using System.Diagnostics;

namespace BussinessObject
{
    public class CreateAccount
    {
        private string firstName;
        /*public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }*/

        private string lastName;
        /*public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }*/

        private string userName;
        /*public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }*/

        private string encriptPass;
        /*public string EncriptPass
        {
            get { return encriptPass; }
            set { encriptPass = value; }
        }*/

        private string email;
        /*public string Email
        {
            get { return email; }
            set { email = value; }
        }*/

        private UserEnum userType;
        public UserEnum UserType
        {
            //get { return userType; }
            set { userType = value; }
        }

        private string teamSelect;
        public string TeamSelect
        {
            //get { return teamSelect; }
            set { teamSelect = value; }
        }

        private string schoolInput;
        public string SchoolInput
        {
            //get { return schoolInput; }
            set { schoolInput = value; }
        }

        private string refCode;
        public string RefCode
        {
            //get { return refCode; }
            set { refCode = value; }
        }

        private CreateAccData dataCreateAcc;

        public CreateAccount()
        {
            this.firstName = "";
            this.lastName = "";
            this.userName = "";
            //this.encriptPass = _PassEncription.Encript(password);
            this.encriptPass = "";
            this.email = "";

            dataCreateAcc = new CreateAccData();
        }

        public CreateAccount(string firstName, string lastName, string userName, string password, string email):this()
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.userName = userName;
            //this.encriptPass = _PassEncription.Encript(password);
            this.encriptPass = password;
            this.email = email;
        }

        public List<string> GetTeams()
        {
            return dataCreateAcc.GetTeams();// returns a string array of the names of teams available
        }

        public string GetUserType(int userID)
        {
            return dataCreateAcc.getUserType(userID);
        }


        public bool Valitate(out string errorMsgs, out int userID)
        {
            //stoplime's validation code
            // Assuming dataCreateAcc object
            errorMsgs = "";
            userID = -1;

            //***Check if Username exists***
            if (dataCreateAcc.validUsername(userName))//returns true if username already exists
            {
                //ERROR: username already exists
                errorMsgs += "Username is already taken \n";
                return false;
            }
            //***Check if Email exists***
            if (dataCreateAcc.validEmail(email))//returns true if Email already exists
            {
                //ERROR: Email already exists
                errorMsgs += "Email is already taken \n";
                return false;
            }

            //Case structure for each user type
            switch (userType)
            {
                case UserEnum.Null:
                    errorMsgs += "ERROR: unselected usertype \n";
                    return false;
                case UserEnum.TeamMember:
                    //Does not require validity
                    break;
                case UserEnum.SchoolRep:
                    //***Check if school exists***
                    if (dataCreateAcc.validSchool(schoolInput))//returns true if school name already exists
                    {
                        //ERROR: School name already exists
                        errorMsgs += "The School entered already has a representative \n";
                        return false;
                    }
                    break;
                case UserEnum.Referee:
                    //***Check if referee code is correct***
                    if (!dataCreateAcc.validRefCode(refCode))//returns true if refCode exists
                    {
                        //ERROR: refCode does not exist
                        errorMsgs += "The Referee code entered does not match with our system \n";
                        return false;
                    }
                    break;
                case UserEnum.SuperReferee:
                    errorMsgs += "ERROR: Restricted usertype \n";
                    return false;
                default:
                    errorMsgs += "ERROR: unknown usertype \n";
                    return false;
            }

            //Validaty check complete
            //Add to database
            try
            {
                Debug.WriteLine("Create Account try block");
                errorMsgs += dataCreateAcc.AddUser(firstName, lastName, userName, encriptPass, email, (int)userType, teamSelect, schoolInput, out userID);
            }catch(Exception e)
            {
                errorMsgs += e + " \n";
                return false;
            }
            
            return true;
        }

    }
}
