using hospitalApp.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hospitalApp.Controllers
{
    public class PatientController : Controller
    {
        // GET: Patient
        hospiitalDbEntities db = new hospiitalDbEntities();

        [HttpGet]
        public ActionResult GetPatient()
        {
            return View(db.Patients.ToList());

        }



        // Get  action for insert secreen
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Patient patient)
        {
            if (ModelState.IsValid)
            {
                db.Patients.Add(patient);
                db.SaveChanges();
                return RedirectToAction("GetPatient");

            }
            return View();

        }





    }
}