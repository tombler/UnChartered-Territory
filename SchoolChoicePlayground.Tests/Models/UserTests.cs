using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchoolChoicePlayground.Models;
using System.Collections.Generic;

namespace SchoolChoicePlayground.Tests.Models
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void UserHasAllProperties()
        {
            User new_user = new User();
            new_user.name = "Hockey Dad";
            new_user.phoneNum = "615 - 555 - 5555";
            new_user.email = "tgriffey@charter.net";
            new_user.alerts = true;
            Assert.IsNotNull(new_user);
            Assert.AreEqual(new_user.name, "Hockey Dad");
            Assert.IsTrue(new_user.alerts);
            Assert.AreEqual(new_user.phoneNum, "615 - 555 - 5555");
        }

        [TestMethod]
        public void UserCanHaveSchools()
        {
            List<School> schools = new List<School>
            {
                new School { name = "LEAD High School" },
                new School { name = "Republic Middle School" }
            };

            User new_user = new User();
            new_user.name = "Hockey Dad";
            new_user.userSchools = schools;
            CollectionAssert.AreEqual(new_user.userSchools, schools);
        }
    }
}
