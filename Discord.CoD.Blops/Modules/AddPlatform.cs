using Discord.CoD.Blops.Models;
using Discord.CoD.Blops.Models.Repositories;
using Discord.Commands;
using System.Threading.Tasks;


namespace Discord.CoD.Blops.Modules
{
    public class AddPlatform : ModuleBase<SocketCommandContext>
    {
        private IRepository<Platform> _repository;

        public AddPlatform(IRepository<Platform> repository)
        {
            _repository = repository;
        }

        [Command("add platform")]
        public async Task AddPlatformAsync(string key, [Remainder]string name)
        {
            var currentPlatform = await _repository.GetOneByFilter(x => x.Key, key);

            if (currentPlatform == null)
            {
                currentPlatform = new Platform
                {
                    Name = name,
                    Key = key
                };

                await _repository.Upsert(currentPlatform);
                await ReplyAsync($"{name} Successfully Created");
            }

            else
            {
                await ReplyAsync($"That platform already exists!");
            }
        }
    }
}
