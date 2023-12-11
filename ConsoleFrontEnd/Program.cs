using DataAccessAPI;
using DataAccessAPI.Repositories;
using Shared.Entities;

IDataGateway<Programmer> programmersGateway =
    new ProgrammersGateway(new HttpClient());

Console.WriteLine(RepeateableText.Separator);
Console.WriteLine("Welcome to a very useless application!");
Console.WriteLine(RepeateableText.Separator);

var repeat = true;

while (repeat)
{
    Console.WriteLine(RepeateableText.Separator);
    Console.WriteLine("1. For a REST Api call");
    Console.WriteLine("9. For a console cleanup");
    Console.WriteLine("0. To exit the program");
    Console.WriteLine(RepeateableText.Separator);

    var menuUserInput = Console.ReadLine();

    switch (menuUserInput)
    {
        case "1":
            var programmers = await programmersGateway.GetAllAsync();
            break;
        case "9":
            Console.Clear();
            break;
        case "0":
            repeat = false;
            break;
        default:
            break;
    }
}

public class RepeateableText
{
    public const string Separator = "==================================";
    public const string YesOrNoOptions = "y/n";
}