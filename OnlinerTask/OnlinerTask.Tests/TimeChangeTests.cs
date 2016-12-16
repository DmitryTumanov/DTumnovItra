using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using OnlinerTask.BLL.Services.Notification;
using OnlinerTask.BLL.Services.TimeChange;
using OnlinerTask.BLL.Services.TimeChange.Implementations;
using OnlinerTask.Data.Repository;
using OnlinerTask.Data.Requests;

namespace OnlinerTask.Tests
{
    [TestClass]
    public class TimeChangeTests
    {
        private Mock<INotification> notificationMock;
        private Mock<ITimeServiceRepository> timeRepositoryMock;

        private static readonly object[] TestUserNamesAndTime =
        {
            new object[]
            {
                null,
                null
            },
            new object[] {
                DateTime.Now,
                null
            },
            new object[] {
                null,
                "test"
            },
            new object[] {
                DateTime.Now,
                "test"
            }
        };

        private ITimeChanger GetTimeChanger()
        {
            notificationMock = new Mock<INotification>();
            timeRepositoryMock = new Mock<ITimeServiceRepository>();
            return new TimeChanger(timeRepositoryMock.Object, notificationMock.Object);
        }

        [TestCaseSource(nameof(TestUserNamesAndTime))]
        public async Task ChangePersonalTime(DateTime time, string username)
        {
            var service = GetTimeChanger();
            var request = new TimeRequest(time);

            await service.ChangePersonalTime(request, username);

            timeRepositoryMock.Verify(mock => mock.ChangeSendEmailTimeAsync(request, username), Times.Once);
            notificationMock.Verify(mock => mock.ChangeSettings(request.Time), Times.Once);
        }
    }
}
