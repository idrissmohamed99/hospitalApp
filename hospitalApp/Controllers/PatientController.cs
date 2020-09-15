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




        //get action for update
        [HttpGet]
        public ActionResult Update(int id)
        {
            return View(db.Patients.Find(id));
        }

        [HttpPost]
        public ActionResult update(Patient patient)

        {
            if (ModelState.IsValid)
            {
                var model = db.Patients.Find(patient.id);
                if (model != null)
                {
                    model.name = patient.name;
                    model.age = patient.age;

                    db.SaveChanges();
                    return RedirectToAction("GetPatient");
                }
            }
            return View();

        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            return View(db.Patients.Find(id));
        }



        [HttpGet]

        public ActionResult Delete(int id)
        {
            return View(db.Patients.Find(id));
        }

        [HttpPost]

        public ActionResult Delete(Patient patient)
        {
            var isExist = db.Patients.Find(patient.id);
            if (isExist != null)
            {
                db.Patients.Remove(isExist);
                db.SaveChanges();
                return RedirectToAction("GetPatient");
            }
            return View();
        }


    }
}