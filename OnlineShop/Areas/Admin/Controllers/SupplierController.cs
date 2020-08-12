using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Models;
using OnlineShop.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Razor;

namespace OnlineShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SupplierController : Controller
    {
        private ApplicationDbContext _db;
        [Obsolete]
        private IHostingEnvironment _he;

        [Obsolete]
        public SupplierController(ApplicationDbContext db, IHostingEnvironment he)
        {
            _db = db;
            _he = he;
        }
        public IActionResult Index()
        {
            return View(_db.Suppliers.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }
        //Create Post method
        [HttpPost]
        [Obsolete]
        public async Task<IActionResult> Create(Supplier supplier,IFormFile image)
        {
            if(ModelState.IsValid)
            {
                if (image != null)
                {
                    var name = Path.Combine(_he.WebRootPath + "/Images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    supplier.Image = "Images/" + image.FileName;
                }
                if (image == null)
                {
                    supplier.Image = "Images/images.jpg";
                }
                _db.Suppliers.Add(supplier);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(supplier);
        }
    }
}