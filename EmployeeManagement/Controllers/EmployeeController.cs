using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
        
        public IActionResult Index()
        {
            var res = from s in _context.Employee select s;

            return View(res.ToList());
        }
        [HttpGet]
        public IActionResult Forgot()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Forgot(ForgotPassword pass)
        {
            if (pass == null)
            {

            }
            else
            {
                if (ModelState.IsValid)
                {
                    List<Model> list;
                    string sql = "exec sp_forgot @email, @password, @cnfpassword";
                    List<SqlParameter> parameters = new List<SqlParameter>()
                    {
                        new SqlParameter{ParameterName = "@email", Value = Convert.ToString(pass.Email) },
                        new SqlParameter{ParameterName = "@password",Value=Convert.ToString(pass.Password)},
                        new SqlParameter{ParameterName = "@cnfpassword", Value=Convert.ToString(pass.ConfirmPassword)}
                    };
                    var res = _context.Database.ExecuteSqlRaw(sql, parameters.ToArray());
                    if (res > 0)
                    {
                        TempData["Success"] = "Password Updated Successfully!";
                        return RedirectToAction("Login", "Employee");
                    }
                }
            }
            return View();
        }
    }
}
