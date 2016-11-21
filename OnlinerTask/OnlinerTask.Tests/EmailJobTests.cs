﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlinerTask.BLL.Services.Job.EmailJob;
using OnlinerTask.BLL.Services.Job.EmailJob.Implementations;
using OnlinerTask.Data.RedisManager;
using OnlinerTask.Data.ScheduleModels;

namespace OnlinerTask.Tests
{
    [TestClass]
    public class EmailJobTests
    {
        private Mock<IEmailManager> emailManagerMock;

        private readonly IEnumerable<UsersUpdateEmail> usersUpdateEmails = new List<UsersUpdateEmail>()
        {
            new UsersUpdateEmail()
            {
                Id = null, ProductName = null, Time = DateTime.Now.TimeOfDay, UserEmail = null
            },
            new UsersUpdateEmail()
            {
                Id = null, ProductName = null, Time = DateTime.Now.TimeOfDay, UserEmail = null
            },
            new UsersUpdateEmail()
            {
                Id = null, ProductName = null, Time = DateTime.Now.TimeOfDay, UserEmail = null
            }
        };

        public IEmailJob GetEmailJob()
        {
            emailManagerMock = new Mock<IEmailManager>();

            emailManagerMock.Setup(mock => mock.GetAll<UsersUpdateEmail>()).Returns(usersUpdateEmails);

            return new EmailJobService(emailManagerMock.Object);
        }

        [TestMethod]
        public void GetUsersUpdateEmails_NothingSend_ServiceCalled()
        {
            var jobExecutter = GetEmailJob();

            jobExecutter.GetUsersUpdateEmails();

            emailManagerMock.Verify(mock=>mock.GetAll<UsersUpdateEmail>(), Times.Once);
        }

        [TestMethod]
        public async Task Execute_NothingSend_ManagerSeveralTimesCalled()
        {
            var jobExecutter = GetEmailJob();

            await jobExecutter.Execute();
            var count = emailManagerMock.Object.GetAll<UsersUpdateEmail>().Count();

            emailManagerMock.Verify(mock => mock.SendMail(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(count));
        }
    }
}
