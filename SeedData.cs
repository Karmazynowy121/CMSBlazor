using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cms.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Cms.Web
{
    public static class SeedData
    {
        public static async Task<IHost> Seed(this IHost host)
        {

            try 
            {
                using (var servicesScoped = host.Services.CreateScope())
                {
                    var userManager = servicesScoped.ServiceProvider.GetService<UserManager<User>>();

                    var adminUser = await userManager.FindByNameAsync("admin");
                    if (adminUser is null)
                    {
                        var user = new User()
                        {
                            UserName = "admin",
                            Email = "admin@admin.com",
                            FullName = "administrator"
                        };

                        var result = await userManager.CreateAsync(user, "Admin1.");
                    }
                }

                return host;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}