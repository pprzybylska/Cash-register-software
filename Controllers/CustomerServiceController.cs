using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Threading.Tasks;
using WebApplication2.Data;
using WebApplication2.Models;


namespace WebApplication2.Controllers
{
    public class CustomerServiceController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CustomerServiceController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var model = new ProductsCartVM();
            IEnumerable<Product> objList = _db.Products;
            IEnumerable<CartItem> cartList = _db.Cart;
            IEnumerable<Bon> bonList = _db.Bony;
            model.Products = objList;
            model.Cart = cartList;
            model.Bons = bonList;

            return View(model);
        }

        public IActionResult DisplayProducts()
        {
            return PartialView();
        }

        public IActionResult DisplayCart()
        {
            return PartialView();
        }

        public IActionResult DisplayBons()
        {
            return PartialView();

        }

        public IActionResult AddToCart(int? id)
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
            return AddToCartPost(obj);
        }

            [HttpPost]
        public IActionResult AddToCartPost(Product obj)
        {
            if (obj == null)
            {
                return NotFound();
            }
            if (_db.Cart.Any(p => p.Product.Id == obj.Id))
            {
                var Itemplus = _db.Cart
                .Where(p => p.Product.Id == obj.Id)
                .FirstOrDefault();

                Itemplus.Amount += 1;
                Itemplus.Price = Itemplus.Amount * Itemplus.Product.ProductPrice;

                if (Itemplus.IsBonUsed == true)
                {
                    Itemplus.Discount += (Itemplus.Discount / (Itemplus.Amount - 1));
                }
                _db.Cart.Update(Itemplus);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                CartItem Item = new()
                {
                    Id = default,
                    Product = obj,
                    Amount = 1,
                    Price = obj.ProductPrice,
                    Discount = 0,
                    IsBonUsed = false
                };
                _db.Cart.Add(Item);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Cart.Find(id);
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
            var obj = _db.Cart.Find(id);

            if (obj == null)
            {
                return NotFound();
            }

            _db.Cart.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }

        public int Sum()
        {
            var sum = 0;
            foreach (var product in _db.Cart)
            {
                sum += product.Price;
            };
            return sum;
        }
    

        public IActionResult UseBon(int? id)
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
            return UseBonPost(id);

        }

        [HttpPost]
        public IActionResult UseBonPost(int? id)
        {
            var obj = _db.Bony.Find(id);

            if (obj == null)
            {
                return NotFound();
            }

            var Item = _db.Cart
            .Where(p => p.Product.Id == obj.ProductID)
            .FirstOrDefault();

            if (Item == null)
            {
                return NotFound();
            }

            if (Item != null)
            {
                if (Item.IsBonUsed == false) 
                { 
                    Item.IsBonUsed = true;
                    obj.Used = true;
                    Item.Price -= obj.Value * Item.Amount;
                    Item.Discount = obj.Value * Item.Amount;
                    _db.Cart.Update(Item);
                    _db.Bony.Update(obj);
                    _db.SaveChanges();
                }
            }

            return RedirectToAction("Index");

        }
    }
}
