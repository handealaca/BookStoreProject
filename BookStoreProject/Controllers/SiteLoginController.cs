using BookStoreProject.Models.ORM.Context;
using BookStoreProject.Models.ORM.Entities;
using BookStoreProject.Models.VM;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookStoreProject.Controllers
{
    public class SiteLoginController : Controller
    {
       
        private readonly BookContext _bookcontext;

        public SiteLoginController(BookContext bookcontext)
        {
            _bookcontext = bookcontext;
        }

       

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserLoginVM model)
        {

            if (ModelState.IsValid)
            {
               
                User user = _bookcontext.Users.FirstOrDefault(x => x.Email == model.Login.EMail && x.Password == model.Login.Password);
                if (user != null)
                {
                    model.Login.Name = user.Name;

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, model.Login.EMail),
                        new Claim(ClaimTypes.Name, model.Login.Name),
                        new Claim(ClaimTypes.Sid, user.ID.ToString()),
                        new Claim(ClaimTypes.UserData, "User")
                     };


                    var userIdentity = new ClaimsIdentity(claims, "login");

                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                    await HttpContext.SignInAsync(principal);

                    user.Lastlogindate = DateTime.Now;

                    _bookcontext.SaveChanges();

                    return RedirectToAction("Index","SiteHome");
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
            return RedirectToAction("Index", "SiteAccount");
        }
    }
}
