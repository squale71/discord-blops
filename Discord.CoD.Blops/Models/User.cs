namespace Discord.CoD.Blops.Models
{
    public class User
    {
        public int ID { get; set; }
        public string DiscordID { get; set; }
        public string DiscordName { get; set; }        
        public Platform Platform { get; set; }
        public string PlatformID { get; set; }
    }
}
