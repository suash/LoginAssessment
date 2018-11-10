namespace LoginAssessment.Data
{
    using System;

    public class LoginSuccess
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public DateTime LoginDateTime { get; set; }

        public LoginPreference LoginPreference { get; set; }

        public LoginDevice LoginDevice { get; set; }

        public ApplicationUser User { get; set; }
    }
}