using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystemVersion2.Manager;
using UniversityManagementSystemVersion2.Models;
using UniversityManagementSystemVersion2.Models.ViewModels;
using UniversityManagementSystemVersion2.Utility;

namespace UniversityManagementSystemVersion2.Controllers
{
    public class FileUploadController : Controller
    {
        private StudentManager studentManager;
        private UploadFileManager fileManager;
        private TeacherManager teacherManager;

        public FileUploadController()
        {
            studentManager = new StudentManager();
            fileManager = new UploadFileManager();
            teacherManager = new TeacherManager();
        }

        // upload file (For Student)
        [HttpGet]
        public ActionResult UploadPdfFile()
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

        [HttpPost]
        public ActionResult UploadPdfFile(AssignmentOrReport assignement, HttpPostedFileBase file)
        {
            if (Request.IsAuthenticated)
            {
                if (studentManager.IsStudentExistsForAuth(System.Web.HttpContext.Current.User.Identity.Name))
                {
                    string regNo = System.Web.HttpContext.Current.User.Identity.Name;
                    Student student = studentManager.GetDepartmentIdByStudentRegNo(regNo);
                    int studentId = student.Id;
                    assignement.StudentId = studentId;

                    assignement.ActionBy = regNo;
                    assignement.ActionDone = ActionUtility.ActionInsert;
                    assignement.UploadDate = DateTime.Now.Date.ToString();
                    assignement.ActionTime = DateTime.Now.ToString();
                    assignement.State = "ACTIVE";

                    if (ModelState.IsValid)
                    {
                        if (file != null && file.ContentLength > 0)
                        {
                            string fileExtention = file.ContentType;
                            if (fileExtention.Equals("application/pdf"))
                            {
                                string fileName = Path.GetFileName(file.FileName);

                                string newFileName = regNo + "_" + assignement.CourseId + "_" + assignement.Session + "_" +
                                                     fileName;

                                var filePath = Path.Combine(Server.MapPath("~/Files/"), newFileName);
                                file.SaveAs(filePath);

                                assignement.Url = "~/Files/" + newFileName;

                                int upload = fileManager.UploadFile(assignement);

                                if (upload == 1)
                                {
                                    ViewBag.Sessions = studentManager.GetAllSessionsForDropDown();
                                    ViewBag.Response = 1;

                                    ModelState.Clear();
                                    return View();
                                }
                                else
                                {
                                    ViewBag.Sessions = studentManager.GetAllSessionsForDropDown();
                                    ViewBag.Response = 0;

                                    return View(assignement);
                                }
                            }
                            else
                            {
                                ViewBag.Sessions = studentManager.GetAllSessionsForDropDown();
                                ViewBag.Response = 2;

                                return View(assignement);
                            }

                        }
                        else
                        {
                            ViewBag.Sessions = studentManager.GetAllSessionsForDropDown();
                            ViewBag.Response = 3;

                            return View(assignement);
                        }
                    }
                    else
                    {
                        ViewBag.Sessions = studentManager.GetAllSessionsForDropDown();
                        ViewBag.Response = 4;

                        return View(assignement);
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

        // get enrolled courses by student id and session
        public JsonResult GetEnrollCourses(string session)
        {
            string regNo = System.Web.HttpContext.Current.User.Identity.Name;
            Student student = studentManager.GetDepartmentIdByStudentRegNo(regNo);
            int studentId = student.Id;

            List<Course> courses = studentManager.GetCoursesByStudentIdAndSession(studentId, session);

            return Json(courses);
        }

        // show and download pdf (For Teacher)
        [HttpGet]
        public ActionResult ShowAssignmentsAndReports()
        {
            if (Request.IsAuthenticated)
            {
                if (teacherManager.IsTeacherExistsForAuth(System.Web.HttpContext.Current.User.Identity.Name))
                {
                    string regNo = System.Web.HttpContext.Current.User.Identity.Name;
                    int id = teacherManager.GetTeacherIdByRegNo(regNo);

                    ViewBag.Courses = teacherManager.GetCoursesTakenByTeacherForDropDown(id);
                    ViewBag.Session = studentManager.GetAllSessionsForDropDown();

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

        public JsonResult GetFilesByCourseIdAndSession(string data)
        {
            int indexOf = data.IndexOf("%");

            string courseIdString = data.Substring(0, indexOf);
            List<AssignmentAndReportViewModel> models = new List<AssignmentAndReportViewModel>();


            if (!courseIdString.Equals("") && data.Length > (indexOf + 1))
            {
                int courseId = Convert.ToInt32(courseIdString);
                string sessionString = data.Substring(indexOf + 1);

                models = fileManager.GetAssignmentAndReportByCourseAndSession(courseId, sessionString);
            }
            else
            {
                models = fileManager.GetAssignmentAndReportByCourseAndSession(0, "");
            }

            return Json(models);
        }

        // show pdf on browser
        public ActionResult ShowFile(string path)
        {
            string filePath = path;
            return File(filePath, "application/pdf");
        }

        // download files 
        public ActionResult DownloadFiles(string path)
        {
            return File(path, System.Net.Mime.MediaTypeNames.Application.Octet, path.Substring(8));
        }

        // deactive all assignments and reports (For Admin)
        [HttpGet]
        public ActionResult DeactiveFiles()
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
        public ActionResult DeactiveFiles(string nullData)
        {
            if (Request.IsAuthenticated)
            {
                if (System.Web.HttpContext.Current.User.Identity.Name.Equals("Admin"))
                {
                    int update = fileManager.DeactiveFiles();

                    if (update == 1)
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

        // view all uploaded files (For Admin)
        [HttpGet]
        public ActionResult ViewAllFiles()
        {
            if (Request.IsAuthenticated)
            {
                if (System.Web.HttpContext.Current.User.Identity.Name.Equals("Admin"))
                {
                    ViewBag.Files = fileManager.ViewAllFiles();
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