using System;

namespace Volleyball.Client.Models
{
    [Serializable]
    public class League
    {
        public int LeagueId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
    }
}
