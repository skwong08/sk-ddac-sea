using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ddac.Models;
using ddac.Models.ViewModels;

namespace ddac.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private MyData db = new MyData();
        // GET: Cart
        public ActionResult Index()
        {
            var ticket = db.Tickets.Where(x => x.Email == User.Identity.Name).ToList();
            return View(ticket);
        }
    }
}