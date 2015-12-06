using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataAccess;

namespace BussinessObject
{
    public class AccountMember
    {
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

        private string school;
        public string School
        {
            get { return school; }
        }

        private string teamname;
        public string Teamname
        {
            get { return teamname; }
        }
        
        private int score;
        public int Score
        {
            get { return score; }
        }

        private AccountMemberData accessData;

        public AccountMember()
        {
            accessData = new AccountMemberData();
        }

        public void setVariables(int userID)
        {
            username = accessData.getUserName(userID);
            email = accessData.getEmail(userID);
            school = accessData.getSchool(userID);
            teamname = accessData.getTeam(userID);
            score = accessData.getScore(userID);
        }
    }
}
