using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchoolChoicePlayground.Models;
using System.Collections.Generic;

namespace SchoolChoicePlayground.Tests
{
    [TestClass]
    public class SchoolTests
    {

        [TestMethod]
        public void SchoolEnsureICanCreateInstance()
        {
            School charter = new School();
            Assert.IsNotNull(charter);
        }

        [TestMethod]
        public void SchoolEnsureCharterCanHaveAllThings()
        {
            // Arrange
            School school = new School();
            school.name = "LEAD High School";
            school.grades = "9-12";
            school.address = new Address
            {
                Line1 = "1704 Heiman St",
                city = "Nashville",
                state = Address.State.TN,
                zip = "37208"
            };
            school.phoneNum = 615 - 555 - 5555;
            school.lat = 86.164893;
            school.lat = -37.164893;
            school.Users = new List<User>
            {
                new User { name = "Tom Griffey" },
                new User { name = "Random Parent" },
                new User { name = "Helicopter Mom" }
            };
            school.level = School.schoolLevel.High;

            // Assert 
            Assert.AreEqual("LEAD High School", school.name);
        }

        [TestMethod]
        public void SchoolEnsureSchoolHasUsers()
        {
            List<User> users = new List<User>
            {
                new User { name = "Tom Griffey" },
                new User { name = "Random Parent" },
                new User { name = "Helicopter Mom" }
            };

            School school = new School();
            school.name = "LEAD High School";
            school.Users = new List<User>
            {
                new User { name = "Tom Griffey" },
                new User { name = "Random Parent" },
                new User { name = "Helicopter Mom" }
            };

            Assert.AreEqual(school.Users[0].name, "Tom Griffey");
        }
    }
}
