﻿namespace LoginAssessment.Controllers
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;

    using LoginAssessment.Data;
    using LoginAssessment.Models;

    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Login(bool? passwordReset = false, bool? registered = false, bool? userNotFound = false, string emailAddress = null)
        {
            this.ViewBag.PasswordReset = passwordReset;
            this.ViewBag.Registered = registered;
            this.ViewBag.UserNotFound = userNotFound;
            this.ViewBag.Email = emailAddress;

            return this.View();
        }

        [HttpGet]
        public IActionResult LoginPassphrase(bool? passphraseReset = false, string emailAddress = null)
        {
            this.ViewBag.PassphraseReset = passphraseReset;
            this.ViewBag.Email = emailAddress;
            return this.View();
        }

        [HttpGet]
        public IActionResult PasswordReset(bool? userNotFound = false)
        {
            this.ViewBag.UserNotFound = userNotFound;
            return this.View();
        }

        [HttpGet]
        public IActionResult PassphraseReset(bool? userNotFound = false)
        {
            this.ViewBag.UserNotFound = userNotFound;
            return this.View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return this.View(new RegisterErrorViewModel());
        }

        [HttpGet]
        public IActionResult LoginSuccess(int loginTypeId = 1)
        {
            this.ViewBag.LoginTypeId = loginTypeId;
            return this.View();
        }

        [HttpGet]
        public IActionResult LoginFail(string emailAddress = null)
        {
            this.ViewBag.Email = emailAddress;
            return this.View();
        }

        [HttpGet]
        public IActionResult LoginFailPassphrase(string emailAddress = null)
        {
            this.ViewBag.Email = emailAddress;
            return this.View();
        }

        [HttpGet]
        public IActionResult ThankYou(string emailAddress = null, bool? passwordFailComplete = false, bool? passphraseFailComplete = false)
        {
            this.ViewBag.Email = emailAddress;
            this.ViewBag.PasswordFailComplete = passwordFailComplete;
            this.ViewBag.passphraseFailComplete = passphraseFailComplete;
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

                        user.LoginEntries.Add(new LoginEntry
                        {
                            LoginDateTime = DateTime.Now,
                            LoginTypeId = (int)LoginTypeEnum.Password,
                            IsSuccessful = true,
                            UserId = user.Id
                        });

                        await this.users.UpdateAsync(user);

                        return this.RedirectToAction("LoginPassphrase", new { emailAddress = model.Email });
                    }
                    else
                    {
                        user.LoginEntries.Add(new LoginEntry
                        {
                            LoginDateTime = DateTime.Now,
                            LoginTypeId = (int)LoginTypeEnum.Password,
                            IsSuccessful = false,
                            UserId = user.Id
                        });

                        await this.users.UpdateAsync(user);

                        return this.RedirectToAction("LoginFail", new { emailAddress = model.Email });
                    }
                }
                else
                {
                    return this.RedirectToAction("Login", new { userNotFound = true });
                }
            }

            return this.RedirectToAction("LoginFail");
        }

        [HttpPost]
        public async Task<IActionResult> LoginPassphrase(LoginPassphraseViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var email = string.Empty;
                if (this.User.Identity.IsAuthenticated)
                {
                    email = this.User.Identity.Name;
                }
                else
                {
                    email = model.Email;
                }

                var user = await this.users.FindByNameAsync(email);
                if (user != null)
                {
                    var authenticationProperties = new AuthenticationProperties
                    {
                        RedirectUri = "~/Home/LoginPassphrase",
                        IsPersistent = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.Add(TimeSpan.FromMinutes(30))
                    };

                    await this.signInManager.SignInAsync(user, authenticationProperties);

                    var passPhraseMatch = user.PassPhrase.Equals(model.PassPhrase, StringComparison.Ordinal);
                    if (passPhraseMatch)
                    {
                        user.LoginEntries.Add(new LoginEntry
                        {
                            LoginDateTime = DateTime.Now,
                            LoginTypeId = (int)LoginTypeEnum.Passphrase,
                            IsSuccessful = true,
                            UserId = user.Id
                        });

                        await this.users.UpdateAsync(user);

                        return this.RedirectToAction("LoginSuccess", new { loginTypeId = (int)LoginTypeEnum.Passphrase });
                    }
                    else
                    {
                        user.LoginEntries.Add(new LoginEntry
                        {
                            LoginDateTime = DateTime.Now,
                            LoginTypeId = (int)LoginTypeEnum.Passphrase,
                            IsSuccessful = false,
                            UserId = user.Id
                        });

                        await this.users.UpdateAsync(user);

                        return this.RedirectToAction("LoginFailPassphrase", new { emailAddress = model.Email });
                    }
                }
                else
                {
                    return this.RedirectToAction("Login");
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
                    PassPhrase = model.PassPhrase,
                    Password = model.Password
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
                        LoginDateTime = DateTime.Now,
                        IsPreviousPasswordUsed = model.IsPreviousPasswordUsed,
                        IsPreviousPassphraseUsed = model.IsPreviousPassphraseUsed,
                        IsPreviousPasswordAndPassphraseUsed = model.IsPreviousPasswordAndPassphraseUsed,
                        Comments = model.Comments,
                        LoginTypeId = model.LoginTypeId
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
                    LoginDateTime = DateTime.Now,
                    IsPreviousPasswordUsed = model.IsPreviousPasswordUsed,
                    IsPreviousPassphraseUsed = model.IsPreviousPassphraseUsed,
                    IsPreviousPasswordAndPassphraseUsed = model.IsPreviousPasswordAndPassphraseUsed,
                    LoginTypeId = (int)LoginTypeEnum.Password,
                    Comments = model.Comments
                });

            var result = await this.users.UpdateAsync(user);
            if (result.Succeeded)
            {
                return this.RedirectToAction("ThankYou", new { emailAddress = model.Email, passwordFailComplete = true });
            }

            return this.RedirectToAction("Login");
        }

        [HttpPost]
        public async Task<IActionResult> LoginFailPassphrase(LoginFailViewModel model)
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
                    LoginDateTime = DateTime.Now,
                    IsPreviousPasswordUsed = model.IsPreviousPasswordUsed,
                    IsPreviousPassphraseUsed = model.IsPreviousPassphraseUsed,
                    IsPreviousPasswordAndPassphraseUsed = model.IsPreviousPasswordAndPassphraseUsed,
                    LoginTypeId = (int)LoginTypeEnum.Passphrase,
                    Comments = model.Comments
                });

            var result = await this.users.UpdateAsync(user);
            if (result.Succeeded)
            {
                return this.RedirectToAction("ThankYou", new { emailAddress = model.Email, passphraseFailComplete = true });
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
                    var previousPassword = user.Password;
                    string resetToken = await this.users.GeneratePasswordResetTokenAsync(user);
                    user.Password = model.Password;
                    var result = await this.users.ResetPasswordAsync(user, resetToken, model.Password);
                    if (result.Succeeded)
                    {
                        user.PasswordChanges.Add(new PasswordChange
                        {
                            PasswordBefore = previousPassword,
                            PasswordAfter = model.Password,
                            PasswordChangedDateTime = DateTime.Now,
                            UserId = user.Id
                        });

                        result = await this.users.UpdateAsync(user);
                        if (result.Succeeded)
                        {
                            return this.RedirectToAction("Login", new { passwordReset = true });
                        }
                    }
                    else
                    {
                        return this.RedirectToAction("PasswordReset");
                    }
                }
                else
                {
                    return this.RedirectToAction("Login", new { userNotFound = true });
                }
            }

            return this.RedirectToAction("Login");
        }

        [HttpPost]
        public async Task<IActionResult> PassphraseReset(LoginPassphraseViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await this.users.FindByNameAsync(model.Email);
                if (user != null)
                {
                    var previousPassphrase = user.PassPhrase;
                    user.PassPhrase = model.PassPhrase;

                    user.PassphraseChanges.Add(new PassphraseChange
                    {
                        PassphraseBefore = previousPassphrase,
                        PassphraseAfter = model.PassPhrase,
                        PassphraseChangedDateTime = DateTime.Now,
                        UserId = user.Id
                    });

                    var result = await this.users.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        return this.RedirectToAction("Login", new { passwordReset = true });
                    }
                }
                else
                {
                    return this.RedirectToAction("PassphraseReset", new { userNotFound = true });
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