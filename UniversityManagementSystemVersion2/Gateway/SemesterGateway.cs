using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementSystemVersion2.Models;

namespace UniversityManagementSystemVersion2.Gateway
{
    public class SemesterGateway:CommonGateway
    {
        // save semester
        public int SaveSemester(Semester semester)
        {
            Context.Semesters.Add(semester);
            int rowsAffected = Context.SaveChanges();
            return rowsAffected;
        }

        // get all semesters
        public List<Semester> GetAllSemesters()
        {
            return Context.Semesters.ToList();
        } 

        // is exists
        public bool IsExists(Semester semester)
        {
            return Context.Semesters.Any(s => s.SemesterName == semester.SemesterName);
        }
    }
}