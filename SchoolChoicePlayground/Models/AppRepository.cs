using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolChoicePlayground.Models
{
    public class AppRepository
    {
        private AppContext _context;
        public AppContext Context { get { return _context; } }

        public AppRepository()
        {
            _context = new AppContext();
        }

        public AppRepository(AppContext a_context)
        {
            _context = a_context;
        }

        // Retrieve all school data - DbSet Schools

        public List<School> GetAllSchools()
        {
            var query = from school in _context.Schools select school;
            List<School> all_schools = query.ToList();
            all_schools.Sort();
            return all_schools;
        }

        public List<User> GetAllUsers()
        {
            var query = from users in _context.Users select users;
            List<User> all_users = query.ToList();
            return all_users;
        }

        // Retrieve specific user's data (profile, schools)
        public User GetUserById(string id)
        {
            var query = from user in _context.Users where user.UserId == id select user;
            return query.SingleOrDefault();
        }

        // Get specific school
        public School GetSchoolById(int id)
        {
            var query = from school in _context.Schools where school.SchoolId == id select school;
            if (query == null)
            {
                throw new InvalidOperationException();
            }
            return query.Single<School>();
        }

        public Address GetSchoolAddress(int id)
        {
            var query = from school in _context.Schools where school.SchoolId == id select school.address;
            return query.Single<Address>();
        }

        public void AddUserToContext(User user_to_add)
        {
            //if (user_to_add.email != null)
            //{
            //    CheckIfUserEmailExists(user_to_add.email);
            //}
            _context.Users.Add(user_to_add);
            _context.SaveChanges();
        }

        private void CheckIfUserEmailExists(string email)
        {
            var query = from user in _context.Users
                        where user.email == email
                        select user;
            if (query != null)
            {
                throw new FormatException();
            }
        }

        // Get User's schools
        public List<School> GetUserSchools(User currentUser)
        {
            var query = from u in _context.Users where u.UserId == currentUser.UserId select u;
            User found_user = query.Single<User>();
            found_user.userSchools.Sort();
            return found_user.userSchools;
        }

        // Add to specific user's schools
        // Does this method really work?
        public void AddSchoolToUserList(User user, School school)
        {
            // Update user's list of schools
            if (user.userSchools == null)
            {
                user.userSchools = new List<School>();
            }
            user.userSchools.Add(school);
            // Save changes in _context
            _context.SaveChanges();
        }

        // Delete from user's schools

        public void RemoveSchoolFromUserList(User currentUser, School schoolToRemove)
        {
            currentUser.userSchools.Remove(schoolToRemove);
            _context.SaveChanges();

        }

        // Update user's profile info, esp. alerts

        

        // Get all alerts. (Sort by dateTime not implemented yet.)

        public List<Alert> GetAllAlerts()
        {
            var query = from alerts in _context.Alerts select alerts;
            List<Alert> all_alerts = query.ToList();
            return all_alerts;
        }

        // Get alert by Id

        public Alert GetAlertById(int id)
        {
            var query = from alert in _context.Alerts
                        where alert.AlertId == id
                        select alert;
            var found_alert = query.Single<Alert>();
            return found_alert;
        }

        // Send alerts based on DateTime (should this method go here?)

        //public void SendAlert()
        //{
        //    string AccountSid = "ACfc8fce3d015bdb7d1fb437612aa9e1a9";
        //    string AuthToken = "f0424530d155f69015933ce371bc71a3";

        //    var twilio = new TwilioRestClient(AccountSid, AuthToken);
        //    var message = twilio.SendSmsMessage("+15088687169", "+15088687169", "Test", "");
        //}

    }
}