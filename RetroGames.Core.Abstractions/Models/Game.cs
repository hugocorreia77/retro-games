namespace RetroGames.Core.Abstractions.Models
{
    public class Game
    {
        public Guid GameId { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public Guid ProviderId { get; set; }
    }
}
