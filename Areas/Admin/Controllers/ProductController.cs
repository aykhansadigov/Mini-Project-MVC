using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Mini_Project.DAL;
using MVC_Mini_Project.Models;

namespace MVC_Mini_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        
        private readonly IWebHostEnvironment _env;

        public ProductController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
        
            var products = await _context.Products
                                         .Include(p => p.Category)
                                         .ToListAsync();
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
          
            ViewBag.Categories = await _context.Categories.ToListAsync();

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
           
            ModelState.Remove("Category");
           

            if (!ModelState.IsValid)
            {
               
                ViewBag.Categories = await _context.Categories.ToListAsync();
                return View(product);
            }

           
            if (product.Photo != null)
            {
           
                if (!product.Photo.ContentType.Contains("image"))
                {
                    ModelState.AddModelError("Photo", "Bu fayl şəkil deyil!");
                    ViewBag.Categories = await _context.Categories.ToListAsync();
                    return View(product);
                }

                
                if (product.Photo.Length > 3 * 1024 * 1024)
                {
                    ModelState.AddModelError("Photo", "Şəkilin ölçüsü 3MB-dan çox ola bilməz!");
                    ViewBag.Categories = await _context.Categories.ToListAsync();
                    return View(product);
                }

          
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(product.Photo.FileName);

              
                string path = Path.Combine(_env.WebRootPath, "img", fileName);

          
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await product.Photo.CopyToAsync(stream);
                }

                product.ImageUrl = fileName;
            }
          

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}