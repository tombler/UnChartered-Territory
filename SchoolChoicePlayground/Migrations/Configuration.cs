namespace SchoolChoicePlayground.Migrations
{
    using Newtonsoft.Json.Linq;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SchoolChoicePlayground.Models.AppContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SchoolChoicePlayground.Models.AppContext context)
        {
            //JObject o1 = JObject.Parse(File.ReadAllText(@"C:\Users\Tom\Documents\GitHubVisualStudio\SchoolChoicePlayground\SchoolChoicePlayground\App_Data\charterSchoolsFinalData.json"));
            //JArray all_schools = (JArray)o1["charterSchoolData"];
            //List<School> all_schools_cs = all_schools.ToObject<List<School>>();
            //List<Address> all_school_addresses = all_schools.ToObject<List<Address>>();

            //for (int i = 0; i < all_schools_cs.Count; i++)
            //{
            //    context.Schools.AddOrUpdate(new School
            //    {
            //        name = all_schools_cs[i].name,
            //        grades = all_schools_cs[i].grades,
            //        address = new Address
            //        {
            //            Line1 = all_school_addresses[i].Line1,
            //            city = all_school_addresses[i].city,
            //            state = all_school_addresses[i].state,
            //            zip = all_school_addresses[i].zip
            //        },
            //        phoneNum = all_schools_cs[i].phoneNum,
            //        lat = all_schools_cs[i].lat,
            //        lng = all_schools_cs[i].lng,
            //        level = all_schools_cs[i].level,
            //        type = all_schools_cs[i].type,
            //        website = all_schools_cs[i].website
            //    });
            //}

            //context.SaveChanges();
        }
    }
}
