using FrontToBackMvc.Models;
using FrontToBackMvc.ViewModels.Auths;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FrontToBackMvc.Controllers
{
    public class AccountController(UserManager<User> _userMeneger) : Controller
    {
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserVM vm)
        {
            if (!ModelState.IsValid) return View();
            User user = new User
            {
                UserName = vm.UserName,
                Email = vm.Email,
                FullName = vm.FullName,
                ProfileImageUrl = "ProfilePhote.img"
            };
            var result = await _userMeneger.CreateAsync(user, vm.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }

            return View();
        }
    }
}
