using Shared.DataRecords.Programmer;

namespace FakeDataSource.DataSources.Fakes;

public sealed class FakeDb : IDataSource
{
    public IEnumerable<Record> Programmers
    {
        get
        {
            return new[]
            {
                new Record(0, "Enrico"),
                new Record(2, "Alexander"),
                new Record(3, "Gianluca"),
                new Record(4, "Sergio"),
                new Record(5, "Tommaso")
            };
        }
    }
}