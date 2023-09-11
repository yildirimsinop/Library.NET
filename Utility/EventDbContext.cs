using Microsoft.EntityFrameworkCore;
using WebApplication1.Models; // BookItem modeli için
// Veri Tabaninda EF Tablo Olusturmasi icin ilgili model siniflarinizi buraya eklemelisiniz.

namespace WebApplication1.Utility
{
    public class EventDbContext : DbContext
    {
        public EventDbContext(DbContextOptions<EventDbContext> options) : base(options) { }

        public DbSet<KitapTuru> BookItems { get; set; }  // BookItem için DbSet tanımlaması
        public DbSet<Kitap> Kitaplar { get; set; }

        public DbSet<Kiralama> Kiralamalar { get; set; }
    }
}
