using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UniversityManagementSystemVersion2.Models
{
    public class StudentResult
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
        [Required(ErrorMessage = "Attendance Number is required")]
        [Range(0, 10, ErrorMessage = "Attandance number is from 0 to 10")]
        public int Attendance { get; set; }
        [Required(ErrorMessage = "Assignment Number is required")]
        [Range(0, 10, ErrorMessage = "Assignment number is from 0 to 10")]
        public decimal Assignement { get; set; }
        [Required(ErrorMessage = "Class Test Number is required")]
        [Range(0, 10, ErrorMessage = "Class Test number is from 0 to 10")]
        public decimal ClassTest { get; set; }
        [Required(ErrorMessage = "Mid Term Number is required")]
        [Range(0, 20, ErrorMessage = "Mid Term number is from 0 to 20")]
        public decimal MidTerm { get; set; }
        [Required(ErrorMessage = "Final Exam Number is required")]
        [Range(0, 50, ErrorMessage = "Final Exam number is from 0 to 50")]
        public decimal FinalExam { get; set; }
        [Required(ErrorMessage = "Point is required")]
        [Range(0.0, 5.0, ErrorMessage = "Point is from 0 to 5.0")]
        public decimal Point { get; set; }
        [Required(ErrorMessage = "Session is required")]
        public string Session { get; set; }
        public string State { get; set; }
        public string PublishStatus { get; set; }
        public string ActionBy { get; set; }
        public string ActionDone { get; set; }
        public string ActionTime { get; set; }
    }
}