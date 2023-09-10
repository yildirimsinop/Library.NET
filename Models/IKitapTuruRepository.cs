namespace WebApplication1.Models
{
    public interface IKitapTuruRepository : IRepository<KitapTuru>
    {
        void Update (KitapTuru bookitem);
        void Save ();
    }
}
