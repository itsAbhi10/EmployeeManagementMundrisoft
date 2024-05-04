using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Microsoft.Win32;
using System.Security.Cryptography;

namespace EmployeeManagement.Controllers
{
    public class EmployeeController : Controller
    {
        public readonly Databasecontext _context;
        public EmployeeController(Databasecontext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Login(RegistrationModel obj)
        {
            if (obj == null)
            {

            }
            else
            {
                if (ModelState.IsValid)
                {
                    var res = _context.RegistrationModel.Any(data => data.EmailId == obj.EmailId && data.Password == obj.Password);
                    if (res)
                    {
                        
                        return RedirectToAction("Employee", "Index");
                    }
                }
            }
            return View(); 
        }
        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Registration(RegistrationModel register)
        {
            if (register == null)
            {

            }
            else
            {
                if (ModelState.IsValid)
                {
                    if (register.Password != register.ConfirmPassword)
                    {
                        ModelState.AddModelError("ConfirmPassword", "Passwords do not match");
                        return View(register);
                    }
                    _context.Add(register);
                    int x = _context.SaveChanges();
                    if (x > 0)
                    {
                        return RedirectToAction("Login", "Employee");
                    }
                }
            }
            return View();
        }
        public IActionResult Logout()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create( EmployeeModel emp)
        {
            if (emp == null)
            {

            }
            else
            {
                if (ModelState.IsValid)
                {
                    _context.Add(emp);
                    int x = _context.SaveChanges();
                    if (x > 0)
                    {
                        return RedirectToAction("Index", "Employee");
                    }

                }
            }
            return View();
        }
        public IActionResult Details(int EID)
        {
            var res = _context.Employee.Find(EID);

            return View(res);
        }
        [HttpGet]
        public IActionResult Edit(int EID)
        {
            var res = _context.Employee.Find(EID);

            return View(res);
        }

        [HttpPost]
        public IActionResult Edit(EmployeeModel obj, int? EID)
        {
            if (obj == null)
            {

            }
            else
            {
                if (ModelState.IsValid)
                {
                    _context.Update(obj);
                    int x = _context.SaveChanges();
                    if (x > 0)
                    {
                        return RedirectToAction("Index", "Employee");
                    }
                }
            }
            return View();
        }
       
        public IActionResult Delete(int EID)
        {
            var res = _context.Employee.Find(EID);

            return View(res);
        }
        //[HttpPost]
        //public IActionResult Delete(EmployeeModel del, int? EID)
        //{
        //    if (del == null)
        //    {

        //    }
        //    else
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            _context.Update(del);
        //            int x = _context.SaveChanges();
        //            if (x > 0)
        //            {
        //                return RedirectToAction("ViewRoom", "Admin");
        //            }
        //        }
        //    }
        //    return View();
        //}
        public IActionResult Index()
        {
            var res = from s in _context.Employee select s;

            return View(res.ToList());
        }

        public IActionResult Forgot()
        {
            return View();
        }
    }
}
