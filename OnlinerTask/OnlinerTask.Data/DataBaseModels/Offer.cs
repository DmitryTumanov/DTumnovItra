namespace OnlinerTask.Data.DataBaseModels
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Offer")]
    public partial class Offer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int? Count { get; set; }

        public virtual Price Price { get; set; }
    }
}
