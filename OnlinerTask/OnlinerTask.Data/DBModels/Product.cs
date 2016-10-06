namespace OnlinerTask.Data.DBModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        public int Id { get; set; }

        [Required]
        public string UserEmail { get; set; }

        public int? ProductId { get; set; }

        [StringLength(50)]
        public string ProductKey { get; set; }

        public string Name { get; set; }

        public string FullName { get; set; }

        public int? ImageId { get; set; }

        public string Description { get; set; }

        public string HtmlUrl { get; set; }

        public int? ReviewId { get; set; }

        public string ReviewUrl { get; set; }

        public int? PriceId { get; set; }

        public virtual Image Image { get; set; }

        public virtual Price Price { get; set; }

        public virtual Review Review { get; set; }
    }
}
