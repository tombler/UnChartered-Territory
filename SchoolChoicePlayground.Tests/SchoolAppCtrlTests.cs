using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Data.Entity;
using SchoolChoicePlayground.Models;
using System.Collections.Generic;
using System.Linq;


namespace SchoolChoicePlayground.Tests
{
    [TestClass]
    public class SchoolAppCtrlTests
    {
        private Mock<SchoolChoicePlayground.Models.AppContext> mock_context;
        private Mock<DbSet<School>> mock_school_set;
        private Mock<DbSet<MyUser>> mock_user_set;
        private Mock<DbSet<ApplicationUser>> mock_app_user_set;
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

        private void ConnectMocksToDataStore(IEnumerable<MyUser> data_store)
        {
            var data_source = data_store.AsQueryable<MyUser>();
            mock_user_set.As<IQueryable<MyUser>>().Setup(data => data.Provider).Returns(data_source.Provider);
            mock_user_set.As<IQueryable<MyUser>>().Setup(data => data.Expression).Returns(data_source.Expression);
            mock_user_set.As<IQueryable<MyUser>>().Setup(data => data.ElementType).Returns(data_source.ElementType);
            mock_user_set.As<IQueryable<MyUser>>().Setup(data => data.GetEnumerator()).Returns(data_source.GetEnumerator());

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

        //private void ConnectMocksToDataStore(IEnumerable<User> data_store)
        //{
        //    var data_source = data_store.AsQueryable<User>();
        //   mock_app_user_set.As<IQueryable<User>>().Setup(data => data.Provider).Returns(data_source.Provider);
        //   mock_app_user_set.As<IQueryable<User>>().Setup(data => data.Expression).Returns(data_source.Expression);
        //   mock_app_user_set.As<IQueryable<User>>().Setup(data => data.ElementType).Returns(data_source.ElementType);
        //   mock_app_user_set.As<IQueryable<User>>().Setup(data => data.GetEnumerator()).Returns(data_source.GetEnumerator());

        //    mock_context.Setup(a => a.Users).Returns(mock_app_user_set.Object);
        //}

        private void AddMockUsersAndSchoolsToDb()
        {
            List<School> schools = new List<School> {
                new School { SchoolId = 123, name = "LEAD Academy" },
                new School { SchoolId = 456, name = "Rocketship Academy" },
                new School { SchoolId = 4326, name = "Brick Church Prep" }
            };
            mock_school_set.Object.AddRange(schools);
            ConnectMocksToDataStore(schools);

            var users = new List<MyUser>
            {
                new MyUser {UserId = 123, name = "Tom Griffey" },
                new MyUser {UserId = 456, name = "Soccer Mom"}
            };
            mock_user_set.Object.AddRange(users);
            ConnectMocksToDataStore(users);
        }

        [TestInitialize]
        public void Initialize()
        {
            mock_context = new Mock<SchoolChoicePlayground.Models.AppContext>();
            mock_school_set = new Mock<DbSet<School>>();
            mock_user_set = new Mock<DbSet<MyUser>>();
            //mock_app_user new Mock<ApplicationUser>>();
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
        public void CheckIfUserExists()
        {
            ApplicationUser real_user = new ApplicationUser();
            string id_to_check = real_user.Id;
            bool result = _repository.CheckIfUserExists(id_to_check);
            Assert.IsFalse(result);
        }

        //[TestMethod]
        //public void AddsNewUserIfNoneExists()
        //{
        //    ApplicationUser real_user = new ApplicationUser();
        //    real_user.Id = "123abc";
        //    real_user.Email = "tom@tom.com";
        //    real_user.PasswordHash = "abcdef123";
        //    List<ApplicationUser> app_users = new List<ApplicationUser>();
        //    app_users.Add(real_user);
        //    mock_app_user_set.Object.AddRange(app_users);
        //    ConnectMocksToDataStore(app_users);

        //    string id_to_check = real_user.Id;
        //    _repository.AddUserIfNoneExists(id_to_check);
        //    Assert.AreEqual(1, _repository.GetAllUsers().Count);
        //}
    }
}
