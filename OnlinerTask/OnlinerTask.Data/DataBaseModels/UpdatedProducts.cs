namespace OnlinerTask.Data.DataBaseModels
{
    using System;

    public partial class UpdatedProducts
    {
        public int Id { get; set; }

        public string UserEmail { get; set; }

        public int? ProductId { get; set; }

        public TimeSpan? TimeToSend { get; set; }

        public virtual Product Product { get; set; }
    }
}
