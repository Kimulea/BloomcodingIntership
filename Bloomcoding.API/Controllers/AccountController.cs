using Bloomcoding.API.Infrastructure.Configurations;
using Bloomcoding.Common.Dtos.Account;
using Bloomcoding.Dal.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Bloomcoding.Domain.Auth;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Bloomcoding.API.Controllers
{
    [Route("api/account")]
    public class AccountController : BaseController
    {
        private readonly AuthOptions _authentificationOptions;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public AccountController(IOptions<AuthOptions> authentificationOptions, SignInManager<User> signInManager
            , UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _authentificationOptions = authentificationOptions.Value;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var checkingPasswordResult = await _signInManager.PasswordSignInAsync(userForLoginDto.Username, userForLoginDto.Password, false, false);

            var claims = new List<Claim>();

            var user = await _userManager.FindByNameAsync(userForLoginDto.Username);
            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            };

            if (checkingPasswordResult.Succeeded)
            {
                var signinCredentials = new SigningCredentials(_authentificationOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256);
                var jwtSecurityToken = new JwtSecurityToken(
                     issuer: _authentificationOptions.Issuer,
                     audience: _authentificationOptions.Audience,
                     claims: claims,
                     expires: DateTime.Now.AddDays(30),
                     signingCredentials: signinCredentials
                );

                var tokenHandler = new JwtSecurityTokenHandler();

                var encodedToken = tokenHandler.WriteToken(jwtSecurityToken);

                return Ok(new { AccessToken = encodedToken });
            }

            return Unauthorized();
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            var userExists = await _userManager.FindByNameAsync(userForRegisterDto.Username);

            if (userExists != null)
                return BadRequest("User already exists");

            User user = new User()
            {
                Email = userForRegisterDto.Email,
                UserName = userForRegisterDto.Username
            };

            var result = await _userManager.CreateAsync(user, userForRegisterDto.Password);

            if (!result.Succeeded)
                return Ok(result.Errors);

            if (await _roleManager.RoleExistsAsync(UserRoles.Student))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Student);
            }

            return Ok("User Created!");
        }

        [AllowAnonymous]
        [HttpPost("Register-Admin")]
        public async Task<IActionResult> RegisterAdmin(UserForRegisterDto userForRegisterDto)
        {
            var userExists = await _userManager.FindByNameAsync(userForRegisterDto.Username);

            if (userExists != null)
                return Ok("User already exists");

            User user = new User()
            {
                Email = userForRegisterDto.Email,
                UserName = userForRegisterDto.Username
            };

            var result = await _userManager.CreateAsync(user, userForRegisterDto.Password);

            if (!result.Succeeded)
                return Ok(result.Errors);

            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Admin);
            }

            return Ok("Admin created!");
        }
    }
}
