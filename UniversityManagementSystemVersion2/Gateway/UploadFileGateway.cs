using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementSystemVersion2.Models;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using UniversityManagementSystemVersion2.Models.ViewModels;
using UniversityManagementSystemVersion2.Utility;

namespace UniversityManagementSystemVersion2.Gateway
{
    public class UploadFileGateway:CommonGateway
    {
        // upload file
        public int SaveFilePath(AssignmentOrReport assignment)
        {
            Context.AssignmentOrReports.Add(assignment);
            int rowsAffected = Context.SaveChanges();
            return rowsAffected;
        }

        // get files by course and session
        public List<AssignmentAndReportViewModel> GetAssignmentAndReportByCourseAndSession(int courseId, string session)
        {
            var query =
                Context.AssignmentOrReports.Include(a => a.Student)
                    .Where(a => a.CourseId == courseId && a.Session == session && a.State == "ACTIVE")
                    .Select(sa => new
                    {
                        Id = sa.Id,
                        RegNo = sa.Student.RegNo,
                        AssignementNo = sa.Name,
                        Date = sa.Date,
                        UploadDate = sa.UploadDate,
                        Type = sa.FileType,
                        Url = sa.Url,
                        UploadedBy = sa.ActionBy
                    });

            List<AssignmentAndReportViewModel> assigments = new List<AssignmentAndReportViewModel>();

            foreach (var result in query)
            {
                AssignmentAndReportViewModel model = new AssignmentAndReportViewModel();

                model.Id = result.Id;
                model.StudentRegNo = result.RegNo;
                model.ReportName = result.AssignementNo;
                model.Type = result.Type;
                model.Date = result.Date;
                model.UploadDate = result.UploadDate;
                model.Url = result.Url;
                model.UploadedBy = result.UploadedBy;

                assigments.Add(model);
            }

            return assigments;
        }

        // get all files
        public List<AssignmentOrReport> GetAllFiles()
        {
            List<AssignmentOrReport> assignmentOrReports = Context.AssignmentOrReports.Where(a => a.State == "ACTIVE").ToList();
            return assignmentOrReports;
        }

        // deactivate exam routine
        public int DeactiveFiles(AssignmentOrReport assignmentOrReport)
        {
            assignmentOrReport.State = "DEACTIVE";
            assignmentOrReport.ActionDone = ActionUtility.ActionDelete;
            assignmentOrReport.ActionTime = DateTime.Now.ToString();

            Context.AssignmentOrReports.AddOrUpdate(assignmentOrReport);
            int rowsAffected = Context.SaveChanges();

            if (rowsAffected > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        // for view all uploaded files
        public List<AssignmentAndReportViewModel> ViewAllFiles()
        {
            var query =
                 Context.AssignmentOrReports.Include(a => a.Student)
                     .Where(a=>a.State == "ACTIVE")
                     .Select(sa => new
                     {
                         Id = sa.Id,
                         RegNo = sa.Student.RegNo,
                         AssignementNo = sa.Name,
                         Date = sa.Date,
                         UploadDate = sa.UploadDate,
                         Type = sa.FileType,
                         Url = sa.Url,
                         UploadedBy = sa.ActionBy
                     });

            List<AssignmentAndReportViewModel> assigments = new List<AssignmentAndReportViewModel>();

            foreach (var result in query)
            {
                AssignmentAndReportViewModel model = new AssignmentAndReportViewModel();

                model.Id = result.Id;
                model.StudentRegNo = result.RegNo;
                model.ReportName = result.AssignementNo;
                model.Type = result.Type;
                model.Date = result.Date;
                model.UploadDate = result.UploadDate;
                model.Url = result.Url;
                model.UploadedBy = result.UploadedBy;

                assigments.Add(model);
            }

            return assigments; 
        } 
 
    }
}