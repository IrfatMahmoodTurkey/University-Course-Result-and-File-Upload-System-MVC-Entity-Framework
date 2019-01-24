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
    public class AdmitController : Controller
    {
        private StudentManager studentManager;

        public AdmitController()
        {
            studentManager = new StudentManager();
        }

        // Make Admid Card Pdf (For Admin)
        [HttpGet]
        public ActionResult MakeAdmid()
        {
            if (Request.IsAuthenticated)
            {
                if (System.Web.HttpContext.Current.User.Identity.Name.Equals("Admin"))
                {
                    ViewBag.GetTimeType = GetActionTime();
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

        // get enrolled students by date
        public JsonResult GetEnrolledStudent(string sessionDate)
        {
            List<Student> students = studentManager.GetStudentBySessionDateForAdmit(sessionDate);
            return Json(students);
        }

        [HttpPost]
        public ActionResult MakeAdmid(AdmitViewModel admit)
        {
            if (Request.IsAuthenticated)
            {
                if (System.Web.HttpContext.Current.User.Identity.Name.Equals("Admin"))
                {
                    int studentId = admit.StudentId;
                    string sessionDate = admit.ActionTypeTime;

                    if (studentManager.IsStudentExists(studentId))
                    {
                        ViewBag.GetTimeType = GetActionTime();
                        StudentViewModel student = studentManager.GetStudentDetailsById(studentId);

                        return new ActionAsPdf("DataInPdf", new { sessionDate = sessionDate, studentId = studentId })
                        {
                            FileName = student.RegNo+"_Admit.pdf"
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
            else
            {
                return HttpNotFound();
            }
        }

        public ActionResult DataInPdf(string sessionDate, int studentId)
        {

            if (studentManager.IsStudentExists(studentId))
            {
                StudentViewModel student = studentManager.GetStudentDetailsById(studentId);

                ViewBag.Image = student.ProfilePicture;
                ViewBag.RegNo = student.RegNo;
                ViewBag.Name = student.Name;
                ViewBag.Email = student.Email;
                ViewBag.DepartmentName = student.DepartmentName;

                ViewBag.Courses = studentManager.GetCoursesByActionDateAndStudentId(sessionDate, studentId);

                return View();
            }
            else
            {
                return HttpNotFound();
            }

        }

        public List<SelectListItem> GetActionTime()
        {
            List<ActionDateViewModel> model = new List<ActionDateViewModel>();

            for (int i = 1; i < 12; i++)
            {
                if (i < 10)
                {
                    model.Add(new ActionDateViewModel()
                    {
                        Value = "/0" + i + "/" + DateTime.Now.Year+" ",
                        Text = i + "-" + DateTime.Now.Year
                    });
                }
                else
                {
                    model.Add(new ActionDateViewModel()
                    {
                        Value = "/" + i + "/" + DateTime.Now.Year+" ",
                        Text = i + "-" + DateTime.Now.Year
                    });
                }
            }

            List<SelectListItem> selectListItems =
                model.ConvertAll(m => new SelectListItem() {Text = m.Text, Value = m.Value});

            List<SelectListItem> mainSelectListItems = new List<SelectListItem>();
            mainSelectListItems.Add(new SelectListItem()
            {
                Text = "-- Select Session Type --",
                Value = "None"
            });

            mainSelectListItems.AddRange(selectListItems);
            return mainSelectListItems;
        } 
    }
}