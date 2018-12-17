namespace LoginAssessment.Data
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class PasswordChange
    {
        [Key]

        public int Id { get; set; }

        public string UserId { get; set; }

        public string PasswordBefore { get; set; }

        public string PasswordAfter { get; set; }

        public DateTime PasswordChangedDateTime { get; set; }

        public ApplicationUser User { get; set; }
    }
}
