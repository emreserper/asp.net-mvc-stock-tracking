using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StockTracking.Models.Entity;

namespace StockTracking.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        DbStockTrackingEntities db = new DbStockTrackingEntities();
        [Authorize]
        public ActionResult Index()
        {
            var categories = db.Categories.ToList();
            return View(categories);
        }

        [HttpGet]
        public ActionResult NewCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewCategory(Categories p)
        {
            db.Categories.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteCategory(int id)
        {
            var cat = db.Categories.Find(id);
            db.Categories.Remove(cat);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult GetCategory(int id)
        {
            var cat = db.Categories.Find(id);
            return View("GetCategory", cat);
        }

        public ActionResult UpdateCategory(Categories c)
        {
            var cat = db.Categories.Find(c.id);
            cat.name = c.name;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}