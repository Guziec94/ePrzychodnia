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
    public class skierowanie_viewController : Controller
    {
        private ePrzychodniaEntities db = new ePrzychodniaEntities();

        // GET: skierowanie_view
        public ActionResult Index()
        {
            return View(db.skierowanie_view.ToList());
        }

        // GET: skierowanie_view/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            skierowanie_view skierowanie_view = db.skierowanie_view.Find(id);
            if (skierowanie_view == null)
            {
                return HttpNotFound();
            }
            return View(skierowanie_view);
        }

        // GET: skierowanie_view/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: skierowanie_view/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_uzytkownika,Numer_skierowania,C_Nazwisko_lekarza,Data_wystawienia")] skierowanie_view skierowanie_view)
        {
            if (ModelState.IsValid)
            {
                db.skierowanie_view.Add(skierowanie_view);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(skierowanie_view);
        }

        // GET: skierowanie_view/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            skierowanie_view skierowanie_view = db.skierowanie_view.Find(id);
            if (skierowanie_view == null)
            {
                return HttpNotFound();
            }
            return View(skierowanie_view);
        }

        // POST: skierowanie_view/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_uzytkownika,Numer_skierowania,C_Nazwisko_lekarza,Data_wystawienia")] skierowanie_view skierowanie_view)
        {
            if (ModelState.IsValid)
            {
                db.Entry(skierowanie_view).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(skierowanie_view);
        }

        // GET: skierowanie_view/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            skierowanie_view skierowanie_view = db.skierowanie_view.Find(id);
            if (skierowanie_view == null)
            {
                return HttpNotFound();
            }
            return View(skierowanie_view);
        }

        // POST: skierowanie_view/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            skierowanie_view skierowanie_view = db.skierowanie_view.Find(id);
            db.skierowanie_view.Remove(skierowanie_view);
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
