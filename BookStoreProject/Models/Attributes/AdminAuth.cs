using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreProject.Models.Attributes
{
    public class AdminAuth : Attribute, IAuthorizationFilter
    {

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {

                string siterole = context.HttpContext.User.Claims.ToArray()[3].Value;

                if (siterole == "Admin")
                {
                    //context.HttpContext.Response.Redirect("/SiteHome/Books");

                }
                else
                {
                    context.HttpContext.Response.Redirect("/Admin/AdminLogin/");
                }
            }
            else
            {
                context.HttpContext.Response.Redirect("/Admin/AdminLogin/");

            }
        }
    }
}
