using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystemVersion2.Gateway;
using UniversityManagementSystemVersion2.Models;

namespace UniversityManagementSystemVersion2.Manager
{
    public class SemesterManager
    {
        private SemesterGateway semesterGateway;

        public SemesterManager()
        {
            semesterGateway = new SemesterGateway();
        }

        // save semester
        public int SaveSemester(Semester semester)
        {
            if (semesterGateway.IsExists(semester))
            {
                return 2;
            }
            else
            {
                int rowsAffected = semesterGateway.SaveSemester(semester);

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

        // get all semesters
        public List<Semester> GetAllSemesters()
        {
            return semesterGateway.GetAllSemesters();
        } 

        // get semesters for dropdown
        public List<SelectListItem> GetAllSemestersForDropDown()
        {
            List<Semester> semesters = GetAllSemesters();

            List<SelectListItem> selectListItems =
                semesters.ConvertAll(s => new SelectListItem() {Text = s.SemesterName, Value = s.Id.ToString()});

            List<SelectListItem> mainSelectListItems = new List<SelectListItem>();
            mainSelectListItems.Add(new SelectListItem(){Text = "-- Select Semester --", Value = ""});
            mainSelectListItems.AddRange(selectListItems);

            return mainSelectListItems;
        } 
    }
}