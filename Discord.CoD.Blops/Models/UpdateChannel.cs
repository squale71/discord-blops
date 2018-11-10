using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace Discord.CoD.Blops.Models
{
    public class UpdateChannel
    {
        [BsonId]
        public ObjectId _Id { get; set; } = ObjectId.GenerateNewId();
        public ulong ChannelId { get; set; }
        public ulong GuildId { get; set; }
        public string Name { get; set; }
    }
}
