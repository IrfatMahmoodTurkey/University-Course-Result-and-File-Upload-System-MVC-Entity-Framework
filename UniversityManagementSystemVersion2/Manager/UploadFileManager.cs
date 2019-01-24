using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementSystemVersion2.Gateway;
using UniversityManagementSystemVersion2.Models;
using UniversityManagementSystemVersion2.Models.ViewModels;

namespace UniversityManagementSystemVersion2.Manager
{
    public class UploadFileManager
    {
        private UploadFileGateway fileGateway;

        public UploadFileManager()
        {
            fileGateway = new UploadFileGateway();
        }

        // upload file
        public int UploadFile(AssignmentOrReport assignment)
        {
            int rowsAffected = fileGateway.SaveFilePath(assignment);

            if (rowsAffected > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        // get files by course and session
        public List<AssignmentAndReportViewModel> GetAssignmentAndReportByCourseAndSession(int courseId, string session)
        {
            return fileGateway.GetAssignmentAndReportByCourseAndSession(courseId, session);
        }

        // deactivate all files
        public int DeactiveFiles()
        {
            List<AssignmentOrReport> assignmentOrReports = fileGateway.GetAllFiles();
            int total = assignmentOrReports.Count;
            int count = 0;

            foreach (AssignmentOrReport assignmentOrReport in assignmentOrReports)
            {
                int update = fileGateway.DeactiveFiles(assignmentOrReport);

                count = count + update;
            }

            if (count == total)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        // view all files
        public List<AssignmentAndReportViewModel> ViewAllFiles()
        {
            return fileGateway.ViewAllFiles();
        } 
    }
}