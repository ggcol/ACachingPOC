namespace ConsoleFrontEnd.Presenters;

public interface IPresenter
{
    public void AsTable<T>(IEnumerable<T> data) where T : class;
}