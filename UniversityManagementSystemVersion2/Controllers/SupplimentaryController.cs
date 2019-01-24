using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystemVersion2.Manager;
using UniversityManagementSystemVersion2.Models;
using UniversityManagementSystemVersion2.Utility;

namespace UniversityManagementSystemVersion2.Controllers
{
    public class SupplimentaryController : Controller
    {
        private TeacherManager teacherManager;
        private StudentManager studentManager;
        private SupplymentaryManager supplymentaryManager;

        public SupplimentaryController()
        {
            teacherManager = new TeacherManager();
            studentManager = new StudentManager();
            supplymentaryManager = new SupplymentaryManager();
        }

        // upload supplymentary (For Teacher)
        [HttpGet]
        public ActionResult UploadSupplymentary()
        {
            if (Request.IsAuthenticated)
            {
                if (teacherManager.IsTeacherExistsForAuth(System.Web.HttpContext.Current.User.Identity.Name))
                {
                    string regNo = System.Web.HttpContext.Current.User.Identity.Name;
                    int id = teacherManager.GetTeacherIdByRegNo(regNo);

                    ViewBag.Courses = teacherManager.GetCoursesTakenByTeacherForDropDown(id);
                    ViewBag.Sessions = studentManager.GetAllSessionsForDropDown();

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
        public ActionResult UploadSupplymentary(Supplimentary supplimentary, HttpPostedFileBase file)
        {
            if (Request.IsAuthenticated)
            {
                if (teacherManager.IsTeacherExistsForAuth(System.Web.HttpContext.Current.User.Identity.Name))
                {
                    string regNo = System.Web.HttpContext.Current.User.Identity.Name;
                    int id = teacherManager.GetTeacherIdByRegNo(regNo);

                    if (ModelState.IsValid)
                    {
                        if (file != null && file.ContentLength > 0)
                        {
                            string fileName = Path.GetFileName(file.FileName);

                            string newFileName = regNo + "_" + supplimentary.Session + "_" + fileName;
                            supplimentary.FileName = newFileName;

                            var filePath = Path.Combine(Server.MapPath("~/SupplymentaryFiles/"), newFileName);
                            file.SaveAs(filePath);

                            supplimentary.FileUrl = "~/SupplymentaryFiles/" + newFileName;
                            supplimentary.ActionBy = regNo;
                            supplimentary.ActionDone = ActionUtility.ActionInsert;
                            supplimentary.State = "ACTIVE";
                            supplimentary.ActionTime = DateTime.Now.ToString();

                            int save = supplymentaryManager.UploadSupplymentary(supplimentary);

                            if (save == 1)
                            {
                                ViewBag.Courses = teacherManager.GetCoursesTakenByTeacherForDropDown(id);
                                ViewBag.Sessions = studentManager.GetAllSessionsForDropDown();
                                ViewBag.Response = 1;

                                ModelState.Clear();
                                return View();
                            }
                            else
                            {
                                ViewBag.Courses = teacherManager.GetCoursesTakenByTeacherForDropDown(id);
                                ViewBag.Sessions = studentManager.GetAllSessionsForDropDown();
                                ViewBag.Response = 0;

                                return View(supplimentary);
                            }
                        }
                        else
                        {
                            ViewBag.Courses = teacherManager.GetCoursesTakenByTeacherForDropDown(id);
                            ViewBag.Sessions = studentManager.GetAllSessionsForDropDown();
                            ViewBag.Response = 2;

                            return View(supplimentary);
                        }
                    }
                    else
                    {
                        ViewBag.Courses = teacherManager.GetCoursesTakenByTeacherForDropDown(id);
                        ViewBag.Sessions = studentManager.GetAllSessionsForDropDown();
                        ViewBag.Response = 4;

                        return View(supplimentary);
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

        // download supplymentaries (For Student)
        [HttpGet]
        public ActionResult DownloadSupplymentary()
        {
            if (Request.IsAuthenticated)
            {
                if (studentManager.IsStudentExistsForAuth(System.Web.HttpContext.Current.User.Identity.Name))
                {
                    ViewBag.Sessions = studentManager.GetAllSessionsForDropDown();

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

        // get enrolled courses by student id and session
        public JsonResult GetEnrollCourses(string session)
        {
            string regNo = System.Web.HttpContext.Current.User.Identity.Name;
            Student student = studentManager.GetDepartmentIdByStudentRegNo(regNo);
            int studentId = student.Id;

            List<Course> courses = studentManager.GetCoursesByStudentIdAndSession(studentId, session);

            return Json(courses);
        }

        public JsonResult GetFilesByCourseIdAndSession(string data)
        {
            int indexOf = data.IndexOf("%");

            string courseIdString = data.Substring(0, indexOf);
            List<Supplimentary> models = new List<Supplimentary>();


            if (!courseIdString.Equals("") && data.Length > (indexOf + 1))
            {
                int courseId = Convert.ToInt32(courseIdString);
                string sessionString = data.Substring(indexOf + 1);

                models = supplymentaryManager.GetFiles(sessionString, courseId);
            }
            else
            {
                models = supplymentaryManager.GetFiles("", 0);
            }

            return Json(models);
        }

        public ActionResult DownloadSupplymentaryFile(string path, string fileName)
        {
            return File(path, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        // deactive supplimentary (For Admin)
        [HttpGet]
        public ActionResult DeactiveSupplimentary()
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
        public ActionResult DeactiveSupplimentary(string nullData)
        {
            if (Request.IsAuthenticated)
            {
                if (System.Web.HttpContext.Current.User.Identity.Name.Equals("Admin"))
                {
                    int deactive = supplymentaryManager.DeactiveFiles();

                    if (deactive == 1)
                    {
                        ViewBag.Response = 1;
                    }
                    else
                    {
                        ViewBag.Response = 0;
                    }

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

        // show supplementary uploaded by teacher (For Teacher)
        [HttpGet]
        public ActionResult ShowUploadedSupplementaries()
        {
            if (Request.IsAuthenticated)
            {
                if (teacherManager.IsTeacherExistsForAuth(System.Web.HttpContext.Current.User.Identity.Name))
                {
                    string regNo = System.Web.HttpContext.Current.User.Identity.Name;
                    int id = teacherManager.GetTeacherIdByRegNo(regNo);

                    ViewBag.Courses = teacherManager.GetCoursesTakenByTeacherForDropDown(id);
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

        // get files by course id
        public JsonResult GetFilesByCourseId(int courseId)
        {
            return Json(supplymentaryManager.GetAllFilesByCourseId(courseId));
        }

	}
}