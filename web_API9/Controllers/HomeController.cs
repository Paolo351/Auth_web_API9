using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;
using AspNetCore.Identity.MongoDB;
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
            //await _signInManager.SignOutAsync();
            return View("Index");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Secret()
        {
            //var user = await _userManager.FindByNameAsync(User.Identity.Name);
            
            var viewModel = new ShowUserViewModel()
            {
                FullName = (await _userManager.FindByNameAsync(User.Identity.Name)).FullName
            //FullName = user.FullName
            };

            return View("Secret", viewModel);
        }

        //[Authorize]
        //[Authorize(Policy = "Claim.DoB")]
        //[Authorize(Roles = "Admin")]
        [Authorize(Policy = "Admin")]
        [HttpGet]
        public async Task<IActionResult> AdminSecret()
        {
            //var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var viewModel = new ShowUserViewModel()
            {
                FullName = (await _userManager.FindByNameAsync(User.Identity.Name)).FullName
                //FullName = user.FullName
            };

            return View("Secret", viewModel);
        }


        [HttpGet]
        public async Task<IActionResult> Login()
        {
            //_ = new UserWithIdentity();

            if (User.Identity.Name != null)
            {
                UserWithIdentity user = await _userManager.FindByNameAsync(User.Identity.Name);

                var viewModel = new ShowUserViewModel()
                {
                    FullName = user.FullName

                };
                return View("LoginOK", viewModel);

            }
            return View("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Login(string UserName, string PasswordHash)
        {
            
            //login functionality
            var user = await _userManager.FindByNameAsync(UserName);

            if (user != null) 
            {
                string z = "";

                switch ((int)user.Role)
                {
                    case 0: z = "Admin"; break;
                    case 1: z = "Schema_guard"; break;
                    case 2: z = "Spectator"; break;

                }
                    
                var userClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, z),

                    new Claim("FirstName", user.FirstName),
                    new Claim("LastName", user.LastName),
                    //new Claim("UserName", user.UserName),
                    new Claim("FullName", user.FullName),
                    //new Claim("Email", user.Email),
                    //new Claim("Role", z)
                };

                var userIdentity = new ClaimsIdentity(userClaims, "user identity");

                var userPrincipal = new ClaimsPrincipal(new[] { userIdentity });
                //-----------------------------------------------------------
                await HttpContext.SignInAsync(userPrincipal);
            }
                

            if (user != null)
            {
                // sign in user
                var signInresult = await _signInManager.PasswordSignInAsync(user, PasswordHash, false, false);

                if (signInresult.Succeeded)
                {
                    var viewModel = new ShowUserViewModel()
                    {
                        FullName = user.FullName
                    };

                    
                    return View("LoginOK", viewModel);
                }
            }

            return View("LoginFailed");
        }

        [HttpGet]
        public IActionResult NoSecret()
        {
            return View("NoSecret");
        }

        //[HttpGet]
        //public IActionResult Register()
        //{
        //    return View("Register");
        //}

        //[HttpPost]
        //public async Task<IActionResult> Register(string FirstName, string LastName, string PasswordHash, string Email, UserRole Role)
        //{
        //    //register functionality
        //    var uzer = new UserWithIdentity(FirstName, LastName, Email, Role);

        //   // var userOrigin = new User("Paul", "Cichocki", PasswordHash, Email, (UserRole)1);

        //    //var user = new UserWithIdentity(uzer);

        //    var result = await _userManager.CreateAsync(uzer, PasswordHash);

        //    if (result.Succeeded)
        //    {
        //        // sign in user
        //        var signInresult = await _signInManager.PasswordSignInAsync(uzer, PasswordHash, false, false);

        //        if (signInresult.Succeeded)
        //        {
        //            return View("LoginOK");

        //        } else return View("LoginFailed");

        //    } else return View("RegisterFailed");

            
        //}

        

        
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
