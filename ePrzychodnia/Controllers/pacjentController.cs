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
    public class pacjentController : Controller
    {
        private ePrzychodniaEntities db = new ePrzychodniaEntities();

        // GET: pacjent
        public ActionResult Index()
        {
            return View(db.pacjent.ToList());
        }

        // GET: pacjent/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pacjent pacjent = db.pacjent.Find(id);
            if (pacjent == null)
            {
                return HttpNotFound();
            }
            return View(pacjent);
        }

        // GET: pacjent/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: pacjent/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_pacjent,nazwisko,imie,wiek,pesel,telefon")] pacjent pacjent)
        {
            if (ModelState.IsValid)
            {
                db.pacjent.Add(pacjent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pacjent);
        }

        // GET: pacjent/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pacjent pacjent = db.pacjent.Find(id);
            if (pacjent == null)
            {
                return HttpNotFound();
            }
            return View(pacjent);
        }

        // POST: pacjent/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_pacjent,nazwisko,imie,wiek,pesel,telefon")] pacjent pacjent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pacjent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pacjent);
        }

        // GET: pacjent/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pacjent pacjent = db.pacjent.Find(id);
            if (pacjent == null)
            {
                return HttpNotFound();
            }
            return View(pacjent);
        }

        // POST: pacjent/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            pacjent pacjent = db.pacjent.Find(id);
            db.pacjent.Remove(pacjent);
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
