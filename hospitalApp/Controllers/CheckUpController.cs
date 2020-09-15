using hospitalApp.Models.Data;
using hospitalApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace hospitalApp.Controllers
{
    public class CheckUpController : Controller
    {
        hospiitalDbEntities db = new hospiitalDbEntities();

        // GET: CheckUp






        public ActionResult GetChecksUp(string sortOrder, string currentFilter, string searchString, int? page)
        {

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

            if (page.HasValue && page < 1)
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

        //وظيفة انشاء كشف جديد
        [HttpGet]
        public ActionResult InsertCheckUp()
        {
            ViewBag.DoctorId = new SelectList(db.doctors.ToList(), "id", "doctorName");
            ViewBag.PatientId = new SelectList(db.Patients.ToList(), "id", "name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InsertCheckUp(InsertCheckViewModel checkViewModel)
        {
            if (ModelState.IsValid)
            {
                CheckUp checkUp = new CheckUp();
                checkUp.CheckType = checkViewModel.CheckType;
                checkUp.DoctorId = checkViewModel.DoctorId;
                checkUp.PatientId = checkViewModel.PatientId;
                checkUp.CreateAt = DateTime.Now;
                db.CheckUps.Add(checkUp);
                db.SaveChanges();
                return RedirectToAction("GetChecksUp");
            }
            ViewBag.DoctorId = new SelectList(db.doctors.ToList(), "id", "doctorName");
            ViewBag.PatientId = new SelectList(db.Patients.ToList(), "id", "name");
            return View(checkViewModel);
        }
        //تعديل كشف
        [HttpGet]
        public ActionResult UpdateCheckUp(int? id)
        {
            if (id != null)
            {
                var checkUp = db.CheckUps.Find(id);
                ViewBag.DoctorId = new SelectList(db.doctors.ToList(), "id", "DoctorName", checkUp.DoctorId);
                ViewBag.PatientId = new SelectList(db.Patients.ToList(), "id", "name", checkUp.PatientId);
                var CheckViewModel = new UpdateCheckUpViewModel();
                CheckViewModel.CheckType = checkUp.CheckType;
                CheckViewModel.id = checkUp.id;
                return View(CheckViewModel);
            }
            return HttpNotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateCheckUp(UpdateCheckUpViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var OldCheckUp = db.CheckUps.Find(viewModel.id);
                OldCheckUp.DoctorId = viewModel.DoctorId;
                OldCheckUp.PatientId = viewModel.PatientId;
                OldCheckUp.CheckType = viewModel.CheckType;
                db.SaveChanges();
                return RedirectToAction("GetChecksUp");
            }
            ViewBag.DoctorId = new SelectList(db.doctors.ToList(), "id", "DoctorName", viewModel.DoctorId);
            ViewBag.PatientId = new SelectList(db.Patients.ToList(), "id", "name", viewModel.PatientId);
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var checkup = db.CheckUps.Find(id);


            CheckUpViewModel viewModel = new CheckUpViewModel
            {
                CheckType = checkup.CheckType,
                doctorName = checkup.doctor.doctorName,
                Patientname = checkup.Patient.name,
                CreateAt = checkup.CreateAt,
                id = checkup.id

            };

            return View(viewModel);
        }



        [HttpGet]

        public ActionResult Delete(int id)
        {
            var checkup = db.CheckUps.Find(id);
            CheckUpViewModel viewModel = new CheckUpViewModel
            {
                CheckType = checkup.CheckType,
                doctorName = checkup.doctor.doctorName,
                Patientname = checkup.Patient.name,
                CreateAt = checkup.CreateAt,
                id = checkup.id

            };

            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Delete(CheckUpViewModel checkUpView)
        {

            var result = db.CheckUps.Find(checkUpView.id);
            db.CheckUps.Remove(result);
            db.SaveChanges();
            return RedirectToAction("GetChecksUp");
        }
    }


}