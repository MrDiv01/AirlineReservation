using AirlineReservation.Areas.Admin.ViewModels;
using AirlineReservation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AirlineReservation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(AdminLoginViewModel adminLogin)
        {
            AppUser appUser = await _userManager.FindByNameAsync(adminLogin.UserName);
            if (appUser == null)
            {
                ModelState.AddModelError("", "UserName or Password Incorrect!");
                return View();
            }
          var result = await _signInManager.PasswordSignInAsync(appUser, adminLogin.Password, false, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "UserName or Password Incorrect!");
                return View();
            }
            return RedirectToAction("Index","Dashboard");
        }
    }
}
