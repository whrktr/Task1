using Microsoft.Extensions.Logging;
using TestTasks.Model;

public sealed class DbLogger : ILogger
{
    private readonly LogContext _logContext;
    public DbLogger(LogContext logContext)
    {
        _logContext = logContext;
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull => default!;

    public bool IsEnabled(LogLevel logLevel) => logLevel == LogLevel.Information;

    public void Log<TState>(
        LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception? exception,
        Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel))
        {
            return;
        }

        var newLog = new Log
        {
            Message = formatter(state, exception),
            Date = DateTime.UtcNow,
        };

        _logContext.Log.Add(newLog);

        _logContext.SaveChanges();
    }
}