using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityManagementSystemVersion2.Models
{
    public class Notice
    {
        public int Id { get; set; }
        public string RegNo { get; set; }
        [Required(ErrorMessage = "Notice Description is required")]
        public string NoticeDescription { get; set; }
        public string DateTime { get; set; }
        public string ActionBy { get; set; }
        public string ActionDone { get; set; }
        public string ActionTime { get; set; }
    }
}