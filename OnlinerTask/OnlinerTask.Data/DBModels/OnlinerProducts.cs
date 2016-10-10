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
            modelBuilder.Entity<Price>()
                .HasOptional(e => e.Offer)
                .WithRequired(e => e.Price)
                .WillCascadeOnDelete();

            modelBuilder.Entity<PriceAmmount>()
                .HasMany(e => e.Prices)
                .WithOptional(e => e.PriceMinAmmount)
                .HasForeignKey(e => e.PriceMinId);

            modelBuilder.Entity<PriceAmmount>()
                .HasMany(e => e.Prices1)
                .WithOptional(e => e.PriceMaxAmmount)
                .HasForeignKey(e => e.PriceMaxId);

            modelBuilder.Entity<Product>()
                .HasOptional(e => e.Image)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Product>()
                .HasOptional(e => e.Price)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Product>()
                .HasOptional(e => e.Review)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete();
        }
    }
}
