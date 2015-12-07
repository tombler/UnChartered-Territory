using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchoolChoicePlayground.Models;

namespace SchoolChoicePlayground.Tests.Models
{
    [TestClass]
    public class AlertTests
    {
        [TestMethod]
        public void AlertCanCreateAnAlert()
        {
            DateTime currentTime = DateTime.Now;
            Alert signUp = new Alert { message = "You subscribed to alerts.", dateToSend = currentTime };
            Assert.IsNotNull(signUp);
            Assert.AreEqual(signUp.dateToSend, currentTime);
        }
    }
}
