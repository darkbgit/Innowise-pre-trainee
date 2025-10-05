namespace Task3.ConsoleApp;

public class App
{
    private readonly Worker _worker;

    private const string _addNewTaskCommand = "1";
    private const string _viewAllTasksCommand = "2";
    private const string _completeTaskCommand = "3";
    private const string _deleteTaskCommand = "4";
    private const string _exitCommand = "5";

    public App(Worker worker)
    {
        _worker = worker;
    }

    public async Task Run()
    {
        bool breakFlag = true;
        while (breakFlag)
        {
            Console.WriteLine(HelpString);
            var command = Console.ReadLine();
            try
            {
                switch (command)
                {
                    case _addNewTaskCommand:
                        await _worker.AddNewTask();
                        break;
                    case _viewAllTasksCommand:
                        await _worker.ViewAllTasks();
                        break;
                    case _completeTaskCommand:
                        await _worker.CompleteTask();
                        break;
                    case _deleteTaskCommand:
                        await _worker.DeleteTask();
                        break;
                    case _exitCommand:
                        breakFlag = false;
                        continue;
                    default:
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }

    private static string HelpString =>
    $"""
    Choose option:
    {_addNewTaskCommand} - Add new task
    {_viewAllTasksCommand} - View all tasks
    {_completeTaskCommand} - Complete task
    {_deleteTaskCommand} - Delete task
    {_exitCommand} - Exit
    """;
}
