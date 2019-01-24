using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementSystemVersion2.Models;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using UniversityManagementSystemVersion2.Models.ViewModels;
using UniversityManagementSystemVersion2.Utility;

namespace UniversityManagementSystemVersion2.Gateway
{
    public class TeacherGateway:CommonGateway
    {
        // save teacher
        public int SaveTeacher(Teacher teacher)
        {
            Context.Teachers.Add(teacher);
            int rowsAffected = Context.SaveChanges();
            return rowsAffected;
        }

        // email exists
        public bool IsEmailExists(Teacher teacher)
        {
            return Context.Teachers.Any(t => t.Email == teacher.Email);
        }

        // email exists update
        public bool IsEmailExistsUpdate(Teacher teacher)
        {
            return Context.Teachers.Any(t => t.Email == teacher.Email && t.Id != teacher.Id);
        }

        //number of teachers for generate RegNo
        public int NoOfTeacherEntry()
        {
            return Context.Teachers.ToList().Count;
        }

        // get timely entry
        public TeacherViewModel GetEntry(string time)
        {
            var result =
                Context.Teachers.Include(t => t.Department).Include(t => t.Designation).Where(t => t.ActionTime == time).FirstOrDefault();

            TeacherViewModel teacherViewModel = new TeacherViewModel();

            teacherViewModel.RegNo = result.RegNo;
            teacherViewModel.Name = result.Name;
            teacherViewModel.Address = result.Address;
            teacherViewModel.Email = result.Email;
            teacherViewModel.ContactNo = result.ContactNo;
            teacherViewModel.DesignationName = result.Designation.DesignationName;
            teacherViewModel.DepartmentName = result.Department.Name;
            teacherViewModel.CreditToBeTaken = result.CreditToBeTaken;

            return teacherViewModel;
        }

        //get all teacher
        public List<Teacher> GetAllTeachers()
        {
            List<Teacher> teachers = Context.Teachers.ToList();
            return teachers;
        }

 
        // get all teachers by TeacherViewModels
        public List<TeacherViewModel> GetAllTeacherInfo()
        {
            var query = Context.Teachers.Include(td => td.Department).Include(ts => ts.Designation).OrderByDescending(t=>t.Id).ToList();
            List<TeacherViewModel> teacherViewModels = new List<TeacherViewModel>();

            foreach (var result in query)
            {
                TeacherViewModel teacherViewModel = new TeacherViewModel();

                teacherViewModel.Id = result.Id;
                teacherViewModel.RegNo = result.RegNo;
                teacherViewModel.Name = result.Name;
                teacherViewModel.Email = result.Email;
                teacherViewModel.DepartmentName = result.Department.Name;
                string profilePictureUrl = result.ProfilePicture;
                teacherViewModel.ProfilePicture = profilePictureUrl.Substring(1);

                teacherViewModels.Add(teacherViewModel);
            }

            return teacherViewModels;
        }

        //get teacher info by id
        public TeacherViewModel GetTeacherInfoById(int teacherId)
        {
            var result =
                Context.Teachers.Include(t => t.Department).Include(t => t.Designation).Where(t => t.Id == teacherId).FirstOrDefault();

            TeacherViewModel teacherViewModel = new TeacherViewModel();

            teacherViewModel.RegNo = result.RegNo;
            teacherViewModel.Name = result.Name;
            teacherViewModel.Address = result.Address;
            teacherViewModel.Email = result.Email;
            teacherViewModel.ContactNo = result.ContactNo;
            teacherViewModel.DesignationId = result.DesignationId;
            teacherViewModel.DesignationName = result.Designation.DesignationName;
            teacherViewModel.DepartmentId = result.DepartmentId;
            teacherViewModel.DepartmentName = result.Department.Name;
            teacherViewModel.CreditToBeTaken = result.CreditToBeTaken;
            string profilePictureUrl = result.ProfilePicture;

            teacherViewModel.ProfilePicture = profilePictureUrl.Substring(1);
            teacherViewModel.ActionBy = result.ActionBy;
            teacherViewModel.ActionDone = result.ActionDone;
            teacherViewModel.ActionTime = result.ActionTime;

            return teacherViewModel;
        }

