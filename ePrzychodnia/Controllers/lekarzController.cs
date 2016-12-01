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
    public class lekarzController : Controller
    {
        private ePrzychodniaEntities db = new ePrzychodniaEntities();

        // GET: lekarz
        public ActionResult Index()
        {
            var lekarz = db.lekarz.Include(l => l.adres);
            return View(lekarz.ToList());
        }

        // GET: lekarz/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            lekarz lekarz = db.lekarz.Find(id);
            if (lekarz == null)
            {
                return HttpNotFound();
            }
            return View(lekarz);
        }

        // GET: lekarz/Create
        public ActionResult Create()
        {
            ViewBag.id_adres = new SelectList(db.adres, "id_adres", "kraj");
            return View();
        }

        // POST: lekarz/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_lekarz,id_adres,nazwisko,imie,pesel,telefon")] lekarz lekarz)
        {
            if (ModelState.IsValid)
            {
                db.lekarz.Add(lekarz);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_adres = new SelectList(db.adres, "id_adres", "kraj", lekarz.id_adres);
            return View(lekarz);
        }

        // GET: lekarz/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            lekarz lekarz = db.lekarz.Find(id);
            if (lekarz == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_adres = new SelectList(db.adres, "id_adres", "kraj", lekarz.id_adres);
            return View(lekarz);
        }

        // POST: lekarz/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_lekarz,id_adres,nazwisko,imie,pesel,telefon")] lekarz lekarz)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lekarz).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_adres = new SelectList(db.adres, "id_adres", "kraj", lekarz.id_adres);
            return View(lekarz);
        }

        // GET: lekarz/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            lekarz lekarz = db.lekarz.Find(id);
            if (lekarz == null)
            {
                return HttpNotFound();
            }
            return View(lekarz);
        }

        // POST: lekarz/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            lekarz lekarz = db.lekarz.Find(id);
            db.lekarz.Remove(lekarz);
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
