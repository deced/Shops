using System.ComponentModel.DataAnnotations;

namespace Shops.App.Models.Identity
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
         
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Passmword")]
        public string Password { get; set; }
         
        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
         
        public string ReturnUrl { get; set; }
    }
}