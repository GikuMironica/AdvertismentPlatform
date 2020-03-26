using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace AdvertismentPlatform.Handlers
{
    
     public class EmailHandler
    {
        private IConfiguration Configuration { get; }
        private SmtpClient client;
        private MailAddress mailFrom;
       
       
        public EmailHandler(IConfiguration configuration)
        {
            Configuration = configuration;
            string username = Configuration.GetConnectionString("address");
            string password = Configuration.GetConnectionString("password");

            client = new SmtpClient("smtp.gmail.com");
            client.Credentials = new System.Net.NetworkCredential(username, password);
            client.Port = 587;
            client.EnableSsl = true;
            mailFrom = new MailAddress("advertismentplatform@gmail.com");
        }

        
        public async Task<bool> SendEmail(string receiver, string subject, string message)
        {
            MailAddress mailTo = new MailAddress(receiver);
            MailMessage mailMessage = new MailMessage(mailFrom, mailTo);
            mailMessage.Body = message;
            mailMessage.Subject = subject;

            try
            {
                await client.SendMailAsync(mailMessage);
            } catch(Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return false;
            }

            return true;
        }
    }
}
