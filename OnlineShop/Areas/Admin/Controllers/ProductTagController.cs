using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Models;
using OnlineShop.Data;

namespace OnlineShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductTagController : Controller
    {
        private ApplicationDbContext _db;
        public ProductTagController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.ProductTags.ToList());
        }
        //Post Index Action Method
        [HttpPost]
        public IActionResult Index(ProductTags productTags)
        {
            var productTag = _db.ProductTags.Where(c => c.ProductTag.Equals(productTags.ProductTag));
            if (productTag == null)
            {
                return NotFound();
            }
            return View(productTag);
        }
        //create get action method

        public ActionResult Create()
        {
            return View();
        }
        //create post action method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind(include: "Id,ProductTag")] ProductTags productTags)
        {
            bool IsProductNameExist = _db.ProductTags.Any(x => x.ProductTag == productTags.ProductTag && x.Id != productTags.Id);
            if (IsProductNameExist == true)
            {
                ModelState.AddModelError("ProductTag", "Already exists");
            }

            if (ModelState.IsValid)
            {
                _db.ProductTags.Add(productTags);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productTags);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productTag = _db.ProductTags.Find(id);
            if (productTag == null)
            {
                return NotFound();
            }
            return View(productTag);
        }
        //Edit post action method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind(include: "Id,ProductTag")] ProductTags productTags)
        {
            bool IsProductNameExist = _db.ProductTags.Any(x => x.ProductTag == productTags.ProductTag && x.Id != productTags.Id);
            if (IsProductNameExist == true)
            {
                ModelState.AddModelError("ProductTag", "Already exists");
            }

            if (ModelState.IsValid)
            {
                _db.Update(productTags);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productTags);
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productTag = _db.ProductTags.Find(id);
            if (productTag == null)
            {
                return NotFound();
            }
            return View(productTag);
        }
        //Details post action method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(ProductTags productTags)
        {
            return RedirectToAction(nameof(Index));
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productTag = _db.ProductTags.Find(id);
            if (productTag == null)
            {
                return NotFound();
            }
            return View(productTag);
        }
        //Delete post action method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, ProductTags productTags)
        {
            if (id == 0)
            {
                return NotFound();
            }
            if (id != productTags.Id)
            {
                return NotFound();
            }
            var productTag = _db.ProductTags.Find(id);
            if (productTag == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _db.Remove(productTag);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productTags);
        }
    }
}