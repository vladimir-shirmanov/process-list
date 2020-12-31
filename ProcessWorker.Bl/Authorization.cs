using System.Linq;
using ProcessWorker.Bl.Interfaces;
using ProcessWorker.Entity;

namespace ProcessWorker.Bl
{
    public class Authorization : IAuthorization
    {
        private readonly IRepository _repository;

        public Authorization(IRepository repository)
        {
            _repository = repository;
        }

        public int? Authorize(string username, string password)
        {
            var user = _repository.GetAll<User>().FirstOrDefault(u => u.UserName == username);
            if (user != null)
            {
                return user.Password == password ? user.Id : null;
            }

            return _repository.Create(new User {UserName = username, Password = password});;
        }
    }
}