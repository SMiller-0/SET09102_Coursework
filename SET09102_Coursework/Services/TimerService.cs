namespace SET09102_Coursework.Services;

public class TimerService : ITimerService
{
    private IDispatcherTimer? _timer;
    private Func<Task>? _callback;
    private bool _isRunning;

    public bool IsRunning => _isRunning;

    public TimerService()
    {
        _timer = Application.Current?.Dispatcher?.CreateTimer();
        _timer!.Tick += Timer_Tick;
    }

    public void Start(TimeSpan interval, Func<Task> callback)
    {
        if (_timer == null) return;
        
        _callback = callback;
        _timer.Interval = interval;
        _timer.Start();
        _isRunning = true;
    }

    public void Stop()
    {
        if (_timer == null) return;
        
        _timer.Stop();
        _isRunning = false;
    }

    private async void Timer_Tick(object? sender, EventArgs e)
    {
        if (_callback != null)
        {
            await _callback.Invoke();
        }
    }

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