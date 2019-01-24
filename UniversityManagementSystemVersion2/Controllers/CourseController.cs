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
    public class CourseController : Controller
    {
        private CourseManager courseManager;
        private DepartmentManager departmentManager;
        private SemesterManager semesterManager;
        private TeacherManager teacherManager;
        private StudentManager studentManager;

        public CourseController()
        {
            courseManager = new CourseManager();
            departmentManager = new DepartmentManager();
            semesterManager = new SemesterManager();
            teacherManager = new TeacherManager();
            studentManager = new StudentManager();
        }
        //// save course (For Admin)
        [HttpGet]
        public ActionResult SaveCourse()
        {
            if (Request.IsAuthenticated)
            {
                if (System.Web.HttpContext.Current.User.Identity.Name.Equals("Admin"))
                {
                    ViewBag.Departments = departmentManager.GetDepartmentForDropDown();
                    ViewBag.Semesters = semesterManager.GetAllSemestersForDropDown();
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
        public ActionResult SaveCourse(Course course)
        {
            if (Request.IsAuthenticated)
            {
                if (System.Web.HttpContext.Current.User.Identity.Name.Equals("Admin"))
                {
                    if (ModelState.IsValid)
                    {
                        course.ActionBy = "Admin";
                        course.ActionDone = ActionUtility.ActionInsert;
                        course.ActionTime = DateTime.Now.ToString();

                        int saved = courseManager.SaveCourse(course);

                        if (saved == 2)
                        {
                            // exists
                            ViewBag.Response = 2;

                            ViewBag.Departments = departmentManager.GetDepartmentForDropDown();
                            ViewBag.Semesters = semesterManager.GetAllSemestersForDropDown();
                            return View(course);
                        }
                        else if (saved == 1)
                        {
                            // success
                            ViewBag.Response = 1;

                            ViewBag.Departments = departmentManager.GetDepartmentForDropDown();
                            ViewBag.Semesters = semesterManager.GetAllSemestersForDropDown();
                            ModelState.Clear();
                            return View();
                        }
                        else
                        {
                            // failed
                            ViewBag.Response = 0;

                            ViewBag.Departments = departmentManager.GetDepartmentForDropDown();
                            ViewBag.Semesters = semesterManager.GetAllSemestersForDropDown();
                            return View(course);
                        }
                    }
                    else
                    {
                        ViewBag.Response = 4;

                        ViewBag.Departments = departmentManager.GetDepartmentForDropDown();
                        ViewBag.Semesters = semesterManager.GetAllSemestersForDropDown();
                        return View(course);
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

        // view all courses (For Admin)
        [HttpGet]
        public ActionResult ViewAllCourses()
        {
            if (Request.IsAuthenticated)
            {
                if (System.Web.HttpContext.Current.User.Identity.Name.Equals("Admin"))
                {
                    ViewBag.Courses = courseManager.GetAllCoursesInfo();
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

        // view course details (For Admin)
        [HttpGet]
        public ActionResult ViewCourseDetails(int id)
        {
            if (Request.IsAuthenticated)
            {
                if (System.Web.HttpContext.Current.User.Identity.Name.Equals("Admin"))
                {
                    if (courseManager.CheckByIdCourseIsExists(id))
                    {
                        return View(courseManager.GetCourseInfoById(id));
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

        // update course (For Admin)
        [HttpGet]
        public ActionResult UpdateCourse(int id)
        {
            if (Request.IsAuthenticated)
            {
                if (System.Web.HttpContext.Current.User.Identity.Name.Equals("Admin"))
                {
                    if (courseManager.CheckByIdCourseIsExists(id))
                    {
                        Course course = courseManager.GetCourseById(id);
                        ViewBag.Departments = departmentManager.GetDepartmentForDropDown();
                        ViewBag.Semesters = semesterManager.GetAllSemestersForDropDown();
                        return View(course);
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

        [HttpPost]
        public ActionResult UpdateCourse(Course course)
        {
            if (Request.IsAuthenticated)
            {
                if (System.Web.HttpContext.Current.User.Identity.Name.Equals("Admin"))
                {
                    if (courseManager.CheckByIdCourseIsExists(course.Id))
                    {
                        if (ModelState.IsValid)
                        {
                            course.ActionBy = "Admin";
                            course.ActionDone = ActionUtility.ActionUpdate;
                            course.ActionTime = DateTime.Now.ToString();

                            int update = courseManager.UpdateCourse(course);

                            if (update == 2)
                            {
                                ViewBag.Response = 2;

                                ViewBag.Departments = departmentManager.GetDepartmentForDropDown();
                                ViewBag.Semesters = semesterManager.GetAllSemestersForDropDown();
                                return View(course);
                            }
                            else if (update == 1)
                            {
                                ViewBag.Response = 1;

                                ViewBag.Departments = departmentManager.GetDepartmentForDropDown();
                                ViewBag.Semesters = semesterManager.GetAllSemestersForDropDown();
                                return View(course);
                            }
                            else
                            {
                                ViewBag.Response = 0;

                                ViewBag.Departments = departmentManager.GetDepartmentForDropDown();
                                ViewBag.Semesters = semesterManager.GetAllSemestersForDropDown();
                                return View(course);
                            }
                        }
                        else
                        {
                            ViewBag.Response = 4;

                            ViewBag.Departments = departmentManager.GetDepartmentForDropDown();
                            ViewBag.Semesters = semesterManager.GetAllSemestersForDropDown();
                            return View(course);
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
            else
            {
                return HttpNotFound();
            }
        }

        // view assigned courses (For ALL)
        [HttpGet]
        public ActionResult ViewCoursesWithAssigned()
        {
            if (Request.IsAuthenticated)
            {
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



                ViewBag.Departments = departmentManager.GetDepartmentForDropDown();
                return View();
            }
            else
            {
                return HttpNotFound();
            }
        }

        // get course with assigned by department
        public JsonResult GetCourseStaticsByDepartment(int departmentId)
        {
            return Json(courseManager.GetAssignedCourses(departmentId));
        }
	}
}