        // get teacher by department
        public List<Teacher> GetTeacherByDepartment(int departmentId)
        {
            var result = Context.Teachers.Where(t => t.DepartmentId == departmentId);
            List<Teacher> teachers = new List<Teacher>();

            foreach (Teacher teacher in result)
            {
                teachers.Add(teacher);
            }

            return teachers;
        }
 
        // list of taken courses
        public List<int> GetTeacherTakenCourses(int teacherId)
        {
            var result = Context.CourseAssigneds.Where(c => c.TeacherId == teacherId && c.State == "ACTIVE");
            
            List<int> courseId = new List<int>();

            foreach (var teacherTakenCourse in result)
            {
                int takenCourseId = teacherTakenCourse.CourseId;
                courseId.Add(takenCourseId);
            }

            return courseId;
        }

        // course assign to teacher
        public int CourseAssign(CourseAssigned courseAssigned)
        {
            Context.CourseAssigneds.Add(courseAssigned);
            int rowsAffected = Context.SaveChanges();
            return rowsAffected;
        }

        // course is already assigned
        public bool IsAlreadyAssigned(CourseAssigned courseAssigned)
        {
            bool isExists = Context.CourseAssigneds.Any(ca => ca.CourseId == courseAssigned.CourseId && ca.State == "ACTIVE");
            return isExists;
        }

        // check teacher id is exists
        public bool IsTeacherIdExists(int id)
        {
            bool isExists = Context.Teachers.Any(t => t.Id == id);
            return isExists;
        }

        // get teacher id by teacher reg no
        public int GetTeacherIdByRegNo(string regNo)
        {
            Teacher teacher = Context.Teachers.Where(t => t.RegNo == regNo).FirstOrDefault();
            return teacher.Id;
        }

        // get courses taken by teacher id
        public List<Course> GetCourseByTeacherId(int id)
        {
            var query = Context.CourseAssigneds.Include(c => c.Course).Where(c => c.TeacherId == id && c.State == "ACTIVE").Select(s => new
            {
                CourseId = s.Course.Id,
                CourseCode = s.Course.Code,
                CourseName = s.Course.Name
            });

            List<Course> courses = new List<Course>();

            foreach (var result in query)
            {
                Course course = new Course();

                course.Id = result.CourseId;
                course.Code = result.CourseCode;
                course.Name = result.CourseName;

                courses.Add(course);
            }

            return courses;
        }
 
        // Update teacher
        public int UpdateTeacher(Teacher teacher)
        {
            Context.Teachers.AddOrUpdate(teacher);
            int rowsAffected = Context.SaveChanges();
            return rowsAffected;
        }

        // get teacher by id
        public Teacher GetTeacherById(int teacherId)
        {
            Teacher teacher = Context.Teachers.Where(t => t.Id == teacherId).FirstOrDefault();
            return teacher;
        }

        // get all assigned courses
        public List<CourseAssigned> GetAllAssignedCourses()
        {
            List<CourseAssigned> courseAssigneds = Context.CourseAssigneds.Where(c => c.State == "ACTIVE").ToList();
            return courseAssigneds;
        } 

        // unassign all courses
        public int UnassignAllCourses(CourseAssigned courseAssigned)
        {
            courseAssigned.State = "DEACTIVE";
            courseAssigned.ActionDone = ActionUtility.ActionDelete;
            courseAssigned.ActionTime = DateTime.Now.ToString();

            Context.CourseAssigneds.AddOrUpdate(courseAssigned);
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

        // check teacher exists by reg no
        public bool IsTeacherExistsForAuth(string regNo)
        {
            bool isExists = Context.Teachers.Any(t => t.RegNo == regNo);
            return isExists;
        }
    }
}