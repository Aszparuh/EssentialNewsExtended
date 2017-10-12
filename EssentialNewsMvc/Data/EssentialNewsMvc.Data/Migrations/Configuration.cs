using EssentialNewsMvc.Data.Models;
using EssentialNewsMvc.Services.Infrastructure;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Hosting;

namespace EssentialNewsMvc.Data.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(EssentialNewsMvc.Data.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            if (!context.Users.Any())
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);

                // Add missing roles
                var role = roleManager.FindByName("Admin");
                if (role == null)
                {
                    role = new IdentityRole("Admin");
                    roleManager.Create(role);
                }

                // Create test users
                var user = userManager.FindByName("admin");
                if (user == null)
                {
                    string adminUserName = ConfigurationManager.AppSettings["AdminUserName"];
                    string adminEmail = ConfigurationManager.AppSettings["AdminEmail"];
                    string adminPassword = ConfigurationManager.AppSettings["Password"];
                    var newUser = new ApplicationUser()
                    {
                        UserName = adminUserName,
                        Email = adminEmail,
                        PhoneNumber = "5551234567",
                    };
                    userManager.Create(newUser, adminPassword);
                    userManager.SetLockoutEnabled(newUser.Id, false);
                    userManager.AddToRole(newUser.Id, "Admin");
                }
            }

            // seed Categories
            if (!context.Categories.Any())
            {
                var categoriesList = new List<Models.NewsCategory>()
                {
                    new NewsCategory() { Name = "Spors" },
                    new NewsCategory() { Name = "Crime" },
                    new NewsCategory() { Name = "Entertainment" }
                };

                foreach (var category in categoriesList)
                {
                    context.Categories.Add(category);
                }

                context.SaveChanges();
            }

            // seed regions
            if (!context.Regions.Any())
            {
                var regions = new List<Region>()
                {
                    new Region() { Name = "World" },
                    new Region() { Name = "Local" }
                };

                foreach (var region in regions)
                {
                    context.Regions.Add(region);
                }

                context.SaveChanges();
            }

            try
            {
                if (!context.NewsArticles.Any())
                {
                    var path = HostingEnvironment.MapPath(@"~/App_Data/Resources/Images/");
                    string adminUserName = ConfigurationManager.AppSettings["AdminUserName"];
                    var articlesToBeSeeded = new NewsArticle[10];
                    var sampleTitle = "Article Title";
                    var sampleContent = "Lorem ipsum dolor sit amet, quis elit quis nunc," +
                        " vulputate at sit lectus aliquam, libero vel. Egestas auctor ac" +
                        " mattis morbi nibh ipsum, habitasse luctus aliquam mauris turpis" +
                        " elit. Quisque et placerat. Rutrum augue elementum vitae elementum," +
                        " litora nunc, nunc urna mauris vestibulum metus lorem eu, ornare nunc" +
                        " tellus eget curabitur, felis vitae. Morbi ornare viverra. Commodo" +
                        " laoreet ligula maecenas a malesuada proin, ullamcorper neque wisi" +
                        " ut placerat, pellentesque nulla fermentum lectus massa ipsum amet.";

                    var subTitle = "Lorem ipsum dolor sit amet, quis elit quis nunc";
                    var categoriesCount = context.Categories.Count();
                    var adminId = context.Users.Where(x => x.UserName == adminUserName).First().Id;
                    var imageProcessService = new ImageProcessService();

                    for (int i = 0; i < articlesToBeSeeded.Length; i++)
                    {
                        articlesToBeSeeded[i] = new NewsArticle();
                        articlesToBeSeeded[i].Title = sampleTitle;
                        articlesToBeSeeded[i].Content = sampleContent;
                        articlesToBeSeeded[i].SampleContent = subTitle;
                        articlesToBeSeeded[i].ApplicationUserId = adminId;

                        var originalImageContent = File.ReadAllBytes(path + "image" + (i + 1) + ".jpg");
                        var thumbnailImageContent = imageProcessService.Resize(originalImageContent, 260, 180);
                        var qualityImageContent = imageProcessService.Resize(originalImageContent, 640, 360);
                        var asideThumbnailImageContent = imageProcessService.Resize(originalImageContent, 141, 106);

                        articlesToBeSeeded[i].Images.Add(new Image()
                        {
                            FileName = "image" + i,
                            ContentType = "jpg",
                            Content = originalImageContent,
                            Type = ImageType.Original
                        });

                        articlesToBeSeeded[i].Images.Add(new Image()
                        {
                            FileName = "image" + i,
                            ContentType = "jpg",
                            Content = thumbnailImageContent,
                            Type = ImageType.Thumbnail
                        });

                        articlesToBeSeeded[i].Images.Add(new Image()
                        {
                            FileName = "image" + i,
                            ContentType = "jpg",
                            Content = qualityImageContent,
                            Type = ImageType.Normal
                        });

                        articlesToBeSeeded[i].Images.Add(new Image()
                        {
                            FileName = "image" + i,
                            ContentType = "jpg",
                            Content = asideThumbnailImageContent,
                            Type = ImageType.AsideThumbnail
                        });

                        context.NewsArticles.Add(articlesToBeSeeded[i]);
                    }

                    context.SaveChanges();
                }
            }
            catch (DbEntityValidationException ex)
            {
                var sb = new StringBuilder();
                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                       "Entity Validation Failed - errors follow:\n" +
                       sb.ToString(), ex);
            }
        } 
    }
}
