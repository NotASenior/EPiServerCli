using Opti.Cli.Domain.Exceptions;

namespace Opti.Cli.Business.Interfaces.Services
{
    public interface ICommandService
    {
        /// <exception cref="TemplateReadingException">Template does not exist, or cannot be accessed.</exception>
        /// <exception cref="TemplateFormattingException">Template cannot be formatted. string.Format.</exception>
        /// <exception cref="ScaffoldingException">Exception when creating the generated code file.</exception>
        Task ExecuteAsync(string command);
    }
}
