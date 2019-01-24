using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystemVersion2.Gateway;
using UniversityManagementSystemVersion2.Models;

namespace UniversityManagementSystemVersion2.Manager
{
    public class DepartmentManager
    {
        private DepartmentGateway departmentGateway;

        public DepartmentManager()
        {
            departmentGateway = new DepartmentGateway();
        }

        // save department
        public int SaveDepartment(Department department)
        {
            if (departmentGateway.IsExists(department))
            {
                return 2;
            }
            else
            {
                int rowsAffcted = departmentGateway.SaveDepartment(department);

                if (rowsAffcted > 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }

        // show all department
        public List<Department> GetAllDepartments()
        {
            return departmentGateway.GetAllDepartments();
        }

        // show first department
        public Department GetFirstDepartments()
        {
            return departmentGateway.GetAllDepartments().FirstOrDefault();
        } 

        // get all department for dropdown
        public List<SelectListItem> GetDepartmentForDropDown()
        {
            List<Department> allDepartments = GetAllDepartments();

            List<SelectListItem> selectListItems = allDepartments.ConvertAll(s=>new SelectListItem(){Text = s.Name, Value = s.Id.ToString()});
            
            List<SelectListItem> mainSelectListItems = new List<SelectListItem>();
            mainSelectListItems.Add(new SelectListItem(){Text = "-- Select Department --", Value = ""});
            mainSelectListItems.AddRange(selectListItems);

            return mainSelectListItems;
        }

        // get all department for dropdown admin home
        public List<SelectListItem> GetDepartmentForDropDownAdminHome()
        {
            List<Department> allDepartments = GetAllDepartments();

            List<SelectListItem> selectListItems = allDepartments.ConvertAll(s => new SelectListItem() { Text = s.Name, Value = s.Id.ToString() });

            return selectListItems;
        }

        // get department by id
        public Department GetDepartmentById(int departmentId)
        {
            return departmentGateway.GetDepartmentById(departmentId);
        }
    }
}