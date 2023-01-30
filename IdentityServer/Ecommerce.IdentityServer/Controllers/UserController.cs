﻿using Ecommerce.IdentityServer.Dto;
using Ecommerce.IdentityServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using static IdentityServer4.IdentityServerConstants;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;

namespace Ecommerce.IdentityServer.Controllers
{
    [Authorize(LocalApi.PolicyName)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpDto signUpDto)
        {
            var user = new ApplicationUser
            {
                UserName = signUpDto.UserName,
                Email = signUpDto.Email,
                City = signUpDto.City,
            };
            var result = await _userManager.CreateAsync(user, signUpDto.Password);
            //neden succeded gelmedi
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            else
            {
                return NoContent();
            }

        }
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var useridClaim = User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub);
            var user= await _userManager.FindByIdAsync(useridClaim.Value);    
            return Ok(new
            {
                id=user.Id, 
                username=user.UserName, 
                email=user.Email,   
                city=user.City
            });
        }
    }
}