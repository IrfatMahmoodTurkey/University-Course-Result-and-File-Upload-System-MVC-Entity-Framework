using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using UniversityManagementSystemVersion2.Manager;
using UniversityManagementSystemVersion2.Models;
using UniversityManagementSystemVersion2.Utility;

namespace UniversityManagementSystemVersion2.Controllers
{
    public class DepartmentController : Controller
    {
        private DepartmentManager departmentManager;

        public DepartmentController()
        {
            departmentManager = new DepartmentManager();
        }

        //save department (For Admin)
        [HttpGet]
        public ActionResult SaveDepartment()
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
        public ActionResult SaveDepartment(Department department)
        {
            if (Request.IsAuthenticated)
            {
                if (System.Web.HttpContext.Current.User.Identity.Name.Equals("Admin"))
                {
                    if (ModelState.IsValid)
                    {
                        department.ActionBy = "Admin";
                        department.ActionDone = ActionUtility.ActionInsert;
                        department.ActionTime = DateTime.Now.ToString();

                        int saved = departmentManager.SaveDepartment(department);

                        if (saved == 2)
                        {
                            // exists
                            ViewBag.Response = 2;
                            return View(department);
                        }
                        else if (saved == 1)
                        {
                            // saved
                            ViewBag.Response = 1;
                            ModelState.Clear();
                            return View();
                        }
                        else
                        {
                            // failed
                            ViewBag.Response = 0;
                            return View(department);
                        }
                    }
                    else
                    {
                        ViewBag.Response = 4;
                        return View(department);
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

        // view all departments (For Admin)
        [HttpGet]
        public ActionResult ViewAllDepartments()
        {
            if (Request.IsAuthenticated)
            {
                if (System.Web.HttpContext.Current.User.Identity.Name.Equals("Admin"))
                {
                    ViewBag.Departments = departmentManager.GetAllDepartments();
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