using FakeDataSource.Data;
using Shared;
using Shared.DataRecords.Programmer;

namespace FakeDataSource.DataSources;

public sealed class FakeSlowDb : IDataSource
{
    public IEnumerable<ProgrammerRecord> Programmers
    {
        get
        {
            Thread.Sleep(Thresholds.DataSource.HeavyLatency);
            return DataPool.Programmers;
        }
    }
}