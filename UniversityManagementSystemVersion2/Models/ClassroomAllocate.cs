using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UniversityManagementSystemVersion2.Models
{
    public class ClassroomAllocate
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Department is required")]
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        [Required(ErrorMessage = "Course is required")]
        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public Course Course { get; set; }
        [Required(ErrorMessage = "Room is required")]
        [ForeignKey("Room")]
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public string Day { get; set; }
        [Required(ErrorMessage = "From Time is required")]
        public string FromTime { get; set; }
        [Required(ErrorMessage = "To Time is required")]
        public string ToTime { get; set; }
        public string State { get; set; }
        public string ActionBy { get; set; }
        public string ActionDone { get; set; }
        public string ActionTime { get; set; } 
    }
}