using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using web_API9.Models;
using web_API9.Models.Application.User;

namespace web_API9.Controllers
{

    public class HomeController : Controller
    {



        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<UserWithIdentity> _userManager;
        private readonly SignInManager<UserWithIdentity> _signInManager;

        public HomeController(
            ILogger<HomeController> logger,
            UserManager<UserWithIdentity> userManager,
            SignInManager<UserWithIdentity> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [HttpGet]
        public IActionResult Index()
        {
            return View("Index");
        }

        [Authorize]
        [HttpGet]
        public IActionResult Secret()
        {
            return View("Secret");
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Login(string UserName, string PasswordHash)
        {
            
            //login functionality
            var user = await _userManager.FindByNameAsync(UserName);

            if (user != null)
            {
                // sign in user
                var signInresult = await _signInManager.PasswordSignInAsync(user, PasswordHash, false, false);

                if (signInresult.Succeeded)
                {
                    return View("LoginOK");
                }
            }

            return View("LoginFailed");
        }

        [HttpGet]
        public IActionResult NoSecret()
        {
            return View("NoSecret");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        public async Task<IActionResult> Register(string FirstName, string LastName, string PasswordHash, string Email, UserRole Role)
        {
            //register functionality
            var uzer = new UserWithIdentity(FirstName, LastName, Email, Role);

           // var userOrigin = new User("Paul", "Cichocki", PasswordHash, Email, (UserRole)1);

            //var user = new UserWithIdentity(uzer);

            var result = await _userManager.CreateAsync(uzer, PasswordHash);

            if (result.Succeeded)
            {
                // sign in user
                var signInresult = await _signInManager.PasswordSignInAsync(uzer, PasswordHash, false, false);

                if (signInresult.Succeeded)
                {
                    return View("LoginOK");

                } else return View("LoginFailed");

            } else return View("RegisterFailed");

            
        }

        

        
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return View("Logout"); 
            
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpGet]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
