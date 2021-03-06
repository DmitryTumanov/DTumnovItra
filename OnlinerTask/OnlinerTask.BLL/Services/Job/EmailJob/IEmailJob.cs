﻿using System.Collections.Generic;
using OnlinerTask.Data.ScheduleModels;

namespace OnlinerTask.BLL.Services.Job.EmailJob
{
    public interface IEmailJob : IJobExecute
    {
        IEnumerable<UsersUpdateEmail> GetUsersUpdateEmails();
    }
}
