using Discord.CoD.Blops.Models.Api;

namespace Discord.CoD.Blops.Api
{
    public static class CodWrapper
    {
        // username, platform
        public const string UserValidateUrlFormat = "https://cod-api.theapinetwork.com/api/validate/bo4/{0}/{1}";

        public static CoDValidateUser ValidateUser(string username, string platform)
        {
            var client = new RestClient(string.Format(UserValidateUrlFormat, username, platform));

            return client.GetData<CoDValidateUser>();
        }

    }
}
