using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchoolChoicePlayground.Models;

namespace SchoolChoicePlayground.Tests.Controllers
{
    [TestClass]
    public class SchoolAppControllerTests
    {
        [TestMethod]
        public void CanGetActualUsersFromDb()
        {
            bool result1 = System.Web.Security.Membership.DeleteUser("tgriffey9389@gmail.com", true);
            bool result2 = System.Web.Security.Membership.DeleteUser("thomas@thomas.com", true);
            bool result3 = System.Web.Security.Membership.DeleteUser("tom@tom.com", true);
            bool result4 = System.Web.Security.Membership.DeleteUser("tgriffey@charter.net", true);
            Assert.IsTrue(result1);
            Assert.IsTrue(result2);
            Assert.IsTrue(result3);
            Assert.IsTrue(result4);
        }
    }
}
