using FakeDataSource.Data;
using Shared;
using Shared.DataRecords.Programmer;

namespace FakeDataSource.DataSources;

public class SharedWithOtherSystemsDb : IDataSource
{
    private readonly TimeSpan _extSysUpdateInterval = TimeSpan.FromSeconds(10);
    private readonly Random _random = new();
    private readonly IEnumerable<ProgrammerRecord> _programmers;
    
    private event EventHandler OnDataChanged;

    public IEnumerable<ProgrammerRecord> Programmers
    {
        get
        {
            //very minimal delay simulating network latency, database access, querying, etc.
            Thread.Sleep(Thresholds.DataSource.NormalLatency);
            return _programmers;
        }
    }
    
    public SharedWithOtherSystemsDb()
    {
        _programmers = DataPool.Programmers;
        AcquireNewMetrics();

        OnDataChanged += EmulateOtherSystemsChanges;

        OnDataChanged.Invoke(this, EventArgs.Empty);
    }

    //this emulates changes on db records from other systems
    private void EmulateOtherSystemsChanges(object? sender, EventArgs e)
    {
        Task.Run(() =>
        {
            Thread.Sleep(_extSysUpdateInterval);

            AcquireNewMetrics();

            OnDataChanged?.Invoke(this, EventArgs.Empty);
        });
    }

    //this emulates changes on db records from other systems
    private void AcquireNewMetrics()
    {
        foreach (var programmer in _programmers)
        {
            if (programmer.IsBurntOut())
            {
                programmer.GoToBurnout();
            }
            else
            {
                programmer.LinePerHour = _random.Next(programmer.LinePerHour,
                    Thresholds.Programmer.BurnoutThreshold);
            }
        }
    }
}