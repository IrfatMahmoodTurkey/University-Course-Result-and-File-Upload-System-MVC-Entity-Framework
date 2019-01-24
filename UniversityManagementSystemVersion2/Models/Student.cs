using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UniversityManagementSystemVersion2.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string RegNo { get; set; }
        [Required(ErrorMessage = "Student Name is required")]
        [StringLength(50, ErrorMessage = "Name must be 50 character below")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email must be valid")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Contact No is required")]
        [StringLength(20, ErrorMessage = "Contact No must be less than 20 character")]
        public string ContactNo { get; set; }
        [Required(ErrorMessage = "Date is required")]
        [StringLength(20, ErrorMessage = "Date must be below 20 character long")]
        public string Date { get; set; }
        [StringLength(500, ErrorMessage = "Address must be less than 500 character long")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Department is required")]
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public string ActionBy { get; set; }
        public string ActionDone { get; set; }
        public string ActionTime { get; set; }
        public string ProfilePicture { get; set; }
        public List<EnrollCourses> EnrollCourseses { get; set; }
        public List<StudentResult> StudentResults { get; set; }
        public List<AssignmentOrReport> AssignmentOrReports { get; set; }

    }
}