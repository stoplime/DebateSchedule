using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataAccess;

namespace BussinessObject
{
    public class AccountSchoolRep
    {
        private AccountSchoolRepData accessData;

        private string myName;
        public string MyName
        {
            get { return myName; }
        }

        private string schoolName;
        public string SchoolName
        {
            get { return schoolName; }
        }

        private List<int> teamIDs;
        public List<int> TeamIDs
        {
            get { return teamIDs; }
        }

        private List<string> teamNames;
        public List<string> TeamNames
        {
            get { return teamNames; }
        }

        private List<List<int>> teamMembers;
        public List<List<int>> TeamMembers
        {
            get { return teamMembers; }
        }
        
        public AccountSchoolRep()
        {
            accessData = new AccountSchoolRepData();
        }

        public void getRepresentative(int schoolRepID)
        {
            accessData.getRepresentative(schoolRepID, out myName, out schoolName);
            accessData.getTeams(schoolRepID, out teamIDs, out teamNames, out teamMembers);
        }

        public string getMemberName(int memberID)
        {
            return accessData.getTeamMemberName(memberID);
        }

        public void addTeam(string teamName, int schoolRepID)
        {
            accessData.addTeam(teamName, accessData.getSchoolID(schoolRepID));
        }
    }
}
