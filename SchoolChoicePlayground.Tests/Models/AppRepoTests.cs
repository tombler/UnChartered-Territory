using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchoolChoicePlayground.Models;
using System.Data.Entity;
using Moq;
using System.Linq;
using System.Collections.Generic;

namespace SchoolChoicePlayground.Tests.Models
{
    [TestClass]
    public class AppRepoTests
    {
        private Mock<SchoolChoicePlayground.Models.AppContext> mock_context;
        private Mock<DbSet<School>> mock_school_set;
        private Mock<DbSet<User>> mock_user_set;
        private Mock<DbSet<Alert>> mock_alert_set;
        private Mock<DbSet<Address>> mock_address_set;
        private AppRepository _repository;

        private void ConnectMocksToDataStore(IEnumerable<School> data_store)
        {
            var data_source = data_store.AsQueryable<School>();
            // var data_source = (data_store as IEnumerable<School>).AsQueryable();
            // Convince LINQ that our Mock DbSet is a (relational) Data store.
            mock_school_set.As<IQueryable<School>>().Setup(data => data.Provider).Returns(data_source.Provider);
            mock_school_set.As<IQueryable<School>>().Setup(data => data.Expression).Returns(data_source.Expression);
            mock_school_set.As<IQueryable<School>>().Setup(data => data.ElementType).Returns(data_source.ElementType);
            mock_school_set.As<IQueryable<School>>().Setup(data => data.GetEnumerator()).Returns(data_source.GetEnumerator());

            // This is Stubbing the Schools property getter
            mock_context.Setup(a => a.Schools).Returns(mock_school_set.Object);
        }

        private void ConnectMocksToDataStore(IEnumerable<User> data_store)
        {
            var data_source = data_store.AsQueryable<User>();
            mock_user_set.As<IQueryable<User>>().Setup(data => data.Provider).Returns(data_source.Provider);
            mock_user_set.As<IQueryable<User>>().Setup(data => data.Expression).Returns(data_source.Expression);
            mock_user_set.As<IQueryable<User>>().Setup(data => data.ElementType).Returns(data_source.ElementType);
            mock_user_set.As<IQueryable<User>>().Setup(data => data.GetEnumerator()).Returns(data_source.GetEnumerator());
      

            mock_context.Setup(a => a.SchoolUsers).Returns(mock_user_set.Object);
        }

        private void ConnectMocksToDataStore(IEnumerable<Address> data_store)
        {
            var data_source = data_store.AsQueryable<Address>();
            mock_address_set.As<IQueryable<Address>>().Setup(data => data.Provider).Returns(data_source.Provider);
            mock_address_set.As<IQueryable<Address>>().Setup(data => data.Expression).Returns(data_source.Expression);
            mock_address_set.As<IQueryable<Address>>().Setup(data => data.ElementType).Returns(data_source.ElementType);
            mock_address_set.As<IQueryable<Address>>().Setup(data => data.GetEnumerator()).Returns(data_source.GetEnumerator());

            mock_context.Setup(a => a.Addresses).Returns(mock_address_set.Object);
        }

        private void AddMockUsersAndSchoolsToDb()
        {
            List<School> schools = new List<School> {
                new School { SchoolId = 123, name = "LEAD Academy" },
                new School { SchoolId = 456, name = "Rocketship Academy" },
                new School { SchoolId = 4326, name = "Brick Church Prep" }
            };
            mock_school_set.Object.AddRange(schools);
            ConnectMocksToDataStore(schools);

            var users = new List<User>
            {
                new User {UserId = 123, name = "Tom Griffey" },
                new User {UserId = 456, name = "Soccer Mom"}
            };
            mock_user_set.Object.AddRange(users);
            ConnectMocksToDataStore(users);
            mock_user_set.Setup(j => j.Add(It.IsAny<User>())).Callback((User s) => users.Add(s));
        }

        [TestInitialize]
        public void Initialize()
        {
            mock_context = new Mock<SchoolChoicePlayground.Models.AppContext>();
            mock_school_set = new Mock<DbSet<School>>();
            mock_user_set = new Mock<DbSet<User>>();
            _repository = new AppRepository(mock_context.Object);
        }

