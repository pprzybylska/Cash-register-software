using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Data;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class EmployeeController : Controller
    {

        private readonly ApplicationDbContext _db;

        public EmployeeController(ApplicationDbContext db)
        {
           _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Employee> objList = _db.Employees;
            return View(objList);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee obj)
        {
            if (ModelState.IsValid)
            {
                _db.Employees.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET Delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Employees.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return DeletePost(id);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        //POST Delete
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Employees.Find(id);

            if (obj == null)
            {
                return NotFound();
            }
            _db.Employees.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }

        //GET Update
        public IActionResult Update(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Employees.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //POST Update
        public IActionResult UpdatePost(Employee obj)
        {
            if (obj == null)
            {
                return NotFound();
            }
            _db.Employees.Update(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
