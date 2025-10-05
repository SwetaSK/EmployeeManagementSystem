using System;
using System.Web;
using System.Web.Mvc;

namespace EmployeeManagementSystem.Filters
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly string _role;

        public CustomAuthorizeAttribute(string role)
        {
            _role = role;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            // Check if the user is logged in
            if (httpContext.Session["Username"] == null)
                return false;

            // Get the role from session
            string userRole = httpContext.Session["Role"]?.ToString();

            // Allow if role matches
            return userRole == _role;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            // Redirect to Login if not authorized
            filterContext.Result = new RedirectResult("/Account/Login");
        }
    }
}
