using ConsoleFrontEnd.Presenters;
using DataAccessAPI;

IDataGateway dataGateway = new DataGateway(new HttpClient());
IPresenter presenter = new ConsolePresenter();

var repeat = true;
var imOnStart = true;
var connectionRetries = 0;
var connected = false;

PrintHeader();

while (repeat)
{
    if (imOnStart)
    {
        Console.WriteLine("Checking connection to the API...");
        connected = await dataGateway.Health.PingAsync().ConfigureAwait(false);
        Console.WriteLine(connected ? "Connected!" : "Not connected!");
        Thread.Sleep(200);
        Console.Clear();
    }

    connectionRetries++;
    if (connectionRetries >= 10)
    {
        Console.WriteLine("Connection retries exceeded, exiting...");
        repeat = false;
    }

    if (connected)
    {
        imOnStart = false;
        Console.WriteLine(RepeatableText.Separator);
        Console.WriteLine("1. For a connection check");
        Console.WriteLine("2. For a REST Api call");
        Console.WriteLine("9. For a console cleanup");
        Console.WriteLine("0. To exit the program");
        Console.WriteLine(RepeatableText.Separator);

        var menuUserInput = Console.ReadLine();

        switch (menuUserInput)
        {
            case "1":
                Console.WriteLine("Checking connection to the API...");
                connected = await dataGateway.Health.PingAsync()
                    .ConfigureAwait(false);
                Console.WriteLine(connected ? "Connected!" : "Not connected!");
                break;
            case "2":
                var programmers = await dataGateway.Programmers.GetAllAsync()
                    .ConfigureAwait(false);
                presenter.AsTable(programmers);
                break;
            case "9":
                Console.Clear();
                PrintHeader();
                break;
            case "0":
                repeat = false;
                break;
            default:
                Console.WriteLine("Invalid input!");
                break;
        }
    }
}

Console.WriteLine("Bye bye!");

static void PrintHeader()
{
    Console.WriteLine(RepeatableText.Separator);
    Console.WriteLine("Welcome to a very useless application!");
}

public static class RepeatableText
{
    public const string Separator = "==================================";
}