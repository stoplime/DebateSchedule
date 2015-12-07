using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataAccess;

namespace BussinessObject
{
    public class EditProfile
    {
        private EditProfileData accessData;

        private string username;
        public string Username
        {
            get { return username; }
        }

        private string email;
        public string Email
        {
            get { return email; }
        }

        private string firstName;
        public string FirstName
        {
            get { return firstName; }
        }

        private string lastName;
        public string LastName
        {
            get { return lastName; }
        }

        public EditProfile()
        {
            accessData = new EditProfileData();
        }

        public bool GetData(int userID)
        {
            return accessData.GetData(userID, out username, out email, out firstName, out lastName);
        }

        public bool UpdateProfile(int userID, string firstName, string lastName, string username, string email, string password)
        {
            return accessData.UpdateProfile(userID, firstName, lastName, username, email, password);
        }
    }
}
