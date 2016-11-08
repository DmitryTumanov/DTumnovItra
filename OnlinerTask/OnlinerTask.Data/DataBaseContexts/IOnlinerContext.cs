using System.Data.Entity;
using System.Threading.Tasks;
using OnlinerTask.Data.DataBaseModels;

namespace OnlinerTask.Data.DataBaseInterfaces
{
    public interface IOnlinerContext
    {
        DbSet<Image> Image { get; set; }
        DbSet<Offer> Offer { get; set; }
        DbSet<Price> Price { get; set; }
        DbSet<PriceAmmount> PriceAmmount { get; set; }
        DbSet<Product> Product { get; set; }
        DbSet<Review> Review { get; set; }
        DbSet<UpdatedProducts> UpdatedProducts { get; set; }
        DbSet<DisturbSchedule> DisturbSchedule { get; set; }

        void SaveChanges();
        Task SaveChangesAsync();
    }
}
