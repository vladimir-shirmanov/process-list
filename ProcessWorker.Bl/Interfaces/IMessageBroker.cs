namespace ProcessWorker.Bl.Interfaces
{
    public interface IMessageBroker
    {
        void SendMessage(int processId);

        void ProcessMessage();
    }
}