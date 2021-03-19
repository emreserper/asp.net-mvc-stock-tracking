using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StockTracking.Models.Entity;
namespace StockTracking.Controllers
{
    public class SaleController : Controller
    {
        // GET: Sale
        DbStockTrackingEntities db = new DbStockTrackingEntities();
        [Authorize]
        public ActionResult Index()
        {
            var sales = db.Sales.ToList();
            return View(sales);
        }

        [HttpGet]
        public ActionResult NewSale()
        {
            //Products
            List<SelectListItem> product = (from x in db.Products.ToList()
                                            select new SelectListItem
                                            {
                                                Text = x.name,
                                                Value = x.id.ToString()
                                            }).ToList();
            ViewBag.drop1 = product;

            //Customers
            List<SelectListItem> cst = (from x in db.Customers.ToList()
                                        select new SelectListItem
                                        {
                                            Text = x.name + " " + x.surname,
                                            Value = x.id.ToString()
                                        }).ToList();
            ViewBag.drop2 = cst;

            //Employees
            List<SelectListItem> empl = (from x in db.Employees.ToList()
                                         select new SelectListItem
                                         {
                                             Text = x.name + " " + x.surname,
                                             Value = x.id.ToString()
                                         }).ToList();
            ViewBag.drop = empl;
            return View();
        }

        [HttpPost]
        public ActionResult NewSale(Sales p)
        {
            var product = db.Products.Where(x => x.id == p.Products.id).FirstOrDefault();
            var customer = db.Customers.Where(x => x.id == p.Customers.id).FirstOrDefault();
            var employee = db.Employees.Where(x => x.id == p.Employees.id).FirstOrDefault();
            p.Products = product;
            p.Customers = customer;
            p.Employees = employee;
            p.date =DateTime.Parse( DateTime.Now.ToShortDateString());
            db.Sales.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}