using Shared.DataRecords.Programmer;

namespace FakeDataSource.Data;

public abstract class DataPool
{
    protected IEnumerable<Record> _programmers => new[]
    {
        new Record(0, "Enrico"),
        new Record(2, "Alexander"),
        new Record(3, "Gianluca"),
        new Record(4, "Sergio"),
        new Record(5, "Tommaso")
    };
}