using AppView.IRepository;
using AppView.Models;
using Microsoft.EntityFrameworkCore;

namespace AppView.AllRepository
{
    public class AllRepos<T> : IAllRepos<T> where T : class
    {
        AppDbContext _context;
        DbSet<T> _dbSet;
        public AllRepos()
        {
            _context = new AppDbContext();


        }
        public AllRepos(AppDbContext context, DbSet<T> dbSet)
        {
            _context = context;
            _dbSet = dbSet;
        }
        public bool Create(T obj)
        {
            try
            {
                _dbSet.Add(obj); // Thêm
                _context.SaveChanges();  // Lưu lại

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(T obj)
        {
            try
            {
                _dbSet.Remove(obj); // Thêm
                _context.SaveChanges();  // Lưu lại
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public ICollection<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T GetById(dynamic id)
        {
            return _dbSet.Find(id);
        }

        public bool Update(T obj)
        {
            try
            {
                _dbSet.Update(obj); // Thêm
                _context.SaveChanges();  // Lưu lại
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
