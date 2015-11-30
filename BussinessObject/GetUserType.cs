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
        
        public bool isUserSuper()
        {
            if (accessData.UserType == "SuperReferee")
            {
                return true;
            }
            return false;
        }

        public bool isUserReferee()
        {
            if (accessData.UserType == "Referee")
            {
                return true;
            }
            return false;
        }

        public bool isUserSchoolRep()
        {
            if (accessData.UserType == "SchoolRep")
            {
                return true;
            }
            return false;
        }
        
        public bool isUserTeamMember()
        {
            if (accessData.UserType == "TeamMember")
            {
                return true;
            }
            return false;
        }
    }
}
