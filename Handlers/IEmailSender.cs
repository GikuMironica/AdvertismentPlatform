using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertismentPlatform.Handlers
{
    interface IEmailSender
    {
        public Task<bool> SendEmail(string receiver, string subject, string message);
    }
}
