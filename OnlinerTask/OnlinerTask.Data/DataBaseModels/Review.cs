namespace OnlinerTask.Data.DataBaseModels
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Review")]
    public partial class Review
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int? Rating { get; set; }

        public int? Count { get; set; }

        public string HtmlUrl { get; set; }

        public virtual Product Product { get; set; }
    }
}
