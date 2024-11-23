using System;
using System.IO;
using System.Threading.Tasks;

public interface IFileLoggerService
{
    Task LogResponseAsync(string responseBody, string? logFileName = null);
}

public class FileLoggerService : IFileLoggerService
{
    private readonly string _logDirectory;

    public FileLoggerService()
    {
        _logDirectory = Path.GetTempPath();
    }

    public async Task LogResponseAsync(string responseBody, string? logFileName = null)
    {
        try
        {
            var fileName = logFileName ?? $"fm_log_{DateTime.UtcNow:yyyyMMdd_HHmmssfff}.txt";
            var fullPath = Path.Combine(_logDirectory, fileName);

            await File.WriteAllTextAsync(fullPath, responseBody);

            Console.WriteLine($"Response logged to file: {fullPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to log response: {ex.Message}");
        }
    }
}
