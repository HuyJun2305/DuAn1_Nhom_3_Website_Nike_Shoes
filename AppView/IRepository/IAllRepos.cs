namespace AppView.IRepository
{
    public interface IAllRepos<T> where T : class
    {
        public ICollection<T> GetAll();
        public T GetById(dynamic id);
        public bool Create(T obj);
        public bool Update(T obj);
        public bool Delete(T obj);
    }
}
