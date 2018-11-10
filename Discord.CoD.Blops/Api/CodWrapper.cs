using Discord.CoD.Blops.Models.Api;

namespace Discord.CoD.Blops.Api
{
    public static class CodWrapper
    {
        // username, platform
        public const string UserValidateUrlFormat = "https://cod-api.theapinetwork.com/api/validate/bo4/{0}/{1}";

        // username, platform, type (multiplayer or blackout), user id
        public const string UserStatsUrlFormat = "https://cod-api.theapinetwork.com/api/stats/bo4/{0}/{1}?type={2}&u={3}";

        public static CodValidateUser ValidateUser(string username, string platform)
        {
            var client = new RestClient(string.Format(UserValidateUrlFormat, username, platform));

            return client.GetData<CodValidateUser>();
        }

        public static CodUserStats GetUserMPStats(string username, string platform, string userId, string type = null)
        {
            var client = new RestClient(string.Format(UserStatsUrlFormat, username, platform, type ?? string.Empty, userId));

            return client.GetData<CodUserStats>();
        }

        public static CodUserBlackoutStats GetUserBlackoutStats(string username, string platform, string userId, string type = null)
        {
            var client = new RestClient(string.Format(UserStatsUrlFormat, username, platform, type ?? string.Empty, userId));

            return client.GetData<CodUserBlackoutStats>();
        }



    }
}
