using Task4.Core.Interfaces.Repositories;
using Task4.Core.Interfaces.Services;
using Task4.Core.Models;
using Task4.Core.Services;
using Task4.DataAccess.Data;
using Task4.DataAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSingleton<InMemoryTask4Context>();
builder.Services.AddTransient<IRepository<Author>, AuthorRepository>();
builder.Services.AddTransient<IRepository<Book>, BookRepository>();
builder.Services.AddTransient<IAuthorService, AuthorService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.MapControllers();

app.Run();
