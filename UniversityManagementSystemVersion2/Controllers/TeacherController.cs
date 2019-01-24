using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using UniversityManagementSystemVersion2.Manager;
using UniversityManagementSystemVersion2.Models;
using UniversityManagementSystemVersion2.Models.ViewModels;
using UniversityManagementSystemVersion2.Utility;

namespace UniversityManagementSystemVersion2.Controllers
{
    public class TeacherController : Controller
    {
        private TeacherManager teacherManager;
        private DesignationManager designationManager;
        private DepartmentManager departmentManager;
        private CourseManager courseManager;

        public TeacherController()
        {
            teacherManager = new TeacherManager();
            designationManager = new DesignationManager();
            departmentManager = new DepartmentManager();
            courseManager = new CourseManager();
        }
        // save teacher (For Admin)
        [HttpGet]
        public ActionResult SaveTeacher()
        {
            if (Request.IsAuthenticated)
            {
                if (System.Web.HttpContext.Current.User.Identity.Name.Equals("Admin"))
                {
                    ViewBag.Designations = designationManager.GetDesignationsForDropDown();
                    ViewBag.Departments = departmentManager.GetDepartmentForDropDown();

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
        public ActionResult SaveTeacher(Teacher teacher)
        {
            if (Request.IsAuthenticated)
            {
                if (System.Web.HttpContext.Current.User.Identity.Name.Equals("Admin"))
                {
                    if (ModelState.IsValid)
                    {
                        teacher.RegNo = GenerateRegNo();
                        teacher.ActionBy = "Admin";
                        teacher.ActionDone = ActionUtility.ActionInsert;
                        teacher.ActionTime = DateTime.Now.ToString();
                        teacher.ProfilePicture = ActionUtility.DefaultProfilePicture;

                        int saved = teacherManager.SaveTeacher(teacher);

                        if (saved == 2)
                        {
                            ViewBag.Response = 2;

                            ViewBag.Designations = designationManager.GetDesignationsForDropDown();
                            ViewBag.Departments = departmentManager.GetDepartmentForDropDown();
                            return View(teacher);
                        }
                        else if (saved == 1)
                        {
                            ViewBag.Response = 1;

                            ViewBag.Designations = designationManager.GetDesignationsForDropDown();
                            ViewBag.Departments = departmentManager.GetDepartmentForDropDown();
                            ModelState.Clear();
                            return RedirectToAction("Register", "Account", new { regNo = teacher.RegNo, password = "teacher12345", hiddenState = "Teacher" });

                        }
                        else
                        {
                            ViewBag.Response = 0;

                            ViewBag.Designations = designationManager.GetDesignationsForDropDown();
                            ViewBag.Departments = departmentManager.GetDepartmentForDropDown();
                            return View(teacher);
                        }
                    }
                    else
                    {
                        ViewBag.Response = 4;

                        ViewBag.Designations = designationManager.GetDesignationsForDropDown();
                        ViewBag.Departments = departmentManager.GetDepartmentForDropDown();
                        return View(teacher);
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

        // generate RegNo
        public string GenerateRegNo()
        {
            string regNo = String.Empty;

            int totalTeacher = teacherManager.NoOfTeacherEntry();

            if (totalTeacher < 9)
            {
                int lastNum = totalTeacher + 1;
                regNo = "T000" + lastNum;
            }
            else if (totalTeacher < 99 && totalTeacher >= 9)
            {
                int lastNum = totalTeacher + 1;
                regNo = "T00" + lastNum;
            }
            else if (totalTeacher < 999 && totalTeacher >= 99)
            {
                int lastNum = totalTeacher + 1;
                regNo = "T0" + lastNum;
            }
            else
            {
                int lastNum = totalTeacher + 1;
                regNo = "T" + lastNum;
            }

            return regNo;
        }

        // course assign to teacher (For Admin)
        [HttpGet]
        public ActionResult CourseAssignToTeacher()
        {
            if (Request.IsAuthenticated)
            {
                if (System.Web.HttpContext.Current.User.Identity.Name.Equals("Admin"))
                {
                    ViewBag.Departments = departmentManager.GetDepartmentForDropDown();
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
        public ActionResult CourseAssignToTeacher(CourseAssigned courseAssigned)
        {
            if (Request.IsAuthenticated)
            {
                if (System.Web.HttpContext.Current.User.Identity.Name.Equals("Admin"))
                {
                    if (ModelState.IsValid)
                    {
                        courseAssigned.ActionBy = "Admin";
                        courseAssigned.ActionDone = ActionUtility.ActionInsert;
                        courseAssigned.ActionTime = DateTime.Now.ToString();
                        courseAssigned.State = "ACTIVE";

                        TeacherViewModel teacherViewModel = teacherManager.GetTeacherInfoById(courseAssigned.TeacherId);
                        teacherViewModel.RemainingCredit = teacherManager.GetTotalTakenCoursesCredit(courseAssigned.TeacherId);
                        Course course = courseManager.GetCourseById(courseAssigned.CourseId);
                        decimal totalCreditWillTaken = course.Credit + teacherViewModel.RemainingCredit;

                        if (teacherViewModel.CreditToBeTaken < totalCreditWillTaken)
                        {
                            ViewBag.Response = 5;

                            ViewBag.Departments = departmentManager.GetDepartmentForDropDown();
                            return View(courseAssigned);
                        }
                        else
                        {
                            int assigned = teacherManager.CourseAssign(courseAssigned);

                            if (assigned == 2)
                            {
                                ViewBag.Response = 2;

                                ViewBag.Departments = departmentManager.GetDepartmentForDropDown();
                                return View(courseAssigned);
                            }
                            else if (assigned == 1)
                            {
                                ViewBag.Response = 1;

                                ViewBag.Departments = departmentManager.GetDepartmentForDropDown();
                                ModelState.Clear();
                                return View();
                            }
                            else
                            {
                                ViewBag.Response = 0;

                                ViewBag.Departments = departmentManager.GetDepartmentForDropDown();
                                return View(courseAssigned);
                            }
                        }
                    }
                    else
                    {
                        ViewBag.Response = 4;

                        ViewBag.Departments = departmentManager.GetDepartmentForDropDown();
                        return View(courseAssigned);
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

        // get teacher by department
        public JsonResult GetTeacherByDepartment(int departmentId)
        {
            List<Teacher> teachers = teacherManager.GetTeacherByDepartment(departmentId);
            return Json(teachers);
        }

        // get teacher info by teacher id
        public JsonResult GetTeacherInfoByTeacherId(int teacherId)
        {
            TeacherViewModel teacherViewModel = teacherManager.GetTeacherInfoById(teacherId);
            teacherViewModel.RemainingCredit = teacherViewModel.CreditToBeTaken -
                                               teacherManager.GetTotalTakenCoursesCredit(teacherId);

            return Json(teacherViewModel);
        }

        // get course by department
        public JsonResult GetCourseByDepartment(int departmentId)
        {
            List<CourseViewModel> courses = courseManager.GetAllCoursesInfoByDepartment(departmentId);
            return Json(courses);
        }

        // get course info by course id
        public JsonResult GetCourseInfoByCourseId(int courseId)
        {
            CourseViewModel courseViewModel = courseManager.GetCourseInfoById(courseId);
            return Json(courseViewModel);
        }

        // upload teacher profile picture (For Teacher)
        [HttpGet]
        public ActionResult UploadProfilePicture()
        {
            if (Request.IsAuthenticated)
            {
                if (teacherManager.IsTeacherExistsForAuth(System.Web.HttpContext.Current.User.Identity.Name))
                {
                    string regNo = System.Web.HttpContext.Current.User.Identity.Name;
                    int id = teacherManager.GetTeacherIdByRegNo(regNo);

                    Teacher teacher = teacherManager.GetTeacherById(id);

                    ViewBag.ProfilePicture = teacher.ProfilePicture.Substring(1);
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
        public ActionResult UploadProfilePicture(HttpPostedFileBase profilePictureFile)
        {
            if (Request.IsAuthenticated)
            {
                if (teacherManager.IsTeacherExistsForAuth(System.Web.HttpContext.Current.User.Identity.Name))
                {
                    string regNo = System.Web.HttpContext.Current.User.Identity.Name;
                    int id = teacherManager.GetTeacherIdByRegNo(regNo);

                    Teacher teacher = teacherManager.GetTeacherById(id);

                    if (profilePictureFile != null && profilePictureFile.ContentLength > 0)
                    {
                        string fileName = Path.GetFileName(profilePictureFile.FileName);

                        string newFileName = teacher.RegNo + "_" + fileName;

                        var fileSave = Path.Combine(Server.MapPath("~/ProfilePictures/"), newFileName);
                        profilePictureFile.SaveAs(fileSave);

                        teacher.ProfilePicture = "~/ProfilePictures/" + newFileName;

                        int update = teacherManager.UpdateTeacher(teacher);

                        if (update == 1)
                        {
                            ViewBag.Response = 1;
                        }
                        else
                        {
                            ViewBag.Response = 0;
                        }
                    }
                    else
                    {
                        ViewBag.Response = 2;
                    }

                    Teacher teacher1 = teacherManager.GetTeacherById(id);

                    ViewBag.ProfilePicture = teacher1.ProfilePicture.Substring(1);
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

        // view all teacher (For Admin)
        [HttpGet]
        public ActionResult ViewAllTeacher()
        {
            if (Request.IsAuthenticated)
            {
                if (System.Web.HttpContext.Current.User.Identity.Name.Equals("Admin"))
                {
                    ViewBag.Teachers = teacherManager.GetAllTeacherInfo();
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

        // view details of teacher (For Admin)
        [HttpGet]
        public ActionResult ViewTeacherDetailsById(int id)
        {
            if (Request.IsAuthenticated)
            {
                if (System.Web.HttpContext.Current.User.Identity.Name.Equals("Admin"))
                {
                    TeacherViewModel teacher = teacherManager.GetTeacherInfoById(id);

                    ViewBag.ProfilePicture = teacher.ProfilePicture;
                    return View(teacher);
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

        // update teacher information (For Admin)
        [HttpGet]
        public ActionResult UpdateTeacherInformations(int id)
        {
            if (Request.IsAuthenticated)
            {
                if (System.Web.HttpContext.Current.User.Identity.Name.Equals("Admin"))
                {
                    if (teacherManager.IsTeacherIdExists(id))
                    {
                        Teacher teacher = teacherManager.GetTeacherById(id);

                        ViewBag.ProfilePicture = teacher.ProfilePicture.Substring(1);

                        ViewBag.Departments = departmentManager.GetDepartmentForDropDown();
                        ViewBag.Designations = designationManager.GetDesignationsForDropDown();

                        return View(teacher);
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
        public ActionResult UpdateTeacherInformations(Teacher teacher)
        {
            if (Request.IsAuthenticated)
            {
                if (System.Web.HttpContext.Current.User.Identity.Name.Equals("Admin"))
                {
                    if (teacherManager.IsTeacherIdExists(teacher.Id))
                    {
                        if (ModelState.IsValid)
                        {
                            teacher.ActionBy = "Admin";
                            teacher.ActionDone = ActionUtility.ActionUpdate;
                            teacher.ActionTime = DateTime.Now.ToString();

                            int update = teacherManager.UpdateInformations(teacher);

                            if (update == 2)
                            {
                                ViewBag.ProfilePicture = teacher.ProfilePicture.Substring(1);

                                ViewBag.Departments = departmentManager.GetDepartmentForDropDown();
                                ViewBag.Designations = designationManager.GetDesignationsForDropDown();
                                ViewBag.Response = 2;

                                return View(teacher);
                            }
                            else if (update == 1)
                            {
                                Teacher teacher1 = teacherManager.GetTeacherById(teacher.Id);

                                ViewBag.ProfilePicture = teacher.ProfilePicture.Substring(1);

                                ViewBag.Departments = departmentManager.GetDepartmentForDropDown();
                                ViewBag.Designations = designationManager.GetDesignationsForDropDown();
                                ViewBag.Response = 1;

                                return View(teacher1);
                            }
                            else
                            {
                                ViewBag.ProfilePicture = teacher.ProfilePicture.Substring(1);

                                ViewBag.Departments = departmentManager.GetDepartmentForDropDown();
                                ViewBag.Designations = designationManager.GetDesignationsForDropDown();
                                ViewBag.Response = 0;

                                return View(teacher);
                            }
                        }
                        else
                        {

                            ViewBag.ProfilePicture = teacher.ProfilePicture.Substring(1);

                            ViewBag.Departments = departmentManager.GetDepartmentForDropDown();
                            ViewBag.Designations = designationManager.GetDesignationsForDropDown();
                            ViewBag.Response = 4;

                            return View(teacher);
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

        // unassign all courses from teachers (For Admin)
        [HttpGet]
        public ActionResult UnassignAllCoursesFromTeacher()
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
        public ActionResult UnassignAllCoursesFromTeacher(string nullData)
        {
            if (Request.IsAuthenticated)
            {
                if (System.Web.HttpContext.Current.User.Identity.Name.Equals("Admin"))
                {
                    int unassign = teacherManager.UnassigneAllCourses();

                    if (unassign == 1)
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

        // view teacher information (For Teacher)
        [HttpGet]
        public ActionResult ViewInformation()
        {
            if (Request.IsAuthenticated)
            {
                if (teacherManager.IsTeacherExistsForAuth(System.Web.HttpContext.Current.User.Identity.Name))
                {
                    string regNo = System.Web.HttpContext.Current.User.Identity.Name;
                    int id = teacherManager.GetTeacherIdByRegNo(regNo);

                    TeacherViewModel teacherViewModel = teacherManager.GetTeacherInfoById(id);
                    ViewBag.ProfilePicture = teacherViewModel.ProfilePicture;
                    return View(teacherViewModel);
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