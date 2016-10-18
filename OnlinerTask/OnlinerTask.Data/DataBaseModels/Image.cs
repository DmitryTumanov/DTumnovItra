namespace OnlinerTask.Data.DataBaseModels
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Image")]
    public partial class Image
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public string Header { get; set; }

        public virtual Product Product { get; set; }
    }
}
