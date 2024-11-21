namespace LiveScore.Models
{
    public class Substitution
    {
        public int SubstitutionId { get; set; }

        // Foreign Keys
        public int PlayerInId { get; set; }
        public int PlayerOutId { get; set; }

        // Navigation Properties
        public Player PlayerIn { get; set; }
        public Player PlayerOut { get; set; }

        public Quarter Quarter { get; set; }
        public TimeSpan Time { get; set; }
    }

}
