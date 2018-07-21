using Volleyball.Client.Enums;

namespace Volleyball.Client.Models
{
    public class Player
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public Team Team { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string CountryOfOrigin { get; set; }
        public int Age { get; set; }
        public Position Position { get; set; }
        public bool IsActive { get; set; }
    }
}
