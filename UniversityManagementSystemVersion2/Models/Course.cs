using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UniversityManagementSystemVersion2.Models
{
    public class Course
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Course Code is required")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Course Code must be at least 5 character long")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Course Name is required")]
        [StringLength(50, ErrorMessage = "Course Name range is 50 character long")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Credit is required")]
        [Range(0.5, 5.0, ErrorMessage = "Credit must be 0.5 to 5.0")]
        public decimal Credit { get; set; }
        [StringLength(100, ErrorMessage = "Description will be below of 100 characters")]
        public string Description { get; set; }

        [ForeignKey("Department")]
        [Required(ErrorMessage = "Department is required")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        [ForeignKey("Semester")]
        [Required(ErrorMessage = "Semester is required")]
        public int SemesterId { get; set; }
        public Semester Semester { get; set; }
        public string ActionBy { get; set; }
        public string ActionDone { get; set; }
        public string ActionTime { get; set; }
        public List<CourseAssigned> CourseAssigned { get; set; }
        public List<ClassroomAllocate> ClassroomAllocates { get; set; }
        public List<EnrollCourses> EnrollCourseses { get; set; }
        public List<StudentResult> StudentResults { get; set; }
        public List<ExamRoutine> ExamRoutine { get; set; }
        public List<AssignmentOrReport> AssignmentOrReports { get; set; }
        public List<Supplimentary> Supplimentaries { get; set; }
    }
}