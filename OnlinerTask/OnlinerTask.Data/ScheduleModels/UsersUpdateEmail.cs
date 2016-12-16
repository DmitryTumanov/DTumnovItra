using System;

namespace OnlinerTask.Data.ScheduleModels
{
    public class UsersUpdateEmail
    {
        public string UserEmail { get; set; }
        public string ProductName { get; set; }
        public string Id { get; set; }
        public TimeSpan Time { get; set; }
    }
}
