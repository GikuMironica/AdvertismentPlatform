using AdvertismentPlatform.Services;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AdvertismentPlatform.Security
{
    public class GoogleRecaptchaService : IGoogleRecaptchaService
    {
        private RecaptchaSettings settings;

        public GoogleRecaptchaService(IOptions<RecaptchaSettings> settings)
        {
            this.settings = settings.Value;
        }

        public virtual async Task<GoogleREspo> VerifyRecaptcha(string token)
        {
            GooglereCaptchaData myData = new GooglereCaptchaData
            {
                response = token,
                secret = settings.ReCAPTCHA_Secret_Key
            };

            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync($"https://www.google.com/recaptcha/api/siteverify?secret={myData.secret}&response={myData.response}");

            var capresp = JsonConvert.DeserializeObject<GoogleREspo>(response);

            return capresp;

        }
    }

    public class GooglereCaptchaData
    {
        public string response { get; set; }

        public string secret { get; set; }
    }

    public class GoogleREspo
    {
        public bool success { get; set; }

        public double score { get; set; }

        public string action { get; set; }

        public DateTime challenge_ts { get; set; }

        public string hostname { get; set; }
    }
}
