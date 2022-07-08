using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShoppingStore
{
    public class PaypalConfiguration
    {
        public readonly static string clientId;
        public readonly static string clientSecret;


        static PaypalConfiguration()
        {
            var config = getconfig();
            clientId = "ASGKywgdBzCoMjLF5IhU5E-4MW5wDNvwsF8x0OuVF0kmNCLptYhR5s6exKjDBlzUytSk0rVg9FTODViA";
            clientSecret = "EDQ4g918Yyb_vslMHe2VGlbmbWLYbgbcX4go3Uv6NoNzhiM8glKNgJnti87odTv9lftd80-XpiQZ9lVK";
        }

        private static Dictionary<string, string> getconfig()
        {
            return PayPal.Api.ConfigManager.Instance.GetProperties();
        }

        private static string GetAccessToken()
        {
            string accessToken = new OAuthTokenCredential(clientId, clientSecret, getconfig()).GetAccessToken();
            return accessToken;
        }
        public static APIContext GetAPIContext()
        {
            APIContext apicontext = new APIContext(GetAccessToken());
            apicontext.Config = getconfig();
            return apicontext;
        }
    }
}