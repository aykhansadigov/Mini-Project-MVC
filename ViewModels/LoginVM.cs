using System.ComponentModel.DataAnnotations;

namespace MVC_Mini_Project.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "İstifadəçi adı və ya Email mütləqdir")]
        public string UsernameOrEmail { get; set; }

        [Required(ErrorMessage = "Şifrə mütləqdir")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}