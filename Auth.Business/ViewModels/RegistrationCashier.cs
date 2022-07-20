using System.ComponentModel.DataAnnotations;

namespace Auth.Business.ViewModels
{
    public  class RegistrationCashier
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

    }
}
