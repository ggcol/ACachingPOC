using ConsoleTableExt;

namespace ConsoleFrontEnd.Presenters;

public class ConsolePresenter : IPresenter
{
    public void AsTable<T>(IEnumerable<T> data) where T : class
    {
        ConsoleTableBuilder.From(data.ToList()).ExportAndWriteLine();
    }
}