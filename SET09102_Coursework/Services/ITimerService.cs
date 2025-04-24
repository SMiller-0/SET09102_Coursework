namespace SET09102_Coursework.Services;

/// <summary>
/// Defines the contract for a timer service that provides periodic task execution functionality.
/// </summary>
/// <remarks>
/// This interface extends IDisposable to ensure proper cleanup of timer resources.
/// Implementations should handle timer initialization, execution, and cleanup.
/// </remarks>
public interface ITimerService : IDisposable
{
    /// <summary>
    /// Starts the timer with the specified interval and callback function.
    /// </summary>
    /// <param name="interval">The time span between timer ticks.</param>
    /// <param name="callback">An asynchronous function to be executed on each timer tick.</param>
    /// <remarks>
    /// The callback function will be invoked periodically based on the specified interval.
    /// If the timer is already running, it should be stopped and restarted with the new parameters.
    /// </remarks>
    void Start(TimeSpan interval, Func<Task> callback);

    /// <summary>
    /// Stops the timer and prevents further execution of the callback function.
    /// </summary>
    /// <remarks>
    /// This method should safely handle being called multiple times or when the timer is not running.
    /// </remarks>
    void Stop();

    /// <summary>
    /// Gets whether the timer is currently running.
    /// </summary>
    /// <value>
    /// True if the timer is active and executing callbacks; otherwise, false.
    /// </value>
    bool IsRunning { get; }
}
