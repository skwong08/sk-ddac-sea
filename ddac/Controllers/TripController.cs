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
    [Authorize(Users = "admin@admin.com")]
    public class TripController : Controller
    {
        private MyData db = new MyData();

        // GET: Trip
        public ActionResult Index()
        {
            return View(db.Trips.ToList());
        }

        // GET: Trip/Details/5
        public ActionResult Details(int? id)
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
            return View(trip);
        }

        // GET: Trip/Create
        public ActionResult Create()
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
            var model = new CreateTripVM()
            {
                DepartureLocation = departureList,
                ArrivalLocation = arrivalList,
                Ship = shipList
            };
            return View(model);
        }

        // POST: Trip/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateTripVM model)
        {
            var trip = new Trip()
            {
                StartDate = model.StartDate,
                Days = model.Days
            };
                        
            var Id = int.Parse(model.DepartureIds.First());
            var location = db.locations.Find(Id);
            trip.DepartureLocation = location;
            trip.dName = location.Name;
                       
            var Id2 = int.Parse(model.ArrivalIds.First());
            var location2 = db.locations.Find(Id2);
            trip.ArrivalLocation = location2;
            trip.aName = location2.Name;
                      
            var Id3 = int.Parse(model.ShipIds.First());
            var ship = db.ships.Find(Id3);
            trip.Ships = ship;
            trip.sName = ship.Name;
            
            db.Trips.Add(trip);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Trip/Edit/5
        public ActionResult Edit(int? id)
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
            return View(trip);
        }

        // POST: Trip/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,StartDate,Days")] Trip trip)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trip).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(trip);
        }

        // GET: Trip/Delete/5
        public ActionResult Delete(int? id)
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
            return View(trip);
        }

        // POST: Trip/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Trip trip = db.Trips.Find(id);          
            db.Trips.Remove(trip);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
