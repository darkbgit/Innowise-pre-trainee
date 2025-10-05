namespace Task2.ConsoleApp;

public class Service
{
    public string ProcessData(string dataName)
    {
        int delayInSeconds = 3;

        Thread.Sleep(delayInSeconds * 1000);

        return $"Processing {dataName} completed in {delayInSeconds} seconds.";
    }

    public async Task<string> ProcessDataAsync(string dataName)
    {
        int delayInSeconds = 3;

        await Task.Delay(delayInSeconds * 1000);

        return $"Processing {dataName} completed in {delayInSeconds} seconds.";
    }
}
