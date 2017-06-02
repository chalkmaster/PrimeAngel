using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace PrimeAngel.Actuators.Notify
{
    public class EmailNotificationAction: IAction
    {
        private readonly MailMessage _message;

        public EmailNotificationAction(MailMessage message)
        {
            _message = message;
        }

        public ActionResult Act()
        {
            var result = new ActionResult
            {
                Action = this,
                Score = 1
            };

            try
            {
                using (var mailSender = new SmtpClient("mail.primesystems.com.br", 587))
                {
                    mailSender.Credentials = new NetworkCredential("osmobile@primesystems.com.br", "Prime@2011");                   
                    mailSender.Send(_message);
                }

                result.Message = "Mail Sent";
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;                
                result.Success = false;
            }
            return result;
        }
    }
}
