using System;
using System.Collections.Generic;
using System.Text;

namespace Discord.CoD.Blops.Models
{
    public class MultiplayerStats
    {
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Kills { get; set; }
        public int KillsConfirmed { get; set; }
        public int Deaths { get; set; }
        public int GamesPlayed { get; set; }
        public int EKIA { get; set; }
        public int LongestKillStreak { get; set; }
        public int TimePlayed { get; set; }
    }
}
