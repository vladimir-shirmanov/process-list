using System.Linq;
using ProcessWorker.Bl.Interfaces;
using ProcessWorker.Entity;

namespace ProcessWorker.Bl
{
    public class ProcessManager : IProcessManager
    {
        private readonly IRepository _repository;
        private readonly IMessageBroker _messageBroker;

        public ProcessManager(IRepository repository, IMessageBroker messageBroker)
        {
            _repository = repository;
            _messageBroker = messageBroker;
        }

        private int Create(AppProcess process)
        {
            int processId = _repository.Create(process);
            _messageBroker.SendMessage(processId);
            return processId;
        }

        public int Create(string processName, int userId)
        {
            return Create(new AppProcess
            {
                Name = processName, AuthorId = userId
            });
        }

        public PagingList<AppProcess> GetProcesses(PagingFilter filter)
        {
            var data = _repository.GetPaged<AppProcess>(filter, null, p => p.Author, p => p.Id).ToList();
            var totalItems = _repository.GetAll<AppProcess>().Count();
            return new PagingList<AppProcess>()
            {
                Data = data,
                TotalItems = totalItems
            };
        }
    }
}