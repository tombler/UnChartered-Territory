using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchoolChoicePlayground.Models;
using Moq;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;

namespace SchoolChoicePlayground.Tests.Models
{
    [TestClass]
    public class AppRepoAlertTests
    {

        private Mock<SchoolAppContext> mock_context;
        private Mock<DbSet<Alert>> mock_alert_set;
        private AppRepository _repository;

        private void ConnectMocksToDataStore(IEnumerable<Alert> data_store)
        {
            var data_source = data_store.AsQueryable<Alert>();
            mock_alert_set.As<IQueryable<Alert>>().Setup(data => data.Provider).Returns(data_source.Provider);
            mock_alert_set.As<IQueryable<Alert>>().Setup(data => data.Expression).Returns(data_source.Expression);
            mock_alert_set.As<IQueryable<Alert>>().Setup(data => data.ElementType).Returns(data_source.ElementType);
            mock_alert_set.As<IQueryable<Alert>>().Setup(data => data.GetEnumerator()).Returns(data_source.GetEnumerator());

            mock_context.Setup(a => a.Alerts).Returns(mock_alert_set.Object);
        }

        [TestInitialize]
        public void Initialize()
        {
            mock_context = new Mock<SchoolAppContext>();
            mock_alert_set = new Mock<DbSet<Alert>>();
            _repository = new AppRepository(mock_context.Object);
        }

        [TestCleanup]
        public void Cleanup()
        {
            mock_context = null;
            mock_alert_set = null;
            _repository = null;
        }

        [TestMethod]
        public void AlertCanCreateAnAlert()
        {
            DateTime currentTime = DateTime.Now;
            Alert signUp = new Alert { message = "You subscribed to alerts.", dateToSend = currentTime };
            Assert.IsNotNull(signUp);
            Assert.AreEqual(signUp.dateToSend, currentTime);
        }

        [TestMethod]
        public void AlertCanGetAllAlerts()
        {
            DateTime currentTime = DateTime.Now;
            List<Alert> alerts = new List<Alert> {
                new Alert { message = "School start.", dateToSend = currentTime },
                new Alert { message = "Application deadline.", dateToSend = new DateTime(2015, 12, 28) }
            };
            mock_alert_set.Object.AddRange(alerts);
            ConnectMocksToDataStore(alerts);
            List<Alert> expected_alerts = _repository.GetAllAlerts();
            Assert.AreEqual(2, expected_alerts.Count());
            Assert.AreEqual(expected_alerts.First().dateToSend, currentTime);
        }

        [TestMethod]
        public void AppRepoCanGetAlertById()
        {
            DateTime currentTime = DateTime.Now;
            List<Alert> alerts = new List<Alert> {
                new Alert { AlertId = 123, message = "School start.", dateToSend = currentTime },
                new Alert { AlertId = 456, message = "Application deadline.", dateToSend = new DateTime(2015, 12, 28) }
            };
            mock_alert_set.Object.AddRange(alerts);
            ConnectMocksToDataStore(alerts);
            // Act
            Alert expected = _repository.GetAlertById(456);
            //Assert
            Assert.AreEqual(expected.message, "Application deadline.");
        }

        //[TestMethod]
        //public void AppRepoCanUseTwilio()
        //{
        //    _repository.SendAlert();
        //}
    }
}
