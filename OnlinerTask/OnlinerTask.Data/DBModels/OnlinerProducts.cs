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

        public virtual DbSet<Image> Image { get; set; }
        public virtual DbSet<Offer> Offer { get; set; }
        public virtual DbSet<Price> Price { get; set; }
        public virtual DbSet<PriceAmmount> PriceAmmount { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Review> Review { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Price>()
                .HasOptional(e => e.Offer)
                .WithRequired(e => e.Price)
                .WillCascadeOnDelete();

            modelBuilder.Entity<PriceAmmount>()
                .HasMany(e => e.Price)
                .WithOptional(e => e.PriceAmmount)
                .HasForeignKey(e => e.PriceMinId);

            modelBuilder.Entity<PriceAmmount>()
                .HasMany(e => e.Price1)
                .WithOptional(e => e.PriceAmmount1)
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
