namespace LoginAssessment.Data
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class PassphraseChange
    {
        [Key]

        public int Id { get; set; }

        public string UserId { get; set; }

        public string PassphraseBefore { get; set; }

        public string PassphraseAfter { get; set; }

        public DateTime PassphraseChangedDateTime { get; set; }

        public ApplicationUser User { get; set; }
    }
}
