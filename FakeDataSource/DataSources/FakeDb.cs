using FakeDataSource.Data;
using Shared.DataRecords.Programmer;

namespace FakeDataSource.DataSources;

public sealed class FakeDb : DataPool, IDataSource
{
    public IEnumerable<Record> Programmers => _programmers;
}