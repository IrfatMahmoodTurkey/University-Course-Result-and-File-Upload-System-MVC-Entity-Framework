using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementSystemVersion2.Gateway;

namespace UniversityManagementSystemVersion2.Manager
{
    public class AdminHomeManager
    {
        private AdminHomeGateway adminHomeGateway;

        public AdminHomeManager()
        {
            adminHomeGateway = new AdminHomeGateway();
        }

        // no of students
        public int NoOfStudents()
        {
            return adminHomeGateway.NoOfStudents();
        }

        // no of teachers
        public int NoOfTeachers()
        {
            return adminHomeGateway.NoOfTeachers();
        }

        // no of courses
        public int NoOfCourses()
        {
            return adminHomeGateway.NoOfCourses();
        }

        // no of departments
        public int NoOfDepartments()
        {
            return adminHomeGateway.NoOfDepartments();
        }

        // reg students by yearly
        public int StudentsByYear(string year)
        {
            return adminHomeGateway.StudentsByYear(year);
        }

        // get reg students by departmentwise year
        public int StudentNoByDepartmentAndPrevYear(int departmentId, string year)
        {
            return adminHomeGateway.StudentNoByDepartmentAndPrevYear(departmentId, year);
        }

        // get reg students by departmentwise year
        public int StudentNoByDepartmentAndRecYear(int departmentId, string year)
        {
            return adminHomeGateway.StudentNoByDepartmentAndRecYear(departmentId, year);
        }

        // get enroll state
        public string GetEnrollsState()
        {
            return adminHomeGateway.GetEnrollsState();
        }

         // get coursewise result
        public int CourseWiseResult(int courseId, decimal point)
        {
            return adminHomeGateway.CourseWiseResult(courseId, point);
        }
    }
}