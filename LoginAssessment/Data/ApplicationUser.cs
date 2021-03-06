namespace LoginAssessment.Data
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.SuccessfulLogins = new List<LoginSuccess>();
            this.FailedLogins = new List<LoginFail>();
            this.PassphraseChanges = new List<PassphraseChange>();
            this.PasswordChanges = new List<PasswordChange>();
            this.LoginEntries = new List<LoginEntry>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [MaxLength(255)]
        public string Password { get; set; }

        public string PassPhrase { get; set; }

        public int Age { get; set; }

        public string Gender { get; set; }

        public List<LoginSuccess> SuccessfulLogins { get; set; }

        public List<LoginFail> FailedLogins { get; set; }

        public List<LoginEntry> LoginEntries { get; set; }

        public List<PasswordChange> PasswordChanges { get; set; }

        public List<PassphraseChange> PassphraseChanges { get; set; }
    }
}