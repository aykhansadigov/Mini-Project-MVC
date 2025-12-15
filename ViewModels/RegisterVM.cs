using System.ComponentModel.DataAnnotations;


namespace MVC_Mini_Project.ViewModels
    {
        public class RegisterVM
        {
            [Required(ErrorMessage = "Ad və Soyad mütləqdir")]
            public string FullName { get; set; }

            [Required(ErrorMessage = "İstifadəçi adı mütləqdir")]
            public string Username { get; set; }

            [Required(ErrorMessage = "Email mütləqdir")]
            [EmailAddress(ErrorMessage = "Düzgün email yazın")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Şifrə mütləqdir")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Required(ErrorMessage = "Şifrə təkrarı mütləqdir")]
            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "Şifrələr eyni deyil")]
            public string ConfirmPassword { get; set; }
        }
}

