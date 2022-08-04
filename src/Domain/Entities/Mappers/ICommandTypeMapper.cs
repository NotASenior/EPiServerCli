using EPiServerCli.Domain.Entities;

namespace EPiServerCli.Domain.Mappers
{
    public interface ICommandTypeMapper
    {
        CommandType Map(string type);
    }
}
