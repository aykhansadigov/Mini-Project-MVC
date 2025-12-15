using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_Mini_Project.Models;
using MVC_Mini_Project.ViewModels;

namespace MVC_Mini_Project.Controllers
{
    public class AccountController : Controller
    {
       
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        
        public IActionResult Register()
        {
            return View();
        }

      
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
          
            if (!ModelState.IsValid)
            {
                return View(registerVM);
            }

        
            AppUser user = new AppUser
            {
                FullName = registerVM.FullName,
                Email = registerVM.Email,
                UserName = registerVM.Username
            };

           
            var result = await _userManager.CreateAsync(user, registerVM.Password);

           
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(registerVM);
            }

           
            await _signInManager.SignInAsync(user, false);

            return RedirectToAction("Index", "Home");
        }


        
        public IActionResult Login()
        {
            return View();
        }

      
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid) return View(loginVM);

           
            var user = await _userManager.FindByNameAsync(loginVM.UsernameOrEmail);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(loginVM.UsernameOrEmail);
            }

            
            if (user == null)
            {
                ModelState.AddModelError("", "İstifadəçi adı və ya şifrə səhvdir!");
                return View(loginVM);
            }

           
            var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, true);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "İstifadəçi adı və ya şifrə səhvdir!");
                return View(loginVM);
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}