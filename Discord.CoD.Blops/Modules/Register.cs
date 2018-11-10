using Discord.CoD.Blops.Models;
using Discord.CoD.Blops.Models.Repositories;
using Discord.Commands;
using System.Threading.Tasks;

namespace Discord.CoD.Blops.Modules
{
    /// <summary>
    /// Registers a discord user to the database, and links them with an in game account.
    /// </summary>
    public class Register : ModuleBase<SocketCommandContext>
    {
        private IRepository<User> _repository;

        public Register(IRepository<User> repository)
        {
            _repository = repository;
        }

        [Command("register")]
        public async Task RegisterAsync()
        {
            User currentUser = await _repository.GetOneByFilter(x => x.DiscordID, Context.Message.Author.Id);

            if (currentUser == null)
            {
                currentUser = new User
                {
                    DiscordID = Context.Message.Author.Id,
                    DiscordName = Context.Message.Author.Username,
                    GuildID = Context.Guild.Id
                };

                await _repository.Upsert(currentUser);
                await ReplyAsync($"{currentUser.DiscordName} Successfully Registered!");
            }

            else
            {
                await ReplyAsync($"That user is already registered!");
            }
        }
    }
}
