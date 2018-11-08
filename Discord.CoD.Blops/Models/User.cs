using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Discord.CoD.Blops.Models
{
    public class User
    {
        [BsonId]
        public ObjectId _Id { get; set; } = ObjectId.GenerateNewId();

        public ulong DiscordID { get; set; }

        public string DiscordName { get; set; }

        public List<UserPlatform> Platforms { get; set; } = new List<UserPlatform>();
    }
}
