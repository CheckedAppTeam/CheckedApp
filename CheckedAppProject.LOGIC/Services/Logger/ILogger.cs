namespace CheckedAppProject.LOGIC.Services.Logger
{
    public interface ILogger
    {
        void LogToConsole(string message);
        void LogToFile(string message);
    }
}