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
    public class chorobaController : Controller
    {
        private ePrzychodniaEntities db = new ePrzychodniaEntities();

        // GET: choroba
        public ActionResult Index()
        {
            return View(db.choroba.ToList());
        }

        // GET: choroba/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            choroba choroba = db.choroba.Find(id);
            if (choroba == null)
            {
                return HttpNotFound();
            }
            return View(choroba);
        }

        // GET: choroba/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: choroba/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_choroba,nazwa_choroby,objawy,diagnoza")] choroba choroba)
        {
            if (ModelState.IsValid)
            {
                db.choroba.Add(choroba);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(choroba);
        }

        // GET: choroba/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            choroba choroba = db.choroba.Find(id);
            if (choroba == null)
            {
                return HttpNotFound();
            }
            return View(choroba);
        }

        // POST: choroba/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_choroba,nazwa_choroby,objawy,diagnoza")] choroba choroba)
        {
            if (ModelState.IsValid)
            {
                db.Entry(choroba).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(choroba);
        }

        // GET: choroba/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            choroba choroba = db.choroba.Find(id);
            if (choroba == null)
            {
                return HttpNotFound();
            }
            return View(choroba);
        }

        // POST: choroba/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            choroba choroba = db.choroba.Find(id);
            db.choroba.Remove(choroba);
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
