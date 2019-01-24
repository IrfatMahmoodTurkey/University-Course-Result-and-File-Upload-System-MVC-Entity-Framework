using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityManagementSystemVersion2.Models.ViewModels
{
    public class StudentViewModel
    {
        public int Id { get; set; }
        public string RegNo { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ContactNo { get; set; }
        public string Date { get; set; }
        public string Address { get; set; }
        public string DepartmentName { get; set; }
        public string ProfilePicture { get; set; }
        public string ActionBy { get; set; }
        public string ActionDone { get; set; }
        public string ActionTime { get; set; }

    }
}