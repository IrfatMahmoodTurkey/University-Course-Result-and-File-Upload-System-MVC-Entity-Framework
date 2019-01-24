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
    public class RoomController : Controller
    {
        private RoomManager roomManager;

        public RoomController()
        {
            roomManager = new RoomManager();
        }

        // save rooom (For Admin)
        [HttpGet]
        public ActionResult SaveRoom()
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
        public ActionResult SaveRoom(Room room)
        {
            if (Request.IsAuthenticated)
            {
                if (System.Web.HttpContext.Current.User.Identity.Name.Equals("Admin"))
                {
                    if (ModelState.IsValid)
                    {
                        room.ActionBy = "Admin";
                        room.ActionDone = ActionUtility.ActionInsert;
                        room.ActionTime = DateTime.Now.ToString();

                        int saved = roomManager.SaveRoom(room);

                        if (saved == 2)
                        {
                            ViewBag.Response = 2;
                            return View(room);
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
                            return View();
                        }
                    }
                    else
                    {
                        ViewBag.Response = 4;
                        return View(room);
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

        // view all rooms (for Admin)
        public ActionResult ViewAllRooms()
        {
            if (Request.IsAuthenticated)
            {
                if (System.Web.HttpContext.Current.User.Identity.Name.Equals("Admin"))
                {
                    ViewBag.Rooms = roomManager.GetAllRooms();
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