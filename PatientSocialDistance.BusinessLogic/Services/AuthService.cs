using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PatientSocialDistance.BusinessLogic.Services.IServices;
using PatientSocialDistance.Persistence.HelpersModels;
using PatientSocialDistance.Persistence.Models;
using PatientSocialDistance.Persistence.NotDbModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PatientSocialDistance.BusinessLogic.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly Jwt _jwt;

        public AuthService(UserManager<ApplicationUser> userManager,
                            RoleManager<IdentityRole> roleManager,
                            IOptions<Jwt> Jwt)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwt = Jwt.Value;
        }

        public async Task<AuthModelResponse> RegisterAsync(RegisterModel registerModel)
        {
            if (await _userManager.FindByEmailAsync(registerModel.Email) is not null)
            {
                return new AuthModelResponse() { Message = "Email is Already register" };
            }
            if (await _userManager.FindByNameAsync(registerModel.Username) is not null)
            {
                return new AuthModelResponse() { Message = "Username is Already register" };
            }

            var user = new ApplicationUser()
            {
                UserName = registerModel.Username,
                Email = registerModel.Email,
                Age = registerModel.Age,
                Name = registerModel.Name,
                NationalId = registerModel.NationalId,
                Nationality = registerModel.Nationality,
                UserTypeId = registerModel.UserTypeId,
                Hospital = registerModel.Hospital,
            };

            var result = await _userManager.CreateAsync(user, registerModel.Password);
            if (!result.Succeeded)
            {
                var errors = String.Empty;
                foreach (var error in result.Errors)
                {
                    errors += $"{error.Description},";
                }
                return new AuthModelResponse() { Message = errors };
            }

            await _userManager.AddToRoleAsync(user, "user");

            var jwtSecurityToken = await CreateJwtToken(user);

            var userRole = await _userManager.GetRolesAsync(user);

            var text = jwtSecurityToken.ValidTo.ToString();
            return new AuthModelResponse()
            {
                Email = user.Email,
                ExpiresOn = text,
                IsAuthenticated = true,
                Username = user.UserName,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Roles = userRole
            };
        }

        public async Task<AuthModelResponse> GetTokenAsync(TokenRequestModel tokenRequestModel)
        {
            var authModel = new AuthModelResponse();
            var user = await _userManager.FindByEmailAsync(tokenRequestModel.Email);

            if (user is null || !await _userManager.CheckPasswordAsync(user, tokenRequestModel.Password))
            {
                authModel.Message = "Email or Password is incorrect";
                return authModel;
            }
            
            var jwtSecurityToken = await CreateJwtToken(user);
            var userRole = await _userManager.GetRolesAsync(user);

            var text = jwtSecurityToken.ValidTo.ToString();
            authModel.Email = user.Email;
            authModel.ExpiresOn = text;
            authModel.IsAuthenticated = true;
            authModel.Username = user.UserName;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authModel.Roles = userRole.ToList();

            return authModel;
        }

        public async Task<string> AddRoleAsync(AddRoleModel addRoleModel)
        {
            var user = await _userManager.FindByIdAsync(addRoleModel.UserId);
           
            if(user is null || !await _roleManager.RoleExistsAsync(addRoleModel.Role))
            {
                return "Invalid user ID or  Role Name";
            }

            if(await _userManager.IsInRoleAsync(user, addRoleModel.Role))
            {
                return "User Already assigned to this Role";
            }

            var result = await _userManager.AddToRoleAsync(user, addRoleModel.Role);

            return result.Succeeded ? String.Empty : "Something Went Wrong";
        }
        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
            {
                roleClaims.Add(new Claim("roles", role));
            }

            var claims = new[]
            {
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id),
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;

        }


    }
}
