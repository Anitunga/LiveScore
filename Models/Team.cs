namespace LiveScore.Models
{
    public class Team
    {
        public int TeamId { get; set; }
        public string Name { get; set; }
        public ICollection<Player> Players { get; set; }

        public int CoachId { get; set; }
        public Coach Coach { get; set; }
    }
}
