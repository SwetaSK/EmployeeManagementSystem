using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountRepository repo = new AccountRepository();

        [HttpGet]
        public ActionResult Login()
        {
            // Redirect if already logged in
            if (Session["Username"] != null)
                return RedirectToAction("Index", "Employee");

            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password, string role)
        {
            username = username?.Trim();
            password = password?.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(role))
            {
                ViewBag.Error = "Please enter username and select a role.";
                return View();
            }

            // ADMIN: must exist in DB and role must be Admin
            if (role == "Admin")
            {
                if (string.IsNullOrEmpty(password))
                {
                    ViewBag.Error = "Password is required for Admin login.";
                    return View();
                }

                var admin = repo.ValidateAdmin(username, password);
                if (admin != null)
                {
                    Session["Username"] = admin.Username;
                    Session["Role"] = "Admin";
                    return RedirectToAction("Index", "Employee");
                }
                ViewBag.Error = "Invalid admin credentials.";
                return View();
            }

            // USER: if username exists in DB and role is User, use DB role; otherwise allow guest user view-only
            if (role == "User")
            {
                var dbUser = repo.GetUserByUsername(username);
                if (dbUser != null && dbUser.Role == "User")
                {
                    Session["Username"] = dbUser.Username;
                    Session["Role"] = "User";
                    return RedirectToAction("Index", "Employee");
                }

                // ALLOW GUEST USER (not in DB) — view-only
                Session["Username"] = username;
                Session["Role"] = "User";        // treat as User for UI, but server checks Admin-only
                Session["IsGuestUser"] = true;  // optional flag
                return RedirectToAction("Index", "Employee");
            }

            ViewBag.Error = "Invalid role selection.";
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
