using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using DataAccess;

namespace BussinessObject
{
    public class AccountSuper
    {
        //needs to add superreferees
        //needs to edit scores

        private AccountSuperData accountData;

        private List<int> refereeList;
        public List<int> RefereeList
        {
            get { return refereeList; }
        }

        private List<string> refereeName;
        public List<string> RefereeName
        {
            get { return refereeName; }
        }

        private List<int> superList;
        public List<int> SuperList
        {
            get { return superList; }
        }

        private List<string> superName;
        public List<string> SuperName
        {
            get { return superName; }
        }

        public AccountSuper()
        {
            accountData = new AccountSuperData();
            getReferees();
            getSupers();
        }

        public void getReferees()
        {
            accountData.getTypeList(out refereeList, out refereeName, "Referee");
        }

        public void getSupers()
        {
            accountData.getTypeList(out superList, out superName, "SuperReferee");
        }
    }
}
