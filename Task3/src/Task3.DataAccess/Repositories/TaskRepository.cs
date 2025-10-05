using Dapper;
using Task3.DataAccess.Connection;
using ProjectTask = Task3.DataAccess.Models.Task;

namespace Task3.DataAccess.Repositories;

public class TaskRepository : IRepository<ProjectTask>
{
    private readonly IConnectionFactory _connectionFactory;

    public TaskRepository(IConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IReadOnlyList<ProjectTask>> GetAllAsync()
    {
        using var connection = _connectionFactory.Create();
        var result = await connection.QueryAsync<ProjectTask>("SELECT * FROM [Tasks]");

        return result.ToList();
    }

    public async Task<ProjectTask?> GetByIdAsync(int id)
    {
        using var connection = _connectionFactory.Create();
        var result = await connection.QuerySingleOrDefaultAsync<ProjectTask>("SELECT * FROM [Tasks] WHERE [Id] = @Id", new { Id = id });

        return result;
    }

    public async Task<int> AddAsync(ProjectTask task)
    {
        using var connection = _connectionFactory.Create();
        var result = await connection.ExecuteAsync(@"INSERT INTO [Tasks] ([Title], [Description], [IsCompleted], [CreatedAt])
            VALUES(@Title, @Description, @IsCompleted, @CreatedAt)", task);

        return result;
    }

    public async Task<int> UpdateAsync(ProjectTask task)
    {
        using var connection = _connectionFactory.Create();
        var result = await connection.ExecuteAsync(@"UPDATE [Tasks] SET [Title] = @Title, [Description] = @Description, [IsCompleted] = @IsCompleted, [CreatedAt] = @CreatedAt
            WHERE [Id] = @Id", task);

        return result;
    }

    public async Task<int> RemoveAsync(int id)
    {
        using var connection = _connectionFactory.Create();
        var result = await connection.ExecuteAsync("DELETE FROM [Tasks] WHERE [Id] = @Id", new { Id = id });

        return result;
    }
}
