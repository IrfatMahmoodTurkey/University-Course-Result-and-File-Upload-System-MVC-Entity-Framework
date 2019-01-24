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
    public class StudentController : Controller
    {
        private StudentManager studentManager;
        private DepartmentManager departmentManager;
        private CourseManager courseManager;

        public StudentController()
        {
            studentManager = new StudentManager();
            departmentManager = new DepartmentManager();
            courseManager = new CourseManager();
        }

        // save student (For Admin)
        [HttpGet]
        public ActionResult SaveStudent()
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
        public ActionResult SaveStudent(Student student)
        {
            if (Request.IsAuthenticated)
            {
                if (System.Web.HttpContext.Current.User.Identity.Name.Equals("Admin"))
                {
                    if (ModelState.IsValid)
                    {
                        student.RegNo = GenerateRegNo(student);
                        student.ActionBy = "Admin";
                        student.ActionDone = ActionUtility.ActionInsert;
                        student.ActionTime = DateTime.Now.ToString();
                        student.ProfilePicture = ActionUtility.DefaultProfilePicture;

                        int saved = studentManager.SaveStudent(student);

                        if (saved == 2)
                        {
                            ViewBag.Response = 2;

                            ViewBag.Departments = departmentManager.GetDepartmentForDropDown();
                            return View(student);
                        }
                        else if (saved == 1)
                        {
                            ViewBag.Response = 1;

                            ViewBag.Departments = departmentManager.GetDepartmentForDropDown();
                            ModelState.Clear();
                            return RedirectToAction("Register", "Account", new { regNo = student.RegNo, password = "student12345", hiddenState = "Student" });
                        }
                        else
                        {
                            ViewBag.Response = 0;

                            ViewBag.Departments = departmentManager.GetDepartmentForDropDown();
                            return View(student);
                        }
                    }
                    else
                    {
                        ViewBag.Response = 4;

                        ViewBag.Departments = departmentManager.GetDepartmentForDropDown();
                        return View(student);
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

        // generate registration no
        public string GenerateRegNo(Student student)
        {
            Department department = studentManager.GetDepartmentById(student.DepartmentId);

            int totalStudent = studentManager.GetStudentNumberByDepartmentAndYear(student);
            string year = student.Date.Substring(6, 4);

            string regNo = String.Empty;

            if (totalStudent < 9)
            {
                regNo = department.Code + year + "00" + (totalStudent + 1);
            }
            else if (totalStudent < 99 && totalStudent >= 9)
            {
                regNo = department.Code +  year  + "0" + (totalStudent + 1);
            }
            else if (totalStudent < 1000 && totalStudent >= 99)
            {
                regNo = department.Code + year +  (totalStudent + 1);
            }
            else
            {
                regNo = department.Code + year + (totalStudent + 1);
            }

            return regNo;
        }

        // view all student (For Admin)
        [HttpGet]
        public ActionResult ViewAllStudents()
        {
            if (Request.IsAuthenticated)
            {
                if (System.Web.HttpContext.Current.User.Identity.Name.Equals("Admin"))
                {
                    ViewBag.Students = studentManager.GetAllStudentDetails();
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

        // view student details by id (For Admin)
        [HttpGet]
        public ActionResult ViewStudentDetails(int id)
        {
            if (Request.IsAuthenticated)
            {
                if (System.Web.HttpContext.Current.User.Identity.Name.Equals("Admin"))
                {
                    if (studentManager.IsStudentExists(id))
                    {
                        StudentViewModel studentViewModel = studentManager.GetStudentDetailsById(id);
                        ViewBag.ProfilePicture = studentViewModel.ProfilePicture;
                        return View(studentViewModel);
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

        // student enroll (For Student)
        [HttpGet]
        public ActionResult EnrollCourse()
        {
            if (Request.IsAuthenticated)
            {
                if (studentManager.IsStudentExistsForAuth(System.Web.HttpContext.Current.User.Identity.Name))
                {
                    string regNo = System.Web.HttpContext.Current.User.Identity.Name;
                    Student student = studentManager.GetDepartmentIdByStudentRegNo(regNo);

                    ViewBag.Courses = courseManager.GetAllCoursesInfoByDepartment(student.DepartmentId);
                    ViewBag.Messages = null;

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
        public ActionResult EnrollCourse(FormCollection formCollection)
        {
            if (Request.IsAuthenticated)
            {
                if (studentManager.IsStudentExistsForAuth(System.Web.HttpContext.Current.User.Identity.Name))
                {
                    List<string> messages = new List<string>();
                    string regNo = System.Web.HttpContext.Current.User.Identity.Name;
                    Student student = studentManager.GetDepartmentIdByStudentRegNo(regNo);
                    int studentId = student.Id;

                    var courseId = Request.Form["course.Id"].Split(',').ToArray();
                    var courseType = Request.Form["courseType"].Split(',').ToArray();

                    int lengthCourseIds = courseId.Length;

                    int[] positions = new int[lengthCourseIds]; ;

                    if (formCollection.AllKeys.Contains("selectedIndex"))
                    {
                        var selectedIndex = Request.Form["selectedIndex"].Split(',').ToArray();


                        for (int i = 0; i < selectedIndex.Length; i++)
                        {
                            int index = Convert.ToInt32(selectedIndex[i]);
                            positions[i] = index;

                            EnrollCourses enrollCourses = new EnrollCourses();

                            enrollCourses.StudentId = studentId;
                            enrollCourses.CourseId = Convert.ToInt32(courseId[index]);
                            enrollCourses.State = "ACTIVE";
                            enrollCourses.Type = courseType[index];
                            enrollCourses.ActionBy = regNo;
                            enrollCourses.ActionDone = ActionUtility.ActionInsert;
                            enrollCourses.ActionTime = DateTime.Now.ToString();

                            int enroll = studentManager.EnrollCourse(enrollCourses);

                            if (enroll == 2)
                            {
                                messages.Add("Course Already Taken");
                            }
                            else if (enroll == 1)
                            {
                                messages.Add("Successfull");
                            }
                            else if (enroll == 3)
                            {
                                messages.Add("Retake course successfully taken");
                            }
                            else if (enroll == 4)
                            {
                                messages.Add("Retake can not taken (Regular not available)");
                            }
                            else if (enroll == 5)
                            {
                                messages.Add("Recourse of course taken successfully");
                            }
                            else if (enroll == 6)
                            {
                                messages.Add("Recourse can not taken (Retake or Previous Recourse not available)");
                            }
                            else if (enroll == 7)
                            {
                                messages.Add("Enrollment disabled");
                            }
                            else
                            {
                                messages.Add("Failed to Enroll");
                            }


                        }
                    }
                    else
                    {
                        ViewBag.Response = 4;
                    }

                    ViewBag.Courses = MessageList(courseManager.GetAllCoursesInfoByDepartment(student.DepartmentId), positions, messages);
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

        // generate message (initial state)
        public List<CourseViewModel> MessageList(List<CourseViewModel> courses, int []positions, List<string> messages)
        {
            for (int i = 0; i < messages.Count; i++)
            {
                courses[positions[i]].Message = messages[i];
            }

            return courses;
        }

        // upload student profile picture (For Student)
        [HttpGet]
        public ActionResult UploadProfilePicture()
        {
            if (Request.IsAuthenticated)
            {
                if (studentManager.IsStudentExistsForAuth(System.Web.HttpContext.Current.User.Identity.Name))
                {
                    string regNo = System.Web.HttpContext.Current.User.Identity.Name;
                    Student student = studentManager.GetDepartmentIdByStudentRegNo(regNo);

                    ViewBag.ProfilePicture = student.ProfilePicture.Substring(1);
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
                if (studentManager.IsStudentExistsForAuth(System.Web.HttpContext.Current.User.Identity.Name))
                {
                    string regNo = System.Web.HttpContext.Current.User.Identity.Name;
                    Student student = studentManager.GetDepartmentIdByStudentRegNo(regNo);


                    if (profilePictureFile != null && profilePictureFile.ContentLength > 0)
                    {
                        string fileName = Path.GetFileName(profilePictureFile.FileName);

                        string newFileName = student.RegNo + "_" + fileName;

                        var fileSave = Path.Combine(Server.MapPath("~/ProfilePictures/"), newFileName);
                        profilePictureFile.SaveAs(fileSave);

                        student.ProfilePicture = "~/ProfilePictures/" + newFileName;

                        int update = studentManager.UpdateStudent(student);

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

                    Student student1 = studentManager.GetDepartmentIdByStudentRegNo(regNo);

                    ViewBag.ProfilePicture = student1.ProfilePicture.Substring(1);
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

        // update student information (For Admin)
        [HttpGet]
        public ActionResult UpdateStudentInformation(int id)
        {
            if (Request.IsAuthenticated)
            {
                if (System.Web.HttpContext.Current.User.Identity.Name.Equals("Admin"))
                {
                    if (studentManager.IsStudentExists(id))
                    {
                        Student student = studentManager.GetStudentById(id);

                        ViewBag.ProfilePicture = student.ProfilePicture.Substring(1);

                        ViewBag.Departments = departmentManager.GetDepartmentForDropDown();

                        return View(student);
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
        public ActionResult UpdateStudentInformation(Student student)
        {
            if (Request.IsAuthenticated)
            {
                if (System.Web.HttpContext.Current.User.Identity.Name.Equals("Admin"))
                {
                    if (studentManager.IsStudentExists(student.Id))
                    {
                        if (ModelState.IsValid)
                        {
                            student.ActionBy = "Admin";
                            student.ActionDone = ActionUtility.ActionUpdate;
                            student.ActionTime = DateTime.Now.ToString();

                            int update = studentManager.UpdateStudentInformation(student);

                            if (update == 2)
                            {
                                ViewBag.Response = 2;
                                ViewBag.ProfilePicture = student.ProfilePicture.Substring(1);

                                ViewBag.Departments = departmentManager.GetDepartmentForDropDown();

                                return View(student);
                            }
                            else if (update == 1)
                            {
                                Student student1 = studentManager.GetStudentById(student.Id);
                                ViewBag.Response = 1;
                                ViewBag.ProfilePicture = student1.ProfilePicture.Substring(1);

                                ViewBag.Departments = departmentManager.GetDepartmentForDropDown();

                                return View(student1);
                            }
                            else
                            {
                                ViewBag.Response = 0;
                                ViewBag.ProfilePicture = student.ProfilePicture.Substring(1);

                                ViewBag.Departments = departmentManager.GetDepartmentForDropDown();

                                return View(student);
                            }
                        }
                        else
                        {
                            ViewBag.Response = 4;
                            ViewBag.ProfilePicture = student.ProfilePicture.Substring(1);

                            ViewBag.Departments = departmentManager.GetDepartmentForDropDown();

                            return View(student);
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

        // view enrolled all Courses (For Student)
        [HttpGet]
        public ActionResult ViewAllEnrolledCourses()
        {
            if (Request.IsAuthenticated)
            {
                if (studentManager.IsStudentExistsForAuth(System.Web.HttpContext.Current.User.Identity.Name))
                {
                    string regNo = System.Web.HttpContext.Current.User.Identity.Name;
                    Student student = studentManager.GetDepartmentIdByStudentRegNo(regNo);

                    ViewBag.EnrollCourses = studentManager.GetEnrollCourseByOnlyStudentId(student.Id);

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


        // show student information in student account (For Student)
        [HttpGet]
        public ActionResult ViewInformation()
        {
            if (Request.IsAuthenticated)
            {
                if (studentManager.IsStudentExistsForAuth(System.Web.HttpContext.Current.User.Identity.Name))
                {
                    string regNo = System.Web.HttpContext.Current.User.Identity.Name;
                    int studentId = studentManager.GetDepartmentIdByStudentRegNo(regNo).Id;

                    StudentViewModel studentInfo = studentManager.GetStudentDetailsById(studentId);

                    ViewBag.ProfilePicture = studentInfo.ProfilePicture;

                    return View(studentInfo);
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