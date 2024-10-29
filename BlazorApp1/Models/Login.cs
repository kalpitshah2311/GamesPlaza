  using System.ComponentModel.DataAnnotations;

    namespace BlazorApp1.Models
    {
        public class Login
        {

            [Required]
            [EmailAddress]
            public required string Email { get; set; }

            [Required]
            public required string Password { get; set; }

        }
    }

