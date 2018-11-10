using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Discord.CoD.Blops.Models.Database;
using MongoDB.Driver;

namespace Discord.CoD.Blops.Models.Repositories
{
    public class GuildRepository : IRepository<Guild>
    {
        public async Task Delete(Guild item)
        {
            using (var db = new MongoDbConnection())
            {
                var collection = db.Context.GetCollection<Guild>("guilds");

                var filterDefinition = new FilterDefinitionBuilder<Guild>().Eq(x => x._Id, item._Id);
                var result = await collection.DeleteOneAsync(filterDefinition);
            }
        }

        public async Task<IEnumerable<Guild>> GetAll()
        {
            List<Guild> guilds = new List<Guild>();

            using (var db = new MongoDbConnection())
            {
                var collection = db.Context.GetCollection<Guild>("guilds");

                using (IAsyncCursor<Guild> cursor = await collection.FindAsync(x => true))
                {
                    while (await cursor.MoveNextAsync())
                    {
                        guilds.Add(cursor.Current.FirstOrDefault());
                    }
                }
            }

            return guilds;
        }

        public async Task<IEnumerable<Guild>> GetManyByFilter<t>(Expression<Func<Guild, t>> filter, IEnumerable<t> value)
        {
            List<Guild> guilds = new List<Guild>();

            using (var db = new MongoDbConnection())
            {
                var collection = db.Context.GetCollection<Guild>("guilds");

                Guild currentPlatform = new Guild();

                var filterDefinition = new FilterDefinitionBuilder<Guild>().In(filter, value);
                using (IAsyncCursor<Guild> cursor = await collection.FindAsync<Guild>(filterDefinition))
                {
                    while (await cursor.MoveNextAsync())
                    {
                        currentPlatform = cursor.Current.FirstOrDefault();

                        guilds.Add(currentPlatform);
                    }
                }
            }

            return guilds;
        }

        public async Task<Guild> GetOneByFilter(Expression<Func<Guild, object>> filter, object value)
        {
            using (var db = new MongoDbConnection())
            {
                var collection = db.Context.GetCollection<Guild>("guilds");

                Guild currentPlatform = new Guild();

                var filterDefinition = new FilterDefinitionBuilder<Guild>().Eq(filter, value);
                using (IAsyncCursor<Guild> cursor = await collection.FindAsync<Guild>(filterDefinition))
                {
                    while (await cursor.MoveNextAsync())
                    {
                        currentPlatform = cursor.Current.FirstOrDefault();
                    }
                }

                return currentPlatform;
            }
        }

        public async Task Upsert(Guild item)
        {
            using (var db = new MongoDbConnection())
            {
                var collection = db.Context.GetCollection<Guild>("guilds");

                var filterDefinition = new FilterDefinitionBuilder<Guild>().Eq(x => x._Id, item._Id);
                var result = await collection.ReplaceOneAsync(filterDefinition, item, new UpdateOptions { IsUpsert = true });
            }
        }
    }
}
