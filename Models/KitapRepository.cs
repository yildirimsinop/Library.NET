using WebApplication1.Utility;

namespace WebApplication1.Models
{
    public class KitapRepository : Repository<Kitap>, IKitapRepository
    {
        private EventDbContext _dbContext;
        public KitapRepository(EventDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Kitap kitap)
        {
            _dbContext.Add(kitap);
        }

        public void Sil(Kitap kitap)
        {
            _dbContext.Remove(kitap);
        }

        public void Guncelle(Kitap kitap)
        {
            _dbContext.Update(kitap);
        }

        public void Kaydet()
        {
            _dbContext.SaveChanges();
        }


    }
}

