namespace LiveScore.Models
{
    public class Player
    {
        public int PlayerId { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public Position Position { get; set; }
        public bool IsStartingPlayer { get; set; }

        // Foreign Key
        public int TeamId { get; set; }
        public Team Team { get; set; }  // Navigation Property
    }

}
