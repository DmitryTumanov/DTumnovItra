using System;

namespace OnlinerTask.Data.Requests
{
    public class TimeRequest
    {
        public TimeRequest() {}

        public TimeRequest(DateTime dateTime)
        {
            Time = dateTime;
        }

        public DateTime Time { get; set; }
    }
}
