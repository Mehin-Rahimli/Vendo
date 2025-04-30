using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendo.Domain.Entities;
using Vendo.Domain.Enums;

namespace Vendo.Persistence.Contexts
{
    public class AppDbContextInitializer
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _userRole;
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _appDbContext;

        public AppDbContextInitializer(
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> userRole,
            IConfiguration configuration,
            AppDbContext appDbContext)
        {
            _userManager = userManager;
            _userRole = userRole;
            _configuration = configuration;
            _appDbContext = appDbContext;
        }

        public async Task InitializeDatabase()
        {
            await _appDbContext.Database.MigrateAsync();
        }

        public async Task CreateRoles()
        {
            foreach (var role in Enum.GetValues(typeof(UserRoles)))
            {
                if (!await _userRole.RoleExistsAsync(role.ToString()))
                {
                    await _userRole.CreateAsync(new IdentityRole { Name = role.ToString() });
                }
            }
        }

        public async Task CreateAdmin()
        {
            AppUser user = new AppUser()
            {
                FullName = "Admin",
                PhoneNumber = _configuration["AdminDatas:Phone"]

            };

            await _userManager.CreateAsync(user, _configuration["AdminDatas:Password"]);
            await _userManager.AddToRoleAsync(user, UserRoles.Admin.ToString());
        }
    }
}
