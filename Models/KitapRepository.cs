using WebApplication1.Utility;

namespace WebApplication1.Models
{
    public class KiralamaRepository : Repository<Kiralama>, IKiralamaRepository
    {
        private EventDbContext _dbContext;
        public KiralamaRepository(EventDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void Guncelle(Kiralama Kiralama)
        {
            _dbContext.Update(Kiralama);
        }

        public void Kaydet()
        {
            _dbContext.SaveChanges();
        }   

       

    }
}

