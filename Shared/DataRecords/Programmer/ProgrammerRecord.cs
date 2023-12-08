namespace Shared.DataRecords.Programmer;

/*
 * This class should be supposed to be a db record.
 * Nevertheless it contains behaviours: this is to reflect possible changes
 * from other systems.
 */
public sealed class ProgrammerRecord
{
    public int Id { get; init; }
    public string Name { get; init; }
    public int LinePerHour { get; set; }
    public int TimesInBurnout { get; set; }

    public ProgrammerRecord(int id, string name, int linePerHour)
    {
        Id = id;
        Name = name;
        LinePerHour = linePerHour;
    }

    public bool IsBurntOut()
    {
        return LinePerHour == Thresholds.Programmer.BurnoutThreshold - 1;
    }

    public void GoToBurnout()
    {
        LinePerHour = 0;
        TimesInBurnout++;
    }
};