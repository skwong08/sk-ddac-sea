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
    public class shipController : Controller
    {
        private MyData db = new MyData();

        // GET: ship
        public ActionResult Index()
        {
            return View(db.ships.ToList());
        }

        // GET: ship/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ship ship = db.ships.Find(id);
            if (ship == null)
            {
                return HttpNotFound();
            }
            return View(ship);
        }

        // GET: ship/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ship/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Total_Classic_Cabin,Total_Luxury_Cabin,Classic_Cabin_Price,Luxury_Cabin_Price")] ship ship)
        {
            if (ModelState.IsValid)
            {
                db.ships.Add(ship);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ship);
        }

        // GET: ship/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ship ship = db.ships.Find(id);
            if (ship == null)
            {
                return HttpNotFound();
            }
            return View(ship);
        }

        // POST: ship/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Total_Classic_Cabin,Total_Luxury_Cabin,Classic_Cabin_Price,Luxury_Cabin_Price")] ship ship)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ship).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ship);
        }

        // GET: ship/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ship ship = db.ships.Find(id);
            if (ship == null)
            {
                return HttpNotFound();
            }
            return View(ship);
        }

        // POST: ship/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ship ship = db.ships.Find(id);
            if (db.Trips.Where(x => x.sName == ship.Name).Any())
            {
                var trips = db.Trips.Where(x => x.sName == ship.Name).ToList();
                foreach (var trip in trips)
                {
                    db.Trips.Remove(trip);
                    db.SaveChanges();
                }
            }
            db.ships.Remove(ship);
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
