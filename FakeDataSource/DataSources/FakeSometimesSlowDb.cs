using FakeDataSource.Data;
using Shared.DataRecords.Programmer;

namespace FakeDataSource.DataSources;

public sealed class FakeSometimesSlowDb : DataPool, IDataSource
{
    private readonly Random _random = new();

    public IEnumerable<Record> Programmers
    {
        get
        {
            if (ImBusy())
            {
                Thread.Sleep(10000);
            }

            return _programmers;
        }
    }

    private bool ImBusy()
    {
        return _random.Next(0, 10) > 5;
    }
}