namespace KSL.API.Extensions
{
    public interface ILogger
    {
        void Info(string message, string tag = null);
        void Warning(string message, string tag = null);
        void Error(string message, string tag = null);
        void Exception(string message, string tag = null);
    }
}
