namespace CheckedAppProject.LOGIC.Services.Logger
{
    public interface IAppLogger
    {
        void LogToConsole(string message);
        void LogToFile(string message);
        void LogException(Exception exception);
        void LogToFileAndConsole(string message);
    }
}