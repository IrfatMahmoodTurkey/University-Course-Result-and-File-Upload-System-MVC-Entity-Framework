using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityManagementSystemVersion2.Models.ViewModels
{
    public class ResultSheetViewModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public int Attendance { get; set; }
        public decimal Assignment { get; set; }
        public decimal ClassTest { get; set; }
        public decimal MidTerm { get; set; }
        public decimal FinalExam { get; set; }
        public decimal Point { get; set; }
        public string Session { get; set; }
        public string StudentRegNo { get; set; }
    }
}