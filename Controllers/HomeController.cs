using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Mini_Project.DAL;
using MVC_Mini_Project.Models;
using MVC_Mini_Project.ViewModels;
using System.Diagnostics;

namespace MVC_Mini_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {

            HomeVM vm = new HomeVM();

           
            vm.Products = await _context.Products.Include(p => p.Category).ToListAsync();

            vm.Categories = await _context.Categories.ToListAsync();

            return View(vm);
        }

        public async Task<IActionResult> Detail(int id)
        {
            if (id == 0) return BadRequest();

           
            var product = await _context.Products
                                        .Include(p => p.Category)
                                        .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null) return NotFound();

           
            var relatedProducts = await _context.Products
                                                .Where(p => p.CategoryId == product.CategoryId && p.Id != id)
                                                .Take(4) 
                                                .ToListAsync();

           
            DetailVM vm = new DetailVM
            {
                Product = product,
                RelatedProducts = relatedProducts
            };

            return View(vm);
        }
    }
}