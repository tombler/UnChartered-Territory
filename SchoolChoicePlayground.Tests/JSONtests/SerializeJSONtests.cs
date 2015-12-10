using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using SchoolChoicePlayground.Models;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace SchoolChoicePlayground.Tests.JSONtests
{
    [TestClass]
    public class SerializeJSONtests
    {
        [TestMethod]
        public void CanGrabJson()
        {
            JObject o1 = JObject.Parse(File.ReadAllText(@"C:\Users\Tom\Documents\GitHubVisualStudio\SchoolChoicePlayground\SchoolChoicePlayground\App_Data\charterSchoolsFinalData.json"));
            JArray all_schools = (JArray)o1["charterSchoolData"];
            List<School> all_schools_cs = all_schools.ToObject<List<School>>();
            List<Address> all_school_addresses = all_schools.ToObject<List<Address>>();
            Assert.IsNotNull(all_schools_cs);
            Assert.AreEqual(30, all_schools_cs.Count);
            School first = all_schools_cs[0];
            Address first_address = all_school_addresses[0];
            Assert.AreEqual("Brick Church College Prep* (LEAD PS)", first.name);
            Assert.AreEqual("5th-8th", first.grades);
            Assert.AreEqual("http://www.brickchurchcollegeprep.org/", first.website);
            Assert.AreEqual("615-806-6317", first.phoneNum);
            Assert.AreEqual(36.220244, first.lat);
            Assert.AreEqual(-86.7807823, first.lng);
            Assert.AreEqual(School.schoolLevel.Middle, first.level);
            Assert.AreEqual(School.schoolType.Charter, first.type);
            Assert.AreEqual(30, all_school_addresses.Count);
            Assert.AreEqual("2835 Brick Church Pike", first_address.Line1);
            Assert.AreEqual("Nashville", first_address.city);
            Assert.AreEqual(Address.State.TN, first_address.state);
            Assert.AreEqual("37207", first_address.zip);
            Assert.IsNull(first.addlInfo);
            Assert.IsNull(first.Users);
        }


    }
}
