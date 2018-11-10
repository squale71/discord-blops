using Discord.CoD.Blops.Helpers;
using Discord.CoD.Blops.Models;
using Discord.CoD.Blops.Models.Repositories;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discord.CoD.Blops.Api
{
    public class RequestPoller
    {
        private IRepository<Guild> _guildRepository;
        private IRepository<User> _userRepository;
        DiscordSocketClient _client;

        /// <summary>
        /// Polls services for changes
        /// </summary>
        /// <param name="client"></param>
        /// <param name="guildRepository"></param>
        /// <param name="userRepository"></param>
        public RequestPoller(DiscordSocketClient client, IRepository<Guild> guildRepository, IRepository<User> userRepository)
        {
            _guildRepository = guildRepository;
            _userRepository = userRepository;
            _client = client;
        }

        /// <summary>
        /// Polls the CoD API service to see if there are any changes to the user stats, if so, broadcast delta of those changes.
        /// </summary>
        /// <returns></returns>
        public async Task PollUsersAsync()
        {
            while (true)
            {
                var guilds = await _guildRepository.GetAll();

                if (guilds == null || !guilds.Any(x => x != null))
                {
                    continue;
                }

                foreach (var guild in guilds)
                {
                    var channelToUpdate = _client.GetChannel(guild.UpdateChannelID) as IMessageChannel;

                    var discordGuild = _client.GetGuild(guild.GuildID);
                    var onlineUsers = discordGuild.Users.Where(x => !x.IsBot && x.Status == UserStatus.Online && (x.Game?.ToString().ToLower() == "call of duty black ops 4" || x.Game?.ToString().ToLower() == "call of duty black ops IV"));

                    var discordUserIDs = onlineUsers.Select(x => x.Id);

                    if (discordUserIDs.Any())
                    {
                        var users = await _userRepository.GetManyByFilter<ulong>(x => x.DiscordID, discordUserIDs);

                        Console.WriteLine($"Users Online: {string.Join(", ", users.Select(x => x.DiscordName))}");

                        // Check for nulls as the repo may return it in the set above
                        foreach (var user in users.Where(x => x != null))
                        {
                            Console.WriteLine($"Checking {user.DiscordName}");
                            foreach (var userPlatform in user.Platforms)
                            {
                                bool hasChanged = false;

                                var oldBoStats = new BlackoutStats()
                                {
                                    Kills = userPlatform.BlackoutStats.Kills,
                                    Deaths = userPlatform.BlackoutStats.Deaths,
                                    Wins = userPlatform.BlackoutStats.Wins,
                                    Losses = userPlatform.BlackoutStats.Losses,
                                    GamesPlayed = userPlatform.BlackoutStats.GamesPlayed
                                };

                                var oldMPStats = new MultiplayerStats()
                                {
                                    Kills = userPlatform.MPStats.Kills,
                                    Deaths = userPlatform.MPStats.Deaths,
                                    Wins = userPlatform.MPStats.Wins,
                                    Losses = userPlatform.MPStats.Losses,
                                    GamesPlayed = userPlatform.MPStats.GamesPlayed,
                                    EKIA = userPlatform.MPStats.EKIA
                                };

                                if (SyncHelper.SyncUserBlackoutStats(userPlatform))
                                {
                                    hasChanged = true;

                                    var killsDelta = userPlatform.BlackoutStats.Kills - oldBoStats.Kills;
                                    var deathsDelta = userPlatform.BlackoutStats.Deaths - oldBoStats.Deaths;
                                    var winsDelta = userPlatform.BlackoutStats.Wins - oldBoStats.Wins;
                                    var lossDelta = userPlatform.BlackoutStats.Losses - oldBoStats.Losses;
                                    var gamesPlayedDelta = userPlatform.BlackoutStats.GamesPlayed - oldBoStats.GamesPlayed;
                                  
                                    var kd = deathsDelta == 0 ? (decimal)killsDelta : ((decimal)killsDelta / (decimal)deathsDelta);

                                    var builder = new EmbedBuilder();

                                    if (gamesPlayedDelta == 1)
                                    {
                                        string winLossValue = "";
                                        if (winsDelta > 0)
                                        {
                                            winLossValue = "WINNER WINNER CHICKEN DINNER!";
                                        }

                                        else if (lossDelta > 0)
                                        {
                                            winLossValue = "LOSER!";
                                        }

                                        builder.WithTitle($"{winLossValue}: Last Blackout Match Stats for {userPlatform.UserPlatformName}");

                                        builder.AddInlineField("Kills", killsDelta);
                                        builder.AddInlineField("Deaths", deathsDelta);
                                        builder.AddInlineField("K/D Ratio", Math.Round(kd, 2));
                                        builder.WithThumbnailUrl("https://i.imgur.com/O6UTdza.jpg");
                                    }

                                    else
                                    {
                                        builder.WithTitle($"Last {gamesPlayedDelta} Blackout Match Results for {userPlatform.UserPlatformName}");

                                        builder.AddInlineField("Kills", killsDelta);
                                        builder.AddInlineField("Deaths", deathsDelta);
                                        builder.AddInlineField("K/D Ratio", Math.Round(kd, 2));
                                        builder.AddInlineField("Wins", winsDelta);
                                        builder.AddInlineField("Losses", lossDelta);
                                        builder.WithThumbnailUrl("https://i.imgur.com/O6UTdza.jpg");
                                    }
                                    

                                    await channelToUpdate.SendMessageAsync("", false, builder);

                                }

                                if (SyncHelper.SyncUserMPStats(userPlatform))
                                {
                                    hasChanged = true;
                                   
                                    var killsDelta = userPlatform.MPStats.EKIA - oldMPStats.EKIA;
                                    var deathsDelta = userPlatform.MPStats.Deaths - oldMPStats.Deaths;
                                    var winsDelta = userPlatform.MPStats.Wins - oldMPStats.Wins;
                                    var lossDelta = userPlatform.MPStats.Losses - oldMPStats.Losses;
                                    var gamesPlayedDelta = userPlatform.MPStats.GamesPlayed - oldMPStats.GamesPlayed;

                                    var kd = deathsDelta == 0 ? (decimal)killsDelta : ((decimal)killsDelta / (decimal)deathsDelta);

                                    var builder = new EmbedBuilder();

                                    if (gamesPlayedDelta == 1)
                                    {
                                        string winLossValue = "";
                                        if (winsDelta > 0)
                                        {
                                            winLossValue = "WINNER!";
                                        }

                                        else if (lossDelta > 0)
                                        {
                                            winLossValue = "LOSER!";
                                        }

                                        builder.WithTitle($"{winLossValue}: Last Multiplayer Match Stats for {userPlatform.UserPlatformName}");

                                        builder.AddInlineField("EKIA", killsDelta);
                                        builder.AddInlineField("Deaths", deathsDelta);
                                        builder.AddInlineField("Ratio", Math.Round(kd, 2));
                                        builder.WithThumbnailUrl("https://i.imgur.com/O6UTdza.jpg");

                                    }
                                    
                                    else
                                    {
                                        builder.WithTitle($"Last {gamesPlayedDelta} Multiplayer Match Results for {userPlatform.UserPlatformName}");

                                        builder.AddInlineField("EKIA", killsDelta);
                                        builder.AddInlineField("Deaths", deathsDelta);
                                        builder.AddInlineField("Ratio", Math.Round(kd, 2));
                                        builder.AddInlineField("Wins", winsDelta);
                                        builder.AddInlineField("Losses", lossDelta);
                                        builder.WithThumbnailUrl("https://i.imgur.com/O6UTdza.jpg");
                                    }

                                    await channelToUpdate.SendMessageAsync("", false, builder);
                                }

                                // If we get here, we know something has changed. Update db.
                                if (hasChanged)
                                {
                                    await _userRepository.Upsert(user);
                                }

                                // TODO: Remove.
                                var liveBOStats = CodWrapper.GetUserBlackoutStats(userPlatform.UrlFriendlyName, userPlatform.PlatformKey, userPlatform.UserPlatformID, "blackout");
                                var liveMPStats = CodWrapper.GetUserMPStats(userPlatform.UrlFriendlyName, userPlatform.PlatformKey, userPlatform.UserPlatformID, "multiplayer");

                                Console.WriteLine($"{userPlatform.UserPlatformName} BO Games Played LIVE/LOCAL: {liveBOStats.Stats.GamesPlayed}/{userPlatform.BlackoutStats.GamesPlayed}");
                                Console.WriteLine($"{userPlatform.UserPlatformName} MP Games Played: {liveMPStats.Stats.GamesPlayed}/{userPlatform.MPStats.GamesPlayed}");
                            }  
                        } 
                    }
                }

                await Task.Delay(20 * 1000);
            }
        }
    }
}
