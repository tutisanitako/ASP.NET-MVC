using AcademicResourceManagement.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace AcademicResourceManagement.Persistence.Data
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // Create Roles
            string[] roleNames = { "Administrator", "Lecturer", "Student" };

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Create Administrator User
            var adminEmail = "admin@university.edu";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                var newAdmin = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FirstName = "System",
                    LastName = "Administrator",
                    EmailConfirmed = true,
                    CreatedDate = DateTime.UtcNow
                };

                var result = await userManager.CreateAsync(newAdmin, "Admin@123");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newAdmin, "Administrator");
                }
            }

            // Create Lecturer User
            var lecturerEmail = "lecturer@university.edu";
            var lecturerUser = await userManager.FindByEmailAsync(lecturerEmail);

            if (lecturerUser == null)
            {
                var newLecturer = new ApplicationUser
                {
                    UserName = lecturerEmail,
                    Email = lecturerEmail,
                    FirstName = "John",
                    LastName = "Lecturer",
                    EmailConfirmed = true,
                    CreatedDate = DateTime.UtcNow
                };

                var result = await userManager.CreateAsync(newLecturer, "Lecturer@123");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newLecturer, "Lecturer");
                }
            }

            // Create Student User
            var studentEmail = "student@university.edu";
            var studentUser = await userManager.FindByEmailAsync(studentEmail);

            if (studentUser == null)
            {
                var newStudent = new ApplicationUser
                {
                    UserName = studentEmail,
                    Email = studentEmail,
                    FirstName = "Jane",
                    LastName = "Student",
                    EmailConfirmed = true,
                    CreatedDate = DateTime.UtcNow
                };

                var result = await userManager.CreateAsync(newStudent, "Student@123");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newStudent, "Student");
                }
            }
        }
    }
}