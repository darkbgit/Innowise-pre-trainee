using Microsoft.Extensions.DependencyInjection;
using Task4.Core.Interfaces.Services;
using Task4.Core.Mapping;
using Task4.Core.Services;

namespace Task4.Core.DI;

public static class ServiceCollectionForCore
{
    public static void RegisterDependencies(IServiceCollection services)
    {
        services.AddScoped<IAuthorService, AuthorService>();
        services.AddScoped<IBookService, BookService>();

        services.AddAutoMapper(cfg => { }, typeof(MappingProfile));
    }
}
