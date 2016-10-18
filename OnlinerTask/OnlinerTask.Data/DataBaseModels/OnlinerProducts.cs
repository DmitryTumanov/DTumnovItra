namespace OnlinerTask.Data.DataBaseModels
{
    using System.Data.Entity;

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
        public virtual DbSet<UpdatedProducts> UpdatedProducts { get; set; }
        public virtual DbSet<DisturbSchedule> DisturbSchedule { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Price>()
                .HasOptional(e => e.Offer)
                .WithRequired(e => e.Price)
                .WillCascadeOnDelete();

            modelBuilder.Entity<PriceAmmount>()
                .HasMany(e => e.Price)
                .WithOptional(e => e.PriceMaxAmmount)
                .HasForeignKey(e => e.PriceMaxId);

            modelBuilder.Entity<PriceAmmount>()
                .HasMany(e => e.Price1)
                .WithOptional(e => e.PriceMinAmmount)
                .HasForeignKey(e => e.PriceMinId);

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

            modelBuilder.Entity<Product>()
                .HasMany(e => e.UpdatedProducts)
                .WithOptional(e => e.Product)
                .WillCascadeOnDelete();
        }
    }
}
