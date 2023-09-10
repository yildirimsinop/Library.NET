using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApplication1.Utility;

namespace WebApplication1.Models
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly EventDbContext _dbContext;
        internal DbSet<T> dbSet; // dbSet = _dbContext.BookItems

        public Repository(EventDbContext dbContext)
        {
            _dbContext = dbContext;
            this.dbSet = dbContext.Set<T>();
        }

      
        public T Get(Expression<Func<T, bool>> filtre)
        {
            IQueryable<T> sorgu = dbSet;
            sorgu = sorgu.Where(filtre);
            return sorgu.FirstOrDefault();
        }
        public IEnumerable<T> GetAll()
        {
            IQueryable<T> sorgu = dbSet;
            return sorgu.ToList();
        }
      
        public void Ekle(T entity)
        {
            _dbContext.Add(entity);
        }

        public void Sil(T entity)
        {
            _dbContext.Remove(entity);
        }

        public void SilRange(IEnumerable<T> entities)
        {
            _dbContext.RemoveRange(entities);
        }
        public void Kaydet()
        {
            _dbContext.SaveChanges();
        }





    }
}
