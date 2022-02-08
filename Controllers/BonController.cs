using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Data;
using WebApplication2.Models;
using WebApplication2.Models.ViewModel;

namespace WebApplication2.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BonController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BonController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Bon> objlist = _db.Bony;
            foreach (var obj in objlist)
            {
                obj.product = _db.Products.FirstOrDefault(u => u.Id == obj.ProductID);
            }
            return View(objlist);
        }
        public IActionResult Create()
        {

            BonVM expenseVM = new BonVM()
            {
                bon = new Bon(),
                TypeDropDown = _db.Products.Select(i => new SelectListItem
                {
                    Text = i.ProductName,
                    Value = i.Id.ToString()
                })
            };

            return View(expenseVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BonVM obj)
        {
            obj.bon.Used = false;
            if (ModelState.IsValid)
            {
                _db.Bony.Add(obj.bon);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Bony.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            obj.product = _db.Products.FirstOrDefault(u => u.Id == obj.ProductID);
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Bon obj)
        {
            if (obj == null)
            {
                return NotFound();
            }
            _db.Bony.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Update(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Bony.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            IEnumerable<SelectListItem> TypeDropDown = _db.Products.Select(i => new SelectListItem
            {
                Text = i.ProductName,
                Value = i.Id.ToString()
            });

            ViewBag.TypeDropDown = TypeDropDown;
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Bon obj)
        {
            if (ModelState.IsValid)
            {
                _db.Bony.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
    }
}
