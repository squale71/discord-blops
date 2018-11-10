using System;

namespace Discord.CoD.Blops.Models.Api
{
    public class CodMatch
    {
        public string Identifier { get; set; }
        public int Kills { get; set; }
        public int Deaths { get; set; }
        public int EKIA { get; set; }
        public int GamesPlayed { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int TotalShots { get; set; }
        public int Captures { get; set; }
        public int Defends { get; set; }
        public int CareerScore { get; set; }
        public int TimePlayed { get; set; }
        public int RankXp { get; set; }
        public int Time { get; set; }
        public DateTime? Format { get; set; }
    }
}
