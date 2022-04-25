using Auth.Business.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Auth.Business.Entities
{
    public class Account : BaseEntity
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string  Password { get; set; }
        public Role Role { get; set; }
        [Required]
        public string StringConfirm { get; set; }
        [Required]
        public bool Confirm { get; set; } = false;
    }
}
