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
    public class zapisController : Controller
    {
        private ePrzychodniaEntities db = new ePrzychodniaEntities();

        // GET: zapis
        public ActionResult Index()
        {
            var zapis = db.zapis.Include(z => z.lekarz).Include(z => z.pacjent);
            return View(zapis.ToList());
        }

        // GET: zapis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            zapis zapis = db.zapis.Find(id);
            if (zapis == null)
            {
                return HttpNotFound();
            }
            return View(zapis);
        }

        // GET: zapis/Create
        public ActionResult Create()
        {
            ViewBag.id_lekarza = new SelectList(db.lekarz, "id_lekarz", "nazwisko");
            ViewBag.id_pacjenta = new SelectList(db.pacjent, "id_pacjent", "nazwisko");
            return View();
        }

        // POST: zapis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_zapisu,id_pacjenta,id_lekarza,data,godzina")] zapis zapis)
        {
            string email_zalogowanego_pacjenta = User.Identity.Name;//email zalogowanego pacjenta, ktory zapisuje sie do kolejki
            pacjent zalogowany_pacjent = db.pacjent.FirstOrDefault(i => i.AspNetUsers.Email == email_zalogowanego_pacjenta);
            if (zalogowany_pacjent != null)
            {
                zapis.id_pacjenta = zalogowany_pacjent.id_pacjent;
            }
            int ilosc_w_kolejce = 0;
            var temp = db.zapis.Include(z => z.lekarz).Include(z => z.pacjent).ToList();
            zapis.godzina = new TimeSpan(0, 480+ ilosc_w_kolejce, 0);
            if (ModelState.IsValid)
            {
                /*db.zapis.Add(zapis);
                db.SaveChanges();*/
                return RedirectToAction("Index");
            }

            ViewBag.id_lekarza = new SelectList(db.lekarz, "id_lekarz", "nazwisko", zapis.id_lekarza);
            ViewBag.id_pacjenta = new SelectList(db.pacjent, "id_pacjent", "nazwisko", zapis.id_pacjenta);
            return View(zapis);
        }

        // GET: zapis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            zapis zapis = db.zapis.Find(id);
            if (zapis == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_lekarza = new SelectList(db.lekarz, "id_lekarz", "nazwisko", zapis.id_lekarza);
            ViewBag.id_pacjenta = new SelectList(db.pacjent, "id_pacjent", "nazwisko", zapis.id_pacjenta);
            return View(zapis);
        }

        // POST: zapis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_zapisu,id_pacjenta,id_lekarza,data,godzina")] zapis zapis)
        {
            if (ModelState.IsValid)
            {
                db.Entry(zapis).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_lekarza = new SelectList(db.lekarz, "id_lekarz", "nazwisko", zapis.id_lekarza);
            ViewBag.id_pacjenta = new SelectList(db.pacjent, "id_pacjent", "nazwisko", zapis.id_pacjenta);
            return View(zapis);
        }

        // GET: zapis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            zapis zapis = db.zapis.Find(id);
            if (zapis == null)
            {
                return HttpNotFound();
            }
            return View(zapis);
        }

        // POST: zapis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            zapis zapis = db.zapis.Find(id);
            db.zapis.Remove(zapis);
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
