using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystemVersion2.Gateway;
using UniversityManagementSystemVersion2.Models;
using UniversityManagementSystemVersion2.Models.ViewModels;

namespace UniversityManagementSystemVersion2.Manager
{
    public class CourseManager
    {
        private CourseGateway courseGateway;

        public CourseManager()
        {
            courseGateway = new CourseGateway();
        }

        // save course
        public int SaveCourse(Course course)
        {
            if (courseGateway.IsExists(course))
            {
                return 2;
            }
            else
            {
                int rowsAffected = courseGateway.SaveCourse(course);

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

        // get all courses
        public List<Course> GetAllCourses()
        {
            return courseGateway.GetAllCourses();
        }

        // get first courses
        public Course GetFirstCourses()
        {
            return courseGateway.GetAllCourses().FirstOrDefault();
        } 

        // get all courses for dropdown
        public List<SelectListItem> GetAllCoursesForDropDown()
        {
            List<Course> courses = GetAllCourses();

            List<SelectListItem> selectListItems =
                courses.ConvertAll(c => new SelectListItem() {Text = c.Name, Value = c.Id.ToString()});

            List<SelectListItem> mainSelectListItems = new List<SelectListItem>();
            mainSelectListItems.Add(new SelectListItem(){Text = "-- Select Course --", Value = ""});

            mainSelectListItems.AddRange(selectListItems);

            return mainSelectListItems;
        }

        // get all courses for dropdown
        public List<SelectListItem> GetAllCoursesForDropDownForAdminHome()
        {
            List<Course> courses = GetAllCourses();

            List<SelectListItem> selectListItems =
                courses.ConvertAll(c => new SelectListItem() { Text = c.Name, Value = c.Id.ToString() });

            return selectListItems;
        }
 
        // get all course with semester and department
        public List<CourseViewModel> GetAllCoursesInfo()
        {
            return courseGateway.GetAllCoursesInfo();
        }

        // check by id course is exists
        public bool CheckByIdCourseIsExists(int id)
        {
            return courseGateway.CheckByIdCourseIsExists(id);
        }

        // get course by id
        public Course GetCourseById(int id)
        {
            return courseGateway.GetCourseById(id);
        }

        // get course by id viewModel
        public CourseViewModel GetCourseInfoById(int id)
        {
            return courseGateway.GetCourseInfoById(id);
        }

        // update course
        public int UpdateCourse(Course course)
        {
            if (courseGateway.IsExistsUpdateOnly(course))
            {
                return 2;
            }
            else
            {
                int rowsAffected = courseGateway.UpdateCourse(course);

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

        // get all courses by department
        public List<CourseViewModel> GetAllCoursesInfoByDepartment(int departmentId)
        {
            return courseGateway.GetAllCoursesInfoByDepartment(departmentId);
        }

        // get assigned course by department
        public List<CourseAssignViewModel> GetAssignedCourses(int departmentId)
        {
            return courseGateway.GetAssignedCourses(departmentId);
        }
 
    }
}