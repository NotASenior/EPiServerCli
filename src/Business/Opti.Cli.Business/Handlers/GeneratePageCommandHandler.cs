using Opti.Cli.Business.Interfaces.Handlers;
using Opti.Cli.Business.Interfaces.Static;
using Opti.Cli.DataAccess.Interfaces.Repositories;
using Opti.Cli.Domain.Entities;
using Opti.Cli.Domain.Exceptions;

namespace Opti.Cli.Business.Handlers
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
            var createControllerTask = repository.CreateAsync($"{directoryPath}/Controllers/{command.Name}Controller.cs", controllerContent);
            var createViewTask = repository.CreateAsync($"{directoryPath}/Views/{command.Name}/Index.cshtml", viewContent);

            try
            {
                await Task.WhenAll(createClassTask, createControllerTask, createViewTask);
            }
            catch
            {
                throw new ScaffoldingException();
            }
        }

        private async Task<(string classContent, string controllerContent, string viewContent)> GetFilesContents(Command command)
        {
            Task<string>? classTemplateTask = repository.GetAsync(Templates.Pages.Class);
            Task<string>? controllerTemplateTask = repository.GetAsync(Templates.Pages.Controller);
            Task<string>? viewTemplateTask = repository.GetAsync(Templates.Pages.View);

            try
            {
                await Task.WhenAll(classTemplateTask, controllerTemplateTask, viewTemplateTask);
            }
            catch
            {
                throw new TemplateReadingException();
            }

            string classTemplate = classTemplateTask.Result;
            string controllerTemplate = controllerTemplateTask.Result;
            string viewTemplate = viewTemplateTask.Result;

            string guid = Guid.NewGuid().ToString();
            string name = command.Name;

            string directoryPath = Directory.GetCurrentDirectory();
            string nameSpace = new DirectoryInfo(directoryPath).Name;

            try
            {
                classTemplate = string.Format(classTemplate, guid, name, nameSpace);
                controllerTemplate = string.Format(controllerTemplate, name, nameSpace);
                viewTemplate = string.Format(viewTemplate, name, nameSpace);
            }
            catch
            {
                throw new TemplateFormattingException();
            }

            return (classTemplate, controllerTemplate, viewTemplate);
        }
    }
}
