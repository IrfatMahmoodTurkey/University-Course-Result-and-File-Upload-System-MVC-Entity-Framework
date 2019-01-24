using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rotativa;
using UniversityManagementSystemVersion2.Manager;
using UniversityManagementSystemVersion2.Models;
using UniversityManagementSystemVersion2.Models.ViewModels;

namespace UniversityManagementSystemVersion2.Controllers
{
    public class ResultController : Controller
    {
        private CourseManager courseManager;
        private StudentManager studentManager;
        private TeacherManager teacherManager;
        private ResultManager resultManager;

        public ResultController()
        {
            courseManager = new CourseManager();
            studentManager = new StudentManager();
            teacherManager = new TeacherManager();
            resultManager = new ResultManager();
        }

        // upload first fifty number (For Teacher)
        [HttpGet]
        public ActionResult UploadFirstFifty()
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

        [HttpPost]
        public ActionResult UploadFirstFifty(StudentResult result)
        {
            if (Request.IsAuthenticated)
            {
                if (teacherManager.IsTeacherExistsForAuth(System.Web.HttpContext.Current.User.Identity.Name))
                {
                    string regNo = System.Web.HttpContext.Current.User.Identity.Name;
                    int id = teacherManager.GetTeacherIdByRegNo(regNo);

                    result.ActionBy = regNo;
                    result.State = "ACTIVE";
                    result.ActionTime = DateTime.Now.ToString();
                    result.FinalExam = 0.0m;
                    result.Point = 0.0m;
                    result.PublishStatus = "NotPublished";

                    if (ModelState.IsValid)
                    {
                        int uploaded = resultManager.UploadFirstFiftyNumber(result);

                        if (uploaded == 1)
                        {
                            ViewBag.Response = 1;

                            ViewBag.Courses = teacherManager.GetCoursesTakenByTeacherForDropDown(id);
                            ViewBag.Session = studentManager.GetAllSessionsForDropDown();

                            ModelState.Clear();
                            return View();
                        }
                        else if (uploaded == 0)
                        {
                            ViewBag.Response = 0;

                            ViewBag.Courses = teacherManager.GetCoursesTakenByTeacherForDropDown(id);
                            ViewBag.Session = studentManager.GetAllSessionsForDropDown();

                            return View(result);
                        }
                        else if (uploaded == 2)
                        {
                            ViewBag.Response = 2;

                            ViewBag.Courses = teacherManager.GetCoursesTakenByTeacherForDropDown(id);
                            ViewBag.Session = studentManager.GetAllSessionsForDropDown();

                            ModelState.Clear();
                            return View();
                        }
                        else if (uploaded == 3)
                        {
                            ViewBag.Response = 3;

                            ViewBag.Courses = teacherManager.GetCoursesTakenByTeacherForDropDown(id);
                            ViewBag.Session = studentManager.GetAllSessionsForDropDown();

                            return View(result);
                        }
                        else
                        {
                            ViewBag.Response = "Update Failed";

                            ViewBag.Courses = teacherManager.GetCoursesTakenByTeacherForDropDown(id);
                            ViewBag.Session = studentManager.GetAllSessionsForDropDown();

                            return View(result);
                        }
                    }
                    else
                    {
                        ViewBag.Response = 4;

                        ViewBag.Courses = teacherManager.GetCoursesTakenByTeacherForDropDown(id);
                        ViewBag.Session = studentManager.GetAllSessionsForDropDown();

                        return View(result);
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

        public JsonResult GetStudent(string data)
        {
            int indexOf = data.IndexOf("%");

            string courseIdString = data.Substring(0, indexOf);
            List<Student> students = new List<Student>();


            if (!courseIdString.Equals("") && data.Length > (indexOf + 1) && indexOf > 0)
            {
                int courseId = Convert.ToInt32(courseIdString);
                string sessionString = data.Substring(indexOf + 1);

                students = studentManager.GetEnrolledStudentByCourseAndSession(courseId, sessionString);
            }
            else if (indexOf <= 0)
            {
                students = studentManager.GetEnrolledStudentByCourseAndSession(0, "");
            }
            else
            {
                students = studentManager.GetEnrolledStudentByCourseAndSession(0, "");
            }
            
            return Json(students);
        }

        // upload final exam result (For Teacher)
        [HttpGet]
        public ActionResult UploadFinalExamResult()
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

        [HttpPost]
        public ActionResult UploadFinalExamResult(StudentResult result)
        {
            if (Request.IsAuthenticated)
            {
                if (teacherManager.IsTeacherExistsForAuth(System.Web.HttpContext.Current.User.Identity.Name))
                {
                    string regNo = System.Web.HttpContext.Current.User.Identity.Name;
                    int id = teacherManager.GetTeacherIdByRegNo(regNo);

                    if (ModelState.IsValid)
                    {
                        result.ActionBy = regNo;
                        result.PublishStatus = "NotPublished";

                        int uploaded = resultManager.UploadLastFiftyResult(result);

                        if (uploaded == 2)
                        {
                            ViewBag.Response = 2;

                            ViewBag.Courses = teacherManager.GetCoursesTakenByTeacherForDropDown(id);
                            ViewBag.Session = studentManager.GetAllSessionsForDropDown();

                            return View(result);
                        }
                        else if (uploaded == 1)
                        {
                            ViewBag.Response = 1;

                            ViewBag.Courses = teacherManager.GetCoursesTakenByTeacherForDropDown(id);
                            ViewBag.Session = studentManager.GetAllSessionsForDropDown();

                            ModelState.Clear();
                            return View();
                        }
                        else
                        {
                            ViewBag.Response = 0;

                            ViewBag.Courses = teacherManager.GetCoursesTakenByTeacherForDropDown(id);
                            ViewBag.Session = studentManager.GetAllSessionsForDropDown();

                            return View(result);
                        }
                    }
                    else
                    {
                        ViewBag.Response = 4;

                        ViewBag.Courses = teacherManager.GetCoursesTakenByTeacherForDropDown(id);
                        ViewBag.Session = studentManager.GetAllSessionsForDropDown();

                        return View(result);
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

        public JsonResult GetStudentWithRetake(string data)
        {
            int indexOf = data.IndexOf("%");

            string courseIdString = data.Substring(0, indexOf);
            List<Student> students = new List<Student>();


            if (!courseIdString.Equals("") && data.Length > (indexOf + 1))
            {
                int courseId = Convert.ToInt32(courseIdString);
                string sessionString = data.Substring(indexOf + 1);

                students = studentManager.GetAllEnrolledStudentByCourseAndSession(courseId, sessionString);
            }
            else
            {
                students = studentManager.GetAllEnrolledStudentByCourseAndSession(0, "");
            }

            return Json(students);
        }

        // show published result of student (For Student)
        [HttpGet]
        public ActionResult ShowPublishedResult()
        {
            if (Request.IsAuthenticated)
            {
                if (studentManager.IsStudentExistsForAuth(System.Web.HttpContext.Current.User.Identity.Name))
                {
                    string regNo = System.Web.HttpContext.Current.User.Identity.Name;
                    Student student = studentManager.GetDepartmentIdByStudentRegNo(regNo);
                    int studentId = student.Id;

                    ViewBag.Results = resultManager.GetPublishedResult(studentId);
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

        // export pdf
        [HttpPost]
        public ActionResult ShowPublishedResult(string noData)
        {
            if (Request.IsAuthenticated)
            {
                if (studentManager.IsStudentExistsForAuth(System.Web.HttpContext.Current.User.Identity.Name))
                {
                    string regNo = System.Web.HttpContext.Current.User.Identity.Name;
                    Student student = studentManager.GetDepartmentIdByStudentRegNo(regNo);
                    int studentId = student.Id;

                    ViewBag.Results = resultManager.GetPublishedResult(studentId);

                    return new ActionAsPdf("MakeResultPdf", new { studentId = studentId })
                    {
                        FileName = student.RegNo + "_ResultSheet.pdf"
                    };
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

        // pdf document
        public ActionResult MakeResultPdf(int studentId)
        {
            if (studentManager.IsStudentExists(studentId))
            {
                StudentViewModel student = studentManager.GetStudentDetailsById(studentId);

                ViewBag.Department = student.DepartmentName;
                ViewBag.Name = student.Name;
                ViewBag.RegNo = student.RegNo;
                ViewBag.Email = student.Email;

                ViewBag.Results = resultManager.GetPublishedResult(studentId);
                return View();
            }
            else
            {
                return HttpNotFound();
            }
        }

        // show result details (For Student)
        [HttpGet]
        public ActionResult ShowDetailsResult(int id)
        {
            if (Request.IsAuthenticated)
            {
                if (studentManager.IsStudentExistsForAuth(System.Web.HttpContext.Current.User.Identity.Name))
                {
                    if (resultManager.IsResultExists(id))
                    {
                        ResultSheetViewModel result = resultManager.GetDetailsResult(id);
                        return View(result);
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
            else
            {
                return HttpNotFound();
            }
        }

        // view all published result (For Admin)
        public ActionResult ViewAllPublishedResultForAdmin()
        {

            if (Request.IsAuthenticated)
            {
                if (System.Web.HttpContext.Current.User.Identity.Name.Equals("Admin"))
                {
                    ViewBag.Results = resultManager.GetAllPublishedResultForAdmin();
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