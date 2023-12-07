namespace FakeDataSource;

public interface IFakeDataSource
{
    public string[] Programmers { get; }
}

public sealed class FakeDb : IFakeDataSource
{
    public string[] Programmers => new []
    {
        "Gianluca",
        "Enrico",
        "Alexander",
        "Tommaso",
        "Sergio"
    };
}