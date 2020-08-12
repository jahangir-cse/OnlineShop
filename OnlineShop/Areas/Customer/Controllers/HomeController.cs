using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineShop.Models;
using OnlineShop.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Http;
using OnlineShop.Utility;
using X.PagedList;
using X.PagedList.Mvc.Core;

namespace OnlineShop.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index(int? page)
        {
            return View(_db.Products.Include(c => c.ProductTypes).Include(c => c.ProductTags).ToList().ToPagedList(page ?? 1,9));
        }

        //private IActionResult View(object p)
        //{
        //    throw new NotImplementedException();
        //}

        //private IActionResult View<TProperty>(Func<Expression<Func<Products, TProperty>>, IIncludableQueryable<Products, TProperty>> include)
        //{
        //    throw new NotImplementedException();
        //}

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        //get product details ction method
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = _db.Products.Include(c => c.ProductTypes).FirstOrDefault(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        //post product details action method
        [HttpPost]
        [ActionName("Details")]
        public ActionResult ProductDetails(int? id)
        {
            List<Products> products = new List<Products>();
            if (id == null)
            {
                return NotFound();
            }
            var product = _db.Products.Include(c => c.ProductTypes).FirstOrDefault(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            products = HttpContext.Session.Get<List<Products>>("products");
            if (products == null)
            {
                products = new List<Products>();
            }
            products.Add(product);
            HttpContext.Session.Set("products", products);
            return View(product);
        }
        //get remove action method
        [ActionName("Remove")]
        public IActionResult RemoveToCart(int? id)
        {
            List<Products> products = HttpContext.Session.Get<List<Products>>("products");
            if (products != null)
            {
                var product = products.FirstOrDefault(c => c.Id == id);
                if (product != null)
                {
                    products.Remove(product);
                    HttpContext.Session.Set("products", products);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Remove(int? id)
        {
            List<Products> products = HttpContext.Session.Get<List<Products>>("products");
            if (products != null)
            {
                var product = products.FirstOrDefault(c => c.Id == id);
                if (product != null)
                {
                    products.Remove(product);
                    HttpContext.Session.Set("products", products);
                }
            }
            return RedirectToAction(nameof(Index));
        }
        //get product cart action method
        public IActionResult Cart()
        {
            List<Products> products = HttpContext.Session.Get<List<Products>>("products");
            if (products == null)
            {
                products = new List<Products>();
            }
            return View(products);
        }
    }
}
