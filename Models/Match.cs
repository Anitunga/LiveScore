namespace LiveScore.Models
{
    public class Match
    {
        public int MatchId { get; set; }
        public DateTime Date { get; set; }
        // Foreign Keys
        public int TeamAId { get; set; }
        public int TeamBId { get; set; }
        // Navigation Properties
        public Team TeamA { get; set; }
        public Team TeamB { get; set; }

        public int TotalQuarters { get; set; }
        public int QuarterDuration { get; set; } // in seconds
        public int TimeoutDuration { get; set; } // in seconds
        public ICollection<Quarter> Quarters { get; set; }

        // Foreign Key for User encoding the game
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
