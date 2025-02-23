﻿using Opti.Cli.Business.Interfaces.Handlers;
using Opti.Cli.Business.Interfaces.Static;
using Opti.Cli.DataAccess.Interfaces.Repositories;
using Opti.Cli.Domain.Entities;
using Opti.Cli.Domain.Exceptions;

namespace Opti.Cli.Business.Handlers
{
    public class GenerateInitializableModuleCommandHandler : ICommandHandler
    {
        private readonly ITemplateRepository repository;

        public GenerateInitializableModuleCommandHandler(ITemplateRepository repository)
        {
            this.repository = repository;
        }

        public async Task ExecuteAsync(Command command)
        {
            string classContent = await GetFilesContents(command);

            string directoryPath = Directory.GetCurrentDirectory();

            // TODO: add a path provider, and remove duplicated code
            var createClassTask = repository.CreateAsync($"{directoryPath}/Infrastructure/Initialization/{command.Name}.cs", classContent);

            try
            {
                await createClassTask;
            }
            catch
            {
                throw new ScaffoldingException();
            }
        }

        private async Task<string> GetFilesContents(Command command)
        {
            Task<string>? classTemplateTask = repository.GetAsync(Templates.InitializableModule.Class);

            try
            {
                await classTemplateTask;
            }
            catch
            {
                throw new TemplateReadingException();
            }

            string classTemplate = classTemplateTask.Result;

            string name = command.Name;

            string directoryPath = Directory.GetCurrentDirectory();
            string nameSpace = new DirectoryInfo(directoryPath).Name;

            try
            {
                classTemplate = string.Format(classTemplate, name, nameSpace);
            }
            catch
            {
                throw new TemplateFormattingException();
            }

            return classTemplate;
        }
    }
}
