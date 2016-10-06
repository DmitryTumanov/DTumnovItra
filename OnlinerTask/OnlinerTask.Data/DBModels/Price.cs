namespace OnlinerTask.Data.DBModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Price")]
    public partial class Price
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Price()
        {
            Products = new HashSet<Product>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int? PriceMinId { get; set; }

        public int? PriceMaxId { get; set; }

        public string HtmlUrl { get; set; }

        public int? OfferId { get; set; }

        public virtual Offer Offer { get; set; }

        public virtual PriceAmmount PriceAmmount { get; set; }

        public virtual PriceAmmount PriceAmmount1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }
    }
}
