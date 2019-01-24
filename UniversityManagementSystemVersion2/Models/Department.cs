using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityManagementSystemVersion2.Models
{
    public class Department
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Department Code is required")]
        [StringLength(7, MinimumLength = 2, ErrorMessage = "Department Code must be 2 to 7 character long")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Department Name is required")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Department Name must be 1 to 50 character long")]
        public string Name { get; set; }
        public string ActionBy { get; set; }
        public string ActionDone { get; set; }
        public string ActionTime { get; set; }
        public List<Course> Courses { get; set; }
        public List<ClassroomAllocate> ClassroomAllocates { get; set; }
        public List<ExamRoutine> ExamRoutines { get; set; }
    }
}