using Shared.DataRecords.Programmer;

namespace FakeDataSource.DataSources.Fakes;

public sealed class FakeSlowDb : IDataSource
{
    public IEnumerable<Record> Programmers
    {
        get
        {
            Task.Delay(10000);
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