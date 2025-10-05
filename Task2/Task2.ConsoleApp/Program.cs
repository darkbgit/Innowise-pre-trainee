using System.Diagnostics;
using Task2.ConsoleApp;

var service = new Service();

var files = new List<string> { "File1", "File2", "File3" };


var timer = new Stopwatch();
timer.Start();
Worker.DoWorkSync(files, service);
timer.Stop();
Console.WriteLine($"Synchronous work completed in {timer.Elapsed.TotalSeconds} seconds");
timer.Reset();

timer.Start();
await Worker.DoWorkAsync(files, service);
timer.Stop();
Console.WriteLine($"Asynchronous work completed in {timer.Elapsed.TotalSeconds} seconds");
