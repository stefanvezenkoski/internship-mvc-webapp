using System;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using InternshipCompaniesManager.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace InternshipCompaniesManager
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            CreateRolesAndAdminUser();
        }

        private void CreateRolesAndAdminUser()
        {
            using (var context = new ApplicationDbContext())
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

                // ??????? ?? ??????? Admin ??? ?? ??????
                if (!roleManager.RoleExists("Admin"))
                {
                    roleManager.Create(new IdentityRole("Admin"));

                    // ??????? ? ????? ???????? ?? ?????? ???????
                    var adminUser = new ApplicationUser
                    {
                        UserName = "admin123@admin.com",
                        Email = "admin123@admin.com",
                        FullName = "?????????????"
                    };

                    string adminPassword = "Admin@123"; // ??????? ?? ????????? ?? ????? ?????????

                    var userResult = userManager.Create(adminUser, adminPassword);

                    if (userResult.Succeeded)
                    {
                        userManager.AddToRole(adminUser.Id, "Admin");
                    }
                }

                // ??????? ?? ??????? Student ??? ?? ??????
                if (!roleManager.RoleExists("Student"))
                {
                    roleManager.Create(new IdentityRole("Student"));
                }
            }
        }
    }
}
