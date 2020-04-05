using AdvertismentPlatform.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertismentPlatform.Services
{
    public interface IGoogleRecaptchaService
    {
        Task<GoogleREspo> VerifyRecaptcha(string token);

    }
}
