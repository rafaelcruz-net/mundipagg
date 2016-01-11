using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MundiPagg.Web.Controllers
{
    public class AccountController : Controller
    {
        // GET: Login
        public ActionResult Create()
        {
            return View();
        }
    }
}