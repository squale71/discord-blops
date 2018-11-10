using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Discord.CoD.Blops.Models
{
    public class Guild
    {
        [BsonId]
        public ObjectId _Id { get; set; } = ObjectId.GenerateNewId();
        public ulong GuildID { get; set; }
        public string Name { get; set; }
        public ulong UpdateChannelID { get; set; }
    }
}
