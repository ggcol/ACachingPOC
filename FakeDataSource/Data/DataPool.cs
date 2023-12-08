using Shared.DataRecords.Programmer;

namespace FakeDataSource.Data;

public static class DataPool
{
    public static IEnumerable<ProgrammerRecord> Programmers => new[]
    {
        new ProgrammerRecord(0, "Enrico", 100),
        new ProgrammerRecord(2, "Alexander", 100),
        new ProgrammerRecord(3, "Gianluca", 100),
        new ProgrammerRecord(4, "Sergio", 100),
        new ProgrammerRecord(5, "Tommaso", 100)
    };
}