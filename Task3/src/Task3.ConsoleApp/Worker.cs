using System.Data.Common;
using Task3.DataAccess.Repositories;
using ProjectTask = Task3.DataAccess.Models.Task;

namespace Task3.ConsoleApp;

public class Worker
{
    private readonly IRepository<ProjectTask> _taskRepository;

    public Worker(IRepository<ProjectTask> taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task AddNewTask()
    {
        Console.WriteLine("Input data for new task");
        var description = GetStringFromConsole("description");
        var title = GetStringFromConsole("title");

        var task = new ProjectTask()
        {
            Description = description,
            Title = title,
            CreatedAt = DateTime.UtcNow
        };

        try
        {
            var result = await _taskRepository.AddAsync(task);

            if (result == 0)
            {
                Console.WriteLine("Task not added");
                return;
            }

            Console.WriteLine("Task added");
        }
        catch (DbException e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public async Task ViewAllTasks()
    {
        IReadOnlyList<ProjectTask> tasks;

        try
        {
            tasks = await _taskRepository.GetAllAsync();
        }
        catch (DbException e)
        {
            Console.WriteLine(e.Message);
            return;
        }

        if (tasks.Count == 0)
        {
            Console.WriteLine("No tasks");
            return;
        }

        Console.WriteLine("All tasks:");
        foreach (var task in tasks)
        {
            Console.WriteLine($"Id: {task.Id}");
            Console.WriteLine($"Description: {task.Description}");
            Console.WriteLine($"Title: {task.Title}");
            Console.WriteLine($"IsCompleted: {task.IsCompleted}");
            Console.WriteLine($"CreatedAt: {task.CreatedAt.ToLocalTime()}");
            Console.WriteLine(new string('_', 20));
        }
    }

    public async Task CompleteTask()
    {
        Console.WriteLine("Input data for completing task");
        var id = GetIntFromConsole("id");

        try
        {
            var task = await _taskRepository.GetByIdAsync(id);

            if (task == null)
            {
                Console.WriteLine("Task not found");
                return;
            }

            if (task.IsCompleted)
            {
                Console.WriteLine("Task already completed");
                return;
            }

            task.IsCompleted = true;
            var result = await _taskRepository.UpdateAsync(task);

            if (result == 0)
            {
                Console.WriteLine("Task not found");
                return;
            }

            Console.WriteLine("Task completed");
        }
        catch (DbException e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public async Task DeleteTask()
    {
        Console.WriteLine("Input data for deleting task");
        var id = GetIntFromConsole("id");

        try
        {
            var result = await _taskRepository.RemoveAsync(id);

            if (result == 0)
            {
                Console.WriteLine("Task not found");
                return;
            }

            Console.WriteLine("Task deleted");
        }
        catch (DbException e)
        {
            Console.WriteLine(e.Message);
        }
    }

    private static string GetStringFromConsole(string description)
    {
        Console.WriteLine($"Input {description}");

        while (true)
        {
            var result = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(result))
            {
                return result;
            }

            Console.WriteLine("Empty string not allowed.");
        }
    }

    private static int GetIntFromConsole(string description)
    {
        Console.WriteLine($"Input {description}");

        while (true)
        {
            var input = Console.ReadLine();
            if (int.TryParse(input, out var result))
                return result;

            Console.WriteLine($"{input} is not valid value integer.");
        }
    }
}
