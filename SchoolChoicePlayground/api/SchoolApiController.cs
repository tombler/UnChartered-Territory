using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using SchoolChoicePlayground.Models;
using Newtonsoft.Json;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using SchoolChoicePlayground.api;

namespace SchoolChoicePlayground.Controllers
{
    public class SchoolApiController : ApiController
    {
        // GET: api/SchoolApi
        public string Get()
        {
            AppRepository repo = new AppRepository();
            //SchoolApplicationController ctrl = new SchoolApplicationController();
            //string user_id = ctrl.GetUserId();
            List<MyUser> all_users = repo.GetAllUsers();
            //List<School> all_schools = repo.GetAllSchools();
            string json = JsonConvert.SerializeObject(all_users, Formatting.Indented);
            bool result = User.Identity.IsAuthenticated;
            return json;
        }

        // GET: api/SchoolApi/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/SchoolApi
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/SchoolApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/SchoolApi/5
        public void Delete(int id)
        {
        }
    }
}
