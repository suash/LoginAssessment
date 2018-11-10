using System.Diagnostics;
using LoginAssessment.Models;
using Microsoft.AspNetCore.Mvc;

namespace LoginAssessment.Controllers
{
    using System;
    using System.Threading.Tasks;

    using LoginAssessment.Data;

    using Microsoft.AspNetCore.Identity;

    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> users;
        private readonly SignInManager<ApplicationUser> signInManager;

        public HomeController(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> users)
        {
            this.users = users;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult LoginPassphrase()
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult PasswordReset()
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult LoginSuccess()
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult LoginFail()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            return this.RedirectToAction("LoginPassphrase");
        }

        [HttpPost]
        public IActionResult LoginPassphrase(LoginPassphraseViewModel model)
        {
            return this.RedirectToAction("LoginPassphrase");
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    Age = model.Age,
                    Email = model.Email,
                    NormalizedEmail = model.Email,
                    NormalizedUserName = model.Email,
                    UserName = model.Email,
                    Gender = model.Gender,
                    PassPhrase = model.PassPhrase
                };

                var result = await this.users.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return this.Ok();
                }

                var message = string.Empty;

                foreach (var identityError in result.Errors)
                {
                    message += identityError.Description + Environment.NewLine;
                }

                return this.BadRequest(message);
            }

            return this.BadRequest("Required fields not set");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
