using System;
using System.Web.Mvc;

namespace MvcCV.Attributes
{
    /// <summary>
    /// Admin authorization attribute
    /// Used for pages that only authenticated admin users can access
    /// </summary>
    public class AdminAuthAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Session check
            if (filterContext.HttpContext.Session["Username"] == null)
            {
                // User not logged in, redirect to login page
                filterContext.Result = new RedirectResult("~/Login/Index");
                return;
            }

            // Session timeout check (30 minutes)
            if (filterContext.HttpContext.Session["LastActivity"] != null)
            {
                DateTime lastActivity = (DateTime)filterContext.HttpContext.Session["LastActivity"];
                if (DateTime.Now.Subtract(lastActivity).TotalMinutes > 30)
                {
                    // Session expired, logout
                    filterContext.HttpContext.Session.Clear();
                    filterContext.Result = new RedirectResult("~/Login/Index");
                    return;
                }
            }

            // Update last activity time
            filterContext.HttpContext.Session["LastActivity"] = DateTime.Now;

            base.OnActionExecuting(filterContext);
        }
    }

    /// <summary>
    /// Secure page attribute
    /// Adds HTTPS enforcement and security headers
    /// </summary>
    public class SecurePageAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var response = filterContext.HttpContext.Response;
            
            // Add security headers
            response.Headers.Add("X-Content-Type-Options", "nosniff");
            response.Headers.Add("X-Frame-Options", "DENY");
            response.Headers.Add("X-XSS-Protection", "1; mode=block");
            response.Headers.Add("Referrer-Policy", "strict-origin-when-cross-origin");
            
            // Cache-Control header (for sensitive pages)
            response.Headers.Add("Cache-Control", "no-cache, no-store, must-revalidate");
            response.Headers.Add("Pragma", "no-cache");
            response.Headers.Add("Expires", "0");

            base.OnActionExecuting(filterContext);
        }
    }
}