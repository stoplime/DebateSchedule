using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using DataAccess;
using System.Diagnostics;

namespace BussinessObject
{
    public class ForgotPass
    {
        private LoginData dataLogin;
        private Random rand;

        private string email;
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public ForgotPass()
        {
            dataLogin = new LoginData();
            rand = new Random();
        }

        public string generateRandomPass()
        {
            char[] bank = "qwertyuiopasdfghjklzxcvbnmQAZWSXEDCRFVTGBYHNUJMIKOLP1234567890!@#$%^&*".ToCharArray();
            string randPass = "";
            int length = rand.Next(12, 30);
            for (int i = 0; i < length; i++)
            {
                randPass += bank[rand.Next(bank.Length)];
            }
            return randPass;
        }

        public bool resetPassword(string toEmail, string pass)
        {
            dataLogin.resetPass(toEmail, pass);
            return true;
        }

        public bool sendResetPassInvite()
        {
            string randPass = generateRandomPass();
            resetPassword(email, randPass);
            try
            {
                string body = "You have requested to reset your password. If this is not you, please ignore. If this is you, then please login to Team Scheduling System with your new generated password and imediately reset it in the my account page: " + randPass;
                var client = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("qv7p12r2@gmail.com", "tcb2fu38"),
                    EnableSsl = true
                };
                client.Send("tss@teamscheduling.com", email, "TSS Referee Invitation", body);
            }
            catch (SmtpException e)
            {
                Debug.WriteLine(e);
            }

            return true;
        }

        public bool Validate(out string errorMsgs)
        {
            //stoplime's validation code
            // Assuming dataLogin object
            errorMsgs = "";

            if (!dataLogin.validEmail(email))
            {
                errorMsgs += "Sorry, the email you have entered is not found";
                return false;
            }
            //finish Validation
            //*********** Send Email to user ************
            sendResetPassInvite();

            return true;
        }

    }
}
