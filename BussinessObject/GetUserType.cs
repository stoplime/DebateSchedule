using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataAccess;

namespace BussinessObject
{
    public class GetUserType
    {
        private GetUserTypeData accessData;

        public GetUserType()
        {
            accessData = new GetUserTypeData();
        }

        public void setUserID(string userID)
        {
            accessData.UserID = userID;
        }
        
        public bool isUserSuper(string userType)
        {
            if (userType == "SuperReferee")
            {
                return true;
            }
            return false;
        }

        public bool isUserReferee(string userType)
        {
            if (userType == "Referee")
            {
                return true;
            }
            return false;
        }

        public bool isUserSchoolRep(string userType)
        {
            if (userType == "SchoolRep")
            {
                return true;
            }
            return false;
        }
        
        public bool isUserTeamMember(string userType)
        {
            if (userType == "TeamMember")
            {
                return true;
            }
            return false;
        }
    }
}
