using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageManager.EntityFramework.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationContext>>()))
            {
                if (context.Users.Any())
                {
                    return;   // alredy exists
                }

                context.Users.AddRange(
                    new User
                    {
                        Login = "admin",
                        Password = "admin",
                        Email="admin@mail.com",
                        Role = UserRoles.Admin
                    },
                    new User
                    {
                        Login = "user",
                        Password = "user",
                        Email = "user@mail.com",
                        Role = UserRoles.RegularUser
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
