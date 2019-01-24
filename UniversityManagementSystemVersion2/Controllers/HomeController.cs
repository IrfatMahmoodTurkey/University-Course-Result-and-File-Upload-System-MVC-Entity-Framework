using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystemVersion2.Manager;
using UniversityManagementSystemVersion2.Models;
using UniversityManagementSystemVersion2.Models.ViewModels;

namespace UniversityManagementSystemVersion2.Controllers
{
    public class HomeController : Controller
    {
        private StudentManager studentManager;
        private StudentHomeManager studentHomeManager;
        private NoticeManager noticeManager;

        private TeacherManager teacherManager;
        private TeacherHomeManager teacherHomeManager;

        private DepartmentManager departmentManager;
        private CourseManager courseManager;
        private AdminHomeManager adminHomeManager;

        public HomeController()
        {
            studentManager = new StudentManager();
            studentHomeManager = new StudentHomeManager();
            noticeManager = new NoticeManager();

            teacherManager = new TeacherManager();
            teacherHomeManager = new TeacherHomeManager();

            departmentManager = new DepartmentManager();
            courseManager = new CourseManager();
            adminHomeManager = new AdminHomeManager();
        }

        // home for Admin
        public ActionResult AdminIndex()
        {
            if (Request.IsAuthenticated)
            {
                if (System.Web.HttpContext.Current.User.Identity.Name.Equals("Admin"))
                {
                    int departmentId = 0;
                    int courseId = 0;

                    // department dropdown
                    ViewBag.Departments = departmentManager.GetDepartmentForDropDownAdminHome();
                    // get first department
                    Department department = departmentManager.GetFirstDepartments();

                    if (department == null)
                    {
                        departmentId = 0;
                    }
                    else
                    {
                        departmentId = department.Id;
                    }
                    


                    // course dropdown
                    ViewBag.Courses = courseManager.GetAllCoursesForDropDownForAdminHome();
                    // get first course
                    Course course = courseManager.GetFirstCourses();

                    if (course != null)
                    {
                        courseId = course.Id;
                    }
                    else
                    {
                        courseId = 0;
                    }

                    int recentYearInt = DateTime.Now.Year;

                    // chart 1
                    ViewBag.YearRec = recentYearInt.ToString();
                    ViewBag.YearOne = (recentYearInt - 1).ToString();
                    ViewBag.YearTwo = (recentYearInt - 2).ToString();
                    ViewBag.YearThree = (recentYearInt - 3).ToString();
                    ViewBag.YearFour = (recentYearInt - 4).ToString();

                    ViewBag.RecentYear = adminHomeManager.StudentsByYear(recentYearInt.ToString());
                    ViewBag.LastOneYear = adminHomeManager.StudentsByYear((recentYearInt - 1).ToString());
                    ViewBag.LastTwoYear = adminHomeManager.StudentsByYear((recentYearInt - 2).ToString());
                    ViewBag.LastThreeYear = adminHomeManager.StudentsByYear((recentYearInt - 3).ToString());
                    ViewBag.LastFourYear = adminHomeManager.StudentsByYear((recentYearInt - 4).ToString());

                    // chart 2
                    ViewBag.ChTwoRec = recentYearInt.ToString();
                    ViewBag.ChTwoPrec = (recentYearInt - 1).ToString();

                    ViewBag.StudentByDeptThis = adminHomeManager.StudentNoByDepartmentAndRecYear(departmentId,
                        recentYearInt.ToString());
                    ViewBag.StudentByDeptPrev = adminHomeManager.StudentNoByDepartmentAndRecYear(departmentId,
                        (recentYearInt - 1).ToString());

                    // enroll state
                    ViewBag.EnrollState = adminHomeManager.GetEnrollsState();

                    // notice
                    ViewBag.Notice = noticeManager.GetAllNotice();

                    // chart 3
                    ViewBag.Four = adminHomeManager.CourseWiseResult(courseId, 4.00m);
                    ViewBag.ThreeFive = adminHomeManager.CourseWiseResult(courseId, 3.50m);
                    ViewBag.Three = adminHomeManager.CourseWiseResult(courseId, 3.00m);
                    ViewBag.Two = adminHomeManager.CourseWiseResult(courseId, 2.00m);
                    ViewBag.One = adminHomeManager.CourseWiseResult(courseId, 1.00m);
                    ViewBag.Zero = adminHomeManager.CourseWiseResult(courseId, 0.00m);


                    // for cards
                    ViewBag.NoOfStudents = adminHomeManager.NoOfStudents();
                    ViewBag.NoOfTeachers = adminHomeManager.NoOfTeachers();
                    ViewBag.NoOfCourses = adminHomeManager.NoOfCourses();
                    ViewBag.NoOfDepartments = adminHomeManager.NoOfDepartments();

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

        // get no of student by year and department
        public JsonResult GetStudentByDepartmentYear(int departmentId)
        {
            StudentByDepartmentYearViewModel model = new StudentByDepartmentYearViewModel();


            int recentYearInt = DateTime.Now.Year;
            model.StudentRecYear = adminHomeManager.StudentNoByDepartmentAndRecYear(departmentId,
                recentYearInt.ToString());
            model.StudentPrevYear = adminHomeManager.StudentNoByDepartmentAndPrevYear(departmentId,
                (recentYearInt-1).ToString());

            return Json(model);
        }

        // get no of student by year and department
        public JsonResult GetStudentByCourseAdmin(int courseId)
        {
            PointsViewModel point = new PointsViewModel();

            point.Four = adminHomeManager.CourseWiseResult(courseId, 4.00m);
            point.ThreeFive = adminHomeManager.CourseWiseResult(courseId, 3.50m);
            point.Three = adminHomeManager.CourseWiseResult(courseId, 3.00m);
            point.Two = adminHomeManager.CourseWiseResult(courseId, 2.00m);
            point.One = adminHomeManager.CourseWiseResult(courseId, 1.00m);
            point.Zero = adminHomeManager.CourseWiseResult(courseId, 0.00m);

            return Json(point);
        }

        // home for Teacher
        public ActionResult TeacherIndex()
        {
            if (Request.IsAuthenticated)
            {
                if (teacherManager.IsTeacherExistsForAuth(System.Web.HttpContext.Current.User.Identity.Name))
                {
                    string regNo = System.Web.HttpContext.Current.User.Identity.Name;
                    int id = teacherManager.GetTeacherIdByRegNo(regNo);
                    int takenFirstCourseId = 0;


                    // cards
                    ViewBag.CoursesNo = teacherHomeManager.GetNoOfCoursesTaken(id);
                    ViewBag.TotalCredit = teacherHomeManager.GetCreditToBeTaken(id);
                    ViewBag.TakenCourseCredit = teacherHomeManager.GetTotalTakenCoursesCredit(id);

                    Course course = teacherManager.GetCourseByTeacherId(id).FirstOrDefault();

                    if (course != null)
                    {
                        takenFirstCourseId = course.Id;
                    }
                    else
                    {
                        takenFirstCourseId = 0;
                    }
                    

                    // charts
                    ViewBag.Four = teacherHomeManager.FourPoint(takenFirstCourseId);
                    ViewBag.ThreeFive = teacherHomeManager.ThreeFivePoint(takenFirstCourseId);
                    ViewBag.Three = teacherHomeManager.ThreePoint(takenFirstCourseId);
                    ViewBag.Two = teacherHomeManager.TwoPoint(takenFirstCourseId);
                    ViewBag.One = teacherHomeManager.OnePoint(takenFirstCourseId);
                    ViewBag.Zero = teacherHomeManager.ZeroPoint(takenFirstCourseId);

                    // charts
                    ViewBag.Pass = teacherHomeManager.GetPassed(takenFirstCourseId);
                    ViewBag.Failed = teacherHomeManager.GetFailed(takenFirstCourseId);

                    ViewBag.Courses = teacherManager.GetCoursesTakenByTeacherForDropDownDashboard(id);

                    // notice
                    ViewBag.Notice = noticeManager.GetAllNotice();

                    // table
                    ViewBag.TakenCourses = teacherHomeManager.GetTakenCourses(id);

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

        // get result by course id
        public JsonResult GetResultByCourseId(int courseId)
        {
            PointsViewModel model = new PointsViewModel();

            model.Four = teacherHomeManager.FourPoint(courseId);
            model.ThreeFive = teacherHomeManager.ThreeFivePoint(courseId);
            model.Three = teacherHomeManager.ThreePoint(courseId);
            model.Two = teacherHomeManager.TwoPoint(courseId);
            model.One = teacherHomeManager.OnePoint(courseId);
            model.Zero = teacherHomeManager.ZeroPoint(courseId);

            return Json(model);
        }

        // get passed failed by course id
        public JsonResult GetPassedFailed(int courseId)
        {
            PassedFailedViewModel model = new PassedFailedViewModel();

            model.Passed = teacherHomeManager.GetPassed(courseId);
            model.Failed = teacherHomeManager.GetFailed(courseId);

            return Json(model);
        }

        // home for Student
        public ActionResult StudentIndex()
        {
            if (Request.IsAuthenticated)
            {
                if (studentManager.IsStudentExistsForAuth(System.Web.HttpContext.Current.User.Identity.Name))
                {
                    string regNo = System.Web.HttpContext.Current.User.Identity.Name;
                    int studentId = studentManager.GetDepartmentIdByStudentRegNo(regNo).Id;

                    // card
                    ViewBag.NoOfEnrollCourses = studentHomeManager.GetTotalEnrolledCourses(studentId);
                    ViewBag.NoOfRegularCourses = studentHomeManager.NoOfRegularCourses(studentId);
                    ViewBag.NoOfRecourseCourses = studentHomeManager.NoOfRecourseCourses(studentId);
                    ViewBag.AverageGPA = studentHomeManager.GetAverageGpaPoint(studentId);
                    
                    // chart 1
                    ViewBag.Four = studentHomeManager.FourPointSubjects(studentId);
                    ViewBag.ThreeFive = studentHomeManager.ThreeFivePointSubjects(studentId);
                    ViewBag.Three = studentHomeManager.ThreePointSubjects(studentId);
                    ViewBag.Two = studentHomeManager.TwoPointSubjects(studentId);
                    ViewBag.One = studentHomeManager.OnePointSubjects(studentId);
                    ViewBag.Zero = studentHomeManager.ZeroPointSubjects(studentId);


                    // chart 2
                    ViewBag.Pass = studentHomeManager.NoOfPassedSubjects(studentId);
                    ViewBag.Fail = studentHomeManager.NoOfFailedSubjects(studentId);

                    // table
                    ViewBag.ShortResults = studentHomeManager.StudentResult(studentId);

                    // notice
                    ViewBag.Notice = noticeManager.GetAllNotice();

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

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
    }
}