using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StockTracking.Models.Entity;
using System.Web.Security;

namespace StockTracking.Controllers
{
    public class LoginScreenController : Controller
    {
        // GET: LoginScreen
        DbStockTrackingEntities db = new DbStockTrackingEntities();
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Admin t)
        {
            var informations = db.Admin.FirstOrDefault(x => x.user == t.user && x.password == t.password);
            if (informations != null)
            {
                FormsAuthentication.SetAuthCookie(informations.user, false);
                return RedirectToAction("Index", "Customer");
            }
            else
            {
                return View();
            }
        }
    }
}