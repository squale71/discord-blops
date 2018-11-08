using Discord.CoD.Blops.Api;
using Discord.CoD.Blops.Models;
using Discord.CoD.Blops.Models.Api;
using Discord.CoD.Blops.Models.Repositories;
using Discord.Commands;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Discord.CoD.Blops.Modules
{
    
    public class RegisterPlatform : ModuleBase<SocketCommandContext>
    {
        private IRepository<User> _userRepository;
        private IRepository<Platform> _platformRepository;

        public RegisterPlatform(IRepository<User> userRepository, IRepository<Platform> platformRepository)
        {
            _userRepository = userRepository;
            _platformRepository = platformRepository;
        }

        [Command("register platform")]
        public async Task RegisterPlatformAsync(string platformKey, [Remainder]string platformUserName)
        {
            User currentUser = await _userRepository.GetOneByFilter(x => x.DiscordID, Context.Message.Author.Id);

            if (currentUser != null)
            {
                var platformToAdd = await _platformRepository.GetOneByFilter(x => x.Key, platformKey);

                if (platformToAdd == null)
                {
                    await ReplyAsync($"That platform doesn't exist.");
                }

                if (currentUser.Platforms.Select(x => x.PlatformKey).Contains(platformToAdd.Key))
                {
                    await ReplyAsync($"This account is already registered to that platform.");
                }

                else
                {
                    CoDValidateUser user = null;

                    try
                    {
                        string userName = platformUserName;

                        // HACK: Replace # with %23 for battle net names.
                        if (platformKey == "bnet")
                        {
                            userName = platformUserName.Replace("#", "%23");
                        }

                        user = CodWrapper.ValidateUser(userName, platformKey);
                    }

                    catch (WebException)
                    {
                        await ReplyAsync($"No user of that username exists on that platform.");
                    }

                    currentUser.Platforms.Add(new UserPlatform { PlatformKey = platformKey, UserPlatformID = user.Id.ToString(), UserPlatformName = user.Username });

                    await _userRepository.Upsert(currentUser);

                    await ReplyAsync($"Your discord account is now registered on that platform.");
                }
            }

            else
            {
                await ReplyAsync($"You need to register first! Use the /blops register command");
            }
        }
    }
}
