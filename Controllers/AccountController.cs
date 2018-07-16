using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

[Route("[controller]/[action]")]
[Authorize]
public class AccountController : Controller
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;

    public AccountController(
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager,
        RoleManager<IdentityRole> roleManager,
        IConfiguration configuration
        )
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<object> Login([FromBody] LoginDto model)
    {
        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

        if (result.Succeeded)
        {
            var appUser = _userManager.Users.SingleOrDefault(r => r.Email == model.Email);
            return await GenerateJwtToken(model.Email, appUser);

        }

        throw new ApplicationException("INVALID_LOGIN_ATTEMPT");
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<object> Register([FromBody] RegisterDto model)
    {
        // var role = new IdentityRole
        // {
        //     Name = "Admin"
        // };

        // await _roleManager.CreateAsync(role);

        var user = new IdentityUser
        {
            UserName = model.Email,
            Email = model.Email
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            // await _userManager.AddToRoleAsync(user, "Admin");
            await _signInManager.SignInAsync(user, false);
            return await GenerateJwtToken(model.Email, user);
        }

        throw new ApplicationException("UNKNOWN_ERROR");
    }

    [HttpGet]
    public async Task<object> Protected()
    {
        return await Task.Run(() => "Protected area");
    }

    private async Task<object> GenerateJwtToken(string email, IdentityUser user)
    {
        var roles = await _userManager.GetRolesAsync(user);
        var roleClaims = new List<Claim>();
        foreach (var item in roles)
        {
            roleClaims.Add(new Claim("roles", item));
        }
        var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
            };
        claims.AddRange(roleClaims);

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

        var token = new JwtSecurityToken(
            issuer: _configuration["JwtIssuer"],
            audience: _configuration["JwtIssuer"],
            claims: claims,
            expires: expires,
            signingCredentials: creds
        );

        return await Task.Run(() => new JwtSecurityTokenHandler().WriteToken(token));
    }

    public class LoginDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

    }

    public class RegisterDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "PASSWORD_MIN_LENGTH", MinimumLength = 6)]
        public string Password { get; set; }
    }
}