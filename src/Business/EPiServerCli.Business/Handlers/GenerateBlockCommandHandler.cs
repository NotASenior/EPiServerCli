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
            var classTemplateTask = repository.GetAsync(Templates.Blocks.Class);
            var viewTemplateTask = repository.GetAsync(Templates.Blocks.View);

            await Task.WhenAll(classTemplateTask, viewTemplateTask);

            string classTemplate = classTemplateTask.Result;
            string viewTemplate = viewTemplateTask.Result;

            string guid = Guid.NewGuid().ToString();
            string name = command.Name;

            classTemplate = string.Format(classTemplate, guid, name);
            viewTemplate = string.Format(viewTemplate, guid, name);

            var createClassTask = repository.CreateAsync($"~/Models/Blocks/{name}.cs", classTemplate);
            var createViewTask = repository.CreateAsync($"~/Views/Shared/Blocks/{name}.cshtml", viewTemplate);

            await Task.WhenAll(createClassTask, createViewTask);
        }
    }
}
