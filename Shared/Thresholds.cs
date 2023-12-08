namespace Shared;

public abstract class Thresholds
{
    public abstract class Programmer
    {
        public const int BurnoutThreshold = 1001;
    }

    public abstract class DataSource
    {
        public const int NormalLatency = 150;
        public const int WickedLatency = 1000;
        public const int HeavyLatency = 10000;
    }
}