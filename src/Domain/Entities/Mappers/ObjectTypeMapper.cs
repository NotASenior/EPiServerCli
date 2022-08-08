using Opti.Cli.Domain.Entities;
using Opti.Cli.Domain.Exceptions;

namespace Opti.Cli.Domain.Mappers
{
    public class ObjectTypeMapper : IObjectTypeMapper
    {
        private readonly IEnumerable<string> types = new List<string>()
        {
            "page",
            "block"
        };

        public ObjectType Map(string type)
        {
            Validate(type);
            type = Autocomplete(type);

            return (type?.ToLower()) switch
            {
                "block" => ObjectType.Block,
                "page" => ObjectType.Page,
                _ => throw new ArgumentOutOfRangeException(nameof(type)),
            };
        }

        private static void Validate(string type)
        {
            type = type ?? throw new ArgumentNullException(nameof(type));

            if (string.IsNullOrEmpty(type?.Trim()))
            {
                throw new ArgumentException(null, nameof(type));
            }
        }

        private string Autocomplete(string type)
        {
            return types
                .FirstOrDefault(x => x.ToLower().Contains(type.ToLower()))!;
        }
    }
}
