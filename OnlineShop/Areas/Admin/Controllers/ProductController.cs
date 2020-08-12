using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Models;
using OnlineShop.Data;
using System.IO;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace OnlineShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private ApplicationDbContext _db;
        [Obsolete]
        private IHostingEnvironment _he;

        [Obsolete]
        public ProductController(ApplicationDbContext db, IHostingEnvironment he)
        {
            _db = db;
            _he = he;
        }
        public IActionResult Index()
        {
            return View(_db.Products.Include(c => c.ProductTypes).Include(f => f.ProductTags).ToList());
        }

        //post index action method
        [HttpPost]
        public IActionResult Index(decimal? lowAmount, decimal? largeAmount)
        {
            var products = _db.Products.Include(c => c.ProductTypes).Include(c => c.ProductTags)
                .Where(c => c.Price >= lowAmount && c.Price <= largeAmount).ToList();
            if (lowAmount == null || largeAmount == null)
            {
                products = _db.Products.Include(c => c.ProductTypes).Include(c => c.ProductTags).ToList();
            }
            return View(products);
        }

        //Get Create method
        public IActionResult Create()
        {
            ViewData["productTypeId"] = new SelectList(_db.ProductTypes.ToList(), "Id", "ProductType");
            ViewData["productTagId"] = new SelectList(_db.ProductTags.ToList(), "Id", "ProductTag");
            return View();
        }
        //Post Create method
        [HttpPost]
        [Obsolete]
        public async Task<IActionResult> Create(Products products, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                var searchProduct = _db.Products.FirstOrDefault(c => c.Name == products.Name);
                if (searchProduct != null)
                {
                    ViewBag.message = "This product is already exist";
                    ViewData["productTypeId"] = new SelectList(_db.ProductTypes.ToList(), "Id", "ProductType");
                    ViewData["productTagId"] = new SelectList(_db.ProductTags.ToList(), "Id", "ProductTag");
                    return View(products);
                }
                if (image != null)
                {
                    var name = Path.Combine(_he.WebRootPath + "/Images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    products.Image = "Images/" + image.FileName;
                }
                if (image == null)
                {
                    products.Image = "Images/images.png";
                }
                _db.Products.Add(products);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(products);
        }

        public ActionResult Edit(int? id)
        {
            ViewData["productTypeId"] = new SelectList(_db.ProductTypes.ToList(), "Id", "ProductType");
            ViewData["productTagId"] = new SelectList(_db.ProductTags.ToList(), "Id", "ProductTag");
            if (id == null)
            {
                return NotFound();
            }
            var product = _db.Products.Include(c => c.ProductTypes).Include(c => c.ProductTags).FirstOrDefault(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        //post edit action method
        [HttpPost]
        [Obsolete]
        public async Task<IActionResult> Edit(Products products, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                var searchProduct = _db.Products.FirstOrDefault(c => c.Name == products.Name);
                if (searchProduct != null)
                {
                    ViewBag.message = "This product is already exist";
                    ViewData["productTypeId"] = new SelectList(_db.ProductTypes.ToList(), "Id", "ProductType");
                    ViewData["productTagId"] = new SelectList(_db.ProductTags.ToList(), "Id", "ProductTag");
                    return View(products);
                }
                if (image != null)
                {
                    var name = Path.Combine(_he.WebRootPath + "/Images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    products.Image = "Images/" + image.FileName;
                }
                if (image == null)
                {
                    products.Image = "Images/images.jpg";
                }
                _db.Products.Update(products);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(products);
        }
        //get details action method
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = _db.Products.Include(c => c.ProductTypes).Include(c => c.ProductTags).FirstOrDefault(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        //get delete action method
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = _db.Products.Include(c => c.ProductTags).Include(c => c.ProductTypes).Where(c => c.Id == id).FirstOrDefault();
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        //post delete action method
        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirm(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = _db.Products.FirstOrDefault(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}