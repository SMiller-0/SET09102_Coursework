namespace SET09102_Coursework.Services;

public interface ITimerService : IDisposable
{
    void Start(TimeSpan interval, Func<Task> callback);
    void Stop();
    bool IsRunning { get; }
}