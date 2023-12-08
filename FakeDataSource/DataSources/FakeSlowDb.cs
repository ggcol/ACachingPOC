using FakeDataSource.Data;
using Shared.DataRecords.Programmer;

namespace FakeDataSource.DataSources;

public sealed class FakeSlowDb : DataPool, IDataSource
{
    public IEnumerable<Record> Programmers
    {
        get
        {
            Thread.Sleep(10000);
            return _programmers;
        }
    }
}