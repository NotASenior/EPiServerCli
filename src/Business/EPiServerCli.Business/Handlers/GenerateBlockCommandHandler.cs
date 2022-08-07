using EPiServerCli.Business.Interfaces.Handlers;
using EPiServerCli.Business.Interfaces.Static;
using EPiServerCli.DataAccess.Interfaces.Repositories;
using EPiServerCli.Domain.Entities;

namespace EPiServerCli.Business.Handlers
{
    public class GenerateBlockCommandHandler : ICommandHandler
    {
        private readonly ITemplateRepository repository;

        public GenerateBlockCommandHandler(ITemplateRepository repository)
        {
            this.repository = repository;
        }

        public async Task ExecuteAsync(Command command)
        {
            (string classContent, string viewContent) = await GetFilesContents(command);

            string directoryPath = Directory.GetCurrentDirectory();

            var createClassTask = repository.CreateAsync($"{directoryPath}/Models/Blocks/{command.Name}.cs", classContent);
            var createViewTask = repository.CreateAsync($"{directoryPath}/Views/Shared/Blocks/{command.Name}.cshtml", viewContent);

            await Task.WhenAll(createClassTask, createViewTask);
        }

        private async Task<(string classContent, string viewContent)> GetFilesContents(Command command)
        {
            Task<string>? classTemplateTask = repository.GetAsync(Templates.Blocks.Class);
            Task<string>? viewTemplateTask = repository.GetAsync(Templates.Blocks.View);

            await Task.WhenAll(classTemplateTask, viewTemplateTask);

            string classTemplate = classTemplateTask.Result;
            string viewTemplate = viewTemplateTask.Result;

            string guid = Guid.NewGuid().ToString();
            string name = command.Name;

            string directoryPath = Directory.GetCurrentDirectory();
            string nameSpace = new DirectoryInfo(directoryPath).Name;

            classTemplate = string.Format(classTemplate, guid, name, nameSpace);
            viewTemplate = string.Format(viewTemplate, name, nameSpace);

            return (classTemplate, viewTemplate);
        }
    }
}
