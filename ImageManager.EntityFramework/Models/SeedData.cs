using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ImageManager.EntityFramework.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            //using (var context = new ApplicationContext(
            //    serviceProvider.GetRequiredService<
            //        DbContextOptions<ApplicationContext>>()))
            //{
            //    if (context.Users.Any())
            //    {
            //        return;   // alredy exists
            //    }
            //    //var user = Hashing.HashPassword("user");
            //    //var admin = Hashing.HashPassword("admin");
            //    context.Users.AddRange(
            //        new User
            //        {
            //            Login = "admin",
            //            Password = admin.HashedPassword,
            //            Salt = admin.Salt,
            //            Email ="admin@mail.com",
            //            Role = UserRoles.Admin
            //        },
            //        new User
            //        {
            //            Login = "user",
            //            Password = user.HashedPassword,
            //            Salt = user.Salt,
            //            Email = "user@mail.com",
            //            Role = UserRoles.RegularUser
            //        }
            //    );
            //    context.SaveChanges();
            //}
        }
    }
}
