using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Discord.CoD.Blops.Models
{
    public class Platform
    {
        [BsonId]
        public ObjectId _Id { get; set; }

        public string Name { get; set; }

        public string Key { get; set; }
    }
}
