using System.ComponentModel.DataAnnotations;

namespace AuthAnySell.ViewModels
{
    public  class RegistrationCashier
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

    }
}
