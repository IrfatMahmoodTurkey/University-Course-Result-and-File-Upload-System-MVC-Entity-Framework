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
    public class DesignationController : Controller
    {
        private DesignationManager designationManager;

        public DesignationController()
        {
            designationManager = new DesignationManager();
        }

        // save designation (For Admin)
        [HttpGet]
        public ActionResult SaveDesignation()
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
        public ActionResult SaveDesignation(Designation designation)
        {
            if (Request.IsAuthenticated)
            {
                if (System.Web.HttpContext.Current.User.Identity.Name.Equals("Admin"))
                {
                    if (ModelState.IsValid)
                    {
                        designation.ActionBy = "Admin";
                        designation.ActionDone = ActionUtility.ActionInsert;
                        designation.ActionTime = DateTime.Now.ToString();

                        int saved = designationManager.SaveDesignation(designation);

                        if (saved == 2)
                        {
                            ViewBag.Response = 2;
                            return View(designation);
                        }
                        else if (saved == 1)
                        {
                            ViewBag.Response = 1;
                            ModelState.Clear();
                            return View();
                        }
                        else
                        {
                            ViewBag.Response = 0;
                            return View(designation);
                        }
                    }
                    else
                    {
                        ViewBag.Response = 4;
                        return View(designation);
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

        // view all designations (For Admin)
        [HttpGet]
        public ActionResult ViewAllDesignations()
        {
            if (Request.IsAuthenticated)
            {
                if (System.Web.HttpContext.Current.User.Identity.Name.Equals("Admin"))
                {
                    ViewBag.Designations = designationManager.GetAllDesignations();
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