        [TestCleanup]
        public void Cleanup()
        {
            mock_context = null;
            mock_school_set = null;
            mock_user_set = null;
            _repository = null;
        }

        [TestMethod]
        public void SchoolAppContextEnsureICanCreateInstance()
        {
            SchoolChoicePlayground.Models.AppContext context = mock_context.Object;
            Assert.IsNotNull(context);
        }

        [TestMethod]
        public void AppRepoEnsureICanCreatInstance()
        {
            Assert.IsNotNull(_repository);
        }

        [TestMethod]
        public void AppRepoEnsureICanGetAllSchools()
        {
            // Arrange
            var expected = new List<School>
            {
                new School {name = "LEAD Academy" },
                new School {name = "Rocketship Academy" },
                new School {name = "Brick Church Prep" }
            };
            mock_school_set.Object.AddRange(expected);
            ConnectMocksToDataStore(expected);
            // Act
            var actual = _repository.GetAllSchools();
            // Assert
            Assert.AreEqual("Brick Church Prep", actual.First().name); // Sorted
        }

        [TestMethod]
        public void AppRepoGetAllUsers()
        {
            AddMockUsersAndSchoolsToDb();
            var all_users = _repository.GetAllUsers();
            Assert.AreEqual(all_users.Count(), 2);
        }

