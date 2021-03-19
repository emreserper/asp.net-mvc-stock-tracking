using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StockTracking.Models.Entity;

namespace StockTracking.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        DbStockTrackingEntities db = new DbStockTrackingEntities();
        [Authorize]
        public ActionResult Index(string p)
        {
            //var products = db.Products.Where(x=> x.status==true).ToList();
            var products= db.Products.Where(x => x.status == true);
            if (!string.IsNullOrEmpty(p))
            {
                products = products.Where(x => x.name.Contains(p) && x.status == true);
            }
            return View(products.ToList());
        }

        [HttpGet]
        public ActionResult NewProduct()
        {
            List<SelectListItem> cat = (from x in db.Categories.ToList()
                                        select new SelectListItem
                                        {
                                            Text = x.name,
                                            Value = x.id.ToString()
                                        }).ToList();
            ViewBag.dropdownCategory = cat;
            return View();
        }

        [HttpPost]
        public ActionResult NewProduct(Products p)
        {
            p.status = true;
            var cat = db.Categories.Where(x => x.id == p.Categories.id).FirstOrDefault();
            p.Categories = cat;
            db.Products.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
         
        }

        public ActionResult GetProduct(int id)
        {
            List<SelectListItem> cat = (from x in db.Categories.ToList() select new SelectListItem
            {
                Text= x.name,
                Value = x.id.ToString()
            }).ToList();
            var prc = db.Products.Find(id);
            ViewBag.productCategory = cat;
            return View("GetProduct", prc);
        }

        public ActionResult UpdateProduct(Products p)
        {
            var product = db.Products.Find(p.id);
            product.name = p.name;
            product.brand = p.brand;
            product.puchase_price = p.puchase_price;
            product.sale_price = p.sale_price;
            product.stock = p.stock;
            var cat = db.Categories.Where(x => x.id == p.Categories.id).FirstOrDefault();
            product.category = cat.id;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteProduct(Products p1)
        {
            var findProduct = db.Products.Find(p1.id);
            findProduct.status = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}