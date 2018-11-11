namespace LoginAssessment
{
    using LoginAssessment.Data;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;

    public static class SetupDefaultUser
    {
        public static void SeedAnonymousUser(this IApplicationBuilder app, UserManager<ApplicationUser> userManager)
        {
            var user = userManager.FindByEmailAsync(Startup.AnonymousUserEmail).GetAwaiter().GetResult();
            if (user == null)
            {
                user = new ApplicationUser
                {
                    Email = Startup.AnonymousUserEmail,
                    UserName = Startup.AnonymousUserEmail,
                    NormalizedUserName = Startup.AnonymousUserEmail,
                    NormalizedEmail = Startup.AnonymousUserEmail,
                    Age = 0,
                    Gender = "Male"
                };

                var result = userManager.CreateAsync(user, "Password.1").GetAwaiter().GetResult();
            }
        }
    }
}
