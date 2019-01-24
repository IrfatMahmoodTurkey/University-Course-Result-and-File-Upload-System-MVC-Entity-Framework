using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UniversityManagementSystemVersion2.Models
{
    public class ExamRoutine
    {
        public int Id { get; set; }
        [ForeignKey("Department")]
        [Required(ErrorMessage = "Department is required")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        [ForeignKey("Course")]
        [Required(ErrorMessage = "Course is required")]
        public int CourseId { get; set; }
        public Course Course { get; set; }
        [Required(ErrorMessage = "Date is required")]
        public string Date { get; set; }
        [Required(ErrorMessage = "From Time is required")]
        public string FromTime { get; set; }
        [Required(ErrorMessage = "To Time is required")]
        public string ToTime { get; set; }
        [Required(ErrorMessage = "Session is required")]
        public string Session { get; set; }
        public string State { get; set; }
        public string ActionBy { get; set; }
        public string ActionDone { get; set; }
        public string ActionTime { get; set; }
    }
}