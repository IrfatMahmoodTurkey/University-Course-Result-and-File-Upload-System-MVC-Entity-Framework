using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystemVersion2.Manager;

namespace UniversityManagementSystemVersion2.Controllers
{
    public class PublishUnpublishedResultController : Controller
    {
        private ResultManager resultManager;

        public PublishUnpublishedResultController()
        {
            resultManager = new ResultManager();
        }

        // publish unpublish result (For Admin)
        [HttpGet]
        public ActionResult PublishUnpublishedResult()
        {
            if (Request.IsAuthenticated)
            {
                if (System.Web.HttpContext.Current.User.Identity.Name.Equals("Admin"))
                {
                    ViewBag.Results = resultManager.GetUnpublishedResultForView();
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
        public ActionResult PublishUnpublishedResult(string noData)
        {
            if (Request.IsAuthenticated)
            {
                if (System.Web.HttpContext.Current.User.Identity.Name.Equals("Admin"))
                {
                    int update = resultManager.PublishUnpublishResult();

                    if (update == 1)
                    {
                        ViewBag.Response = 1;
                    }
                    else
                    {
                        ViewBag.Response = 0;
                    }

                    ViewBag.Results = resultManager.GetUnpublishedResultForView();
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