namespace LiveScore.Models
{
    public class Event
    {
        public int EventId { get; set; }
        public EventType Type { get; set; }
        public Player PlayerInvolved { get; set; }
        public int PointsScored { get; set; }
        public TimeSpan Time { get; set; }
        public FoulType? Foul { get; set; }

        // Foreign Key to Quarter
        public int QuarterId { get; set; }
        public Quarter Quarter { get; set; }
    }

}
