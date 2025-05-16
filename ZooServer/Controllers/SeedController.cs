using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Globalization;
using zooModel;

namespace ZooServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedController(ProjectSourceContext context, IHostEnvironment environment,
        UserManager<ZooUser> userManager) : ControllerBase
    {
        [HttpPost("Users")]
        public async Task ImportUsersAsync()
        {
            ZooUser user = new()
            {
                UserName = "user",
                Email = "user@email.com",
                SecurityStamp = Guid.NewGuid().ToString()
            };
            IdentityResult x = await userManager.CreateAsync(user, "Password0!");
            int y = await context.SaveChangesAsync();

        }
    }
}
