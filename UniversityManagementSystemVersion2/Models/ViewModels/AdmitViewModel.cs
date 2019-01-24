using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityManagementSystemVersion2.Models.ViewModels
{
    public class AdmitViewModel
    {
        [Required(ErrorMessage = "Session Type is Required")]
        public string ActionTypeTime { get; set; }
        [Required(ErrorMessage = "Student is Required")]
        public int StudentId { get; set; }
    }
}