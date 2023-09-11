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
            _dbContext.Kitaplar.Include(k => k.KitapTuru).Include(k=>k.KitapTuruId);
        }

      
        public T Get(Expression<Func<T, bool>> filtre, string? includeProps = null)
        {
            IQueryable<T> sorgu = dbSet;
            sorgu = sorgu.Where(filtre);
            if (!string.IsNullOrEmpty(includeProps))
            {
                foreach (var includeProp in includeProps.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    sorgu = sorgu.Include(includeProp);
                }
            }
            return sorgu.FirstOrDefault();
        }
        public IEnumerable<T> GetAll(string? includeProps = null)
        {
            IQueryable<T> sorgu = dbSet;

            if (!string.IsNullOrEmpty(includeProps))
            {
                foreach (var includeProp in includeProps.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    sorgu = sorgu.Include(includeProp);
                }
            }
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
