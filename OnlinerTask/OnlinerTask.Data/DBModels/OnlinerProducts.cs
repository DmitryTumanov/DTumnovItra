namespace OnlinerTask.Data.DBModels
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class OnlinerProducts : DbContext
    {
        public OnlinerProducts()
            : base("name=OnlinerProducts")
        {
        }

        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<Offer> Offers { get; set; }
        public virtual DbSet<Price> Prices { get; set; }
        public virtual DbSet<PriceAmmount> PriceAmmounts { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Image>()
                .HasMany(e => e.Products)
                .WithOptional(e => e.Image)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Offer>()
                .HasMany(e => e.Prices)
                .WithOptional(e => e.Offer)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Price>()
                .HasMany(e => e.Products)
                .WithOptional(e => e.Price)
                .WillCascadeOnDelete();

            modelBuilder.Entity<PriceAmmount>()
                .HasMany(e => e.Prices)
                .WithOptional(e => e.PriceAmmount)
                .HasForeignKey(e => e.PriceMinId);

            modelBuilder.Entity<PriceAmmount>()
                .HasMany(e => e.Prices1)
                .WithOptional(e => e.PriceAmmount1)
                .HasForeignKey(e => e.PriceMaxId);

            modelBuilder.Entity<Review>()
                .HasMany(e => e.Products)
                .WithOptional(e => e.Review)
                .WillCascadeOnDelete();
        }
    }
}
