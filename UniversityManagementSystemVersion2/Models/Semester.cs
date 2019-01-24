using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityManagementSystemVersion2.Models
{
    public class Semester
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Semester is required")]
        [StringLength(10, ErrorMessage = "Semester is 1 to 10 characters long")]
        public string SemesterName { get; set; }
        public string ActionBy { get; set; }
        public string ActionDone { get; set; }
        public string ActionTime { get; set; }
        public List<Course> Courses { get; set; }
    }
}