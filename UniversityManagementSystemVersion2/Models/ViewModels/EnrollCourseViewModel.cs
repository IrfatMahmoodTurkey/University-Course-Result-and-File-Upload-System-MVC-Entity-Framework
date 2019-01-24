using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityManagementSystemVersion2.Models.ViewModels
{
    public class EnrollCourseViewModel
    {
        public int Id { get; set; }
        public string StudentRegNo { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public decimal CourseCredit { get; set; }
        public string Session { get; set; }
        public string Type { get; set; }
    }
}