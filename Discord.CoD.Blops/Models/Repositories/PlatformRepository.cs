using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Discord.CoD.Blops.Models.Database;
using MongoDB.Driver;

namespace Discord.CoD.Blops.Models.Repositories
{
    public class PlatformRepository : IRepository<Platform>
    {
        public async Task Upsert(Platform item)
        {
            using (var db = new MongoDbConnection())
            {
                var collection = db.Context.GetCollection<Platform>("platforms");

                var filterDefinition = new FilterDefinitionBuilder<Platform>().Eq(x => x._Id, item._Id);
                var result = await collection.ReplaceOneAsync(filterDefinition, item, new UpdateOptions { IsUpsert = true });
            }
        }

        public async Task Delete(Platform item)
        {
            using (var db = new MongoDbConnection())
            {
                var collection = db.Context.GetCollection<Platform>("platforms");

                var filterDefinition = new FilterDefinitionBuilder<Platform>().Eq(x => x._Id, item._Id);
                var result = await collection.DeleteOneAsync(filterDefinition);
            }
        }

        public async Task<IEnumerable<Platform>> GetManyByFilter(Expression<Func<Platform, object>> filter, object value)
        {
            List<Platform> platforms = new List<Platform>();

            using (var db = new MongoDbConnection())
            {
                var collection = db.Context.GetCollection<Platform>("platforms");

                Platform currentPlatform = new Platform();

                var filterDefinition = new FilterDefinitionBuilder<Platform>().Eq(filter, value);
                using (IAsyncCursor<Platform> cursor = await collection.FindAsync<Platform>(filterDefinition))
                {
                    while (await cursor.MoveNextAsync())
                    {
                        currentPlatform = cursor.Current.FirstOrDefault();

                        platforms.Add(currentPlatform);
                    }
                }
            }

            return platforms;
        }

        public async Task<Platform> GetOneByFilter(Expression<Func<Platform, object>> filter, object value)
        {
            using (var db = new MongoDbConnection())
            {
                var collection = db.Context.GetCollection<Platform>("platforms");

                Platform currentPlatform = new Platform();

                var filterDefinition = new FilterDefinitionBuilder<Platform>().Eq(filter, value);
                using (IAsyncCursor<Platform> cursor = await collection.FindAsync<Platform>(filterDefinition))
                {
                    while (await cursor.MoveNextAsync())
                    {
                        currentPlatform = cursor.Current.FirstOrDefault();
                    }
                }

                return currentPlatform;
            }
        }
    }
}
