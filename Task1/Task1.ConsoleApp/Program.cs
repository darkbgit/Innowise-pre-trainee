using Task1.ConsoleApp;

AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

Calculator.Run();

static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
{
    Console.WriteLine("Error " + (e.ExceptionObject as Exception)?.Message);
    Console.WriteLine("Application will be terminated. Press any key...");
    Console.ReadKey();
}