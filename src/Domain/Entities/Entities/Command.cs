namespace Opti.Cli.Domain.Entities
{
    public class Command
    {
        public CommandType Type { get; }
        public ObjectType ObjectType { get; }
        public string Name { get; }
        public IEnumerable<string> Path { get; }
        public IEnumerable<string> Options { get; }

        public Command(CommandType type, ObjectType objectType, string name, IEnumerable<string> path, IEnumerable<string> options)
        {
            Type = type;
            ObjectType = objectType;
            Name = name;
            Path = path;
            Options = options;
        }
    }
}
