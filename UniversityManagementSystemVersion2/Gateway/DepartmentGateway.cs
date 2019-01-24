using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementSystemVersion2.Models;

namespace UniversityManagementSystemVersion2.Gateway
{
    public class DepartmentGateway:CommonGateway
    {
        // save department in database
        public int SaveDepartment(Department department)
        {
            Context.Departments.Add(department);
            int rowsAffected = Context.SaveChanges();

            return rowsAffected;
        }

        // get all departments
        public List<Department> GetAllDepartments()
        {
            return Context.Departments.ToList();
        }
 
        // check department code and name is exists
        public bool IsExists(Department department)
        {
            bool hasRows = Context.Departments.Any(d => d.Code == department.Code || d.Name == department.Name);
            return hasRows;
        }

        // get department  by id
        public Department GetDepartmentById(int departmentId)
        {
            Department department = Context.Departments.Where(d => d.Id == departmentId).FirstOrDefault();
            return department;
        }
    }
}