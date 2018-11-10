using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Discord.CoD.Blops.Models.Api
{
    public class CodMatchInfo
    {
        public int MatchDuration { get; set; }
        public string MatchType { get; set; }
        public string MatchMapId { get; set; }
        public string MatchMode { get; set; }
    }
}
