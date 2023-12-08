using FakeDataSource.Data;
using Shared;
using Shared.DataRecords.Programmer;

namespace FakeDataSource.DataSources;

public sealed class FakeDb : IDataSource
{
    public IEnumerable<ProgrammerRecord> Programmers
    {
        get
        {
            //very minimal delay simulating network latency, database access, querying, etc.
            Thread.Sleep(Thresholds.DataSource.NormalLatency);
            return DataPool.Programmers;
        }
    }
}