using static LiveScore.Models.Enums;

namespace LiveScore.Models
{
    public class Foul
    {
        public int FoulId { get; set; }

        // Foreign Key
        public int PlayerId { get; set; }

        // Navigation Property
        public Player Player { get; set; }
        public FoulType FoulType { get; set; }
        public Quarter Quarter { get; set; }
        public TimeSpan Time { get; set; }
    }


}
