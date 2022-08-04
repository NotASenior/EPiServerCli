using EPiServerCli.Domain.Entities;

namespace EPiServerCli.Domain.Mappers
{
    public interface ICommandMapper
    {
        Command Map(string command);
    }
}
