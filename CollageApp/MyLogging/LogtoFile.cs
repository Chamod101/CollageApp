namespace CollageApp.MyLogging
{
    public class LogtoFile : IMyLogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine("Log to file");
        }
    }
}
