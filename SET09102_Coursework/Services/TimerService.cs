namespace SET09102_Coursework.Services;

/// <summary>
/// Provides timer functionality for periodic task execution in the application.
/// Implements ITimerService interface and manages a dispatcher timer for async operations.
/// </summary>
public class TimerService : ITimerService
{
    private IDispatcherTimer? _timer;
    private Func<Task>? _callback;
    private bool _isRunning;

    /// <summary>
    /// Gets a value indicating whether the timer is currently running.
    /// </summary>
    /// <value>True if the timer is running; otherwise, false.</value>
    public bool IsRunning => _isRunning;

    /// <summary>
    /// Initialises a new instance of the TimerService class.
    /// Creates and configures a dispatcher timer for the current application.
    /// </summary>
    public TimerService()
    {
        _timer = Application.Current?.Dispatcher?.CreateTimer();
        _timer!.Tick += Timer_Tick;
    }

    /// <summary>
    /// Starts the timer with the specified interval and callback function.
    /// </summary>
    /// <param name="interval">The time interval between timer ticks.</param>
    /// <param name="callback">The async function to execute on each timer tick.</param>
    /// <remarks>
    /// If the timer is null, the method will return without starting.
    /// The callback function is stored and executed on each timer tick.
    /// </remarks>
    public void Start(TimeSpan interval, Func<Task> callback)
    {
        if (_timer == null) return;
        
        _callback = callback;
        _timer.Interval = interval;
        _timer.Start();
        _isRunning = true;
    }

    /// <summary>
    /// Stops the timer if it is currently running.
    /// </summary>
    /// <remarks>
    /// If the timer is null, the method will return without performing any action.
    /// </remarks>
    public void Stop()
    {
        if (_timer == null) return;
        
        _timer.Stop();
        _isRunning = false;
    }

    /// <summary>
    /// Event handler for timer tick events.
    /// Executes the stored callback function if one exists.
    /// </summary>
    /// <param name="sender">The source of the timer event.</param>
    /// <param name="e">Event arguments.</param>
    /// <remarks>
    /// This method is async void as it is an event handler.
    /// It will await the callback execution if a callback is registered.
    /// </remarks>
    private async void Timer_Tick(object? sender, EventArgs e)
    {
        if (_callback != null)
        {
            await _callback.Invoke();
        }
    }

    /// <summary>
    /// Disposes of the timer service resources.
    /// Implements the IDisposable pattern.
    /// </summary>
    /// <remarks>
    /// Unregisters the tick event handler, stops the timer, and releases resources.
    /// This method should be called when the timer service is no longer needed.
    /// </remarks>
    public void Dispose()
    {
        if (_timer != null)
        {
            _timer.Tick -= Timer_Tick;
            _timer.Stop();
            _timer = null;
        }
        GC.SuppressFinalize(this);
    }
}
