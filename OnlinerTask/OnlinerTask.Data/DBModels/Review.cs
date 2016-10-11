namespace OnlinerTask.Data.DBModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

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
