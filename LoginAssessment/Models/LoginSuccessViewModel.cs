namespace LoginAssessment.Models
{
    public class LoginSuccessViewModel
    {
        public int LoginDeviceId { get; set; }

        public int LoginPreferenceId { get; set; }

        public bool IsPreviousPasswordOrPassphraseUsed { get; set; }

        public string Comments { get; set; }
    }
}