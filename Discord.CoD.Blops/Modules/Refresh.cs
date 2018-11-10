using Discord.CoD.Blops.Helpers;
using Discord.CoD.Blops.Models;
using Discord.CoD.Blops.Models.Repositories;
using Discord.Commands;
using System.Linq;
using System.Threading.Tasks;

namespace Discord.CoD.Blops.Modules
{
    public class Refresh : ModuleBase<SocketCommandContext>
    {
        private IRepository<User> _repository;

        public Refresh(IRepository<User> repository)
        {
            _repository = repository;
        }

        [Command("refresh")]
        public async Task RefreshStats(string platformKey)
        {
            User currentUser = await _repository.GetOneByFilter(x => x.DiscordID, Context.Message.Author.Id);

            if (currentUser != null)
            {
                var userPlatform = currentUser.Platforms.Where(x => x.PlatformKey == platformKey).FirstOrDefault();

                if (userPlatform != null)
                {
                    userPlatform.SyncUserStats();

                    await _repository.Upsert(currentUser);

                    await ReplyAsync($"Your stats have been refreshed.");
                }

                else
                {
                    await ReplyAsync($"This account is not registered to that platform.");
                }
            }

            else
            {
                await ReplyAsync($"You are not registered yet!");
            }
        }
    }
}
