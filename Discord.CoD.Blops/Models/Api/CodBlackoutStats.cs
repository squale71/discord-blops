﻿using System.Collections.Generic;

namespace Discord.CoD.Blops.Models.Api
{
    public class CodBlackoutStats
    {
        public CodBlackoutData BlackoutExtra { get; set; }
        public int HighestKillGame { get; set; }
        public int HighestEKIAgame { get; set; }
        public int HighestTotalShots { get; set; }
        public int Level { get; set; }
        public int MaxLevel { get; set; }
        public int Prestige { get; set; }
        public int PrestigeId { get; set; }
        public int MaxPrestige { get; set; }
        public int LevelXpGained { get; set; }
        public int LevelXpRemainder { get; set; }
        public int Kills { get; set; }
        public int KillsConfirmed { get; set; }
        public int Deaths { get; set; }
        public int GamesPlayed { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Melee { get; set; }
        public int Hits { get; set; }
        public int Misses { get; set; }
        public int Rankxp { get; set; }
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
        public int Revives { get; set; }
        public int TimePlayed { get; set; }
        public bool WeaponData { get; set; }
        public bool GameModeData { get; set; }
        public bool MapData { get; set; }
    }
}