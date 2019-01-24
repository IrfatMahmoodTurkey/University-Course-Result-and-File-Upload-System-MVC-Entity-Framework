using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementSystemVersion2.Models;

namespace UniversityManagementSystemVersion2.Gateway
{
    public class AdminHomeGateway:CommonGateway
    {
        // no of students
        public int NoOfStudents()
        {
            int total = Context.Students.Count();
            return total;
        }

        // no of teachers
        public int NoOfTeachers()
        {
            int total = Context.Teachers.Count();
            return total;
        }

        // no of courses
        public int NoOfCourses()
        {
            int total = Context.Courses.Count();
            return total;
        }

        // no of departments
        public int NoOfDepartments()
        {
            int total = Context.Departments.Count();
            return total;
        }

        // reg students by yearly
        public int StudentsByYear(string year)
        {
            int total = Context.Students.Where(s => s.Date.EndsWith(year)).Count();
            return total;
        }

        // get reg students by departmentwise year
        public int StudentNoByDepartmentAndPrevYear(int departmentId, string year)
        {
            int total = Context.Students.Where(s => s.Date.EndsWith(year) && s.DepartmentId == departmentId).Count();
            return total;
        }

        // get reg students by departmentwise year
        public int StudentNoByDepartmentAndRecYear(int departmentId, string year)
        {
            int total = Context.Students.Where(s => s.Date.EndsWith(year) && s.DepartmentId == departmentId).Count();
            return total;
        }

        // get enroll state
        public string GetEnrollsState()
        {
            EnrollState enrollState= Context.EnrollStates.FirstOrDefault();

            if (enrollState == null)
            {
                return "No State Set Yet";
            }
            else
            {
                return enrollState.State;
            }
        }

        // get coursewise result
        public int CourseWiseResult(int courseId, decimal point)
        {
            int total =
                Context.StudentResults.Where(
                    r =>
                        r.CourseId == courseId && r.Point == point && r.State == "ACTIVE" &&
                        r.PublishStatus == "Published").Count();
            return total;
        }
    }
}