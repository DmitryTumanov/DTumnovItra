namespace OnlinerTask.Data.DataBaseModels
{
    using System.ComponentModel.DataAnnotations.Schema;

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
