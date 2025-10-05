using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmployeeManagementSystem.Filters;

namespace EmployeeManagementSystem.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeRepository repo = new EmployeeRepository();

        // GET: Employee
        public ActionResult Index()
        {
            var employees = repo.GetAllEmployees(); // returns List<Employee>
            return View(employees);
        }

        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            var employee = repo.GetEmployeeById(id);
            if (employee == null) return HttpNotFound();
            return View(employee);
        }

        // GET: Employee/Create
        [CustomAuthorize("Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize("Admin")]
        public ActionResult Create(EmployeeModel model)
        {
            if (ModelState.IsValid)
            {
                repo.AddEmployee(model);
                TempData["Success"] = "Employee added successfully.";
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Employee/Edit/5
        [CustomAuthorize("Admin")]
        public ActionResult Edit(int id)
        {
            var employee = repo.GetEmployeeById(id);
            if (employee == null) return HttpNotFound();
            return View(employee);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize("Admin")]
        public ActionResult Edit(EmployeeModel model)
        {
            if (ModelState.IsValid)
            {
                repo.UpdateEmployee(model);
                TempData["Success"] = "Employee updated successfully.";
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Employee/Delete/5
        [CustomAuthorize("Admin")]
        public ActionResult Delete(int id)
        {
            var employee = repo.GetEmployeeById(id);
            if (employee == null) return HttpNotFound();
            return View(employee);
        }

        // POST: Employee/DeleteConfirmed/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("DeleteConfirmed")]
        [CustomAuthorize("Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            repo.DeleteEmployee(id);
            TempData["Success"] = "Employee deleted successfully.";
            return RedirectToAction("Index");
        }
    }
}