using DataAccessAPI.Repositories.Health;
using DataAccessAPI.Repositories.Programmer;

namespace DataAccessAPI;

public interface IDataGateway
{
    IHealthRepository Health { get; }
    IProgrammersRepository Programmers { get; }
}