namespace OnlinerTask.Data.DataBaseModels
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

        public TimeSpan? TimeToSend { get; set; }

        public virtual Product Product { get; set; }
    }
}
