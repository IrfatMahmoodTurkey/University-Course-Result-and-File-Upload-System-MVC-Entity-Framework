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
    public class NoticeController : Controller
    {
        private NoticeManager noticeManager;

        public NoticeController()
        {
            noticeManager = new NoticeManager();
        }

        // add notice (For Admin)
        [HttpGet]
        public ActionResult AddNotice()
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
        public ActionResult AddNotice(Notice notice)
        {
            if (Request.IsAuthenticated)
            {
                if (System.Web.HttpContext.Current.User.Identity.Name.Equals("Admin"))
                {
                    if (ModelState.IsValid)
                    {
                        notice.RegNo = "Admin";
                        notice.DateTime = DateTime.Now.ToString();
                        notice.ActionBy = "Admin";
                        notice.ActionDone = ActionUtility.ActionInsert;
                        notice.ActionTime = DateTime.Now.ToString();

                        int save = noticeManager.UploadNotice(notice);

                        if (save == 1)
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
                        ViewBag.Response = 3;
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