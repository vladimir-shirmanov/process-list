using ProcessWorker.Entity;

namespace ProcessWorker.Bl.Interfaces
{
    public interface IProcessManager
    {
        int Create(string processName, int userId);
        
        PagingList<AppProcess> GetProcesses(PagingFilter filter);
    }
}