namespace Discord.CoD.Blops.Models.Api
{
    public class CodWeaponData
    {
        public string Identifier { get; set; }
        public string Name { get; set; }
        public int Kills { get; set; }
        public int Backstabber_Kill { get; set; }
        public int Deaths { get; set; }
        public int TimesUsed { get; set; }
        public int Used { get; set; }
        public int DeathsDuringUse { get; set; }
        public int Hits { get; set; }
        public int EKIA { get; set; }
        public int Destroyed { get; set; }
        public int Headshots { get; set; }
        public int Shots { get; set; }
        public int Assists { get; set; }
        public int DamageDone { get; set; }
    }
}
