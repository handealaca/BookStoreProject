using BookStoreProject.Models.Types;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreProject.Models.Attributes
{
    public class RoleControl : ActionFilterAttribute
    {
        string pagerole = "0";
        public RoleControl(EnumRole rolenumber)
        {
            pagerole = Convert.ToInt32(rolenumber).ToString();
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string roles = context.HttpContext.User.Claims.ToArray()[2].Value;

            if (roles != null)
            {
                string[] rolenames = roles.Split(';');

                bool authority = false;

                foreach (var item in rolenames)
                {
                    if (item.Trim() == pagerole)
                    {
                        authority = true;
                    }

                }

                if (authority)
                {
                    base.OnActionExecuting(context);
                }
                else
                {
                    context.HttpContext.Response.Redirect("/Admin/Error/NoAccess");
                }

            }
            else
            {
                context.HttpContext.Response.Redirect("/Admin/Error/NoAccess");
            }

           
        }
    }
}
