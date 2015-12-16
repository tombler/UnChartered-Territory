using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SchoolChoicePlayground.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web;
using System.Web.Http;

namespace SchoolChoicePlayground.api
{
    public class BaseApiController : ApiController
    {
        private ApplicationUser _member;

        public ApplicationUserManager UserManager
        {
            get { return HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
        }

        public string UserIdentityId
        {
            get
            {
                var user = UserManager.FindByName(User.Identity.Name);
                return user.Id;
            }
        }

        public ApplicationUser UserRecord
        {
            get
            {
                if (_member != null)
                {
                    return _member;
                }
                _member = UserManager.FindByEmail(Thread.CurrentPrincipal.Identity.Name);
                return _member;
            }
            set { _member = value; }
        }
    }
}
