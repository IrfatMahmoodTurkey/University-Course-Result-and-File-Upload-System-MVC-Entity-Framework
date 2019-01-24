using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UniversityManagementSystemVersion2.Models
{
    public class AssignmentOrReport
    {
        public int Id { get; set; }
        [ForeignKey("Student")]
        [Required(ErrorMessage = "Student is required")]
        public int StudentId { get; set; }
        public Student Student { get; set; }
        [ForeignKey("Course")]
        [Required(ErrorMessage = "Course is required")]
        public int CourseId { get; set; }
        public Course Course { get; set; }
        [Required(ErrorMessage = "Session is required")]
        public string Session { get; set; }
        [Required(ErrorMessage = "Assignment or Report Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Date is required")]
        public string Date { get; set; }
        public string UploadDate { get; set; }
        [Required(ErrorMessage = "File Type required")]
        public string FileType { get; set; }
        public string Url { get; set; }
        public string State { get; set; }
        public string ActionBy { get; set; }
        public string ActionDone { get; set; }
        public string ActionTime { get; set; }
    }
}