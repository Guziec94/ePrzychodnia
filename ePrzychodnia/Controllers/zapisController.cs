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
    [Authorize(Roles = "Pacjent")]
    public class zapisController : Controller
    {
        private ePrzychodniaEntities db = new ePrzychodniaEntities();

        // GET: zapis
        public ActionResult Index()
        {
            string userID = User.Identity.GetUserId();
            pacjent zalogowany_pacjent = db.pacjent.FirstOrDefault(i => i.id_uzytkownika == userID);
            var id_pacjent = zalogowany_pacjent.id_pacjent;
            ViewBag.Data = id_pacjent;
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
            var lista_lekarzy = db.lekarz.Select(s => new
            {
                id_lekarza = s.id_lekarz,
                nazwisko = "dr "+s.imie + " " + s.nazwisko
            }).ToList();
            ViewBag.id_lekarza = new SelectList(lista_lekarzy, "id_lekarza", "nazwisko");
            var dzisiaj = DateTime.Now;
            dzisiaj = dzisiaj.AddDays(1);
            ViewBag.data = dzisiaj.ToString("yyyy-MM-dd");
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
            int ilosc_w_kolejce;
            do
            {
                List<zapis> temp = db.zapis.ToList();
                ilosc_w_kolejce = temp.FindAll(z => z.data == zapis.data && z.id_lekarza == zapis.id_lekarza).Count;
                if (ilosc_w_kolejce == 24)
                {
                    zapis.data = ((DateTime)zapis.data).AddDays(1);
                    ilosc_w_kolejce = -1;
                }
            } while (ilosc_w_kolejce == -1);
            zapis.godzina = new TimeSpan(0, 480 + 20 * (ilosc_w_kolejce % 24), 0);
            if (ModelState.IsValid)
            {
                db.zapis.Add(zapis);
                db.SaveChanges();
                return Content("<script language='javascript' type='text/javascript'>alert('Zapisałeś/aś się do lekarza na najbliższy termin tj.: "+((DateTime)zapis.data).ToString("yyyy-MM-dd") + " "+zapis.godzina+"');window.location.href =\"http://localhost:1768/zapis/index\";</script>");
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
