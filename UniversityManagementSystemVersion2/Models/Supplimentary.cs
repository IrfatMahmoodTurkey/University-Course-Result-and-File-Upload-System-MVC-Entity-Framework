using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UniversityManagementSystemVersion2.Models
{
    public class Supplimentary
    {
        public int Id { get; set; }
        [ForeignKey("Course")]
        [Required(ErrorMessage = "Course is required")]
        public int CourseId { get; set; }
        public Course Course { get; set; }
        [Required(ErrorMessage = "Session is required")]
        public string Session { get; set; }

        [Required(ErrorMessage = "Namr is required")]
        public string Name { get; set; }
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public string State { get; set; }
        public string ActionBy { get; set; }
        public string ActionDone { get; set; }
        public string ActionTime { get; set; }
    }
}