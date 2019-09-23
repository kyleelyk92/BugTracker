using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker_2019_09_17.Models
{
    public class HelperClass
    {
        public static ApplicationDbContext GetDbContext()
        {
            var db = new ApplicationDbContext();
            return db;
        }
        public static ApplicationUserManager MakeUserManager()
        {
            var um = new ApplicationUserManager(new UserStore<ApplicationUser>(GetDbContext()));
            return um;
        }
        public static RoleManager<IdentityRole> MakeRoleManager()
        {
            var rm = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(GetDbContext()));
            return rm;
        }
    }
}