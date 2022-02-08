using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Owin.Security.DataProtection;
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
    public class CashiersController : Controller
    {
        private readonly ApplicationDbContext _db;
        UserManager<ApplicationUser> _userManager;
        SignInManager<ApplicationUser> _signInManager;
        RoleManager<IdentityRole> _roleManager;

        public CashiersController(ApplicationDbContext db, UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            var doctors = (from user in _db.Users
                           join userRoles in _db.UserRoles on user.Id equals userRoles.UserId
                           join roles in _db.Roles.Where(x => x.Name == Role.Role.Kasjer) on userRoles.RoleId equals roles.Id
                           select new CashierVM
                           {
                               Id = user.Id,
                               Name = user.Name,
                               Surname = user.Surname,
                               Email = user.Email,
                               Transactions = user.Transactions
                           }
                 ).ToList();
            return View(doctors);
        }

        public async Task<ActionResult> Delete(string? id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeletePost(string? id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            await _userManager.DeleteAsync(user);

            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Update(string? id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //POST Update
        public async Task<ActionResult> UpdatePost(ApplicationUser obj)
        {
            string id = obj.Id;
            var user = await _userManager.FindByIdAsync(id);
            user.Name = obj.Name;
            user.Surname = obj.Surname;
            user.Email = obj.Email;
            user.UserName = obj.Email;
            if (user == null)
            {
                return NotFound();
            }
            await _userManager.UpdateAsync(user);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }

        public async Task<ActionResult> ChangePassword(string? id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var changepasswordvm = new ChangePasswordVM
            {
                Id = user.Id,
                Password = null
            };
            if (user == null)
            {
                return NotFound();
            }
            return View(changepasswordvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //POST Update
        public async Task<ActionResult> ChangePassword(ChangePasswordVM obj)
        { 
            if(ModelState.IsValid)
            {
                ViewBag.Message = "Hasło zmienione pomyślnie!";
                var user = await _userManager.FindByIdAsync(obj.Id);
                await _userManager.RemovePasswordAsync(user);
                var result = await _userManager.AddPasswordAsync(user, obj.Password);
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                    ViewBag.Message = "";
                }
            }
            return View(obj);


        }
    }
}
