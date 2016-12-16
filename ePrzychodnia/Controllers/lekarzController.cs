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
using System.Reflection;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ePrzychodnia.Controllers
{
    [Authorize(Roles = "Admin")]
    public class lekarzController : Controller
    {
        private ePrzychodniaEntities db = new ePrzychodniaEntities();

        // GET: lekarz
        public ActionResult Index()
        {
            var lekarz = db.lekarz;
            return View(lekarz.ToList());
        }

        // GET: lekarz/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            lekarz lekarz = db.lekarz.Find(id);
            if (lekarz == null)
            {
                return HttpNotFound();
            }
            return View(lekarz);
        }

        // GET: lekarz/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: lekarz/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_lekarz,nazwisko,imie,pesel,telefon,Email,Password,ConfirmPassword")] lekarz lekarz)
        {
            if (lekarz.Email == null || lekarz.Password == null || lekarz.Password != lekarz.ConfirmPassword)//tego nie obejmuje sprawdzenie modelu, dlatego jest sprawdzane oddzielnie
            {
                return View(lekarz);
            }
            if (ModelState.IsValid)
            {
                ApplicationDbContext context = new ApplicationDbContext();
                var UserManagerr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

                var user = new ApplicationUser { UserName = lekarz.Email, Email = lekarz.Email };
                var result = UserManagerr.Create(user, lekarz.Password);
                if (result.Succeeded)
                {
                    UserManagerr.AddToRoleAsync(user.Id, lekarz.UserRoles);
                    lekarz.id_uzytkownika = user.Id;
                    db.lekarz.Add(lekarz);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(lekarz);//jesli cos nie halo to wracamy do formularza
        }

        // GET: lekarz/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            lekarz lekarz = db.lekarz.Find(id);
            if (lekarz == null)
            {
                return HttpNotFound();
            }
            return View(lekarz);
        }

        // POST: lekarz/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_lekarz,nazwisko,imie,pesel,telefon")] lekarz lekarz)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lekarz).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lekarz);
        }

        // GET: lekarz/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            lekarz lekarz = db.lekarz.Find(id);
            if (lekarz == null)
            {
                return HttpNotFound();
            }
            return View(lekarz);
        }

        // POST: lekarz/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            lekarz lekarz = db.lekarz.Find(id);
            db.lekarz.Remove(lekarz);
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
