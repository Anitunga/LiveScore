namespace LiveScore.Models
{
    public class Enums
    {
    }

    public enum EventType
    {
        Score,
        Foul,
        Timeout,
        Substitution
    }

    public enum Position
    {
        PointGuard,
        ShootingGuard,
        SmallForward,
        PowerForward,
        Center
    }

    public enum FoulType
    {
        Personal,
        Technical,
        Flagrant
    }
}
