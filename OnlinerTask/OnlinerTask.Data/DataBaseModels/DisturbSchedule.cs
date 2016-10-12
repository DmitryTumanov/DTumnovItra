namespace OnlinerTask.Data.DataBaseModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DisturbSchedule")]
    public partial class DisturbSchedule
    {
        public int Id { get; set; }

        public string UserEmail { get; set; }

        public DateTime? TimeComplete { get; set; }
    }
}
