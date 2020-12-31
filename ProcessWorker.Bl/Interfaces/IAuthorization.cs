namespace ProcessWorker.Bl.Interfaces
{
    public interface IAuthorization
    {
        int? Authorize(string username, string password);
    }
}