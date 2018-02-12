using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Engineering_Project.Models.Transmit;
using Engineering_Project.Service.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Engineering_Project.DataAccess
{
    public class AccountDataAccess : IAccountDataAccess
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;

        public AccountDataAccess(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager,
            IAuthenticationService service, IPasswordHasher<ApplicationUser> passwordHasher, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _passwordHasher = passwordHasher;
            _signInManager = signInManager;
        }

        public async Task<object> AddRole(ApplicationRole role)
        {
            IdentityResult createResult = await _roleManager.CreateAsync(role);
            IdentityResult addClaimResult =
                await _roleManager.AddClaimAsync(role, new Claim(ClaimTypes.Role, role.Name));

            if (createResult.Succeeded && addClaimResult.Succeeded)
            {
                return addClaimResult;
            }

            
            return _roleManager.DeleteAsync(role);
        }

        public async Task<bool> AddUser(UserRegisterTransmitModel model)
        {
            ApplicationUser user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Locale = model.Locale
            };

            IdentityResult createResult = await _userManager.CreateAsync(user, model.Password);
            IdentityResult addClaimResult =
                await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "User"));
            if (createResult.Succeeded && addClaimResult.Succeeded)
            {
                ApplicationRole applicationRole = await _roleManager.FindByNameAsync("User");
                if (applicationRole != null)
                {
                    throw new ArgumentException($"Not find role User", "original");
                }

                IdentityResult roleResult = await _userManager.AddToRoleAsync(user, applicationRole.Name);
                if (roleResult.Succeeded)
                    return true;
            }

            await _userManager.DeleteAsync(user);
            return false;
        }

        public async Task<bool> ChangeRole(AdminChangeRoleTransmitModel model)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(model.UserName);
            IList<string> roles = await _userManager.GetRolesAsync(user);
            IdentityResult deleteResult = await _userManager.RemoveFromRolesAsync(user, roles);
            IdentityResult addResult = new IdentityResult();
            if (deleteResult.Succeeded)
            {
                addResult = await _userManager.AddToRoleAsync(user, model.Role);
            }

            return addResult.Succeeded;
        }

        public async Task<string> GetUserIdAsync(string userName)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(userName);
            return user.Id;
        }

        public async Task<object> Authenticate(UserSignInTransmitModel model)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null)
            {
                if (_passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password) ==
                    PasswordVerificationResult.Success)
                {
                    IList<Claim> userClaims = await _userManager.GetClaimsAsync(user);

                    IEnumerable<Claim> claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Email, user.Email)
                    }.Union(userClaims);

                    SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                        "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIyMzRnZzQ1NCIsIm5hbWUiOiJBZG1pbiIsImFkbWluIjp0cnVlfQ.Nmgnewz76zmtuRgrNTpIEaHJueWdulBy5X-Zpg_Lh_s"));
                    SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    JwtSecurityToken token = new JwtSecurityToken(
                        issuer:
                        "rqDkbIK9YfjFSJYm49x8k8pFkxhX4bJVjEnG059heD6HQrF59F7yVi5V0wJPXBNpTFPmHDmTMoIhYMYnADAqPx",
                        audience:
                        "!de&6Yw8GgcG9!^MQ9Qg4FYv*Ggm8RcpJ93yZUj%z9*6VU62%aXKjU7$ND#*X$jbG@k$CB@7%y*X%qb25r&!#y",
                        claims: claims,
                        expires: DateTime.UtcNow.AddMinutes(5),
                        signingCredentials: creds
                    );

                    var result = (new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = (5 / 60.0 / 24.0),
                        name = user.UserName,
                        locale = user.Locale
                    });

                    return result;
                }
            }

            return null;
        }

        public async Task<bool> DeleteUser(DeleteUserTransmitModel model)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(model.Email);
            IdentityResult deleteResult = await _userManager.DeleteAsync(user);
            return deleteResult.Succeeded;
        }

//        public async Task<bool> ResetPassword(UserChangePasswordTrasnmitModel model)
//        {
//            ApplicationUser user = new ApplicationUser();
//            IdentityResult resetResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
//            return resetResult.Succeeded;
//        }
    }
}