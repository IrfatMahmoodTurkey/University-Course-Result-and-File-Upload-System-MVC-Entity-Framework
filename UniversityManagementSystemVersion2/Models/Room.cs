using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityManagementSystemVersion2.Models
{
    public class Room
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Room No is required")]
        [StringLength(10, ErrorMessage = "Room No is less than 10 characters")]
        public string RoomNo { get; set; }
        public string ActionBy { get; set; }
        public string ActionDone { get; set; }
        public string ActionTime { get; set; }
        public List<ClassroomAllocate> ClassroomAllocates { get; set; }
    }
}