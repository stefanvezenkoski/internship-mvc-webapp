using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using InternshipCompaniesManager.Models;

namespace InternshipCompaniesManager.Controllers
{
    public class CompaniesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Companies
        public ActionResult Index()
        {
            // Проследи информации за улогите до View-то
            ViewBag.IsAdmin = User.IsInRole("Admin");
            ViewBag.IsStudent = User.IsInRole("Student");
            ViewBag.IsAnonymous = !User.Identity.IsAuthenticated;

            return View(db.Companies.ToList());
        }

        // GET: Companies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Company company = db.Companies.Find(id);
            if (company == null)
                return HttpNotFound();

            return View(company);
        }

        private List<SelectListItem> GetSectors()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Value = "ИТ", Text = "Информациски технологии" },
                new SelectListItem { Value = "Finance", Text = "Финансии" },
                new SelectListItem { Value = "Education", Text = "Образование" },
                new SelectListItem { Value = "Healthcare", Text = "Здравство и социјална заштита" },
                new SelectListItem { Value = "Construction", Text = "Градежништво" },
                new SelectListItem { Value = "Tourism", Text = "Туризам" },
                new SelectListItem { Value = "Transport", Text = "Транспорт" },
                new SelectListItem { Value = "Retail", Text = "Трговија" },
                new SelectListItem { Value = "Manufacturing", Text = "Производство" }
            };
        }

        // GET: Companies/Create
        [Authorize(Roles = "Admin")] // Само админ може да креира
        public ActionResult Create()
        {
            ViewBag.Sectors = GetSectors();
            return View();
        }

        // POST: Companies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")] // Само админ може да креира
        public ActionResult Create([Bind(Include = "Id,Name,Position,Sector,Email,ContactPhone,ImageUrl,Address,OpenPositions")] Company company)
        {
            if (ModelState.IsValid)
            {
                db.Companies.Add(company);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Sectors = GetSectors();
            return View(company);
        }

        // GET: Companies/Edit/5
        [Authorize(Roles = "Admin")] // Само админ може да менува
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Company company = db.Companies.Find(id);
            if (company == null)
                return HttpNotFound();

            ViewBag.Sectors = GetSectors();
            return View(company);
        }

        // POST: Companies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")] // Само админ може да менува
        public ActionResult Edit([Bind(Include = "Id,Name,Position,Sector,Email,ContactPhone,ImageUrl,Address,OpenPositions, Description")] Company company)
        {
            if (ModelState.IsValid)
            {
                db.Entry(company).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Sectors = GetSectors();
            return View(company);
        }

        // GET: Companies/Delete/5
        [Authorize(Roles = "Admin")] // Само админ може да брише
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Company company = db.Companies.Find(id);
            if (company == null)
                return HttpNotFound();

            return View(company);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")] // Само админ може да брише
        public ActionResult DeleteConfirmed(int id)
        {
            Company company = db.Companies.Find(id);
            db.Companies.Remove(company);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult Partners()
        {
            var companies = db.Companies
                .GroupBy(c => c.Name)
                .Select(g => g.FirstOrDefault())
                .ToList();

            return View(companies);
        }
    }


}
