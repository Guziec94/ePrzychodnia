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
    public class wizytaController : Controller
    {
        private ePrzychodniaEntities db = new ePrzychodniaEntities();

        // GET: wizyta
        public ActionResult Index()
        {
            string userID = User.Identity.GetUserId(); //dla zalogowanego, ktory uzupelnia swoje dane
            lekarz zalogowany_lekarz = db.lekarz.FirstOrDefault(i => i.id_uzytkownika == userID);
            var id_lekarz=zalogowany_lekarz.id_lekarz;
            ViewBag.Data = id_lekarz;
            var wizyta = db.wizyta.Include(w => w.badanie).Include(w => w.choroba).Include(w => w.recepta).Include(w => w.skierowanie);
            List<zapis> lista_zapisow = db.zapis.Where(z => z.data == DateTime.Today&& z.id_lekarza == zalogowany_lekarz.id_lekarz).ToList();
            ViewBag.lista_zapisow = lista_zapisow;
            return View(wizyta.ToList());
        }

        // GET: wizyta/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            wizyta wizyta = db.wizyta.Find(id);
            if (wizyta == null)
            {
                return HttpNotFound();
            }
            return View(wizyta);
        }

        // GET: wizyta/Create
        public ActionResult Create()
        {
            ViewBag.id_badanie = new SelectList(db.badanie, "id_badanie", "opis_badania");
            ViewBag.id_choroba = new SelectList(db.choroba, "id_choroba", "nazwa_choroby");
            ViewBag.id_recepta = new SelectList(db.recepta, "id_recepta", "lek_i_dawkowanie");
            ViewBag.id_skierowanie = new SelectList(db.skierowanie, "id_skierowanie", "id_skierowanie");
            return View();
        }

        // POST: wizyta/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_wizyta,id_skierowanie,id_recepta,id_choroba,id_badanie,id_lekarz,id_pacjent,data_wizyty")] wizyta wizyta)
        {
            if (ModelState.IsValid)
            {
                db.wizyta.Add(wizyta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_badanie = new SelectList(db.badanie, "id_badanie", "opis_badania", wizyta.id_badanie);
            ViewBag.id_choroba = new SelectList(db.choroba, "id_choroba", "nazwa_choroby", wizyta.id_choroba);
            ViewBag.id_recepta = new SelectList(db.recepta, "id_recepta", "lek_i_dawkowanie", wizyta.id_recepta);
            ViewBag.id_skierowanie = new SelectList(db.skierowanie, "id_skierowanie", "id_skierowanie", wizyta.id_skierowanie);
            return View(wizyta);
        }

        // GET: wizyta/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            wizyta wizyta = db.wizyta.Find(id);
            if (wizyta == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_badanie = new SelectList(db.badanie, "id_badanie", "opis_badania", wizyta.id_badanie);
            ViewBag.id_choroba = new SelectList(db.choroba, "id_choroba", "nazwa_choroby", wizyta.id_choroba);
            ViewBag.id_recepta = new SelectList(db.recepta, "id_recepta", "lek_i_dawkowanie", wizyta.id_recepta);
            ViewBag.id_skierowanie = new SelectList(db.skierowanie, "id_skierowanie", "id_skierowanie", wizyta.id_skierowanie);
            return View(wizyta);
        }

        // POST: wizyta/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_wizyta,id_skierowanie,id_recepta,id_choroba,id_badanie,id_lekarz,id_pacjent,data_wizyty")] wizyta wizyta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wizyta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_badanie = new SelectList(db.badanie, "id_badanie", "opis_badania", wizyta.id_badanie);
            ViewBag.id_choroba = new SelectList(db.choroba, "id_choroba", "nazwa_choroby", wizyta.id_choroba);
            ViewBag.id_recepta = new SelectList(db.recepta, "id_recepta", "lek_i_dawkowanie", wizyta.id_recepta);
            ViewBag.id_skierowanie = new SelectList(db.skierowanie, "id_skierowanie", "id_skierowanie", wizyta.id_skierowanie);
            return View(wizyta);
        }

        // GET: wizyta/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            wizyta wizyta = db.wizyta.Find(id);
            if (wizyta == null)
            {
                return HttpNotFound();
            }
            return View(wizyta);
        }

        // POST: wizyta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            wizyta wizyta = db.wizyta.Find(id);
            db.wizyta.Remove(wizyta);
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
