using MVC_Mini_Project.Models;

namespace MVC_Mini_Project.ViewModels
{
    public class DetailVM
    {
        public Product Product { get; set; }
        public List<Product> RelatedProducts { get; set; }
    }
}