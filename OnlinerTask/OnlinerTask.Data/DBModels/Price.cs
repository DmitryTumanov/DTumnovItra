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
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int? PriceMinId { get; set; }

        public int? PriceMaxId { get; set; }

        public string HtmlUrl { get; set; }

        public virtual Offer Offer { get; set; }

        public virtual PriceAmmount PriceMinAmmount { get; set; }

        public virtual PriceAmmount PriceMaxAmmount { get; set; }

        public virtual Product Product { get; set; }
    }
}
