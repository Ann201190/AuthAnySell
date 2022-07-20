using System.ComponentModel.DataAnnotations;

namespace Auth.Business.ViewModels
{
    public class Authorization
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
