using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystemVersion2.Manager;
using UniversityManagementSystemVersion2.Models;
using UniversityManagementSystemVersion2.Models.ViewModels;
using UniversityManagementSystemVersion2.Utility;

namespace UniversityManagementSystemVersion2.Controllers
{
    public class ExamRoutineController : Controller
    {
        private CourseManager courseManager;
        private StudentManager studentManager;
        private ExamRoutineManager routineManager;
        private DepartmentManager departmentManager;
        private TeacherManager teacherManager;

        public ExamRoutineController()
        {
            courseManager = new CourseManager();
            studentManager = new StudentManager();
            routineManager = new ExamRoutineManager();
            departmentManager = new DepartmentManager();
            teacherManager = new TeacherManager();

        }

        // upload exam routine (For Admin)
        [HttpGet]
        public ActionResult UploadRoutine()
        {
            if (Request.IsAuthenticated)
            {
                if (System.Web.HttpContext.Current.User.Identity.Name.Equals("Admin"))
                {
                    ViewBag.Departments = departmentManager.GetDepartmentForDropDown();
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
        public ActionResult UploadRoutine(ExamRoutine examRoutine)
        {
            if (Request.IsAuthenticated)
            {
                if (System.Web.HttpContext.Current.User.Identity.Name.Equals("Admin"))
                {
                    if (ModelState.IsValid)
                    {
                        examRoutine.State = "ACTIVE";
                        examRoutine.ActionBy = "Admin";
                        examRoutine.ActionDone = ActionUtility.ActionInsert;
                        examRoutine.ActionTime = DateTime.Now.ToString();

                        TimeSpan fromTime = DateTime.Parse(examRoutine.FromTime).TimeOfDay;
                        TimeSpan toTime = DateTime.Parse(examRoutine.ToTime).TimeOfDay;
                        TimeSpan difference = DateTime.Parse("03:00:00").TimeOfDay;

                        if ((toTime - fromTime) <= difference)
                        {
                            int upload = routineManager.UploadRoutine(examRoutine);

                            if (upload == 2)
                            {
                                ViewBag.Departments = departmentManager.GetDepartmentForDropDown();
                                ViewBag.Sessions = studentManager.GetAllSessionsForDropDown();
                                ViewBag.Response = 2;

                                return View(examRoutine);
                            }
                            if (upload == 3)
                            {
                                ViewBag.Departments = departmentManager.GetDepartmentForDropDown();
                                ViewBag.Sessions = studentManager.GetAllSessionsForDropDown();
                                ViewBag.Response = 3;

                                return View(examRoutine);
                            }
                            else if (upload == 1)
                            {
                                ViewBag.Departments = departmentManager.GetDepartmentForDropDown();
                                ViewBag.Sessions = studentManager.GetAllSessionsForDropDown();
                                ViewBag.Response = 1;

                                ModelState.Clear();
                                return View();
                            }
                            else
                            {
                                ViewBag.Departments = departmentManager.GetDepartmentForDropDown();
                                ViewBag.Sessions = studentManager.GetAllSessionsForDropDown();
                                ViewBag.Response = 0;

                                return View(examRoutine);
                            }
                        }
                        else
                        {
                            ViewBag.Departments = departmentManager.GetDepartmentForDropDown();
                            ViewBag.Sessions = studentManager.GetAllSessionsForDropDown();
                            ViewBag.Response = 4;

                            return View(examRoutine);
                        }
                    }
                    else
                    {
                        ViewBag.Departments = departmentManager.GetDepartmentForDropDown();
                        ViewBag.Sessions = studentManager.GetAllSessionsForDropDown();
                        ViewBag.Response = 5;

                        return View();
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

        // get course by department
        public JsonResult GetCoursesByDepartment(int departmentId)
        {
            return Json(courseManager.GetAllCoursesInfoByDepartment(departmentId));
        }

        // show exam routine by department (For ALL)
        [HttpGet]
        public ActionResult ViewExamRoutine()
        {
            if (Request.IsAuthenticated)
            {
                ViewBag.Departments = departmentManager.GetDepartmentForDropDown();
                ViewBag.Sessions = studentManager.GetAllSessionsForDropDown();

                if (System.Web.HttpContext.Current.User.Identity.Name.Equals("Admin"))
                {
                    ViewBag.Nav = 1;
                }
                else if (studentManager.IsStudentExistsForAuth(System.Web.HttpContext.Current.User.Identity.Name))
                {
                    ViewBag.Nav = 2;
                }
                else if (teacherManager.IsTeacherExistsForAuth(System.Web.HttpContext.Current.User.Identity.Name))
                {
                    ViewBag.Nav = 3;
                }

                return View();
            }
            else
            {
                return HttpNotFound();
            }
        }

        public JsonResult GetExamRoutine(string data)
        {
            int indexOf = data.IndexOf("%");

            string departmentIdString = data.Substring(0, indexOf);
            List<ExamRoutineViewModel> routine = new List<ExamRoutineViewModel>();


            if (!departmentIdString.Equals("") && data.Length > (indexOf + 1))
            {
                int departmentId = Convert.ToInt32(departmentIdString);
                string sessionString = data.Substring(indexOf + 1);

                routine = routineManager.GetExamRoutine(departmentId, sessionString);
            }
            else
            {
                routine = routineManager.GetExamRoutine(0, "");
            }

            return Json(routine);
        }

        // deactivate exam routine (For Admin)
        [HttpGet]
        public ActionResult DeactivateExamSchedule()
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
        public ActionResult DeactivateExamSchedule(string nullData)
        {
            if (Request.IsAuthenticated)
            {
                if (System.Web.HttpContext.Current.User.Identity.Name.Equals("Admin"))
                {
                    int deactive = routineManager.DeactivateExamRoutine();

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
	}
}