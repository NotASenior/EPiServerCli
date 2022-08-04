using EPiServerCli.Domain.Entities;
using EPiServerCli.Domain.Exceptions;

namespace EPiServerCli.Domain.Mappers
{
    public class ObjectTypeMapper : IObjectTypeMapper
    {
        public ObjectType Map(string type)
        {
            type = type ?? throw new ArgumentNullException(nameof(type));

            if (string.IsNullOrEmpty(type?.Trim()))
            {
                throw new ArgumentException(nameof(type));
            }

            switch (type.ToLower())
            {
                case "block": return ObjectType.Block;
                case "page": return ObjectType.Page;
            }

            throw new ArgumentOutOfRangeException(nameof(type));
        }
    }
}
