using Discord.Commands;
using System.Threading.Tasks;

namespace Discord.CoD.Blops.Modules
{
    public class Ping : ModuleBase<SocketCommandContext>
    {
        [Command("ping")]
        public async Task PingAsync()
        {
            await ReplyAsync("Hello World!");
        }
    }
}
