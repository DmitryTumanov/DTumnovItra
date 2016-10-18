namespace OnlinerTask.Data.DataBaseModels
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("DisturbSchedule")]
    public partial class DisturbSchedule
    {
        public int Id { get; set; }

        public string UserEmail { get; set; }

        public DateTime? TimeComplete { get; set; }
    }
}
