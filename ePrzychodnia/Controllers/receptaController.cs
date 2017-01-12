using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ePrzychodnia.Models;
using Microsoft.AspNet.Identity;

namespace ePrzychodnia.Controllers
{
    public class receptaController : Controller
    {
        private ePrzychodniaEntities db = new ePrzychodniaEntities();

        // GET: recepta
        public ActionResult Index()
        {
            string userID = User.Identity.GetUserId();
            lekarz zalogowany_lekarz = db.lekarz.FirstOrDefault(i => i.id_uzytkownika == userID);
            var id_lekarz = zalogowany_lekarz.id_lekarz;
            ViewBag.Data = id_lekarz;
            var recepta = db.recepta.Include(r => r.lekarz).Include(r => r.pacjent);
            return View(recepta.ToList());
        }

        // GET: recepta/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            recepta recepta = db.recepta.Find(id);
            if (recepta == null)
            {
                return HttpNotFound();
            }
            return View(recepta);
        }

        // GET: recepta/Create
        public ActionResult Create()
        {
            ViewBag.id_lekarz = new SelectList(db.lekarz, "id_lekarz", "nazwisko");
            ViewBag.id_pacjent = new SelectList(db.pacjent, "id_pacjent", "nazwisko");
            return View();
        }

        // POST: recepta/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_recepta,id_pacjent,id_lekarz,lek_i_dawkowanie,data_wystawienia")] recepta recepta)
        {
            if (ModelState.IsValid)
            {
                recepta.data_wystawienia = DateTime.Now;
                db.recepta.Add(recepta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_lekarz = new SelectList(db.lekarz, "id_lekarz", "nazwisko", recepta.id_lekarz);
            ViewBag.id_pacjent = new SelectList(db.pacjent, "id_pacjent", "nazwisko", recepta.id_pacjent);
            return View(recepta);
        }

        // GET: recepta/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            recepta recepta = db.recepta.Find(id);
            if (recepta == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_lekarz = new SelectList(db.lekarz, "id_lekarz", "nazwisko", recepta.id_lekarz);
            ViewBag.id_pacjent = new SelectList(db.pacjent, "id_pacjent", "nazwisko", recepta.id_pacjent);
            return View(recepta);
        }

        // POST: recepta/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_recepta,id_pacjent,id_lekarz,lek_i_dawkowanie,data_wystawienia")] recepta recepta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recepta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_lekarz = new SelectList(db.lekarz, "id_lekarz", "nazwisko", recepta.id_lekarz);
            ViewBag.id_pacjent = new SelectList(db.pacjent, "id_pacjent", "nazwisko", recepta.id_pacjent);
            return View(recepta);
        }

        // GET: recepta/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            recepta recepta = db.recepta.Find(id);
            if (recepta == null)
            {
                return HttpNotFound();
            }
            return View(recepta);
        }

        // POST: recepta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            recepta recepta = db.recepta.Find(id);
            db.recepta.Remove(recepta);
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
