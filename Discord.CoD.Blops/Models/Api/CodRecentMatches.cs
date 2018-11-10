namespace Discord.CoD.Blops.Models.Api
{
    public class CodRecentMatches
    {
        public bool Success { get; set; }
        public int Rows { get; set; }
        public string Game { get; set; }
        public string Platform { get; set; }
        public CodMatchEntry[] Entries { get; set; }
    }
}
