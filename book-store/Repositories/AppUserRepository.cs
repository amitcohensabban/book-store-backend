using book_store.Data;
using book_store.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace book_store.Repositories
{
    public class AppUserRepository :IAppUserRepository
    {
        private readonly BookStoreContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AppUserRepository(BookStoreContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<IdentityResult> SignUp(SignupModel signupModel)
        {
            Console.WriteLine("SignUpAction");
            AppUser user = new()
            {
                FirstName = signupModel.FirstName,
                LastName = signupModel.LastName,
                Email = signupModel.Email,
                UserName = signupModel.Email
            };
            var result = await _userManager.CreateAsync(user, signupModel.Password);
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task<string> Login(LoginModel loginModel)
        {
            var result = await _signInManager.PasswordSignInAsync(loginModel.Email, loginModel.Password, false, false);
            if (!result.Succeeded)
            {
                return null;
            }
            var user = await _userManager.FindByEmailAsync(loginModel.Email);
            var roles = await _userManager.GetRolesAsync(user);
            string token = NewToken(loginModel.Email, user.Id,roles);
            await _context.SaveChangesAsync();

            return token;
        }
        private string NewToken(string email, string id, IEnumerable<string> roles)
        {
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, email),
                new Claim(ClaimTypes.NameIdentifier, id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            foreach (var role in roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role)); 
            }
            var authSigninKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256Signature)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
