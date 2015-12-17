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
    public class UserApiController : ApiController
    {
        public AppRepository Repo { get; set; }

        public UserApiController() : base() // Inherits all props and methods from base SchoolAppController
        {
            Repo = new AppRepository();
        }

        // GET: api/UserApi
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

        // GET: api/UserApi/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/UserApi
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/UserApi/5
        public void Put(int id, MyUser updatedUser)
        {
            Repo.UpdateUserProfile(updatedUser);
        }

        // DELETE: api/UserApi/5
        public void Delete(int id)
        {
        }
    }
}
