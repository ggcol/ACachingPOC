using Shared.DataRecords.Programmer;

namespace FakeDataSource;

public interface IDataSource
{
    public IEnumerable<Record> Programmers { get; }
}