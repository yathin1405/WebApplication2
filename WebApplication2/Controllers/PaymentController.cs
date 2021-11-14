using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Controllers
{
    public class PaymentController : Controller
    {
        // GET: Payment
        public ActionResult Success()
        {
            return View();
        }


        // GET: Payment - Cancelled
        public ActionResult Cancelled()
        {
            return View();
        }

        // GET: Payment - Notify
        public ActionResult Notify()
        {
            return View();
        }
    }
}
