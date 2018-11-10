namespace LoginAssessment.Data
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.SuccessfulLogins = new List<LoginSuccess>();
            this.FailedLogins = new List<LoginFail>();    
        }

        public string FirstName { get; set; }
	
        public string LastName { get; set; }

        public string PasswordPhrase { get; set; }

        public List<LoginSuccess> SuccessfulLogins { get; set; }

        public List<LoginFail> FailedLogins { get; set; }
    }
}