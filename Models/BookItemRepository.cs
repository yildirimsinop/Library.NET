using WebApplication1.Utility;

namespace WebApplication1.Models
{
    public class BookItemRepository : Repository<KitapTuru>, IKitapTuruRepository
    {
        private EventDbContext _dbContext;
        public BookItemRepository(EventDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void Update(KitapTuru bookitem)
        {
            _dbContext.Update(bookitem);
        }
    }
}

