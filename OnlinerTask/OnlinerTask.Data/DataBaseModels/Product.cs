namespace OnlinerTask.Data.DataBaseModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Product")]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            UpdatedProducts = new HashSet<UpdatedProducts>();
        }

        public int Id { get; set; }

        [Required]
        public string UserEmail { get; set; }

        public int? ProductId { get; set; }

        [StringLength(50)]
        public string ProductKey { get; set; }

        public string Name { get; set; }

        public string FullName { get; set; }

        public string Description { get; set; }

        public string HtmlUrl { get; set; }

        public string ReviewUrl { get; set; }

        public virtual Image Image { get; set; }

        public virtual Price Price { get; set; }

        public virtual Review Review { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UpdatedProducts> UpdatedProducts { get; set; }
    }
}
