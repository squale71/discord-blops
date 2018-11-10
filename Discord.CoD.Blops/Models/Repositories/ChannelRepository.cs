using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Discord.CoD.Blops.Models.Database;
using MongoDB.Driver;

namespace Discord.CoD.Blops.Models.Repositories
{
    public class ChannelRepository : IRepository<UpdateChannel>
    {
        public async Task Delete(UpdateChannel item)
        {
            using (var db = new MongoDbConnection())
            {
                var collection = db.Context.GetCollection<UpdateChannel>("updatechannels");

                var filterDefinition = new FilterDefinitionBuilder<UpdateChannel>().Eq(x => x._Id, item._Id);
                var result = await collection.DeleteOneAsync(filterDefinition);
            }
        }

        public async Task<IEnumerable<UpdateChannel>> GetAll()
        {
            List<UpdateChannel> UpdateChannels = new List<UpdateChannel>();

            using (var db = new MongoDbConnection())
            {
                var collection = db.Context.GetCollection<UpdateChannel>("updatechannels");

                using (IAsyncCursor<UpdateChannel> cursor = await collection.FindAsync(x => true))
                {
                    while (await cursor.MoveNextAsync())
                    {
                        UpdateChannels.Add(cursor.Current.FirstOrDefault());
                    }
                }
            }

            return UpdateChannels;
        }

        public async Task<IEnumerable<UpdateChannel>> GetManyByFilter<t>(Expression<Func<UpdateChannel, t>> filter, IEnumerable<t> value)
        {
            List<UpdateChannel> UpdateChannels = new List<UpdateChannel>();

            using (var db = new MongoDbConnection())
            {
                var collection = db.Context.GetCollection<UpdateChannel>("updatechannels");

                UpdateChannel currentPlatform = new UpdateChannel();

                var filterDefinition = new FilterDefinitionBuilder<UpdateChannel>().In(filter, value);
                using (IAsyncCursor<UpdateChannel> cursor = await collection.FindAsync<UpdateChannel>(filterDefinition))
                {
                    while (await cursor.MoveNextAsync())
                    {
                        currentPlatform = cursor.Current.FirstOrDefault();

                        UpdateChannels.Add(currentPlatform);
                    }
                }
            }

            return UpdateChannels;
        }

        public async Task<UpdateChannel> GetOneByFilter(Expression<Func<UpdateChannel, object>> filter, object value)
        {
            using (var db = new MongoDbConnection())
            {
                var collection = db.Context.GetCollection<UpdateChannel>("updatechannels");

                UpdateChannel currentPlatform = new UpdateChannel();

                var filterDefinition = new FilterDefinitionBuilder<UpdateChannel>().Eq(filter, value);
                using (IAsyncCursor<UpdateChannel> cursor = await collection.FindAsync<UpdateChannel>(filterDefinition))
                {
                    while (await cursor.MoveNextAsync())
                    {
                        currentPlatform = cursor.Current.FirstOrDefault();
                    }
                }

                return currentPlatform;
            }
        }

        public async Task Upsert(UpdateChannel item)
        {
            using (var db = new MongoDbConnection())
            {
                var collection = db.Context.GetCollection<UpdateChannel>("UpdateChannels");

                var filterDefinition = new FilterDefinitionBuilder<UpdateChannel>().Eq(x => x._Id, item._Id);
                var result = await collection.ReplaceOneAsync(filterDefinition, item, new UpdateOptions { IsUpsert = true });
            }
        }
    }
}
