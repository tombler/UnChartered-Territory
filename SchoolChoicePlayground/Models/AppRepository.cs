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

        // Retrieve specific user's data (profile, schools)

        // Add to specific user's schools

        // Delete from user's schools

        // Update user's data (profile)

    }
}