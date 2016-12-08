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
    public class badanieController : Controller
    {
        private ePrzychodniaEntities db = new ePrzychodniaEntities();

        // GET: badanie
        public ActionResult Index()
        {
            var badanie = db.badanie.Include(b => b.lekarz).Include(b => b.pacjent);
            return View(badanie.ToList());
        }

        // GET: badanie/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            badanie badanie = db.badanie.Find(id);
            if (badanie == null)
            {
                return HttpNotFound();
            }
            return View(badanie);
        }

        // GET: badanie/Create
        public ActionResult Create()
        {
            ViewBag.id_lekarz = new SelectList(db.lekarz, "id_lekarz", "nazwisko");
            ViewBag.id_pacjent = new SelectList(db.pacjent, "id_pacjent", "nazwisko");
            return View();
        }

        // POST: badanie/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_badanie,id_lekarz,id_pacjent,opis_badania,data_badania")] badanie badanie)
        {
            if (ModelState.IsValid)
            {
                db.badanie.Add(badanie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_lekarz = new SelectList(db.lekarz, "id_lekarz", "nazwisko", badanie.id_lekarz);
            ViewBag.id_pacjent = new SelectList(db.pacjent, "id_pacjent", "nazwisko", badanie.id_pacjent);
            return View(badanie);
        }

        // GET: badanie/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            badanie badanie = db.badanie.Find(id);
            if (badanie == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_lekarz = new SelectList(db.lekarz, "id_lekarz", "nazwisko", badanie.id_lekarz);
            ViewBag.id_pacjent = new SelectList(db.pacjent, "id_pacjent", "nazwisko", badanie.id_pacjent);
            return View(badanie);
        }

        // POST: badanie/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_badanie,id_lekarz,id_pacjent,opis_badania,data_badania")] badanie badanie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(badanie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_lekarz = new SelectList(db.lekarz, "id_lekarz", "nazwisko", badanie.id_lekarz);
            ViewBag.id_pacjent = new SelectList(db.pacjent, "id_pacjent", "nazwisko", badanie.id_pacjent);
            return View(badanie);
        }

        // GET: badanie/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            badanie badanie = db.badanie.Find(id);
            if (badanie == null)
            {
                return HttpNotFound();
            }
            return View(badanie);
        }

        // POST: badanie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            badanie badanie = db.badanie.Find(id);
            db.badanie.Remove(badanie);
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
