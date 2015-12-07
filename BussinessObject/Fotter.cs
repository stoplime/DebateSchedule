using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataAccess;

namespace BussinessObject
{
    public class Fotter
    {
        private FooterData foot;

        private List<string> names;
        public List<string> Names
        {
            get { return names; }
        }

        private List<string> emails;
        public List<string> Emails
        {
            get { return emails; }
        }

        public Fotter()
        {
            foot = new FooterData();
        }

        public bool getSupers()
        {
            return foot.getSuperList(out names, out emails);
        }
    }
}
