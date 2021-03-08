using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Models;
using OnlineShop.Data;
using Microsoft.AspNetCore.Authorization;

namespace OnlineShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize]
    public class ProductTypesController : Controller
    {
        private ApplicationDbContext _db;
        public ProductTypesController(ApplicationDbContext db)
        {
            _db = db;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View(_db.ProductTypes.ToList());
        }
        //Post Index Action Method
        [HttpPost]
        public IActionResult Index(ProductTypes productTypes)
        {
            var productType = _db.ProductTypes.Where(c => c.ProductType.Equals(productTypes.ProductType));
            if (productType == null)
            {
                return NotFound();
            }
            return View(productType);
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,ProductName,ProductQuantity,UnitPrice")]
        //                    Product product)
        //{

        //    bool IsProductNameExist = db.Products.Any
        //         (x => x.ProductName == product.ProductName && x.Id != product.Id);
        //    if (IsProductNameExist == true)
        //    {
        //        ModelState.AddModelError("ProductName", "ProductName already exists");
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        db.Products.Add(product);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(product);
        //}

        //create get action method
        
        public ActionResult Create()
        {
            return View();
        }
        //create post action method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind(include: "Id,ProductType")] ProductTypes productTypes)
        {
            bool IsProductNameExist = _db.ProductTypes.Any(x => x.ProductType == productTypes.ProductType && x.Id != productTypes.Id);
            if (IsProductNameExist == true)
            {
                ModelState.AddModelError("ProductType", "Already exists");
            }

            if (ModelState.IsValid)
            {
                _db.ProductTypes.Add(productTypes);
                await _db.SaveChangesAsync();
                TempData["save"] = "Product Type has been saved";
                return RedirectToAction(nameof(Index));
            }
            return View(productTypes);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productType = _db.ProductTypes.Find(id);
            if (productType == null)
            {
                return NotFound();
            }
            return View(productType);
        }
        //Edit post action method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind(include: "Id,ProductType")] ProductTypes productTypes)
        {
            bool IsProductNameExist = _db.ProductTypes.Any(x => x.ProductType == productTypes.ProductType && x.Id != productTypes.Id);
            if (IsProductNameExist == true)
            {
                ModelState.AddModelError("ProductType", "Already exists");
            }

            if (ModelState.IsValid)
            {
                _db.Update(productTypes);
                await _db.SaveChangesAsync();
                TempData["edit"] = "Edit Successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(productTypes);
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productType = _db.ProductTypes.Find(id);
            if (productType == null)
            {
                return NotFound();
            }
            return View(productType);
        }
        //Details post action method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(ProductTypes productTypes)
        {
            return RedirectToAction(nameof(Index));
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productType = _db.ProductTypes.Find(id);
            if (productType == null)
            {
                return NotFound();
            }
            return View(productType);
        }
        //Delete post action method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, ProductTypes productTypes)
        {
            if (id == 0)
            {
                return NotFound();
            }
            if (id != productTypes.Id)
            {
                return NotFound();
            }
            var productType = _db.ProductTypes.Find(id);
            if (productType == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _db.Remove(productType);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productTypes);
        }
    }
}