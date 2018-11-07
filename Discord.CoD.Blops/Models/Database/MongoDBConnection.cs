using Discord.CoD.Blops.App_Start;
using MongoDB.Driver;

namespace Discord.CoD.Blops.Models.Database
{
    public class MongoDbConnection : DbConnection
    {
        private MongoClient _client;
        private IMongoDatabase _db;

        public MongoDbConnection()
        {
            _client = new MongoClient(Configuration.Instance.GetConnectionString("MongoDB"));
            _db = _client.GetDatabase(Configuration.Instance.Get("DatabaseName"));
        }

        public override void Dispose()
        {
        }
    }
}
