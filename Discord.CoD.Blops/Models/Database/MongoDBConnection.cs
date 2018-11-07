using Discord.CoD.Blops.App_Start;
using MongoDB.Driver;

namespace Discord.CoD.Blops.Models.Database
{
    public class MongoDbConnection : DbConnection
    {
        private MongoClient _client;


        public IMongoDatabase Context { get; }

        public MongoDbConnection()
        {
            _client = new MongoClient(Configuration.Instance.Get("DatabaseConnectionString"));
            Context = _client.GetDatabase(Configuration.Instance.Get("DatabaseName"));
        }

        // Doesn't actually have any way to dispose, but keeping this here for now.
        public override void Dispose()
        {
        }
    }
}
