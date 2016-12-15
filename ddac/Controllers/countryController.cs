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
    public class countryController : Controller
    {
        private MyData db = new MyData();

        // GET: country
        public ActionResult Index()
        {
            return View(db.countrys.ToList());
        }

        // GET: country/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            country country = db.countrys.Find(id);
            if (country == null)
            {
                return HttpNotFound();
            }
            return View(country);
        }

        // GET: country/Create
        public ActionResult Create()
        {
            var allLocations = db.locations.OrderBy(x => x.Name).ToList();
            var checkBoxListItems = new List<CheckBoxListItem>();
            foreach (var location in allLocations)
            {
                checkBoxListItems.Add(new CheckBoxListItem()
                {
                    ID = location.Id,
                    Display = location.Name,
                    IsChecked = false
                });
            }
            var model = new CreateCountryVM()
            {
                Locations = checkBoxListItems
            };
            return View(model);
        }

        // POST: country/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateCountryVM model)
        {
            var selectedLocations = model.Locations.Where(x => x.IsChecked).Select(x => x.ID).ToList();
            var country = new country()
            {
                Name = model.Name,
            };
            foreach (var locationID in selectedLocations)
            {
                var location = db.locations.Find(locationID);
                country.locations.Add(location);
            }
            db.countrys.Add(country);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: country/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var country = db.countrys.Include(x => x.locations).Where(x => x.Id == id).First();
            if (country == null)
            {
                return HttpNotFound();
            }
            var model = new EditCountryVM()
            {
                Id = country.Id,
                Name = country.Name
            };
            var countryLocations = db.countrys.Where(x => x.Id == id).First().locations.ToList();
            var allLocations = db.locations.OrderBy(x => x.Name).ToList();
            var checkBoxListItems = new List<CheckBoxListItem>();
            foreach (var location in allLocations)
            {
                checkBoxListItems.Add(new CheckBoxListItem()
                {
                    ID = location.Id,
                    Display = location.Name,
                    IsChecked = countryLocations.Where(x => x.Id == location.Id).Any()
                });
            }
            model.Locations = checkBoxListItems;
            return View(model);
        }

        // POST: country/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditCountryVM model)
        {
            var selectedLocations = model.Locations.Where(x => x.IsChecked).Select(x => x.ID).ToList();
            var country = db.countrys.Where(x => x.Id == model.Id).First();
            country.Name = model.Name;
            country.locations.Clear();
            foreach (var locationID in selectedLocations)
            {
                var location = db.locations.Find(locationID);
                country.locations.Add(location);
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: country/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            country country = db.countrys.Find(id);
            if (country == null)
            {
                return HttpNotFound();
            }
            return View(country);
        }

        // POST: country/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            country country = db.countrys.Find(id);
            country.locations.Clear();
            db.SaveChanges();
            db.countrys.Remove(country);
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
