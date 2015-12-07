using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchoolChoicePlayground.Models;
using System.Text.RegularExpressions;

namespace SchoolChoicePlayground.Tests.Models
{
    [TestClass]
    public class AddressTests
    {
        [TestMethod]
        public void AddressCanCreateValidAddress()
        {
            Address schoolAddress = new Address();
            schoolAddress.Line1 = "1704 Heiman St";
            schoolAddress.city = "Nashville";
            schoolAddress.state = Address.State.TN;
            schoolAddress.zip = "37208";

            Assert.AreEqual(schoolAddress.Line1, "1704 Heiman St");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void AddressInvalidZipTooLong()
        {
            Address schoolAddress = new Address();
            schoolAddress.Line1 = "1704 Heiman St";
            schoolAddress.city = "Nashville";
            schoolAddress.state = Address.State.TN;
            schoolAddress.zip = "3720334";
            schoolAddress.Validate();  
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void AddressInvalidZipTooShort()
        {
            Address schoolAddress = new Address();
            schoolAddress.Line1 = "1704 Heiman St";
            schoolAddress.city = "Nashville";
            schoolAddress.state = Address.State.TN;
            schoolAddress.zip = "374";
            schoolAddress.Validate();       
        }

        [TestMethod]
        public void AddressValidZip9Digits()
        {
            Address schoolAddress = new Address();
            schoolAddress.Line1 = "1704 Heiman St";
            schoolAddress.city = "Nashville";
            schoolAddress.state = Address.State.TN;
            schoolAddress.zip = "37208-3434";
            schoolAddress.Validate();
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void AddressInvalidCityFormat()
        {
            Address schoolAddress = new Address();
            schoolAddress.Line1 = "1704 Heiman St";
            schoolAddress.city = "nashville";
            schoolAddress.state = Address.State.TN;
            schoolAddress.zip = "3720334";
            schoolAddress.Validate();
        }

        [TestMethod]
        public void AddressValidCityFormat()
        {
            Address schoolAddress = new Address();
            schoolAddress.Line1 = "1704 Heiman St";
            schoolAddress.city = "Nashville";
            schoolAddress.state = Address.State.TN;
            schoolAddress.zip = "37208";
            schoolAddress.Validate();
        }

        [TestMethod]
        public void MyTestMethod()
        {

        }

    }
}
