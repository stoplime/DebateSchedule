using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Diagnostics;

using DataAccess;

namespace BussinessObject
{
    public class AccountSuper
    {
        //needs to add superreferees
        //needs to edit scores

        private AccountSuperData accountData;

        private string myEmail;

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

        private List<string> refereeEmail;
        public List<string> RefereeEmail
        {
            get { return refereeEmail; }
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

        private List<string> superEmail;
        public List<string> SuperEmail
        {
            get { return superEmail; }
        }

        public AccountSuper()
        {
            accountData = new AccountSuperData();
            getReferees();
        }

        public void getReferees()
        {
            accountData.getTypeList(out refereeList, out refereeName, out refereeEmail, "Referee");
        }

        public void getSupers(int myID)
        {
            accountData.getTypeList(out superList, out superName, out superEmail, "SuperReferee");
            for (int i = 0; i < superList.Count; i++)
            {
                if (superList[i] == myID)
                {
                    myEmail = superEmail[i];
                    break;
                }
            }
        }

        public string generateCode()
        {
            byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            byte[] key = Guid.NewGuid().ToByteArray();
            return Convert.ToBase64String(time.Concat(key).ToArray());
        }

        public bool sendRefereeInvitation(string toEmail)
        {
            MailMessage mail = new MailMessage(myEmail, toEmail);
            SmtpClient client = new SmtpClient();
            client.Port = 25;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Host = "smtp.google.com";
            mail.Subject = "TSS Referee Invitation";
            mail.Body = "You have been invited to be a referee of the Team Scheduling System. You have 24 hours to accept this invitation. Use the code below when creating a new user by navigating to login and create new account. Make sure you choose the referee user type to enter in the referee code: " + generateCode();
            client.Send(mail);
            return true;
        }

        public bool sendSuperInvitation(int refereeID)
        {
            //update referee here
            accountData.updateRefereeToSuper(refereeID);
            MailMessage mail = new MailMessage(myEmail, accountData.GetEmailFromPersID(refereeID));
            SmtpClient client = new SmtpClient();
            client.Port = 25;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Host = "smtp.google.com";
            mail.Subject = "TSS Super Referee Invitation";
            mail.Body = "You have been invited to be a super referee of the Team Scheduling System. Your account has been automatically updated to a Super Referee account";
            client.Send(mail);
            return true;
        }
    }
}
