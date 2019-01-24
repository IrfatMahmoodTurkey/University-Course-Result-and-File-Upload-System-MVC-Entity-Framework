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
    public class ClassroomAllocationController : Controller
    {
        private DepartmentManager departmentManager;
        private CourseManager courseManager;
        private RoomManager roomManager;
        private ClassRoomAllocationManager classRoomAllocationManager;
        private StudentManager studentManager;
        private TeacherManager teacherManager;

        public ClassroomAllocationController()
        {
            departmentManager = new DepartmentManager();
            courseManager = new CourseManager();
            roomManager = new RoomManager();
            classRoomAllocationManager = new ClassRoomAllocationManager();
            studentManager = new StudentManager();
            teacherManager = new TeacherManager();
        }

        // allocate classroom (Only Admin)
        [HttpGet]
        public ActionResult ClassRoomAllocation()
        {
            if (Request.IsAuthenticated)
            {
                if (System.Web.HttpContext.Current.User.Identity.Name.Equals("Admin"))
                {
                    ViewBag.Departments = departmentManager.GetDepartmentForDropDown();
                    ViewBag.Courses = courseManager.GetAllCoursesForDropDown();
                    ViewBag.Rooms = roomManager.GetAllRoomsForDropDown();
                    ViewBag.Days = DayForDropDown();

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
        public ActionResult ClassRoomAllocation(ClassroomAllocate allocate)
        {
            if (Request.IsAuthenticated)
            {
                if (System.Web.HttpContext.Current.User.Identity.Name.Equals("Admin"))
                {
                    if (ModelState.IsValid)
                    {
                        // check time range
                        TimeSpan fromTime = DateTime.Parse(allocate.FromTime).TimeOfDay;
                        TimeSpan toTime = DateTime.Parse(allocate.ToTime).TimeOfDay;

                        string timeDiffString = "03:00:00";
                        TimeSpan timeDifference = DateTime.Parse(timeDiffString).TimeOfDay;

                        if (fromTime > toTime)
                        {
                            ViewBag.Departments = departmentManager.GetDepartmentForDropDown();
                            ViewBag.Courses = courseManager.GetAllCoursesForDropDown();
                            ViewBag.Rooms = roomManager.GetAllRoomsForDropDown();
                            ViewBag.Days = DayForDropDown();
                            ViewBag.Response = 2;

                            return View(allocate);
                        }
                        else if ((fromTime - toTime) > timeDifference)
                        {
                            ViewBag.Departments = departmentManager.GetDepartmentForDropDown();
                            ViewBag.Courses = courseManager.GetAllCoursesForDropDown();
                            ViewBag.Rooms = roomManager.GetAllRoomsForDropDown();
                            ViewBag.Days = DayForDropDown();
                            ViewBag.Response = 11;

                            return View(allocate);
                        }
                        else
                        {
                            allocate.ActionBy = "Admin";
                            allocate.ActionDone = ActionUtility.ActionInsert;
                            allocate.ActionTime = DateTime.Now.ToString();
                            allocate.State = "ACTIVE";

                            int allocation = classRoomAllocationManager.Allocate(allocate);

                            if (allocation == 1)
                            {
                                ViewBag.Response = 1;
                                ModelState.Clear();

                                ViewBag.Departments = departmentManager.GetDepartmentForDropDown();
                                ViewBag.Courses = courseManager.GetAllCoursesForDropDown();
                                ViewBag.Rooms = roomManager.GetAllRoomsForDropDown();
                                ViewBag.Days = DayForDropDown();
                                return View();
                            }
                            else if (allocation == 5)
                            {
                                ViewBag.Response = 5;

                                ViewBag.Departments = departmentManager.GetDepartmentForDropDown();
                                ViewBag.Courses = courseManager.GetAllCoursesForDropDown();
                                ViewBag.Rooms = roomManager.GetAllRoomsForDropDown();
                                ViewBag.Days = DayForDropDown();
                                return View(allocate);
                            }
                            else if (allocation == 10)
                            {
                                ViewBag.Response = 10;

                                ViewBag.Departments = departmentManager.GetDepartmentForDropDown();
                                ViewBag.Courses = courseManager.GetAllCoursesForDropDown();
                                ViewBag.Rooms = roomManager.GetAllRoomsForDropDown();
                                ViewBag.Days = DayForDropDown();
                                return View(allocate);
                            }
                            else
                            {
                                ViewBag.Response = 0;

                                ViewBag.Departments = departmentManager.GetDepartmentForDropDown();
                                ViewBag.Courses = courseManager.GetAllCoursesForDropDown();
                                ViewBag.Rooms = roomManager.GetAllRoomsForDropDown();
                                ViewBag.Days = DayForDropDown();

                                return View(allocate);
                            }
                        }
                    }
                    else
                    {
                        ViewBag.Response = 3;

                        ViewBag.Departments = departmentManager.GetDepartmentForDropDown();
                        ViewBag.Courses = courseManager.GetAllCoursesForDropDown();
                        ViewBag.Rooms = roomManager.GetAllRoomsForDropDown();
                        ViewBag.Days = DayForDropDown();
                        return View(allocate);
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

        // make manual day
        public List<SelectListItem> DayForDropDown()
        {
            List<SelectListItem> days = new List<SelectListItem>();

            days.Add(new SelectListItem() { Text = "Saturday", Value = "Sat" });
            days.Add(new SelectListItem() { Text = "Sunday", Value = "Sun" });
            days.Add(new SelectListItem() { Text = "Monday", Value = "Mon" });
            days.Add(new SelectListItem() { Text = "Tuesday", Value = "Tue" });
            days.Add(new SelectListItem() { Text = "Wednesday", Value = "Wed" });
            days.Add(new SelectListItem() { Text = "Thursday", Value = "Thus" });
            days.Add(new SelectListItem() { Text = "Friday", Value = "Fri" });

            return days;
        }
 
        // get courses by department
        public JsonResult GetCoursesByDepartment(int departmentId)
        {
            return Json(courseManager.GetAllCoursesInfoByDepartment(departmentId));
        }

        //show class schedule by department (For ALL)
        [HttpGet]
        public ActionResult ViewClassScheduleByDepartmentId()
        {
            if (Request.IsAuthenticated)
            {
                // check by which is auth (For Navigation bar)
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

        public JsonResult GetScheduleByDepartment(int departmentId)
        {
            return Json(classRoomAllocationManager.GetScheduleByDepartmentId(departmentId));
        }

        // unallocate all courses (For Admin)
        [HttpGet]
        public ActionResult UnallocateCourses()
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
        public ActionResult UnallocateCourses(string nullData)
        {
            if (Request.IsAuthenticated)
            {
                if (System.Web.HttpContext.Current.User.Identity.Name.Equals("Admin"))
                {
                    int unallocate = classRoomAllocationManager.UnallocateAllCourses();

                    if (unallocate == 1)
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