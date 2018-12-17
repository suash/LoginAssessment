﻿namespace LoginAssessment.Data
{
    using System.ComponentModel.DataAnnotations;

    public class LoginType
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}