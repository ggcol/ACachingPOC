using FakeDataSource.Data;
using Shared;
using Shared.DataRecords.Programmer;

namespace FakeDataSource.DataSources;

public sealed class FakeSometimesSlowDb : IDataSource
{
    private readonly Random _random = new();

    public IEnumerable<ProgrammerRecord> Programmers
    {
        get
        {
            Thread.Sleep(ImBusy()
                ? Thresholds.DataSource.HeavyLatency
                : Thresholds.DataSource.NormalLatency);

            return DataPool.Programmers;
        }
    }

    private bool ImBusy()
    {
        return _random.Next(0, 10) > 5;
    }
}