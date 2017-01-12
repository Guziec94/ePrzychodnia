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
    public class KompleksowaWizytaController : Controller
    {
        private ePrzychodniaEntities db = new ePrzychodniaEntities();

        // GET: KompleksowaWizyta
        public ActionResult Index()
        {
            return View(db.KompleksowaWizytas.ToList());
        }

        // GET: KompleksowaWizyta/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KompleksowaWizyta kompleksowaWizyta = db.KompleksowaWizytas.Find(id);
            if (kompleksowaWizyta == null)
            {
                return HttpNotFound();
            }
            return View(kompleksowaWizyta);
        }

        // GET: KompleksowaWizyta/Create
        public ActionResult Create()
        {
            KompleksowaWizyta mod = new KompleksowaWizyta();
            mod.wypelnione_badanie = false;
            mod.wypelnione_choroba = false;
            mod.wypelnione_recepta = false;
            return View(mod);
        }

        // POST: KompleksowaWizyta/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_wizyta,id_skierowanie,id_recepta,id_choroba,id_badanie,id_lekarz,id_pacjent,data_wizyty,data_wystawienia_skierowanie,data_badania,data_wystawienia_recepta,opis_badania,lek_i_dawkowanie,nazwa_choroby,objawy,diagnoza,wypelnione_badanie,wypelnione_recepta,wypelnione_choroba")] KompleksowaWizyta KW)
        {
            if (ModelState.IsValid)
            {
                wizyta wizyta_dz = new wizyta();//dz = do zapisu
                wizyta_dz.id_pacjent = KW.id_pacjent;
                wizyta_dz.data_wizyty = KW.data_wizyty;

                string cos = User.Identity.GetUserId();
                lekarz zalogowany_lekarz = db.lekarz.FirstOrDefault(i => i.id_uzytkownika == cos);
                wizyta_dz.id_lekarz = KW.id_lekarz = zalogowany_lekarz.id_lekarz;

                if (KW.wypelnione_badanie==true)
                {
                    badanie badanie_dz = new badanie();
                    badanie_dz.id_lekarz = KW.id_lekarz;
                    badanie_dz.id_pacjent = KW.id_pacjent;
                    badanie_dz.data_badania = KW.data_badania;
                    badanie_dz.opis_badania = KW.opis_badania;
                    db.badanie.Add(badanie_dz);
                    db.SaveChanges();
                    int id_bad = db.badanie.OrderByDescending(x => x.id_badanie).Take(1).Single().id_badanie;
                    KW.id_badanie = wizyta_dz.id_badanie = id_bad;

                    skierowanie skierowanie_dz = new skierowanie();
                    skierowanie_dz.data_wystawienia = KW.data_wystawienia_skierowanie;
                    skierowanie_dz.id_badanie = KW.id_badanie;
                    skierowanie_dz.id_lekarz = KW.id_lekarz;
                    skierowanie_dz.id_pacjent = KW.id_pacjent;
                    db.skierowanie.Add(skierowanie_dz);
                    db.SaveChanges();
                    KW.id_skierowanie = wizyta_dz.id_skierowanie = db.skierowanie.OrderByDescending(x => x.id_skierowanie).Take(1).Single().id_skierowanie;
                }
                if (KW.wypelnione_choroba == true)
                {
                    choroba choroba_dz = new choroba();
                    choroba_dz.diagnoza = KW.diagnoza;
                    choroba_dz.nazwa_choroby = KW.nazwa_choroby;
                    choroba_dz.objawy = KW.objawy;
                    db.choroba.Add(choroba_dz);
                    db.SaveChanges();
                    KW.id_choroba = wizyta_dz.id_choroba = db.choroba.OrderByDescending(x => x.id_choroba).Take(1).Single().id_choroba;
                }
                if (KW.wypelnione_recepta == true)
                {
                    recepta recepta_dz = new recepta();
                    recepta_dz.id_lekarz = KW.id_lekarz;
                    recepta_dz.id_pacjent = KW.id_pacjent;
                    recepta_dz.lek_i_dawkowanie = KW.lek_i_dawkowanie;
                    recepta_dz.data_wystawienia = KW.data_wystawienia_recepta;
                    db.recepta.Add(recepta_dz);
                    db.SaveChanges();
                    KW.id_recepta = wizyta_dz.id_recepta = db.recepta.OrderByDescending(x => x.id_recepta).Take(1).Single().id_recepta;
                    
                }
                db.wizyta.Add(wizyta_dz);
                db.SaveChanges();
                return RedirectToAction("Index", "Wizyta");
            }

            return View(KW);
        }

        // GET: KompleksowaWizyta/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KompleksowaWizyta kompleksowaWizyta = db.KompleksowaWizytas.Find(id);
            if (kompleksowaWizyta == null)
            {
                return HttpNotFound();
            }
            return View(kompleksowaWizyta);
        }

        // POST: KompleksowaWizyta/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_wizyta,id_skierowanie,id_recepta,id_choroba,id_badanie,id_lekarz,id_pacjent,data_wizyty,data_wystawienia_skierowanie,data_badania,data_wystawienia_recepta,opis_badania,lek_i_dawkowanie,nazwa_choroby,objawy,diagnoza")] KompleksowaWizyta kompleksowaWizyta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kompleksowaWizyta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kompleksowaWizyta);
        }

        // GET: KompleksowaWizyta/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KompleksowaWizyta kompleksowaWizyta = db.KompleksowaWizytas.Find(id);
            if (kompleksowaWizyta == null)
            {
                return HttpNotFound();
            }
            return View(kompleksowaWizyta);
        }

        // POST: KompleksowaWizyta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KompleksowaWizyta kompleksowaWizyta = db.KompleksowaWizytas.Find(id);
            db.KompleksowaWizytas.Remove(kompleksowaWizyta);
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
