using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementSystemVersion2.Models;

namespace UniversityManagementSystemVersion2.Gateway
{
    public class RoomGateway:CommonGateway
    {
        // save room
        public int SaveRoom(Room room)
        {
            Context.Rooms.Add(room);
            int rowsAffected = Context.SaveChanges();
            return rowsAffected;;
        }

        // check room already exists
        public bool IsExists(Room room)
        {
            bool isExists = Context.Rooms.Any(r => r.RoomNo == room.RoomNo);
            return isExists;
        }

        // get all room no
        public List<Room> GetAllRooms()
        {
            return Context.Rooms.ToList();
        } 
    }
}