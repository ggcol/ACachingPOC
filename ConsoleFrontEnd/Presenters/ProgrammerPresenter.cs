using Shared.Entities;

namespace ConsoleFrontEnd.Presenters;

public class ProgrammerPresenter
{
    private readonly Programmer _programmer;

    public ProgrammerPresenter(Programmer programmer)
    {
        _programmer = programmer;
    }
    
    public void Display()
    {
        Console.WriteLine("--------------------");
        Console.WriteLine($"Programmer: {_programmer.Id} - {_programmer.Name}");
        Console.WriteLine($"Line-per-hour: {_programmer.LinePerHour}");
        Console.WriteLine($"Times-in-burnout: {_programmer.TimesInBurnout}");
    }
}