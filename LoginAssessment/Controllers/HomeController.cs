using System.Diagnostics;
using LoginAssessment.Models;
using Microsoft.AspNetCore.Mvc;

namespace LoginAssessment.Controllers
{
    using System;
    using System.Threading.Tasks;

    using LoginAssessment.Data;

    using Microsoft.AspNetCore.Authentication;
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
        public IActionResult Login(bool? passwordReset = false, bool? registered = false)
        {
            this.ViewBag.PasswordReset = passwordReset;
            this.ViewBag.Registered = registered;

            return this.View();
        }

        [HttpGet]
        public IActionResult LoginPassphrase(bool? passphraseReset = false)
        {
            this.ViewBag.PassphraseReset = passphraseReset;
            return this.View();
        }

        [HttpGet]
        public IActionResult PasswordReset()
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult PassphraseReset()
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return this.View(new RegisterErrorViewModel());
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

        [HttpGet]
        public IActionResult ThankYou()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await this.users.FindByNameAsync(model.Email);
                if (user != null)
                {
                    if (await this.users.CheckPasswordAsync(user, model.Password))
                    {
                        var authenticationProperties = new AuthenticationProperties
                        {
                            RedirectUri = "~/Home/LoginPassphrase",
                            IsPersistent = true,
                            ExpiresUtc = DateTimeOffset.UtcNow.Add(TimeSpan.FromMinutes(30))
                        };

                        await this.signInManager.SignInAsync(user, authenticationProperties);
                        return this.RedirectToAction("LoginPassphrase");
                    }
                }
                else
                {
                    return this.RedirectToAction("LoginFail");
                }
            }

            return this.RedirectToAction("LoginFail");
        }

        [HttpPost]
        public async Task<IActionResult> LoginPassphrase(LoginPassphraseViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await this.users.FindByNameAsync(this.User.Identity.Name);
                if (user != null)
                {
                    var passPhraseMatch = user.PassPhrase.Equals(model.PassPhrase, StringComparison.Ordinal);
                    if (passPhraseMatch)
                    {
                        return this.RedirectToAction("LoginSuccess");
                    }
                }
            }

            return this.RedirectToAction("LoginFail");
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
                    return this.RedirectToAction("Login", new { registered = true });
                }

                var message = string.Empty;

                foreach (var identityError in result.Errors)
                {
                    message += identityError.Description + Environment.NewLine;
                }

                return this.View("Register", new RegisterErrorViewModel { ErrorMessage = message });
            }

            return this.BadRequest("Required fields not set");
        }

        [HttpPost]
        public async Task<IActionResult> LoginSuccess(LoginSuccessViewModel model)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                var user = await this.users.FindByEmailAsync(this.User.Identity.Name);
                user.SuccessfulLogins.Add(
                    new LoginSuccess
                    {
                        LoginDeviceId = model.LoginDeviceId,
                        LoginPreferenceId = model.LoginPreferenceId,
                        LoginDateTime = DateTime.Now
                    });

                var result = await this.users.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return this.RedirectToAction("ThankYou");
                }
            }

            return this.RedirectToAction("Login");
        }

        [HttpPost]
        public async Task<IActionResult> LoginFail(LoginFailViewModel model)
        {
            ApplicationUser user;
            if (this.User.Identity.IsAuthenticated)
            {
                user = await this.users.FindByEmailAsync(this.User.Identity.Name);
            }
            else
            {
                user = await this.users.FindByEmailAsync(Startup.AnonymousUserEmail);
            }

            user.FailedLogins.Add(
                new LoginFail
                {
                    LoginDeviceId = model.LoginDeviceId,
                    LoginPreferenceId = model.LoginPreferenceId,
                    LoginReasonId = model.LoginReasonId,
                    LoginDateTime = DateTime.Now
                });

            var result = await this.users.UpdateAsync(user);
            if (result.Succeeded)
            {
                return this.RedirectToAction("ThankYou");
            }

            return this.RedirectToAction("Login");
        }

        [HttpPost]
        public async Task<IActionResult> PasswordReset(LoginViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await this.users.FindByNameAsync(model.Email);
                if (user != null)
                {
                    string resetToken = await this.users.GeneratePasswordResetTokenAsync(user);
                    var result = await this.users.ResetPasswordAsync(user, resetToken, model.Password);

                    if (result.Succeeded)
                    {
                        return this.RedirectToAction("Login", new { passwordReset = true });
                    }
                }
                else
                {
                    return this.RedirectToAction("LoginFail");
                }
            }

            return this.RedirectToAction("LoginFail");
        }

        [HttpPost]
        public async Task<IActionResult> PassphraseReset(LoginPassphraseViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await this.users.FindByNameAsync(this.User.Identity.Name);
                if (user != null)
                {
                    user.PassPhrase = model.PassPhrase;
                    var result = await this.users.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        return this.RedirectToAction("LoginPassphrase", new { passphraseReset = true });
                    }
                }
                else
                {
                    return this.RedirectToAction("LoginFail");
                }
            }

            return this.RedirectToAction("LoginFail");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
