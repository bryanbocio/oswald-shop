using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser()
                {
                    DisplayName = "Bryan",
                    Email = "bbocio@test.com",
                    UserName = "bbocio@test.com",
                    Address = new Address
                    {
                        FirstName ="Bryan",
                        LastName = "Bocio",
                        Street = "street 4th, #289 Los mameyes, SDE",
                        City = "Santo Domingo Este",
                        State = "Santo Domingo Este",
                        ZipCode ="10023"
                    }
                };
                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }
    }
}
