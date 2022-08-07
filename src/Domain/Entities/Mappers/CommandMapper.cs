using EPiServerCli.Domain.Entities;
using EPiServerCli.Domain.Exceptions;

namespace EPiServerCli.Domain.Mappers
{
    public class CommandMapper : ICommandMapper
    {
        private readonly IObjectTypeMapper? objectTypeMapper;
        private readonly ICommandTypeMapper? commandTypeMapper;

        public CommandMapper(IObjectTypeMapper? objectTypeMapper, ICommandTypeMapper? commandTypeMapper)
        {
            this.objectTypeMapper = objectTypeMapper;
            this.commandTypeMapper = commandTypeMapper;
        }

        public Command Map(string command)
        {
            command = command ?? throw new ArgumentNullException(nameof(command));

            if (string.IsNullOrEmpty(command?.Trim()))
            {
                throw new ArgumentException(null, nameof(command));
            }

            string[]? commandParts = command.Trim().Split(' ');
            commandParts = commandParts
                .Where(x => !string.IsNullOrEmpty(x?.Trim()))
                .ToArray();

            if (commandParts.Length < 3) throw new CommandNotValidException();

            string name = commandParts[2];
            CommandType commandType = commandTypeMapper!.Map(commandParts[0]);
            ObjectType objectType = objectTypeMapper!.Map(commandParts[1]);

            return new Command(commandType, objectType, name, new List<string>(), new List<string>());
        }
    }
}
