using FakeDataSource.Data;
using Shared.DataRecords.Programmer;

namespace FakeDataSource.DataSources;

public sealed class FakeDb : DataPool, IDataSource
{
    public IEnumerable<Record> Programmers
    {
        get
        {
            //very minimal delay simulating network latency, database access, etc.
            Thread.Sleep(150);
            return _programmers;
        }
    }
}