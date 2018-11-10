using Discord.CoD.Blops.Models;
using Discord.CoD.Blops.Models.Repositories;
using Discord.Commands;
using System.Threading.Tasks;

namespace Discord.CoD.Blops.Modules
{
    public class RegisterUpdatesChannel : ModuleBase<SocketCommandContext>
    {
        private IRepository<UpdateChannel> _repository;
       
        public RegisterUpdatesChannel(IRepository<UpdateChannel> repository)
        {
            _repository = repository;
        }

        [Command("register-updates-channel")]
        public async Task Register()
        {
            var currentChannel = await _repository.GetOneByFilter(x => x.ChannelId, Context.Channel.Id);

            if (currentChannel == null)
            {
                currentChannel = new UpdateChannel
                {
                    Name = Context.Channel.Name,
                    GuildId = Context.Guild.Id,
                    ChannelId = Context.Channel.Id
                };

                await _repository.Upsert(currentChannel);
                await ReplyAsync($"{currentChannel.Name} Channel Successfully Registered!");
            }

            else
            {
                await ReplyAsync($"This channel has already been registered!");
            }
        }
    }
}
