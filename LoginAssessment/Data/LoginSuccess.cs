namespace LoginAssessment.Data
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class LoginSuccess
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }

        public int LoginPreferenceId { get; set; }

        public int LoginDeviceId { get; set; }

        [MaxLength(5000)]
        public string Comments { get; set; }

        public bool IsPreviousPasswordUsed { get; set; }

        public bool IsPreviousPassphraseUsed { get; set; }

        public bool IsPreviousPasswordAndPassphraseUsed { get; set; }

        public DateTime LoginDateTime { get; set; }

        public LoginPreference LoginPreference { get; set; }

        public LoginDevice LoginDevice { get; set; }

        public ApplicationUser User { get; set; }
    }
}