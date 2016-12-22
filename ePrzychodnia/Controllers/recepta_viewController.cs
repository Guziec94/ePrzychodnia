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
    public class recepta_viewController : Controller
    {
        private ePrzychodniaEntities db = new ePrzychodniaEntities();

        // GET: recepta_view
        public ActionResult Index()
        {
            ViewBag.data = User.Identity.GetUserId();
            return View(db.recepta_view.ToList());
        }

        // GET: recepta_view/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            recepta_view recepta_view = db.recepta_view.Find(id);
            if (recepta_view == null)
            {
                return HttpNotFound();
            }
            return View(recepta_view);
        }

        // GET: recepta_view/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: recepta_view/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_uzytkownika,Numer_recepty,Data_wystawienia,Nazwa_leku_i_dawkowanie")] recepta_view recepta_view)
        {
            if (ModelState.IsValid)
            {
                db.recepta_view.Add(recepta_view);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(recepta_view);
        }

        // GET: recepta_view/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            recepta_view recepta_view = db.recepta_view.Find(id);
            if (recepta_view == null)
            {
                return HttpNotFound();
            }
            return View(recepta_view);
        }

        // POST: recepta_view/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_uzytkownika,Numer_recepty,Data_wystawienia,Nazwa_leku_i_dawkowanie")] recepta_view recepta_view)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recepta_view).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(recepta_view);
        }

        // GET: recepta_view/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            recepta_view recepta_view = db.recepta_view.Find(id);
            if (recepta_view == null)
            {
                return HttpNotFound();
            }
            return View(recepta_view);
        }

        // POST: recepta_view/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            recepta_view recepta_view = db.recepta_view.Find(id);
            db.recepta_view.Remove(recepta_view);
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
