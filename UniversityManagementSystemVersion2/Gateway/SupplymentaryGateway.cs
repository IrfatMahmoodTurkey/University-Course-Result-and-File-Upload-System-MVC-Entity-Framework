using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using UniversityManagementSystemVersion2.Models;
using UniversityManagementSystemVersion2.Utility;

namespace UniversityManagementSystemVersion2.Gateway
{
    public class SupplymentaryGateway:CommonGateway
    {
        // upload suplymentary
        public int UploadSupplymentary(Supplimentary supplimentary)
        {
            Context.Supplimentaries.Add(supplimentary);
            int rowsAffected = Context.SaveChanges();

            return rowsAffected;
        }

        // get files by session and courseId
        public List<Supplimentary> GetFiles(string session, int courseId)
        {
            List<Supplimentary> supplimentaries =
                Context.Supplimentaries.Where(s => s.CourseId == courseId && s.Session == session && s.State == "ACTIVE")
                    .ToList();
            return supplimentaries;
        }

        // get all files
        public List<Supplimentary> GetAllFiles()
        {
            List<Supplimentary> supplimentaries = Context.Supplimentaries.Where(s => s.State == "ACTIVE").ToList();
            return supplimentaries;
        }

        // get all files by course id
        public List<Supplimentary> GetAllFilesByCourseId(int courseId)
        {
            List<Supplimentary> supplimentaries = Context.Supplimentaries.Where(s => s.State == "ACTIVE" && s.CourseId == courseId).ToList();
            return supplimentaries;
        }

        // deactivate exam routine
        public int DeactiveFiles(Supplimentary supplimentary)
        {
            supplimentary.State = "DEACTIVE";
            supplimentary.ActionDone = ActionUtility.ActionDelete;
            supplimentary.ActionTime = DateTime.Now.ToString();

            Context.Supplimentaries.AddOrUpdate(supplimentary);
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
    }
}