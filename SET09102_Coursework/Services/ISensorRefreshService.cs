using SET09102_Coursework.Models;

namespace SET09102_Coursework.Services;

public interface ISensorRefreshService : IDisposable
{
    event EventHandler<IEnumerable<Sensor>> SensorsRefreshed;
    Task StartAutoRefresh(int intervalSeconds);
    Task RefreshSensors();
}
