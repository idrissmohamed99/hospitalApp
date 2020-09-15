using hospitalApp.Models.Data;
using hospitalApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using PagedList;

namespace hospitalApp.Controllers
{
    public class DoctorController : Controller
    {
        // GET: Doctor
        hospiitalDbEntities db = new hospiitalDbEntities();

        [HttpGet]
        public ActionResult GetDoctors()



               ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
ViewBag.CurrentFilter = searchString;


            IQueryable<CheckUp> Checks = db.CheckUps;

            if (!string.IsNullOrEmpty(searchString))
            {
                Checks = Checks.Where(s => s.doctor.doctorName.Contains(searchString)
          || s.Patient.name.Contains(searchString) || s.CheckType.Contains(searchString) || s.CreateAt.ToString().Contains(searchString));
            }

            var viewModels = new List<CheckUpViewModel>();

            if (Checks != null)
            {




                foreach (var item in Checks)
                {
                    var viewModel = new CheckUpViewModel();

viewModel.id = item.id;
                    viewModel.doctorName = item.doctor.doctorName;
                    viewModel.Patientname = item.Patient.name;
                    viewModel.CreateAt = item.CreateAt;
                    viewModel.CheckType = item.CheckType;
                    viewModels.Add(viewModel);

                }
            }

            if (page.HasValue && page< 1)
            {
                return null;
            }
            // retrieve list from database/wherever9
            var listUnpaged = viewModels.ToList();
// page the list
const int pageSize = 10;
var listPaged = listUnpaged.ToPagedList(page ?? 1, pageSize);
            // return a 404 if user browses to pages beyond last page. special case first page if no items exist
            if (listPaged.PageNumber != 1 && page.HasValue && page > listPaged.PageCount)
            {
                return null;
            }

            return View(listPaged);
        }






















        {
            return View(db.doctors.ToList());

        }
        /////Get action for insert Doctor

        [HttpGet]

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(doctor doctor)
        {
            if (ModelState.IsValid)
            {

                db.doctors.Add(doctor);
                db.SaveChanges();

                return RedirectToAction("GetDoctors");
            }
            return View();

        }


        //get action for update
        [HttpGet]
        public ActionResult Update(int id)
        {
            return View(db.doctors.Find(id));
        }

        [HttpPost]
        public ActionResult Update(doctor doctor)

        {
            if (ModelState.IsValid)
            {
                var model = db.doctors.Find(doctor.id);
                if (model != null)
                {
                    model.doctorName = doctor.doctorName;
                    model.speclisht = doctor.speclisht;

                    db.SaveChanges();
                    return RedirectToAction("GetDoctors");
                }
            }
            return View();


        }



        [HttpGet]

        public ActionResult Details(int id)
        {
            return View(db.doctors.Find(id));
        }


        public ActionResult Delete(int id)
        {
            return View(db.doctors.Find(id));
        }
        [HttpPost]
        public ActionResult Delete( doctor doctor)
        {
            var isExisit = db.doctors.Find(doctor.id);
            if (isExisit != null)
            {
                db.doctors.Remove(isExisit);
                db.SaveChanges();
                return RedirectToAction("GetDoctors");

            }
            return View();
        }

    }

}


