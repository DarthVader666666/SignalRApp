using System.ComponentModel.DataAnnotations;

namespace SignalRApp.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Type Email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Type Password.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
