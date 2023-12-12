namespace Shared;

public abstract class Routes
{
    public abstract class Health
    {
        public const string Ping = "Ping";
    }
    
    public abstract class Programmer
    {
        public const string Get = "Get";
        public const string GetAll = "GetAll";
        public const string WickedGet = "WickedGet";
    }
}