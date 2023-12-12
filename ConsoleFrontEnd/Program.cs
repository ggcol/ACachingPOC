using ConsoleFrontEnd.Presenters;
using DataAccessAPI;
using DataAccessAPI.Repositories;
using Shared.Entities;

IDataGateway<Programmer> programmersGateway =
    new ProgrammersGateway(new HttpClient());

PrintHeader();

var repeat = true;

while (repeat)
{
    Console.WriteLine(RepeatableText.Separator);
    Console.WriteLine("1. For a REST Api call");
    Console.WriteLine("9. For a console cleanup");
    Console.WriteLine("0. To exit the program");
    Console.WriteLine(RepeatableText.Separator);

    var menuUserInput = Console.ReadLine();

    switch (menuUserInput)
    {
        case "1":
            var programmers = await programmersGateway.GetAllAsync();
            var presenters = programmers.Select(p => new ProgrammerPresenter(p));
            foreach (var presenter in presenters)
            {
                presenter.Display();
            }
            break;
        case "9":
            Console.Clear();
            PrintHeader();
            break;
        case "0":
            repeat = false;
            break;
        default:
            break;
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