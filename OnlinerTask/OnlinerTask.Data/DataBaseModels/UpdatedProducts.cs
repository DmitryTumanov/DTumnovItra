namespace OnlinerTask.Data.DBModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UpdatedProducts
    {
        public int Id { get; set; }

        public string UserEmail { get; set; }

        public int? ProductId { get; set; }

        public DateTime? TimeToSend { get; set; }

        public virtual Product Product { get; set; }
    }
}
