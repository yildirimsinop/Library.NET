using System.Linq.Expressions;

namespace WebApplication1.Models
{
    public interface IRepository<T> where T : class
    {

        // T -> Kitap Turu
        IEnumerable<T> GetAll(string? includeProps = null);
        T Get(Expression<Func<T, bool>> filtre, string? includeProps = null);
        void Ekle (T entity);
        void Sil (T entity);
        void SilRange(IEnumerable<T> entities);
    }
}
