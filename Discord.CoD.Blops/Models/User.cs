using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Discord.CoD.Blops.Models
{
    public class User
    {
        [BsonId]
        public ObjectId _Id { get; set; }

        public string DiscordID { get; set; }

        public string DiscordName { get; set; }   
        
        public Platform Platform { get; set; }
    }
}
