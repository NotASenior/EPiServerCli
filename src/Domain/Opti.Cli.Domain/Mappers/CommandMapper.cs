using Opti.Cli.Domain.Entities;
using Opti.Cli.Domain.Exceptions;

namespace Opti.Cli.Domain.Mappers
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
            Validate(command);
            string[] commandParts = GetCommandParts(command);
            Validate(commandParts);

            CommandType commandType = commandTypeMapper!.Map(commandParts[0]);
            ObjectType objectType = objectTypeMapper!.Map(commandParts[1]);
            string name = AutocompleteName(commandParts[2], objectType);

            return new Command(commandType, objectType, name, new List<string>(), new List<string>());
        }

        private static void Validate(string command)
        {
            command = command ?? throw new ArgumentNullException(nameof(command));
            if (string.IsNullOrEmpty(command?.Trim()))
            {
                throw new ArgumentException(null, nameof(command));
            }
        }

        private static void Validate(string[] commandParts)
        {
            if (commandParts.Length < 3) throw new CommandNotValidException();
        }

        private static string[] GetCommandParts(string command)
        {
            string[]? commandParts = command.Trim().Split(' ');
            commandParts = commandParts
                .Where(x => !string.IsNullOrEmpty(x?.Trim()))
                .ToArray();
            return commandParts;
        }

        private static string AutocompleteName(string name, ObjectType objectType)
        {
            string suffix = GetSuffix(objectType);

            if (!name.EndsWith(suffix)) return name + suffix;

            return name;
        }

        private static string GetSuffix(ObjectType objectType)
        {
            switch (objectType)
            {
                case ObjectType.Page: return "Page";
                case ObjectType.Block: return "Block";
                case ObjectType.SelectionFactory: return "SelectionFactory";
                case ObjectType.InitializableModule: return "Initialization";
            }

            throw new ArgumentOutOfRangeException(nameof(objectType));
        }
    }
}
