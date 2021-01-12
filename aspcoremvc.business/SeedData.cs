using System;
using System.Linq;
using System.Security.Claims;
using System.Collections.Generic;
using Business.Services;
using Business.Services.Interfaces;
using Common;
using Common.Dto;
using Data;
using Data.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Domain.User;
using Common.Enums;
using Microsoft.EntityFrameworkCore.Storage;

namespace Business
{
    public static class SeedData
    {

        public static void Initialize(AppDbContext context, IServiceProvider serviceProvider)
        {
            var conf = (IConfiguration)serviceProvider.GetService(typeof(IConfiguration));

            if (context.Database.ProviderName.IndexOf("InMemory") == -1 && bool.Parse(conf["AppConfig:AutoMigrate"]))
                context.Database.Migrate();


            var roleManager = (RoleManager<Role>)serviceProvider.GetService(typeof(RoleManager<Role>));
            var userManager = (UserManager<User>)serviceProvider.GetService(typeof(UserManager<User>));

            var pages = new List<Page>();

            if (!context.Pages.Any())
            {
                // Users
                pages.Add(new Page { TitleCode = "menu_UserOperations", PageLevel = 1, Id = 1, MenuIcon = "far fa-user", ParentId = null });
                pages.Add(new Page { TitleCode = "menu_Users", PageLevel = 2, Controller = "User", Action = "User", ParentId = 1 });
                pages.Add(new Page { TitleCode = "menu_Roles", PageLevel = 2, Controller = "User", Action = "Role", ParentId = 1 });
                pages.Add(new Page { TitleCode = "menu_ChangePassword", PageLevel = 1, Controller = "User", Action = "ChangePassword", ParentId = null, HideInMenu = true });

                // Admin
                pages.Add(new Page { TitleCode = "menu_Admin", PageLevel = 1, Id = 3, DisplayOrder = 4, MenuIcon = "fas fa-hammer", ParentId = null });
                pages.Add(new Page { TitleCode = "menu_AdminSettings", PageLevel = 2, Controller = "Admin", Action = "Settings", ParentId = 3 });
                pages.Add(new Page { TitleCode = "menu_SelectCustomer", PageLevel = 2, Controller = "Admin", Action = "SelectCustomer", ParentId = 3 });
                pages.Add(new Page { TitleCode = "menu_Packages", PageLevel = 2, Controller = "MembershipPackages", Action = "List", ParentId = 3 });
                pages.Add(new Page { TitleCode = "menu_PackageCustomer", PageLevel = 2, Controller = "MembershipPackages", Action = "Customers", ParentId = 3, HideInMenu = true });
                pages.Add(new Page { TitleCode = "menu_Outlook", PageLevel = 2, Controller = "Home", Action = "OutlookMailSettings", ParentId = 3 });
                pages.Add(new Page { TitleCode = "menu_CustomerReport", PageLevel = 2, Controller = "Admin", Action = "CustomerReport", ParentId = 3 });


                // Home
                pages.Add(new Page { TitleCode = "menu_HomePage", Controller = "Home", Action = "Index", PageLevel = 1, DisplayOrder = 1, MenuIcon = "fas fa-home", ParentId = null, HideInMenu = true });



                var mainPages = pages.Where(x => x.ParentId == null).ToList();

                foreach (var mainPage in mainPages)
                {
                    int oldPageId = mainPage.Id;
                    mainPage.Id = 0;
                    context.Pages.Add(mainPage);
                    context.SaveChanges();

                    pages.Remove(mainPage);

                    var subItems = pages.Where(x => x.ParentId == oldPageId).ToList();

                    if (subItems.Count > 0)
                    {
                        foreach (var subItem in subItems)
                        {
                            subItem.Id = 0;
                            subItem.ParentId = mainPage.Id;
                            context.Pages.Add(subItem);
                            pages.Remove(subItem);
                        }
                        context.SaveChanges();
                    }
                }
            }

            if (!roleManager.Roles.Any())
            {
                roleManager.CreateAsync(new Role { Name = "superAdmin", StartPageId = 1, IsActive = true, IsDeleted = false }).Wait();
                roleManager.CreateAsync(new Role { Name = "globalAdmin", StartPageId = 1, IsActive = true, IsDeleted = false }).Wait();
                roleManager.CreateAsync(new Role { Name = "employee", StartPageId = 1, IsActive = true, IsDeleted = false }).Wait();

                var pageList = context.Pages
                    .Where(x => !x.IsDeleted)
                    .ToList();

                #region Super Admin

                var superAdminRole = roleManager.Roles.First(p => p.Name == "superAdmin");
                foreach (var page in pageList)
                {
                    roleManager.AddClaimAsync(superAdminRole, new Claim("pid", page.Id.ToString())).Wait();
                }

                #endregion
                #region Global Admin

                var globalAdminRole = roleManager.Roles.First(p => p.Name == "globalAdmin");
                var superAdminMenuPage = pageList.First(c => c.TitleCode == "menu_Admin");
                var globalAdminPages = pageList
                    .Where(c => c.Id != superAdminMenuPage.Id && c.ParentId != superAdminMenuPage.Id)
                    .ToList();
                foreach (var page in globalAdminPages)
                {
                    roleManager.AddClaimAsync(globalAdminRole, new Claim("pid", page.Id.ToString())).Wait();
                }

                #endregion
                #region Employee
                var pageTitlesForEmployee = new List<string> { "menu_HomePage", "menu_UserOperations", "menu_Users", "menu_MainMenuDashboard", "menu_DashboardReport", "menu_Dashboard", "menu_ChangePassword" };
                var employeeRole = roleManager.Roles.First(p => p.Name == "employee");
                var employeeAdminPages = globalAdminPages
                    .Where(c => pageTitlesForEmployee.Contains(c.TitleCode))
                    .ToList();
                foreach (var page in employeeAdminPages)
                {
                    roleManager.AddClaimAsync(employeeRole, new Claim("pid", page.Id.ToString())).Wait();
                }

                #endregion

            }

            if (!userManager.Users.Any())
            {
                var defaultPassword = conf["DefaultUserSettings:Password"];
                AddUserAndAssignRole(userManager, "superadmin", "superAdmin", 1, defaultPassword);
                AddUserAndAssignRole(userManager, "globaladmin", "globalAdmin", 1, defaultPassword);
                AddUserAndAssignRole(userManager, "employee", "employee", 1, defaultPassword);
                AddUserAndAssignRole(userManager, "adminpanel", "adminpanel", 1, "m.12345");
            }



            Language languageTr;

            if (!context.Languages.Any())
            {
                languageTr = new Language()
                {
                    Code = "tr-TR",
                    Name = "Türkçe",
                    IsActive = true
                };

                Language languageEn = new Language()
                {
                    Code = "en-US",
                    Name = "English",
                    IsActive = true
                };

                context.Languages.Add(languageTr);
                context.Languages.Add(languageEn);
                context.SaveChanges();
            }
            else
            {
                languageTr = context.Languages.First(x => x.Code == "tr-TR");
            }


            //if (!context.EwsSettings.Any())
            //{
            //    context.EwsSettings.Add(new EwsSetting()
            //    {
            //        UserName = conf["EwsSettings:UserName"],
            //        Password = conf["EwsSettings:Password"],
            //        Url = conf["EwsSettings:Url"]
            //    });

            //    context.SaveChanges();
            //}

        }

        public static void AddUserAndAssignRole(UserManager<User> userManager, string userName, string role, int? companyId, string password)
        {

            var task3 = userManager.CreateAsync(new User
            {
                Email = $"{userName}@bilgeadam.com",
                UserName = userName,
                IsDeleted = false,
                IsActive = true,
                FirstName = userName,
                LastName = "",
                //LoginTypeId = (int)LoginTypes.Standart,
            }, password);

            task3.Wait();

            if (role == null)
                return;


            var task4 = userManager.FindByNameAsync(userName);
            task4.Wait();

            var task5 = userManager.AddToRoleAsync(task4.Result, role);
            task5.Wait();
        }



    }
}