using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PolicyBaseAuth.Auth;
using PolicyBaseAuth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolicyBaseAuth.Controllers
{
    [ApiController]
    [Route("api/[Controller]/[Action]")]
    public class HomeController : ControllerBase
    {
        private readonly BookStoreContext bookStoreContext;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;

        public HomeController(BookStoreContext bookStoreContext,SignInManager<IdentityUser> signInManager,UserManager<IdentityUser> userManager)
        {
            this.bookStoreContext = bookStoreContext;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }
        public IActionResult UserList()
        {
            var users = bookStoreContext.Users.ToList();
            return Ok(users);
        }

        public IActionResult GetPermissions()
        {
            var permissionFields = typeof(Permissions).GetFields();
            var fieldnames = permissionFields.Select(c => c.Name).ToList();
            return Ok(fieldnames);
        }


        [Authorize(Policy = Permissions.readBook)]
        public string GetBookWithId()
        {
            return "hello";
        }

        
    }
}
