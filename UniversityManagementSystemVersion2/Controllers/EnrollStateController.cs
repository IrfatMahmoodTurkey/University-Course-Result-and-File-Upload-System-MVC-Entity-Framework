using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystemVersion2.Manager;
using UniversityManagementSystemVersion2.Models;

namespace UniversityManagementSystemVersion2.Controllers
{
    public class EnrollStateController : Controller
    {
        private StudentManager studentManager;

        public EnrollStateController()
        {
            studentManager = new StudentManager();
        }

        // change enroll state (For Admin)
        [HttpGet]
        public ActionResult ChangeState()
        {
            if (Request.IsAuthenticated)
            {
                if (System.Web.HttpContext.Current.User.Identity.Name.Equals("Admin"))
                {
                    DateTime todayDate = DateTime.Today;
                    string checkDateString;

                    if (DateTime.Now.Month < 10)
                    {
                        checkDateString = "28/0" + DateTime.Now.Month + "/" + DateTime.Now.Year + " 00:00:00";
                    }
                    else
                    {
                        checkDateString = "28/" + DateTime.Now.Month + "/" + DateTime.Now.Year + " 00:00:00";
                    }

                    DateTime checkDate = DateTime.Parse(checkDateString);

                    // check date is less than 28
                    if (todayDate < checkDate)
                    {
                        ViewBag.EnrollEnable = "Enroll Can Enable";
                    }
                    else
                    {
                        ViewBag.EnrollEnable = "Enroll Can not be Enabled (Enroll can enable before 28 date)";
                    }

                    EnrollState enrollState = studentManager.GetEnrollStateSingle();

                    if (enrollState == null)
                    {
                        ViewBag.State = "Not set yet";
                    }
                    else
                    {
                        ViewBag.State = enrollState.State;
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

        [HttpPost]
        public ActionResult ChangeState(EnrollState enrollState)
        {
            if (Request.IsAuthenticated)
            {
                if (System.Web.HttpContext.Current.User.Identity.Name.Equals("Admin"))
                {
                    DateTime todayDate = DateTime.Today;
                    string checkDateString;

                    if (DateTime.Now.Month < 10)
                    {
                        checkDateString = "28/0" + DateTime.Now.Month + "/" + DateTime.Now.Year + " 00:00:00";
                    }
                    else
                    {
                        checkDateString = "28/" + DateTime.Now.Month + "/" + DateTime.Now.Year + " 00:00:00";
                    }

                    DateTime checkDate = DateTime.Parse(checkDateString);

                    if (enrollState.State.Equals("Enabled"))
                    {
                        // check date is less than 28
                        if (todayDate < checkDate)
                        {
                            enrollState.ActionBy = "Admin";
                            int changeState = studentManager.ChangeEnrollState(enrollState);

                            if (changeState == 1)
                            {
                                ViewBag.Response = 1;
                            }
                            else
                            {
                                ViewBag.Response = 0;
                            }

                            ViewBag.EnrollEnable = "Enroll Can Enable";
                        }
                        else
                        {
                            ViewBag.Response = 2;
                            ViewBag.EnrollEnable = "Enroll Can not be Enabled (Enroll can enable before 28 date)";
                        }
                    }
                    else
                    {
                        enrollState.ActionBy = "Admin";
                        int changeState = studentManager.ChangeEnrollState(enrollState);

                        if (changeState == 1)
                        {
                            ViewBag.Response = 1;
                        }
                        else
                        {
                            ViewBag.Response = 0;
                        }

                        // check date is less than 28
                        if (todayDate < checkDate)
                        {
                            ViewBag.EnrollEnable = "Enroll Can Enable";
                        }
                        else
                        {
                            ViewBag.EnrollEnable = "Enroll Can not be Enabled (Enroll can enable before 28 date)";
                        }
                    }

                    ViewBag.State = studentManager.GetEnrollStateSingle().State;

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