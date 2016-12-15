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
    public class cruiseController : Controller
    {
        private MyData db = new MyData();

        // GET: cruise
        public ActionResult Index()
        {
            return View(db.cruises.ToList());
        }

        // GET: cruise/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cruise cruise = db.cruises.Find(id);
            if (cruise == null)
            {
                return HttpNotFound();
            }
            return View(cruise);
        }

        // GET: cruise/Create
        public ActionResult Create()
        {
            var allShips = db.ships.OrderBy(x => x.Name).ToList();
            var checkBoxListItems = new List<CheckBoxListItem>();
            foreach (var ship in allShips)
            {
                checkBoxListItems.Add(new CheckBoxListItem()
                {
                    ID = ship.Id,
                    Display = ship.Name,
                    IsChecked = false
                });
            }
            var model = new CreateCruiseVM()
            {
                Ships = checkBoxListItems
            };
            return View(model);
        }

        // POST: cruise/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateCruiseVM model)
        {
            var selectedShips = model.Ships.Where(x => x.IsChecked).Select(x => x.ID).ToList();
            var cruise = new cruise()
            {
                Name = model.Name,
            };
            foreach (var shipID in selectedShips)
            {
                var ship = db.ships.Find(shipID);
                cruise.ships.Add(ship);
            }
            db.cruises.Add(cruise);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: cruise/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var cruise = db.cruises.Include(x => x.ships).Where(x => x.Id == id).First();
            if (cruise == null)
            {
                return HttpNotFound();
            }
            var model = new EditCruiseVM()
            {
                Id = cruise.Id,
                Name = cruise.Name
            };
            var cruiseShips = db.cruises.Where(x => x.Id == id).First().ships.ToList();
            var allShips = db.ships.OrderBy(x => x.Name).ToList();
            var checkBoxListItems = new List<CheckBoxListItem>();
            foreach (var ship in allShips)
            {
                checkBoxListItems.Add(new CheckBoxListItem()
                {
                    ID = ship.Id,
                    Display = ship.Name,
                    IsChecked = cruiseShips.Where(x => x.Id == ship.Id).Any()
                });
            }
            model.Ships = checkBoxListItems;
            return View(model);
        }

        // POST: cruise/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditCruiseVM model)
        {
            var selectedShips = model.Ships.Where(x => x.IsChecked).Select(x => x.ID).ToList();
            var cruise = db.cruises.Where(x => x.Id == model.Id).First();
            cruise.Name = model.Name;
            cruise.ships.Clear();
            foreach (var shipID in selectedShips)
            {
                var ship = db.ships.Find(shipID);
                cruise.ships.Add(ship);
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: cruise/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cruise cruise = db.cruises.Find(id);
            if (cruise == null)
            {
                return HttpNotFound();
            }
            return View(cruise);
        }

        // POST: cruise/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            cruise cruise = db.cruises.Find(id);
            cruise.ships.Clear();
            db.SaveChanges();
            db.cruises.Remove(cruise);
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
