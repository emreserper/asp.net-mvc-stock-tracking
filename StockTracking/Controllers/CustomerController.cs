using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StockTracking.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace StockTracking.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        DbStockTrackingEntities db = new DbStockTrackingEntities();
        [Authorize]
        public ActionResult Index(int page = 1)
        {
            //var CustomerList = db.Customers.ToList();
            var CustomerList = db.Customers.Where(x => x.status == true).ToList().ToPagedList(page, 3);
            return View(CustomerList);
        }

        [HttpGet]
        public ActionResult NewCustomer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewCustomer(Customers p)
        {
            if (!ModelState.IsValid)
            {
                return View("NewCustomer");
            }
            p.status = true;
            db.Customers.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteCustomer(Customers p)
        {
            var findCustomer = db.Customers.Find(p.id);
            findCustomer.status = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult GetCustomer(int id)
        {
            var customer = db.Customers.Find(id);
            return View("GetCustomer", customer);
        }

        public ActionResult UpdateCustomer(Customers t)
        {
            var customer = db.Customers.Find(t.id);
            customer.name = t.name;
            customer.surname = t.surname;
            customer.city = t.city;
            customer.balance = t.balance;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}