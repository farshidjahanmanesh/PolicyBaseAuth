using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolicyBaseAuth.Controllers
{
    [ApiController]
    [Route("api/[Controller]/[Action]")]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;

        public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }
        public async Task<IActionResult> CreateAccount(string username, string password)
        {
            var result = await userManager.CreateAsync(new IdentityUser()
            {
                Email = username,
                UserName = username
            }, password);
            return Ok();
        }
        [Authorize]
        public IActionResult ShowClaims()
        {
            return Ok(User.Claims.Select(c => new { c.Type, c.Value }).ToList());
        }
        [Authorize]
        public async Task<IActionResult> SignOutUser()
        {
            await signInManager.SignOutAsync();
            return Ok();
        }
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await userManager.FindByEmailAsync(email);
            await signInManager.PasswordSignInAsync(user, password, false, false);
            return Ok();
        }

        public async Task<IActionResult> SetPermissionForUser(string userId, string PermissionName)
        {
            var user = await userManager.FindByIdAsync(userId);
            var claim = new System.Security.Claims.Claim("authorization", PermissionName);
            await userManager.AddClaimAsync(user, claim);
            return Ok();
        }
    }
}
