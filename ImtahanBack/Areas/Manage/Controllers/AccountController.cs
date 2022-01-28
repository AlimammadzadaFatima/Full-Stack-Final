using ImtahanBack.Areas.ViewModel;
using ImtahanBack.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImtahanBack.Areas.Manage.Controllers
{
    [Area("manage")]
    public class AccountController : Controller
    {
        private readonly RoleManager<IdentityRole> _role;
        private readonly SignInManager<AppUser> _sign;
        private readonly UserManager<AppUser> _user;

        public AccountController(RoleManager<IdentityRole> role, SignInManager <AppUser> sign, UserManager<AppUser> user)
        {
            _role = role;
            _sign = sign;
            _user = user;
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AdminLoginViewModel AdminVM)
        {
            AppUser admin = _user.Users.FirstOrDefault(x => x.NormalizedUserName == AdminVM.UserName.ToUpper() && x.IsAdmin);
            if (admin == null)
            {
                ModelState.AddModelError("", "Parol ve ya ad sehvdir");
                return View();
            }
           
         
           var result= await _sign.PasswordSignInAsync(admin, AdminVM.Password, false, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Parol ve ya ad sehvdir");
                return View();
            }
          
                return RedirectToAction("index", "dashboard");
        }
        public async Task<IActionResult> LogOut()
        {
            await _sign.SignOutAsync();
            return RedirectToAction("login", "account");
        }

        //public async Task<IActionResult> CreateRole()
        //{
        //    var role1 = await _role.CreateAsync(new IdentityRole("SuperAdmin"));
        //    var role2 = await _role.CreateAsync(new IdentityRole("Admin"));
        //    var role3 = await _role.CreateAsync(new IdentityRole("User"));
        //    var admin = new AppUser { FullName = "Super Admin", UserName="SuperAdmin", IsAdmin = true };
        //    var result = await _user.CreateAsync(admin, "SuperAdmin123");
        //    var adminrole = await _user.AddToRoleAsync(admin, "SuperAdmin");
        //    return Ok(adminrole);

        //}
    }
}
