using System.Collections.Generic;

namespace Discord.CoD.Blops.Models.Api
{
    public class CodUserStats
    {
        public string Identifier { get; set; }
        public CodUser User { get; set; }
        public CodCache Cache { get; set; }
        public CodStats Stats { get; set; }
        public List<CodMatch> Matches { get; set; }
        public List<CodWeaponData> WeaponData { get; set; }
    }

    public class CodUserBlackoutStats
    {
        public string Identifier { get; set; }
        public CodUser User { get; set; }
        public CodCache Cache { get; set; }
        public CodBlackoutStats Stats { get; set; }
        public List<CodMatch> Matches { get; set; }
        public List<CodWeaponData> WeaponData { get; set; }
    }
}
