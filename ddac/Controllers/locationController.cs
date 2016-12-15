using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ddac.Models;

namespace ddac.Controllers
{
    [Authorize(Users = "admin@admin.com")]
    public class locationController : Controller
    {
        private MyData db = new MyData();

        // GET: location
        public ActionResult Index()
        {
            return View(db.locations.ToList());
        }

        // GET: location/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            location location = db.locations.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        // GET: location/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: location/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] location location)
        {
            if (ModelState.IsValid)
            {
                db.locations.Add(location);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(location);
        }

        // GET: location/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            location location = db.locations.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        // POST: location/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] location location)
        {
            if (ModelState.IsValid)
            {
                db.Entry(location).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(location);
        }

        // GET: location/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            location location = db.locations.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        // POST: location/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            location location = db.locations.Find(id);
            if (db.Trips.Where(x => x.dName == location.Name).Any())
            {
                var trips = db.Trips.Where(x => x.dName == location.Name).ToList();
                foreach (var trip in trips)
                {
                    db.Trips.Remove(trip);
                    db.SaveChanges();
                }
            }
            if (db.Trips.Where(x => x.aName == location.Name).Any())
            {
                var trips = db.Trips.Where(x => x.aName == location.Name).ToList();
                foreach (var trip in trips)
                {
                    db.Trips.Remove(trip);
                    db.SaveChanges();
                }
            }
            db.locations.Remove(location);
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