        [TestMethod]
        public void AppRepoCanAddUser()
        {

            AddMockUsersAndSchoolsToDb();
            var newUser = new User { name = "Deliquent Parent" };
            _repository.AddUserToContext(newUser);
            List<User> all_users = _repository.GetAllUsers();
            Assert.AreEqual(3, all_users.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void AppRepoEnsureNoDuplicateEmails()
        {
            var users = new List<User>();
            User first_user = new User
            {
                UserId = 999,
                name = "Tom",
                email = "tgriffey@charter.net"
            };

            User second_user = new User
            {
                UserId = 434,
                name = "Joe",
                email = "tgriffey@charter.net"
            };

            mock_user_set.Object.AddRange(users);
            ConnectMocksToDataStore(users);

            _repository.AddUserToContext(first_user);
            _repository.AddUserToContext(second_user);
        }

        [TestMethod]
        public void AppRepoGetUserById()
        {
            // Arrange
            var expected = new List<User>
            {
                new User {UserId = 123, name = "Tom Griffey" },
                 new User {UserId = 456, name = "Soccer Mom" }
            };
            mock_user_set.Object.AddRange(expected);
            ConnectMocksToDataStore(expected);
            // Act
            var actual = _repository.GetUserById(456);
            // Assert
            Assert.AreEqual("Soccer Mom", actual.name);
        }

        [TestMethod]
        public void AppRepoCanGetAllSchoolsForUser() // Returned list should be sorted
        {
            //Arrange
            AddMockUsersAndSchoolsToDb();
            var currentUser = _repository.GetUserById(123);
            currentUser.userSchools = new List<School> {
                new School { SchoolId = 123, name = "LEAD Academy" },
                new School { SchoolId = 456, name = "Rocketship Academy" },
                new School { SchoolId = 789, name = "Brick Church Prep" }
            };

            List<School> currentUserSchools = _repository.GetUserSchools(currentUser);
            Assert.AreEqual(3, currentUserSchools.Count());
            Assert.AreEqual("Brick Church Prep", currentUserSchools.First().name);
        }

        [TestMethod]
        public void AppRepoCanGetSchoolById()
        {
            //Arrange
            List<School> schools = new List<School> {
                new School { SchoolId = 123, name = "LEAD Academy" },
                new School { SchoolId = 456, name = "Rocketship Academy" }
            };
            mock_school_set.Object.AddRange(schools);
            ConnectMocksToDataStore(schools);
            //Act
            School expected = _repository.GetSchoolById(456);
            //Assert
            Assert.AreEqual(expected.name, "Rocketship Academy");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AppRepoGetSchoolByIdFailsIfIdDoesntExist()
        {
            //Arrange
            List<School> schools = new List<School> {
                new School { SchoolId = 123, name = "LEAD Academy" },
                new School { SchoolId = 456, name = "Rocketship Academy" }
            };
            mock_school_set.Object.AddRange(schools);
            ConnectMocksToDataStore(schools);
            //Act
            School expected = _repository.GetSchoolById(8882);
        }

        [TestMethod]
        public void AppRepoUserCanAddSchoolsEmptyList()
        {
            //Arrange
            AddMockUsersAndSchoolsToDb();
            var currentUser = _repository.GetUserById(123); // User has no schools in their list
            var schoolToAdd = _repository.GetSchoolById(456);
            //mock_user_set.Setup(u => u.).Add(It.IsAny<School>()).Callback((School s) => expectedUserSchools.Add(s));
            //Act
            _repository.AddSchoolToUserList(currentUser, schoolToAdd);
            //Assert
            Assert.AreEqual(1, currentUser.userSchools.Count());
            Assert.IsInstanceOfType(currentUser.userSchools.First(), typeof(School));
            // Why does this fail / how does Mock db store List of userSchools?
            //CollectionAssert.AreEqual(expectedUserSchools, currentUser.userSchools);
        }

        [TestMethod]
        public void AppRepoUserCanAddSchoolToExistingList()
        {
            //Arrange
            AddMockUsersAndSchoolsToDb();
            var currentUser = _repository.GetUserById(456);
            currentUser.userSchools = new List<School> {
                new School { SchoolId = 123, name = "LEAD Academy" },
                new School { SchoolId = 456, name = "Rocketship Academy" }
            };
            var schoolToAdd = _repository.GetSchoolById(4326);
            //Act
            _repository.AddSchoolToUserList(currentUser, schoolToAdd);
            //Assert
            Assert.AreEqual(3, currentUser.userSchools.Count());
        }

        [TestMethod]
        public void AppRepoCanUpdateUserProfileEmptyFields()
        {
            AddMockUsersAndSchoolsToDb();
            var currentUser = _repository.GetUserById(456); // only has name
            currentUser.phoneNum = "555-555-5555";
            currentUser.email = "tgriffey@charter.net";
            //_repository.UpdateUserProfile();
        }

        [TestMethod]
        public void AppRepoUserCanAddMultipleSchools()
        {
            AddMockUsersAndSchoolsToDb();
            var currentUser = _repository.GetUserById(123);
            var schoolToAdd1 = _repository.GetSchoolById(456);
            var schoolToAdd2 = _repository.GetSchoolById(123);
            _repository.AddSchoolToUserList(currentUser, schoolToAdd1);
            _repository.AddSchoolToUserList(currentUser, schoolToAdd2);

            Assert.AreEqual(2, currentUser.userSchools.Count());
        }

        [TestMethod]
        public void AppRepoUserCanRemoveSchoolFromList()
        {
            AddMockUsersAndSchoolsToDb();
            var currentUser = _repository.GetUserById(123);
            var schoolToAdd1 = _repository.GetSchoolById(456);
            var schoolToAdd2 = _repository.GetSchoolById(123);
            _repository.AddSchoolToUserList(currentUser, schoolToAdd1);
            _repository.AddSchoolToUserList(currentUser, schoolToAdd2);
            School schoolToRemove = _repository.GetSchoolById(123);
            //Act
            _repository.RemoveSchoolFromUserList(currentUser, schoolToRemove);
            //Assert
            Assert.AreEqual(1, currentUser.userSchools.Count());
        }

        [TestMethod]
        public void AppRepoCanRetrieveAddressOfSchool()
        {
            List<School> schools = new List<School> {
                new School { SchoolId = 123,
                    name = "LEAD Academy",
                    address = new Address { Line1 = "1704 Herman St", city = "Nashville", state = Address.State.TN, zip = "37208" }
                },
                new School { SchoolId = 456, name = "Rocketship Academy" }
            };
            mock_school_set.Object.AddRange(schools);
            ConnectMocksToDataStore(schools);


            Address expected = _repository.GetSchoolAddress(123);
            Assert.AreEqual(expected.Line1, "1704 Herman St");
        }

    }
}
