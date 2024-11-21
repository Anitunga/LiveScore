namespace LiveScore.Models
{
    public class Quarter
    {
        public int QuarterId { get; set; }
        public int Number { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int TeamAScore { get; set; }
        public int TeamBScore { get; set; }
        public ICollection<Event> Events { get; set; }
    }
}