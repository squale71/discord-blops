using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Discord.CoD.Blops.Models.Database;
using MongoDB.Driver;

namespace Discord.CoD.Blops.Models.Repositories
{
    public class UserRepository : IRepository<User>
    {
        public async Task Delete(User item)
        {
            using (var db = new MongoDbConnection())
            {
                var collection = db.Context.GetCollection<User>("users");

                var filterDefinition = new FilterDefinitionBuilder<User>().Eq(x => x._Id, item._Id);
                var result = await collection.DeleteOneAsync(filterDefinition);
            }
        }

        public async Task<IEnumerable<User>> GetManyByFilter(Expression<Func<User, object>> filter, object value)
        {
            List<User> users = new List<User>();

            using (var db = new MongoDbConnection())
            {
                var collection = db.Context.GetCollection<User>("users");

                User currentPlatform = new User();

                var filterDefinition = new FilterDefinitionBuilder<User>().Eq(filter, value);
                using (IAsyncCursor<User> cursor = await collection.FindAsync<User>(filterDefinition))
                {
                    while (await cursor.MoveNextAsync())
                    {
                        currentPlatform = cursor.Current.FirstOrDefault();

                        users.Add(currentPlatform);
                    }
                }
            }

            return users;
        }

        public async Task<User> GetOneByFilter(Expression<Func<User, object>> filter, object value)
        {
            using (var db = new MongoDbConnection())
            {
                var collection = db.Context.GetCollection<User>("users");

                User currentUser = new User();

                var filterDefinition = new FilterDefinitionBuilder<User>().Eq(filter, value);
                using (IAsyncCursor<User> cursor = await collection.FindAsync<User>(filterDefinition))
                {
                    while (await cursor.MoveNextAsync())
                    {
                        currentUser = cursor.Current.FirstOrDefault();
                    }
                }

                return currentUser;
            }
        }

        public async Task Upsert(User item)
        {
            using (var db = new MongoDbConnection())
            {
                var collection = db.Context.GetCollection<User>("users");

                var filterDefinition = new FilterDefinitionBuilder<User>().Eq(x => x._Id, item._Id);
                var result = await collection.ReplaceOneAsync(filterDefinition, item, new UpdateOptions { IsUpsert = true });
            }
        }
    }
}
