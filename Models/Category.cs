using System.ComponentModel.DataAnnotations;

namespace MVC_Mini_Project.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Kateqoriya adı boş ola bilməz")]
        public string Name { get; set; }

        public string? ImageUrl { get; set; } 
        public List<Product>? Products { get; set; }
    }
}
