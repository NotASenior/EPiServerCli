using EPiServerCli.Domain.Entities;

namespace EPiServerCli.Domain.Mappers
{
    public class CommandTypeMapper : ICommandTypeMapper
    {
        private readonly IEnumerable<string> types = new List<string>()
        {
            "generate"
        };

        public CommandType Map(string type)
        {
            Validate(type);
            type = Autocomplete(type);

            switch (type?.ToLower())
            {
                case "generate": return CommandType.Generate;
            }

            throw new ArgumentOutOfRangeException(nameof(type));
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
