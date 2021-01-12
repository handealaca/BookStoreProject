using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreProject.Models.Attributes
{
    public class SiteAuth : AuthorizeAttribute, IAuthorizationFilter
    {

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {

               string siterole = context.HttpContext.User.Claims.ToArray()[3].Value;

                if (siterole == "User")
                {
                    //context.HttpContext.Response.Redirect("/SiteHome/Books");

                }
                else
                {
                    context.HttpContext.Response.Redirect("/SiteAccount/");
                }
            }
            else
            {
                //context.HttpContext.Response.Redirect("/SiteError/NoAccess");
                context.HttpContext.Response.Redirect("/SiteAccount/");

            }
        }
    }
}
