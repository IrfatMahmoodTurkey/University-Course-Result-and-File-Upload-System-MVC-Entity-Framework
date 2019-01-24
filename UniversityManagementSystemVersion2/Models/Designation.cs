using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityManagementSystemVersion2.Models
{
    public class Designation
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Designation is required")]
        [StringLength(50, ErrorMessage = "Designation must be 50 characters below")]
        public string DesignationName { get; set; }
        public string ActionBy { get; set; }
        public string ActionDone { get; set; }
        public string ActionTime { get; set; }
    }
}