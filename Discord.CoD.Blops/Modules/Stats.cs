using Discord.CoD.Blops.Api;
using Discord.CoD.Blops.Models;
using Discord.CoD.Blops.Models.Api;
using Discord.CoD.Blops.Models.Repositories;
using Discord.Commands;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Discord.CoD.Blops.Modules
{
    public class Stats : ModuleBase<SocketCommandContext>
    {
        private IRepository<User> _userRepository;
        private IRepository<Platform> _platformRepository;

        public Stats(IRepository<User> userRepository, IRepository<Platform> platformRepository)
        {
            _userRepository = userRepository;
            _platformRepository = platformRepository;
        }

        [Command("stats-mp")]
        public async Task GetStatsMP([Remainder]string platformKey)
        {
            User currentUser = await _userRepository.GetOneByFilter(x => x.DiscordID, Context.Message.Author.Id);

            if (currentUser != null)
            {
                if (!currentUser.Platforms.Select(x => x.PlatformKey == platformKey).Any())
                {
                    await ReplyAsync($"Your account is not registered with that platform!");
                }

                var platform = currentUser.Platforms.Where(x => x.PlatformKey == platformKey).Single();

                var data = CodWrapper.GetUserMPStats(platform.UrlFriendlyName, platform.PlatformKey, platform.UserPlatformID, "multiplayer");

                

                await ReplyAsync($"Not ready.");
                
            }

            else
            {
                await ReplyAsync($"You need to register before you can check your stats!");
            }
        }

        [Command("stats-blackout")]
        public async Task GetStatsBlackout([Remainder]string platformKey)
        {
            User currentUser = await _userRepository.GetOneByFilter(x => x.DiscordID, Context.Message.Author.Id);

            if (currentUser != null)
            {
                if (!currentUser.Platforms.Select(x => x.PlatformKey == platformKey).Any())
                {
                    await ReplyAsync($"Your account is not registered with that platform!");
                }

                var platform = currentUser.Platforms.Where(x => x.PlatformKey == platformKey).Single();

                var builder = new EmbedBuilder();

                var kd = ((decimal)platform.BlackoutStats.Kills / (decimal)platform.BlackoutStats.Deaths);

                builder.WithTitle($"{platform.UserPlatformName} Blackout Stats");
                builder.AddInlineField("Wins", platform.BlackoutStats.Wins);
                builder.AddInlineField("Losses", platform.BlackoutStats.Losses);
                builder.AddInlineField("Kills", platform.BlackoutStats.Kills);
                builder.AddInlineField("Deaths", platform.BlackoutStats.Deaths);
                builder.AddInlineField("K/D Ratio", Math.Round(kd, 2));
                builder.WithThumbnailUrl("https://i.imgur.com/O6UTdza.jpg");

                await Context.Channel.SendMessageAsync("", false, builder);
            }

            else
            {
                await ReplyAsync($"You need to register before you can check your stats!");
            }
        }
    }
}
