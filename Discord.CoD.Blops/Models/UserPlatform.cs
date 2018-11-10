namespace Discord.CoD.Blops.Models
{
    public class UserPlatform
    {
        public string PlatformKey { get; set; }
        public string UserPlatformID { get; set; }
        public string UserPlatformName { get; set; }
        public BlackoutStats BlackoutStats { get; set; }
        public MultiplayerStats MPStats { get; set; }

        public string UrlFriendlyName => UserPlatformName.Replace("#", "%23");
    }
}
