using BookStoreProject.Models.ORM.Context;
using BookStoreProject.Models.ORM.Entities;
using BookStoreProject.Models.VM;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookStoreProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminLoginController : Controller
    {
        private readonly BookContext _bookcontext;

        public AdminLoginController(BookContext bookcontext)
        {
            _bookcontext = bookcontext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginVM model)
        {

            if (ModelState.IsValid)
            {
                AdminUser adminuser = _bookcontext.AdminUsers.FirstOrDefault(x => x.Email == model.EMail && x.Password == model.Password);
                if (adminuser != null)
                {
                    model.Name = adminuser.Name;

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, model.EMail),
                        new Claim(ClaimTypes.Name, model.Name),
                        new Claim(ClaimTypes.Role,adminuser.Role)
                     };
               

                    var userIdentity = new ClaimsIdentity(claims, "login");

                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                    await HttpContext.SignInAsync(principal);

                    adminuser.Lastlogindate = DateTime.Now;

                    _bookcontext.SaveChanges();

                    return RedirectToRoute("default", new { controller = "AdminHome", action = "Index" });
                }
                else
                {
                    ViewBag.error = "Email or Password is wrong!";
                    return View();
                }
            }
            else
            {

                return View();
            }

        }


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }

    }
}

