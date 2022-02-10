using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
    public class TransactionsController : Controller
    {
        private readonly ApplicationDbContext _db;
        UserManager<ApplicationUser> _userManager;

        public TransactionsController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            IEnumerable<Transaction> objList = _db.Transactions;
            return View(objList);
        }
        public Task<IActionResult> FinalizeGet()
        {
            IEnumerable<CartItem> objList = _db.Cart;
            return FinalizePost(objList);// dodalem (IActionResult) bo tak kazal vs
        }

        [HttpPost]
        public async Task<IActionResult> FinalizePost(IEnumerable<CartItem> objList)
        {
            int sum = 0, discount = 0;
            string name;

            foreach(var item in objList)
            {
                sum += item.Price;
                discount += item.Discount;
            }

            name = User.Identity.Name;

            ApplicationUser CurrentUser = _db.Users
            .Where(p => p.UserName == name)
            .FirstOrDefault();

            //powiekszenie ilosci transkacji zalogowanego kasjera
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            user.Transactions++;
            await _userManager.UpdateAsync(user);
            // sprawdz czy to dodalem do dobrej akcji
            Transaction NewTransaction = new();
            NewTransaction.Sum = sum;
            NewTransaction.Discount = discount;
            NewTransaction.User = CurrentUser;

            _db.Transactions.Add(NewTransaction);
            _db.Cart.RemoveRange(_db.Cart);
            _db.SaveChanges();

            return RedirectToAction("Index", "CustomerService");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Transactions.Find(id);
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
            var obj = _db.Transactions.Find(id);

            if (obj == null)
            {
                return NotFound();
            }
            _db.Transactions.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
