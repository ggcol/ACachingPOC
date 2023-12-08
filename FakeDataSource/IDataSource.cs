using Shared.DataRecords.Programmer;

namespace FakeDataSource;

public interface IDataSource
{
    public IEnumerable<ProgrammerRecord> Programmers { get; }
}