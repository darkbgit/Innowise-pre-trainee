namespace Task2.ConsoleApp;

public class Worker
{
    public static async Task DoWorkAsync(IEnumerable<string> files, Service service)
    {
        WriteLineWithData("Starting processing data asynchronously");

        List<Task<string>> tasks = [];

        foreach (var file in files)
        {
            tasks.Add(service.ProcessDataAsync(file));
        }

        while (tasks.Count > 0)
        {
            var completedTask = await Task.WhenAny(tasks);
            tasks.Remove(completedTask);

            var result = await completedTask;

            WriteLineWithData(result);
        }

        WriteLineWithData("Ending processing data asynchronously");
    }

    public static void DoWorkSync(IEnumerable<string> files, Service service)
    {
        WriteLineWithData("Starting processing data synchronously");

        foreach (var file in files)
        {
            var result = service.ProcessData(file);
            WriteLineWithData(result);
        }

        WriteLineWithData("Ending processing data synchronously");
    }

    private static void WriteLineWithData(string message)
    {
        Console.WriteLine(DateTime.Now + " " + message);
    }
}
