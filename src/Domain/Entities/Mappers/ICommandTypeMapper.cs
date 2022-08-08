using Opti.Cli.Domain.Entities;

namespace Opti.Cli.Domain.Mappers
{
    public interface ICommandTypeMapper
    {
        CommandType Map(string type);
    }
}
