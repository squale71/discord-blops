using Discord.CoD.Blops.Models;
using Discord.CoD.Blops.Models.Repositories;
using Discord.Commands;
using System.Threading.Tasks;

namespace Discord.CoD.Blops.Modules
{
    public class RegisterGuild : ModuleBase<SocketCommandContext>
    {
        private IRepository<Guild> _repository;
       
        public RegisterGuild(IRepository<Guild> repository)
        {
            _repository = repository;
        }

        [Command("register-server")]
        public async Task Register()
        {
            var currentGuild = await _repository.GetOneByFilter(x => x.GuildID, Context.Guild.Id);

            if (currentGuild == null)
            {
                currentGuild = new Guild
                {
                    Name = Context.Guild.Name,
                    GuildID = Context.Guild.Id,
                    UpdateChannelID = Context.Channel.Id
                };

                await _repository.Upsert(currentGuild);
                await ReplyAsync($"{currentGuild.Name} Server Successfully Registered!");
            }

            else
            {
                await ReplyAsync($"This server has already been registered!");
            }
        }
    }
}
