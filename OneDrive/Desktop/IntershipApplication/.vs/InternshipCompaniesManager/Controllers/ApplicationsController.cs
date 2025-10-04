using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InternshipCompaniesManager.Models;
using Microsoft.AspNet.Identity;

namespace InternshipCompaniesManager.Controllers
{
    public class ApplicationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        [Authorize(Roles = "Student")]
        public ActionResult Apply(int companyId)
        {
            var company = db.Companies.Find(companyId);
            if (company == null)
            {
                return HttpNotFound();
            }

            var model = new Application
            {
                CompanyId = companyId,
                Company = company
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Student")]
        public ActionResult Apply(Application model, HttpPostedFileBase CVFile)
        {
            try
            {
                string userId = User.Identity.GetUserId();

                if (db.Applications.Any(a => a.CompanyId == model.CompanyId && a.UserId == userId))
                {
                    TempData["ErrorMessage"] = "Веќе имате аплицирано за оваа компанија.";
                    return RedirectToAction("Index", "Companies");
                }

                if (string.IsNullOrEmpty(model.Q1) || string.IsNullOrEmpty(model.Q2))
                {
                    ModelState.AddModelError("", "Мора да одговорите на двете прашања.");
                    return View(model);
                }

                if (CVFile == null || CVFile.ContentLength == 0)
                {
                    ModelState.AddModelError("CVFile", "Мора да прикачите CV фајл.");
                    return View(model);
                }

                var uploadDir = Server.MapPath("~/UploadedCVs/");
                Directory.CreateDirectory(uploadDir);
                var fileName = $"{userId}_{DateTime.Now:yyyyMMddHHmmss}_{Path.GetFileName(CVFile.FileName)}";
                var path = Path.Combine(uploadDir, fileName);
                CVFile.SaveAs(path);

                model.UserId = userId;
                model.AppliedOn = DateTime.Now;
                model.Status = "Pending";
                model.CVFilePath = "/UploadedCVs/" + fileName;

                db.Applications.Add(model);
                db.SaveChanges();

                TempData["SuccessMessage"] = "Апликацијата е успешно испратена!";
                return RedirectToAction("MyApplications");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Грешка при зачувување: " + ex.Message);
                return View(model);
            }
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateStatus(int applicationId, string newStatus)
        {
            var application = db.Applications.FirstOrDefault(a => a.Id == applicationId);
            if (application == null)
            {
                return HttpNotFound();
            }

            application.Status = newStatus;
            db.SaveChanges();

            return RedirectToAction("AllApplications");
        }

        [Authorize(Roles = "Student")]
        public ActionResult MyApplications()
        {
            string userId = User.Identity.GetUserId();
            var applications = db.Applications
                                .Include(a => a.Company)
                                .Where(a => a.UserId == userId)
                                .ToList();
            return View(applications);
        }


        [Authorize]
        public ActionResult Details(int id)
        {
            var application = db.Applications
                               .Include(a => a.Company)
                               .Include(a => a.User)
                               .FirstOrDefault(a => a.Id == id);

            if (application == null)
            {
                return HttpNotFound();
            }

            if (User.IsInRole("Student") && application.UserId != User.Identity.GetUserId())
            {
                return new HttpUnauthorizedResult();
            }

            return View(application);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var application = db.Applications.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }

          

            if (!string.IsNullOrEmpty(application.CVFilePath))
            {
                var filePath = Server.MapPath(application.CVFilePath);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }

            db.Applications.Remove(application);
            db.SaveChanges();

            return RedirectToAction("MyApplications");
        }



        [Authorize(Roles = "Admin")]
        public ActionResult AllApplications()
        {
            var applications = db.Applications
                                .Include(a => a.Company)
                                .Include(a => a.User)
                                .ToList();
            return View(applications);
        }


    }
}
