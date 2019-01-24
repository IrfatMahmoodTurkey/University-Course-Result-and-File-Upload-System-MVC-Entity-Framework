using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityManagementSystemVersion2.Models.ViewModels
{
    public class AssignmentAndReportViewModel
    {
        public int Id { get; set; }
        public string StudentRegNo { get; set; }
        public string ReportName { get; set; }
        public string Date { get; set; }
        public string UploadDate { get; set; }
        public string Url { get; set; }
        public string Type { get; set; }
        public string UploadedBy { get; set; }
    }
}