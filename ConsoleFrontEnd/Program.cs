using DataAccessAPI;
using DataAccessAPI.Repositories;
using Shared.Entities;

IDataGateway<Programmer> programmersGateway = new ProgrammersGateway();

Console.WriteLine(RepeateableText.Separator);
Console.WriteLine("Welcome to a very useless application!");
Console.WriteLine(RepeateableText.Separator);

var repeat = true;

while (repeat)
{
    Console.WriteLine(RepeateableText.Separator);
    Console.WriteLine("1. For a REST Api call");
    Console.WriteLine("9. For a console cleanup");
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
        default:
            break;
    }

    Console.WriteLine(RepeateableText.Separator);
    Console.WriteLine("Other useless actions?");
    Console.WriteLine(RepeateableText.YesOrNoOptions);
    Console.WriteLine(RepeateableText.Separator);
    
    var repeatUserInput = Console.ReadLine();
    repeat = EvaluateForRepeat(repeatUserInput);
}

static bool EvaluateForRepeat(string? input)
{
    return input is not null &&
           input.Equals("y", StringComparison.OrdinalIgnoreCase);
}

public class RepeateableText
{
    public const string Separator = "==================================";
    public const string YesOrNoOptions = "y/n";
}