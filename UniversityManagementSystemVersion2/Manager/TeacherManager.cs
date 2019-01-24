using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystemVersion2.Controllers;
using UniversityManagementSystemVersion2.Gateway;
using UniversityManagementSystemVersion2.Models;
using UniversityManagementSystemVersion2.Models.ViewModels;

namespace UniversityManagementSystemVersion2.Manager
{
    public class TeacherManager
    {
        private TeacherGateway teacherGateway;
        private DepartmentGateway departmentGateway;
        private DesignationGateway designationGateway;
        private CourseGateway courseGateway;

        public TeacherManager()
        {
            teacherGateway = new TeacherGateway();
            departmentGateway = new DepartmentGateway();
            designationGateway = new DesignationGateway();
            courseGateway = new CourseGateway();
        }

        // save teacher
        public int SaveTeacher(Teacher teacher)
        {
            if (teacherGateway.IsEmailExists(teacher))
            {
                return 2;
            }
            else
            {
                RegisterViewModel model = new RegisterViewModel();
                model.UserName = teacher.RegNo;
                model.Password = "teacher12345";
                model.ConfirmPassword = "teacher12345";

                int rowsAffected = teacherGateway.SaveTeacher(teacher);
                

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

        // get teacher no entry
        public int NoOfTeacherEntry()
        {
            return teacherGateway.NoOfTeacherEntry();
        }

        // get all teacher
        public List<Teacher> GetAllTeachers()
        {
            return teacherGateway.GetAllTeachers();
        }
 
        // get all teachers by TeacherViewModels
        public List<TeacherViewModel> GetAllTeacherInfo()
        {
            return teacherGateway.GetAllTeacherInfo();
        }

        //get teacher for dropdown
        public List<SelectListItem> GetTeacherForDropDown()
        {
            List<Teacher> teachers = GetAllTeachers();

            List<SelectListItem> selectListItems = teachers.ConvertAll(t => new SelectListItem() {Text = t.Name, Value = t.Id.ToString()});

            List<SelectListItem> mainSelectListItems = new List<SelectListItem>();
            mainSelectListItems.Add(new SelectListItem(){Text = "-- Select Teacher --", Value = ""});
            mainSelectListItems.AddRange(selectListItems);

            return mainSelectListItems;
        } 

        // get timely entry
        public TeacherViewModel GetEntry(string time)
        {
            return teacherGateway.GetEntry(time);
        }

        // get teacher by department
        public List<Teacher> GetTeacherByDepartment(int departmentId)
        {
            return teacherGateway.GetTeacherByDepartment(departmentId);
        }

        // get total taken course
        public decimal GetTotalTakenCoursesCredit(int teacherId)
        {
            List<int> takenCourseId = teacherGateway.GetTeacherTakenCourses(teacherId);

            decimal totalTakenCourseCredit = 0.0m;

            foreach (int courseId in takenCourseId)
            {
                CourseViewModel courseViewModel = courseGateway.GetCourseInfoById(courseId);
                totalTakenCourseCredit = totalTakenCourseCredit + courseViewModel.Credit;
            }

            return totalTakenCourseCredit;
        }

        // get teacher info by id
        public TeacherViewModel GetTeacherInfoById(int teacherId)
        {
            return teacherGateway.GetTeacherInfoById(teacherId);
        }

        // course assign to teacher
        public int CourseAssign(CourseAssigned courseAssigned)
        {
            if (teacherGateway.IsAlreadyAssigned(courseAssigned))
            {
                return 2;
            }
            else
            {
                int rowsAffected = teacherGateway.CourseAssign(courseAssigned);

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
 
        // check teacher id is exists
        public bool IsTeacherIdExists(int id)
        {
            return teacherGateway.IsTeacherIdExists(id);
        }

        // get course by teacherId
        public List<Course> GetCourseByTeacherId(int id)
        {
            return teacherGateway.GetCourseByTeacherId(id);
        }


        // get courses taken by teacher for drop down dashboard
        public List<SelectListItem> GetCoursesTakenByTeacherForDropDownDashboard(int id)
        {
            List<Course> courses = teacherGateway.GetCourseByTeacherId(id);

            List<SelectListItem> selectListItems = courses.ConvertAll(c => new SelectListItem()
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });

            return selectListItems;
        }

        // get courses taken by teacher for drop down
        public List<SelectListItem> GetCoursesTakenByTeacherForDropDown(int id)
        {
            List<Course> courses = teacherGateway.GetCourseByTeacherId(id);

            List<SelectListItem> selectListItems = courses.ConvertAll(c => new SelectListItem()
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });

            List<SelectListItem> mainSelectListItems = new List<SelectListItem>();
            mainSelectListItems.Add(new SelectListItem()
            {
                Text = "-- Select Course --",
                Value = ""
            });

            mainSelectListItems.AddRange(selectListItems);

            return mainSelectListItems;
        }
 
        // get teacher id by teacher reg no
        public int GetTeacherIdByRegNo(string regNo)
        {
            return teacherGateway.GetTeacherIdByRegNo(regNo);
        }

        // get teacher by id
        public Teacher GetTeacherById(int teacherId)
        {
            return teacherGateway.GetTeacherById(teacherId);
        }

        // update teacher
        public int UpdateTeacher(Teacher teacher)
        {
            int rowsAffected = teacherGateway.UpdateTeacher(teacher);

            if (rowsAffected > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        // update teacher information Admin
        public int UpdateInformations(Teacher teacher)
        {
            if (teacherGateway.IsEmailExistsUpdate(teacher))
            {
                return 2;
            }
            else
            {
                int rowsAffected = teacherGateway.UpdateTeacher(teacher);

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

        // unassign all courses
        public int UnassigneAllCourses()
        {
            List<CourseAssigned> courseAssigneds = teacherGateway.GetAllAssignedCourses();
            int total = courseAssigneds.Count;

            int count = 0;

            foreach (CourseAssigned course in courseAssigneds)
            {
                int returnData = teacherGateway.UnassignAllCourses(course);

                count = count + returnData;
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

        // check teacher exists by reg no
        public bool IsTeacherExistsForAuth(string regNo)
        {
            return teacherGateway.IsTeacherExistsForAuth(regNo);
        }
    }
}