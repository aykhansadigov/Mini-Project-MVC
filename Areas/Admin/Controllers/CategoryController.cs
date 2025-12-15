using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Mini_Project.DAL;    
using MVC_Mini_Project.Models;  
TAM BITIRE BILMEIDM 
namespace MVC_Mini_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

       
        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories.ToListAsync();
            return View(categories);
        }

        
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        
        //[HttpPost]
        //public async Task<IActionResult> Create(Category category)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View();
        //    }

      
        //    bool isExists = await _context.Categories.AnyAsync(c => c.Name.ToLower() == category.Name.ToLower());
        //    if (isExists)
        //    {
        //        ModelState.AddModelError("Name", "Bu adda kateqoriya artıq mövcuddur");
        //        return View();
        //    }

        //    await _context.Categories.AddAsync(category);
        //    await _context.SaveChangesAsync();

        //    return RedirectToAction(nameof(Index));
        //}
        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            
            ModelState.Remove("Products");
            ModelState.Remove("ImageUrl");

           
            if (string.IsNullOrEmpty(category.Name))
            {
                ModelState.AddModelError("Name", "Zəhmət olmasa kateqoriya adını yazın!");
                return View(category);
            }

            
            if (!ModelState.IsValid)
            {
                return View(category);
            }

            
            bool isExists = await _context.Categories.AnyAsync(c => c.Name.ToLower() == category.Name.ToLower());

            if (isExists)
            {
                ModelState.AddModelError("Name", "Bu adda kateqoriya artıq mövcuddur");
                return View(category);
            }

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
