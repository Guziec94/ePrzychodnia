using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ePrzychodnia.Models;

namespace ePrzychodnia.Controllers
{
    public class skierowanieController : Controller
    {
        private ePrzychodniaEntities db = new ePrzychodniaEntities();

        // GET: skierowanie
        public ActionResult Index()
        {
            return View(db.skierowanie.ToList());
        }

        // GET: skierowanie/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            skierowanie skierowanie = db.skierowanie.Find(id);
            if (skierowanie == null)
            {
                return HttpNotFound();
            }
            return View(skierowanie);
        }

        // GET: skierowanie/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: skierowanie/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_skierowanie,id_pacjent,id_badanie,id_lekarz,data_wystawienia")] skierowanie skierowanie)
        {
            if (ModelState.IsValid)
            {
                db.skierowanie.Add(skierowanie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(skierowanie);
        }

        // GET: skierowanie/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            skierowanie skierowanie = db.skierowanie.Find(id);
            if (skierowanie == null)
            {
                return HttpNotFound();
            }
            return View(skierowanie);
        }

        // POST: skierowanie/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_skierowanie,id_pacjent,id_badanie,id_lekarz,data_wystawienia")] skierowanie skierowanie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(skierowanie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(skierowanie);
        }

        // GET: skierowanie/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            skierowanie skierowanie = db.skierowanie.Find(id);
            if (skierowanie == null)
            {
                return HttpNotFound();
            }
            return View(skierowanie);
        }

        // POST: skierowanie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            skierowanie skierowanie = db.skierowanie.Find(id);
            db.skierowanie.Remove(skierowanie);
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
