using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UniversityManagementSystemVersion2.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string RegNo { get; set; }
        [Required(ErrorMessage = "Teacher Name is required")]
        [StringLength(100, ErrorMessage = "Name lenght must be below of 100 characters")]
        public string Name { get; set; }
        [StringLength(500, ErrorMessage = "Address lenght must be below of 500 characters")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Email Address is required")]
        [EmailAddress(ErrorMessage = "Email Address must be in correct format")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Contact No is required")]
        [StringLength(100, ErrorMessage = "Contact No must be at 100 characters")]
        public string ContactNo { get; set; }
        [ForeignKey("Designation")]
        [Required(ErrorMessage = "Designation is required")]
        public int DesignationId { get; set; }
        public Designation Designation { get; set; }
        [ForeignKey("Department")]
        [Required(ErrorMessage = "Department is required")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        [Required(ErrorMessage = "Credit To Be Taken is required")]
        [Range(0.0, Double.MaxValue, ErrorMessage = "Credit To Be Taken is not less then zero")]
        public decimal CreditToBeTaken { get; set; }
        public string ActionBy { get; set; }
        public string ActionDone { get; set; }
        public string ActionTime { get; set; }
        public string ProfilePicture { get; set; }
        public List<CourseAssigned> CourseAssigned { get; set; }
    }
}