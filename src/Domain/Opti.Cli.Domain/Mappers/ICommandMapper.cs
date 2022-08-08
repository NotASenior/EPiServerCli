using Opti.Cli.Domain.Entities;

namespace Opti.Cli.Domain.Mappers
{
    public interface ICommandMapper
    {
        Command Map(string command);
    }
}
