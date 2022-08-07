using EPiServerCli.Domain.Entities;

namespace EPiServerCli.Business.Interfaces.Handlers
{
    public interface ICommandHandler
    {
        /// <exception cref="TemplateReadingException">Template does not exist, or cannot be accessed.</exception>
        /// <exception cref="TemplateFormattingException">Template cannot be formatted. string.Format.</exception>
        /// <exception cref="ScaffoldingException">Exception when creating the generated code file.</exception>
        Task ExecuteAsync(Command command);
    }
}
