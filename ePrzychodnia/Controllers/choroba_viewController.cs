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
    public class choroba_viewController : Controller
    {
        private ePrzychodniaEntities db = new ePrzychodniaEntities();

        // GET: choroba_view
        public ActionResult Index()
        {
            ViewBag.data= User.Identity.GetUserId();
            return View(db.choroba_view.ToList());
        }

        // GET: choroba_view/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            choroba_view choroba_view = db.choroba_view.Find(id);
            if (choroba_view == null)
            {
                return HttpNotFound();
            }
            return View(choroba_view);
        }

        // GET: choroba_view/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: choroba_view/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_uzytkownika,nazwisko,nazwa_choroby,diagnoza,objawy,id_pacjent")] choroba_view choroba_view)
        {
            if (ModelState.IsValid)
            {
                db.choroba_view.Add(choroba_view);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(choroba_view);
        }

        // GET: choroba_view/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            choroba_view choroba_view = db.choroba_view.Find(id);
            if (choroba_view == null)
            {
                return HttpNotFound();
            }
            return View(choroba_view);
        }

        // POST: choroba_view/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_uzytkownika,nazwisko,nazwa_choroby,diagnoza,objawy,id_pacjent")] choroba_view choroba_view)
        {
            if (ModelState.IsValid)
            {
                db.Entry(choroba_view).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(choroba_view);
        }

        // GET: choroba_view/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            choroba_view choroba_view = db.choroba_view.Find(id);
            if (choroba_view == null)
            {
                return HttpNotFound();
            }
            return View(choroba_view);
        }

        // POST: choroba_view/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            choroba_view choroba_view = db.choroba_view.Find(id);
            db.choroba_view.Remove(choroba_view);
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
