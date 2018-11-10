using Discord.CoD.Blops.Api;
using Discord.CoD.Blops.Models;

namespace Discord.CoD.Blops.Helpers
{
    public static class SyncHelper
    {
        public static void SyncUserStats(this UserPlatform user)
        {
            var blackoutStats = CodWrapper.GetUserBlackoutStats(user.UrlFriendlyName, user.PlatformKey, user.UserPlatformID, "blackout");

            user.BlackoutStats = new BlackoutStats
            {
                Kills = blackoutStats.Stats.Kills,
                KillsConfirmed = blackoutStats.Stats.KillsConfirmed,
                Deaths = blackoutStats.Stats.Deaths,
                GamesPlayed = blackoutStats.Stats.GamesPlayed,
                Wins = blackoutStats.Stats.Wins,
                Losses = blackoutStats.Stats.Losses,
                TimePlayed = blackoutStats.Stats.TimePlayed,
                Top5PlacementTeam = blackoutStats.Stats.BlackoutExtra.Top5placementsolo,
                Top10PlacementTeam = blackoutStats.Stats.BlackoutExtra.Top10placementteam,
                Top5PlacementSolo = blackoutStats.Stats.BlackoutExtra.Top5placementsolo,
                Top25PlacementSolo = blackoutStats.Stats.BlackoutExtra.Top25placementsolo,
                MostKillsInAGame = blackoutStats.Stats.BlackoutExtra.Mostkillsinagame
            };

            var mpStats = CodWrapper.GetUserMPStats(user.UrlFriendlyName, user.PlatformKey, user.UserPlatformID, "multiplayer");

            user.MPStats = new MultiplayerStats
            {
                Kills = mpStats.Stats.Kills,
                KillsConfirmed = mpStats.Stats.KillsConfirmed,
                Deaths = mpStats.Stats.Deaths,
                GamesPlayed = mpStats.Stats.GamesPlayed,
                Wins = mpStats.Stats.Wins,
                Losses = mpStats.Stats.Losses,
                TimePlayed = mpStats.Stats.TimePlayed,
                EKIA = mpStats.Stats.EKIA,
                LongestKillStreak = mpStats.Stats.LongestKillStreak
            };
        }

        public static bool SyncUserBlackoutStats(this UserPlatform user)
        {
            var latestBlackoutStats = CodWrapper.GetUserBlackoutStats(user.UrlFriendlyName, user.PlatformKey, user.UserPlatformID, "blackout");

            var totalGames = latestBlackoutStats.Stats.GamesPlayed;
            var localTotalGames = user.BlackoutStats.GamesPlayed;

            // Do stuff since total games are now different.
            if (totalGames != localTotalGames)
            {
                user.BlackoutStats = new BlackoutStats
                {
                    Kills = latestBlackoutStats.Stats.Kills,
                    KillsConfirmed = latestBlackoutStats.Stats.KillsConfirmed,
                    Deaths = latestBlackoutStats.Stats.Deaths,
                    GamesPlayed = totalGames,
                    Wins = latestBlackoutStats.Stats.Wins,
                    Losses = latestBlackoutStats.Stats.Losses,
                    TimePlayed = latestBlackoutStats.Stats.TimePlayed,
                    Top5PlacementTeam = latestBlackoutStats.Stats.BlackoutExtra.Top5placementsolo,
                    Top10PlacementTeam = latestBlackoutStats.Stats.BlackoutExtra.Top10placementteam,
                    Top5PlacementSolo = latestBlackoutStats.Stats.BlackoutExtra.Top5placementsolo,
                    Top25PlacementSolo = latestBlackoutStats.Stats.BlackoutExtra.Top25placementsolo,
                    MostKillsInAGame = latestBlackoutStats.Stats.BlackoutExtra.Mostkillsinagame
                };

                return true;
            }

            return false;
        }

        public static bool SyncUserMPStats(this UserPlatform user)
        {
            var mpStats = CodWrapper.GetUserMPStats(user.UrlFriendlyName, user.PlatformKey, user.UserPlatformID, "multiplayer");

            var totalGames = mpStats.Stats.GamesPlayed;
            var localTotalGames = user.MPStats.GamesPlayed;

            // Do stuff since total games are now different.
            if (totalGames != localTotalGames)
            {
                user.MPStats = new MultiplayerStats
                {
                    Kills = mpStats.Stats.Kills,
                    KillsConfirmed = mpStats.Stats.KillsConfirmed,
                    Deaths = mpStats.Stats.Deaths,
                    GamesPlayed = totalGames,
                    Wins = mpStats.Stats.Wins,
                    Losses = mpStats.Stats.Losses,
                    TimePlayed = mpStats.Stats.TimePlayed,
                    EKIA = mpStats.Stats.EKIA,
                    LongestKillStreak = mpStats.Stats.LongestKillStreak
                };

                return true;
            }

            return false;
        }
    }
}
