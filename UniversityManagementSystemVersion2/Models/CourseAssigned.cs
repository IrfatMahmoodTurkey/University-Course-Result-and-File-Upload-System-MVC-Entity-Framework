using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UniversityManagementSystemVersion2.Models
{
    public class CourseAssigned
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Teacher is required")]
        [ForeignKey("Teacher")]
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        [Required(ErrorMessage = "Course is required")]
        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public string State { get; set; }
        public string ActionBy { get; set; }
        public string ActionDone { get; set; }
        public string ActionTime { get; set; }
    }
}