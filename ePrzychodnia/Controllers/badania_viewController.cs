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
    public class badania_viewController : Controller
    {
        private ePrzychodniaEntities db = new ePrzychodniaEntities();

        // GET: badania_view
        public ActionResult Index()
        {
            return View(db.badania_view.ToList());
        }

        // GET: badania_view/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            badania_view badania_view = db.badania_view.Find(id);
            if (badania_view == null)
            {
                return HttpNotFound();
            }
            return View(badania_view);
        }

        // GET: badania_view/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: badania_view/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_uzytkownika,Nazwisko_lekarza,id_badanie,Data_badania,C_Opis_badania")] badania_view badania_view)
        {
            if (ModelState.IsValid)
            {
                db.badania_view.Add(badania_view);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(badania_view);
        }

        // GET: badania_view/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            badania_view badania_view = db.badania_view.Find(id);
            if (badania_view == null)
            {
                return HttpNotFound();
            }
            return View(badania_view);
        }

        // POST: badania_view/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_uzytkownika,Nazwisko_lekarza,id_badanie,Data_badania,C_Opis_badania")] badania_view badania_view)
        {
            if (ModelState.IsValid)
            {
                db.Entry(badania_view).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(badania_view);
        }

        // GET: badania_view/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            badania_view badania_view = db.badania_view.Find(id);
            if (badania_view == null)
            {
                return HttpNotFound();
            }
            return View(badania_view);
        }

        // POST: badania_view/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            badania_view badania_view = db.badania_view.Find(id);
            db.badania_view.Remove(badania_view);
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
