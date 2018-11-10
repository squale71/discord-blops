using System;
using System.Collections.Generic;
using System.Text;

namespace Discord.CoD.Blops.Models.Api
{
    public class CodUser
    {
        public string ID { get; set; }
        public string Username { get; set; }
        public string Platform { get; set; }
        public string Title { get; set; }
        public string Avatar { get; set; }
    }
}
