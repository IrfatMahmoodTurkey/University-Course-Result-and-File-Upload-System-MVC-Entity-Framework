using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystemVersion2.Manager;
using UniversityManagementSystemVersion2.Models;
using UniversityManagementSystemVersion2.Utility;

namespace UniversityManagementSystemVersion2.Controllers
{
    public class SemesterController : Controller
    {
        private SemesterManager semesterManager;

        public SemesterController()
        {
            semesterManager = new SemesterManager();
        }

        // save semester (For Admin)
        [HttpGet]
        public ActionResult SaveSemester()
        {
            if (Request.IsAuthenticated)
            {
                if (System.Web.HttpContext.Current.User.Identity.Name.Equals("Admin"))
                {
                    return View();
                }
                else
                {
                    return HttpNotFound();
                }
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        public ActionResult SaveSemester(Semester semester)
        {
            if (Request.IsAuthenticated)
            {
                if (System.Web.HttpContext.Current.User.Identity.Name.Equals("Admin"))
                {
                    if (ModelState.IsValid)
                    {
                        semester.ActionBy = "Admin";
                        semester.ActionDone = ActionUtility.ActionInsert;
                        semester.ActionTime = DateTime.Now.ToString();

                        int saved = semesterManager.SaveSemester(semester);

                        if (saved == 2)
                        {
                            // exists
                            ViewBag.Response = 2;
                            return View(semester);
                        }
                        else if (saved == 1)
                        {
                            // succsess
                            ViewBag.Response = 1;
                            ModelState.Clear();
                            return View();
                        }
                        else
                        {
                            // failed
                            ViewBag.Response = 0;
                            return View(semester);
                        }
                    }
                    else
                    {
                        ViewBag.Response = 4;
                        return View(semester);
                    }
                }
                else
                {
                    return HttpNotFound();
                }
            }
            else
            {
                return HttpNotFound();
            }
        }

        // view all semesters (For Admin)
        [HttpGet]
        public ActionResult ViewAllSemesters()
        {
            if (Request.IsAuthenticated)
            {
                if (System.Web.HttpContext.Current.User.Identity.Name.Equals("Admin"))
                {
                    ViewBag.Semesters = semesterManager.GetAllSemesters();
                    return View();
                }
                else
                {
                    return HttpNotFound();
                }
            }
            else
            {
                return HttpNotFound();
            }
        }
	}
}