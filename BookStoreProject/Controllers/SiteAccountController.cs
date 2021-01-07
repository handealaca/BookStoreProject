using BookStoreProject.Models.ORM.Context;
using BookStoreProject.Models.ORM.Entities;
using BookStoreProject.Models.VM;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace BookStoreProject.Controllers
{
    public class SiteAccountController : Controller
    {
        private readonly BookContext _bookcontext;
        public SiteAccountController(BookContext bookcontext)
        {
            _bookcontext = bookcontext;
        }

        public ActionResult Index()
        {
            var model = new UserLoginVM();
            model.Login = new LoginVM();
            model.register = new UserVM();
            return View(model);
        }


        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {

            if (ModelState.IsValid)
            {

                User user = _bookcontext.Users.FirstOrDefault(x => x.Email == model.EMail && x.Password == model.Password);
                if (user != null)
                {
                    model.Name = user.Name;

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, model.EMail),
                        new Claim(ClaimTypes.Name, model.Name),
                        new Claim(ClaimTypes.Sid, user.ID.ToString()),
                        new Claim(ClaimTypes.UserData, "User")
                     };

                    //TempData["UserEmail"] = model.EMail;
                
                    
                    TempData["UserID"] = user.ID.ToString();

                    TempData["UserName"] = model.Name;


                    var userIdentity = new ClaimsIdentity(claims, "login");

                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                    await HttpContext.SignInAsync(principal);

                    user.Lastlogindate = DateTime.Now;

                    _bookcontext.SaveChanges();

                    return RedirectToAction("Index", "SiteHome");
                }
                else
                {
                    ViewBag.error = "Email or Password is wrong!";
                    return View("Index", new UserLoginVM { register = null, Login = null });

                    //return RedirectToAction("Index", "SiteAccount");
                }
            }

            else
            {
                return View("Index", new UserLoginVM { register = null, Login = model });
            }

        }



        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            //FormsAuthentication.LogOut();
            HttpContext.User =
                new GenericPrincipal(new GenericIdentity(string.Empty), null);

            return RedirectToAction("Index", "SiteAccount");
        }

        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Register(UserVM model)
        {
            if (ModelState.IsValid)
            {

                User user = new User();
                user.Password = model.UserPassword;
                user.Email = model.UserEmail;

                _bookcontext.Users.Add(user);
                _bookcontext.SaveChanges();

                int userid = user.ID;
                //return Redirect("/SiteAccount/Edit/" + userid);
                return RedirectToAction("Edit", "UserAction", new { id = userid });
            }

            else
            {
                return View("Index", new UserLoginVM { register = model, Login = null });

                //return Redirect("/SiteAccount/Index/");
            }




        }
    }
}
