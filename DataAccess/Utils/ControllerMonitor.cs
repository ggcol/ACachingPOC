using System.Diagnostics;

namespace DataAccess.Utils;

public interface IControllerMonitor
{
    public void Start(string key);
    public TimeSpan Stop(string key);
}

public class ControllerMonitor : IControllerMonitor
{
    private readonly IDictionary<string, Stopwatch> _stopwatches =
        new Dictionary<string, Stopwatch>();

    public void Start(string key)
    {
        if (!_stopwatches.ContainsKey(key))
        {
            _stopwatches.Add(key, new Stopwatch());
        }

        _stopwatches[key].Start();
    }

    public TimeSpan Stop(string key)
    {
        if (!_stopwatches.ContainsKey(key))
        {
            throw new KeyNotFoundException(
                $"No stopwatch found for key: {key}");
        }

        _stopwatches[key].Stop();
        var elapsed = _stopwatches[key].Elapsed;
        _stopwatches.Remove(key);
        return elapsed;
    }
}