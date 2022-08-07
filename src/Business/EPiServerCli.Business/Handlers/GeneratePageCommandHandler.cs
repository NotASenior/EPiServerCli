using EPiServerCli.Business.Interfaces.Handlers;
using EPiServerCli.Business.Interfaces.Static;
using EPiServerCli.DataAccess.Interfaces.Repositories;
using EPiServerCli.Domain.Entities;

namespace EPiServerCli.Business.Handlers
{
    public class GeneratePageCommandHandler : ICommandHandler
    {
        private readonly ITemplateRepository repository;

        public GeneratePageCommandHandler(ITemplateRepository repository)
        {
            this.repository = repository;
        }

        public async Task ExecuteAsync(Command command)
        {
            (string classContent, string controllerContent, string viewContent) = await GetFilesContents(command);

            string directoryPath = Directory.GetCurrentDirectory();

            var createClassTask = repository.CreateAsync($"{directoryPath}/Models/Pages/{command.Name}.cs", classContent);
            var createControllerTask = repository.CreateAsync($"{directoryPath}/Controllers/{command.Name}.cs", controllerContent);
            var createViewTask = repository.CreateAsync($"{directoryPath}/Views/{command.Name}/Index.cshtml", viewContent);

            await Task.WhenAll(createClassTask, createControllerTask, createViewTask);
        }

        private async Task<(string classContent, string controllerContent, string viewContent)> GetFilesContents(Command command)
        {
            Task<string>? classTemplateTask = repository.GetAsync(Templates.Pages.Class);
            Task<string>? controllerTemplateTask = repository.GetAsync(Templates.Pages.Controller);
            Task<string>? viewTemplateTask = repository.GetAsync(Templates.Pages.View);

            await Task.WhenAll(classTemplateTask, controllerTemplateTask, viewTemplateTask);

            string classTemplate = classTemplateTask.Result;
            string controllerTemplate = controllerTemplateTask.Result;
            string viewTemplate = viewTemplateTask.Result;

            string guid = Guid.NewGuid().ToString();
            string name = command.Name;

            string directoryPath = Directory.GetCurrentDirectory();
            string nameSpace = new DirectoryInfo(directoryPath).Name;

            classTemplate = string.Format(classTemplate, guid, name, nameSpace);
            controllerTemplate = string.Format(controllerTemplate, name, nameSpace);
            viewTemplate = string.Format(viewTemplate, name, nameSpace);

            return (classTemplate, controllerTemplate, viewTemplate);
        }
    }
}
