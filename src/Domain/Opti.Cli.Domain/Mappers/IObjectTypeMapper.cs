using Opti.Cli.Domain.Entities;

namespace Opti.Cli.Domain.Mappers
{
    public interface IObjectTypeMapper
    {
        ObjectType Map(string type);
    }
}
