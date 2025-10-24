using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Task4.DataAccess.Data;
using Task4.DataAccess.Repositories;
using Task4.Domain.Entities;
using Task4.Domain.Interfaces;

namespace Task4.DataAccess.DI;

public static class ServiceCollectionForDataAccess
{
    public static void RegisterDependencies(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<Task4Context>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IRepository<Author>, AuthorRepository>();
        services.AddScoped<IRepository<Book>, BookRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}