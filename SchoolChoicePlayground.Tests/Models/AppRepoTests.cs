using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchoolChoicePlayground.Models;
using System.Data.Entity;
using Moq;
using System.Linq;

namespace SchoolChoicePlayground.Tests.Models
{
    [TestClass]
    public class AppRepoTests
    {
        private Mock<SchoolChoicePlayground.Models.AppContext> _context;
        private Mock<DbSet<School>> mock_school_set;
        private Mock<DbSet<User>> mock_user_set;
        private Mock<DbSet<Alert>> mock_alert_set;
        private Mock<DbSet<Address>> mock_address_set;
        private AppRepository _repository;

        
        
        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
