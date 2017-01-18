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
using Microsoft.AspNet.Identity.EntityFramework;

namespace ePrzychodnia.Controllers
{
    public class pacjentController : Controller
    {
        private ePrzychodniaEntities db = new ePrzychodniaEntities();

        // GET: pacjent
        public ActionResult Index()
        {
            return View(db.pacjent.ToList());
        }

        // GET: pacjent/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pacjent pacjent = db.pacjent.Find(id);
            if (pacjent == null)
            {
                return HttpNotFound();
            }
            return View(pacjent);
        }

        // GET: pacjent/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: pacjent/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_pacjent,nazwisko,imie,wiek,pesel,telefon,Email,Password,ConfirmPassword")] pacjent pacjent)
        {
            if (pacjent.Email == null || pacjent.Password == null || pacjent.Password != pacjent.ConfirmPassword)//tego nie obejmuje sprawdzenie modelu, dlatego jest sprawdzane oddzielnie
            {
                return View(pacjent);
            }
            if (ModelState.IsValid)
            {
                ApplicationDbContext context = new ApplicationDbContext();
                var UserManagerr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

                var user = new ApplicationUser { UserName = pacjent.Email, Email = pacjent.Email };
                var result = UserManagerr.Create(user, pacjent.Password);
                if (result.Succeeded)
                {
                    UserManagerr.AddToRoleAsync(user.Id, pacjent.UserRoles);
                    pacjent.id_uzytkownika = user.Id;
                    db.pacjent.Add(pacjent);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(pacjent);
        }

        // GET: pacjent/Edit/5
        public ActionResult Edit(int? id)
        {
            ePrzychodniaEntities db = new ePrzychodniaEntities();
            if (id!=null)
            {
                pacjent pacjent_z_id = db.pacjent.FirstOrDefault(i => i.id_pacjent == id);
                return View(pacjent_z_id);
            }
            string cos = User.Identity.GetUserId();//dla zalogowanego pacjenta, ktory uzupelnia swoje dane
            pacjent zalogowany_pacjent = db.pacjent.FirstOrDefault(i => i.id_uzytkownika == cos);
            if (zalogowany_pacjent != null)
            {
                return View(zalogowany_pacjent);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        // POST: pacjent/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_pacjent,nazwisko,imie,wiek,pesel,telefon,id_uzytkownika")] pacjent pacjent)
        {
            string org_id = (db.pacjent.AsNoTracking().Where(i => i.id_pacjent== pacjent.id_pacjent)).First().id_uzytkownika;
            if (ModelState.IsValid)
            {
                db.Entry(pacjent).State = EntityState.Modified;
                pacjent.id_uzytkownika = org_id;
                db.SaveChanges();
                return RedirectToAction("Index","Manage");
            }
            return View(pacjent);
        }

        // GET: pacjent/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pacjent pacjent = db.pacjent.Find(id);
            if (pacjent == null)
            {
                return HttpNotFound();
            }
            return View(pacjent);
        }

        // POST: pacjent/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            pacjent pacjent = db.pacjent.Find(id);
            db.pacjent.Remove(pacjent);
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
