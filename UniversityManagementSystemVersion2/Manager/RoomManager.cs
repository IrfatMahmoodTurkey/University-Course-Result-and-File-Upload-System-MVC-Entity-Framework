using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystemVersion2.Gateway;
using UniversityManagementSystemVersion2.Models;

namespace UniversityManagementSystemVersion2.Manager
{
    public class RoomManager
    {
        private RoomGateway roomGateway;

        public RoomManager()
        {
            roomGateway = new RoomGateway();
        }

        // save room
        public int SaveRoom(Room room)
        {
            if (roomGateway.IsExists(room))
            {
                return 2;
            }
            else
            {
                int rowsAffected = roomGateway.SaveRoom(room);

                if (rowsAffected > 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }

        // get all rooms
        public List<Room> GetAllRooms()
        {
            return roomGateway.GetAllRooms();
        } 

        // get all room for dropdown
        public List<SelectListItem> GetAllRoomsForDropDown()
        {
            List<Room> rooms = roomGateway.GetAllRooms();

            List<SelectListItem> selectListItems =
                rooms.ConvertAll(c => new SelectListItem() {Text = c.RoomNo, Value = c.Id.ToString()});

            List<SelectListItem> mainSelectListItems = new List<SelectListItem>();
            mainSelectListItems.Add(new SelectListItem(){Text = "-- Select Room --", Value = ""});

            mainSelectListItems.AddRange(selectListItems);

            return mainSelectListItems;
        } 
    }
}