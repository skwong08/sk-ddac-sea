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
    public class TicketController : Controller
    {
        private MyData db = new MyData();
        // GET: Ticket
        public ActionResult Index()
        {
            var allShips = db.ships.OrderBy(x => x.Name).ToList();
            var allLocations = db.locations.OrderBy(x => x.Name).ToList();
            List<SelectListItem> shipItems = new List<SelectListItem>();
            List<SelectListItem> departureItems = new List<SelectListItem>();
            List<SelectListItem> arrivalItems = new List<SelectListItem>();
            foreach (var ship in allShips)
            {
                var item = new SelectListItem
                {
                    Value = ship.Id.ToString(),
                    Text = ship.Name
                };
                shipItems.Add(item);
            }
            foreach (var location in allLocations)
            {
                var item = new SelectListItem
                {
                    Value = location.Id.ToString(),
                    Text = location.Name
                };
                departureItems.Add(item);
            }
            foreach (var location in allLocations)
            {
                var item = new SelectListItem
                {
                    Value = location.Id.ToString(),
                    Text = location.Name
                };
                arrivalItems.Add(item);
            }
            SelectList shipList = new SelectList(shipItems.OrderBy(i => i.Text), "Value", "Text");
            SelectList departureList = new SelectList(departureItems.OrderBy(i => i.Text), "Value", "Text");
            SelectList arrivalList = new SelectList(arrivalItems.OrderBy(i => i.Text), "Value", "Text");
            var model = new TicketIndexVM()
            {
                DepartureLocation = departureList,
                ArrivalLocation = arrivalList,
                Ship = shipList
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(TicketIndexVM model)
        {
            if (model.DepartureIds.Any())
            {
                var dId = int.Parse(model.DepartureIds.First());
                
                return RedirectToAction("dTrip", new { id = dId });
            }
            if (model.ArrivalIds.Any())
            {
                var aId = int.Parse(model.ArrivalIds.First());

                return RedirectToAction("aTrip", new { id = aId });
            }
            if (model.ShipIds.Any())
            {
                var sId = int.Parse(model.ShipIds.First());

                return RedirectToAction("sTrip", new { id = sId });
            }
            return RedirectToAction("Index");
        }

        public ActionResult dTrip(int id)
        {
            var location = db.locations.Find(id);           
            return View(db.Trips.Where(x => x.dName == location.Name).ToList());
        }
        public ActionResult aTrip(int id)
        {
            var location = db.locations.Find(id);
            return View(db.Trips.Where(x => x.aName == location.Name).ToList());
        }
        public ActionResult sTrip(int id)
        {
            var ship = db.ships.Find(id);
            return View(db.Trips.Where(x => x.sName == ship.Name).ToList());
        }

        //get
        public ActionResult Purchase(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trip trip = db.Trips.Find(id);
            if (trip == null)
            {
                return HttpNotFound();
            }
            var ship = db.ships.Where(x => x.Name == trip.sName).ToList().First();
            var model = new PurchaseVM(){
                tripId = trip.Id,
                shipName = ship.Name,
                Total_Classic_Cabin = ship.Total_Classic_Cabin,
                Total_Luxury_Cabin = ship.Total_Luxury_Cabin,
                Classic_Cabin_Price = ship.Classic_Cabin_Price,
                Luxury_Cabin_Price = ship.Luxury_Cabin_Price
            };

            return View(model);
        }

        //get
        public ActionResult Confirmation(string name, string type, int id)
        {
            var ship = db.ships.Where(x => x.Name == name).ToList().First();
            var trip = db.Trips.Where(x => x.Id == id).ToList().First();
            var model = new ConfirmationVM()
            {
                Email = User.Identity.Name,
                DepartureLocation = trip.dName,
                ArrivalLocation = trip.aName,
                StartDate = trip.StartDate,
                DayDuration = trip.Days,
                shipName = ship.Name
            };
            if (type == "Classic")
            {
                model.Type = "Classic";
                model.Price = ship.Classic_Cabin_Price;
            }
            if (type == "Luxury")
            {
                model.Type = "Luxury";
                model.Price = ship.Luxury_Cabin_Price;
            }

            return View(model);
        }

        //post
        [HttpPost]
        public ActionResult Confirmation(ConfirmationVM model)
        {
            var ticket = new Ticket()
            {
                FullName = model.FullName,
                Email = model.Email,
                Contact = model.Contact,
                Type = model.Type,
                Price = model.Price,
                DepartureLocation = model.DepartureLocation,
                ArrivalLocation = model.ArrivalLocation,
                StartDate = model.StartDate,
                DayDuration = model.DayDuration
            };
            if (model.Type == "Classic")
            {
                var ship = db.ships.Where(x => x.Name == model.shipName).ToList().First();
                ship.Total_Classic_Cabin--;
                db.SaveChanges();
            }
            if (model.Type == "Luxury")
            {
                var ship = db.ships.Where(x => x.Name == model.shipName).ToList().First();
                ship.Total_Luxury_Cabin--;
                db.SaveChanges();
            }
            db.Tickets.Add(ticket);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}