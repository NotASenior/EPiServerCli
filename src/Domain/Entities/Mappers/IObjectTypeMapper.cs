using EPiServerCli.Domain.Entities;

namespace EPiServerCli.Domain.Mappers
{
    public interface IObjectTypeMapper
    {
        ObjectType Map(string type);
    }
}
