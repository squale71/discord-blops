namespace Discord.CoD.Blops.Models.Api
{
    public class CodStats
    {
        public int Level { get; set; }
        public int MaxLevel { get; set; }
        public int Prestige { get; set; }
        public int PrestigeId { get; set; }
        public int MaxPrestige { get; set; }
        public int Kills { get; set; }
        public int KillsConfirmed { get; set; }
        public int Deaths { get; set; }
        public int GamesPlayed { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Melee { get; set; }
        public int Hits { get; set; }
        public int Misses { get; set; }
        public int RankXp { get; set; }
        public int CareerScore { get; set; }
        public int TotalHeals { get; set; }
        public int EKIA { get; set; }
        public int LongestKillStreak { get; set; }
        public int CurWinStreak { get; set; }
        public int TotalShots { get; set; }
        public int TeamKills { get; set; }
        public int Suicides { get; set; }
        public int Offends { get; set; }
        public int KillsDenied { get; set; }
        public int Captures { get; set; }
        public int Defends { get; set; }
        public int TimePlayed { get; set; }
        public bool WeaponData { get; set; }
    }
}
