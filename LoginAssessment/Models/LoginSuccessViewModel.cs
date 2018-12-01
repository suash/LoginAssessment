namespace LoginAssessment.Models
{
    public class LoginSuccessViewModel
    {
        public int LoginDeviceId { get; set; }

        public int LoginPreferenceId { get; set; }

        public bool IsPreviousPasswordUsed { get; set; }

        public bool IsPreviousPassphraseUsed { get; set; }

        public bool IsPreviousPasswordAndPassphraseUsed
        {
            get
            {
                return this.IsPreviousPasswordUsed && this.IsPreviousPassphraseUsed;
            }
        }

        public string Comments { get; set; }

        public string Email { get; set; }
    }
}