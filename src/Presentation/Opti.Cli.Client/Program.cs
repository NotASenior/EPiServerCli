using Opti.Cli.Business.Factories;
using Opti.Cli.Business.Handlers;
using Opti.Cli.Business.Interfaces.Factories;
using Opti.Cli.Business.Interfaces.Handlers;
using Opti.Cli.Business.Interfaces.Services;
using Opti.Cli.Business.Services;
using Opti.Cli.DataAccess.Interfaces.Repositories;
using Opti.Cli.DataAccess.Interfaces.Services;
using Opti.Cli.DataAccess.Repositories;
using Opti.Cli.DataAccess.Services;
using Opti.Cli.Domain.Mappers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using Opti.Cli.Domain.Exceptions;

if (args.Length == 0)
{
    ShowHelp();
    return;
}

using IHost host = Host.CreateDefaultBuilder(args)
.ConfigureServices((_, services) =>
    services
        .AddScoped<ICommandMapper, CommandMapper>()
        .AddScoped<ICommandTypeMapper, CommandTypeMapper>()
        .AddScoped<IObjectTypeMapper, ObjectTypeMapper>()
        .AddScoped<IFileService, FileService>()
        .AddScoped<ITemplateRepository, TemplateRepository>()
        .AddScoped<ICommandHandlerFactory, CommandHandlerFactory>()
        .AddScoped<ICommandHandler, GeneratePageCommandHandler>()
        .AddScoped<ICommandHandler, GenerateBlockCommandHandler>()
        .AddScoped<ICommandService, CommandService>())
.Build();

IServiceProvider services = host.Services;
using IServiceScope serviceScope = services.CreateScope();
IServiceProvider provider = serviceScope.ServiceProvider;

ICommandService commandService = provider.GetRequiredService<ICommandService>();

try
{
    await commandService.ExecuteAsync(string.Join(' ', args));
    Console.WriteLine("Generated!");
}
catch (TemplateReadingException)
{
    Console.WriteLine("Error reading the templates");
}
catch (TemplateFormattingException)
{
    Console.WriteLine("Error formatting the templates");
}
catch (ScaffoldingException)
{
    Console.WriteLine("Error generating the files");
}

void ShowHelp()
{
    var versionString = Assembly.GetEntryAssembly()?
                                .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?
                                .InformationalVersion
                                .ToString();

    Console.WriteLine($"opti v{versionString}");
    Console.WriteLine("-------------");
    Console.WriteLine("\nUsage:");
    Console.WriteLine("opti generate page Test");
}
