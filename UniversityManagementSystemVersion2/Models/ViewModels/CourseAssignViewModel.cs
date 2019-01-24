using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityManagementSystemVersion2.Models.ViewModels
{
    public class CourseAssignViewModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Semester { get; set; }
        public string AssignTo { get; set; }
    }
}