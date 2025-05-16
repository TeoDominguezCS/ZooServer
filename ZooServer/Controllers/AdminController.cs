using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZooServer.DTOS;
using zooModel;
using LoginRequest = ZooServer.DTOS.LoginRequest;
using System.IdentityModel.Tokens.Jwt;


namespace ZooServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController(UserManager<ZooUser> userManager, JwtHandler jwtHandler): ControllerBase
    {
        [HttpPost("Login")]
        public async Task<ActionResult> LoginAsync(LoginRequest request)
        {
            ZooUser user = await userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                return Unauthorized("Unknown User");
            }

            bool success = await userManager.CheckPasswordAsync(user, request.Password);
            if (!success)
            {
                return Unauthorized("Wrong Username/Password");
            }

            JwtSecurityToken token = await jwtHandler.GetTokenAsync(user);

            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new LoginResponse
            {
                Success = true,
                Message = "Successful Login",
                Token = tokenString,
            });
        }
    }
}