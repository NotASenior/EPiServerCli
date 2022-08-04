using EPiServerCli.Domain.Entities;

namespace EPiServerCli.Domain.Mappers
{
    public class CommandTypeMapper : ICommandTypeMapper
    {
        public CommandType Map(string type)
        {
            type = type ?? throw new ArgumentNullException(nameof(type));

            if (string.IsNullOrEmpty(type?.Trim()))
            {
                throw new ArgumentException(nameof(type));
            }

            switch (type.ToLower())
            {
                case "generate": return CommandType.Generate;
            }

            throw new ArgumentOutOfRangeException(nameof(type));
        }
    }
}
