using System.Collections.Generic;

namespace Discord.CoD.Blops.Models.Api
{
    public class CodMatchEntry
    {
        public string Mid { get; set; }
        public int UtcStart { get; set; }
        public int UtcEnd { get; set; }
        public CodMatchInfo MatchInfo { get; set; }
        public CodTeams Teams { get; set; }
        public Dictionary<string, long>[] PlayerEntries { get; set; }
    }
}
