using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SchoolChoicePlayground.Models;

namespace SchoolChoicePlayground.Controllers
{
    public class SchoolApiController : ApiController
    {
        // GET: api/SchoolApi
        public IEnumerable<School> Get()
        {
            AppRepository repo = new AppRepository();
            List<School> all_schools = repo.GetAllSchools();
            return all_schools;
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
