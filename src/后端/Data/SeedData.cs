using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using icechat.api.Models;
using icechat.api.Utils;

namespace icechat.api.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationDbContext>>()))
            {
                // Look for any movies.
                if (context.Users.Any())
                {
                    return;   // DB has been seeded
                }
                context.Users.AddRange(
                    new Users
                    {
                        Email = "1739600291@qq.com",
                        PasswordHash = SHAUtil.SHA256Encrypt("123456"),
                        UserName = "icefjl",
                        NickName = "icefjl",
                        CreatedTime = DateTime.Now,
                        UpdatedTime = DateTime.Now,
                        Role = SystemConstants.ROLE_USER,
                        Status = SystemConstants.STATUS_SUCCESS
                    },
                    new Users
                    {
                        Email = "1739600291@qq.com",
                        PasswordHash = SHAUtil.SHA256Encrypt("123456"),
                        UserName = "testky",
                        NickName = "testky",
                        CreatedTime = DateTime.Now,
                        UpdatedTime = DateTime.Now,
                        Role = SystemConstants.ROLE_USER,
                        Status = SystemConstants.STATUS_APPLY
                    },
                    new Users
                    {
                        Email = "1739600291@qq.com",
                        PasswordHash = SHAUtil.SHA256Encrypt("123456"),
                        UserName = "admin",
                        NickName = "admin",
                        CreatedTime = DateTime.Now,
                        UpdatedTime = DateTime.Now,
                        Role = SystemConstants.ROLE_ADMIN,
                        Status = SystemConstants.STATUS_SUCCESS
                    }
                );
                context.SaveChanges();
            }

            
        }
        public static void Reset(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<ApplicationDbContext>>()))
            {
                if (!context.Users.Any())
                {
                    return;   
                }
                for (int i = 0; i < context?.Users.Count(); i++)
                {
                    var user = context.Users.First();
                    context.Users.Remove(user);
                    context?.SaveChanges();
                }

                context?.SaveChanges();
            }
        }
    }
}
