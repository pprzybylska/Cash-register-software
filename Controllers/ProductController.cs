using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Data;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ProductController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> objList = _db.Products;
            return View(objList);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product obj)
        {
            if(ModelState.IsValid)
            {
                _db.Products.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET Delete
        public IActionResult Delete(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Products.Find(id);
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
            var obj = _db.Products.Find(id);

            var Item = _db.Cart
            .Where(p => p.Product.Id == obj.Id)
            .FirstOrDefault();

            var Bon = _db.Bony
            .Where(p => p.ProductID == obj.Id)
            .FirstOrDefault();

            if (obj == null)
            {
                return NotFound();
            }

            if (Item != null)
            {
                _db.Cart.Remove(Item);
                _db.SaveChanges();
            }

            if (Bon != null)
            {
                _db.Bony.Remove(Bon);
                _db.SaveChanges();
            }
            _db.Products.Remove(obj);
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
            var obj = _db.Products.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //POST Update
        public IActionResult UpdatePost(Product obj)
        {
            if (obj == null)
            {
                return NotFound();
            }
            _db.Products.Update(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
