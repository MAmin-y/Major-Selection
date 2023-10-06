using System.ComponentModel.DataAnnotations;

namespace MajorSelection.Models.ViewModel
{
    public class Login
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }
    }
}
