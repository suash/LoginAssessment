namespace LoginAssessment.Data
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class LoginEntry
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }

        public int LoginTypeId { get; set; }

        public bool IsSuccessful { get; set; }

        public DateTime LoginDateTime { get; set; }

        public LoginType LoginType { get; set; }

        public ApplicationUser User { get; set; }
    }
}