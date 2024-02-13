using Task01.Model.Data;

namespace Task01.Model.Accsess
{
    public class Session
    {
        public User User { get; }
        public Repository Repository { get; }
        public DateTime Start { get; }
        public DateTime? End { get; set; }
        public Session(User user, Repository repository)
        {
            User = user;
            Repository = repository;
            Start = DateTime.Now;
        }
    }
}
