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
    public class godziny_przyjecController : Controller
    {
        private ePrzychodniaEntities db = new ePrzychodniaEntities();

        // GET: godziny_przyjec
        public ActionResult Index()
        {
            var godziny_przyjec = db.godziny_przyjec.Include(g => g.lekarz);
            return View(godziny_przyjec.ToList());
        }

        // GET: godziny_przyjec/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            godziny_przyjec godziny_przyjec = db.godziny_przyjec.Find(id);
            if (godziny_przyjec == null)
            {
                return HttpNotFound();
            }
            return View(godziny_przyjec);
        }

        // GET: godziny_przyjec/Create
        public ActionResult Create()
        {
            ViewBag.id_pracownik = new SelectList(db.lekarz, "id_lekarz", "nazwisko");
            return View();
        }

        // POST: godziny_przyjec/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_godziny_przyjec,id_pracownik,dzien_tygodnia,godz_od,godz_do")] godziny_przyjec godziny_przyjec)
        {
            if (ModelState.IsValid)
            {
                db.godziny_przyjec.Add(godziny_przyjec);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_pracownik = new SelectList(db.lekarz, "id_lekarz", "nazwisko", godziny_przyjec.id_pracownik);
            return View(godziny_przyjec);
        }

        // GET: godziny_przyjec/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            godziny_przyjec godziny_przyjec = db.godziny_przyjec.Find(id);
            if (godziny_przyjec == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_pracownik = new SelectList(db.lekarz, "id_lekarz", "nazwisko", godziny_przyjec.id_pracownik);
            return View(godziny_przyjec);
        }

        // POST: godziny_przyjec/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_godziny_przyjec,id_pracownik,dzien_tygodnia,godz_od,godz_do")] godziny_przyjec godziny_przyjec)
        {
            if (ModelState.IsValid)
            {
                db.Entry(godziny_przyjec).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_pracownik = new SelectList(db.lekarz, "id_lekarz", "nazwisko", godziny_przyjec.id_pracownik);
            return View(godziny_przyjec);
        }

        // GET: godziny_przyjec/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            godziny_przyjec godziny_przyjec = db.godziny_przyjec.Find(id);
            if (godziny_przyjec == null)
            {
                return HttpNotFound();
            }
            return View(godziny_przyjec);
        }

        // POST: godziny_przyjec/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            godziny_przyjec godziny_przyjec = db.godziny_przyjec.Find(id);
            db.godziny_przyjec.Remove(godziny_przyjec);
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
