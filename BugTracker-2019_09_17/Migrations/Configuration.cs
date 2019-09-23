namespace BugTracker_2019_09_17.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using BugTracker_2019_09_17.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<BugTracker_2019_09_17.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "BugTracker_2019_09_17.Models.ApplicationDbContext";
        }

        protected override void Seed(BugTracker_2019_09_17.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            if(!context.Roles.Any(r=> r.Name == "Admin"))
            {
                var role1 = new IdentityRole { Name = "Admin" };
                var role2 = new IdentityRole { Name = "ProjectManager" };
                var role3 = new IdentityRole { Name = "Developer" };
                var role4 = new IdentityRole { Name = "Submitter" };

                context.Roles.AddOrUpdate(role1, role2, role3, role4);

                context.SaveChanges();
            }

            if(!context.Users.Any(u => u.UserName == "Alex")){

                UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

                var user1 = new ApplicationUser { UserName = "Alex", Email = "Als@email.com" };
                var pw = "password";

                userManager.Create(user1, pw);
            }
        }
    }
}
