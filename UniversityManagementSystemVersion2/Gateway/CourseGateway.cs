using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Web.WebPages;
using UniversityManagementSystemVersion2.Models;
using UniversityManagementSystemVersion2.Models.ViewModels;

namespace UniversityManagementSystemVersion2.Gateway
{
    public class CourseGateway:CommonGateway
    {
        // save course
        public int SaveCourse(Course course)
        {
            Context.Courses.Add(course);
            int rowsAffected = Context.SaveChanges();
            return rowsAffected;
        }

        // is exists
        public bool IsExists(Course course)
        {
            return Context.Courses.Any(c => c.Code == course.Code || c.Name == course.Name);
        }

        // is exists update only
        public bool IsExistsUpdateOnly(Course course)
        {
            return Context.Courses.Where(c=>c.Id != course.Id).Any(c => c.Code == course.Code || c.Name == course.Name);
        }

        // get all courses
        public List<Course> GetAllCourses()
        {
            return Context.Courses.ToList();
        }

        // get course by id
        public Course GetCourseById(int id)
        {
            return Context.Courses.Where(c => c.Id == id).FirstOrDefault();
        }
 
        // get all course with semester and department
        public List<CourseViewModel> GetAllCoursesInfo()
        {
            var result = Context.Courses.Include(d => d.Department).Include(s => s.Semester);
            List<CourseViewModel> courseViewModels = new List<CourseViewModel>();

            foreach (Course course in result)
            {
                CourseViewModel courseViewModel = new CourseViewModel();

                courseViewModel.Id = course.Id;
                courseViewModel.Code = course.Code;
                courseViewModel.Name = course.Name;
                courseViewModel.Credit = course.Credit;
                courseViewModel.Description = course.Description;
                courseViewModel.DepartmentName = course.Department.Name;
                courseViewModel.SemesterName = course.Semester.SemesterName;
                courseViewModel.ActionBy = course.ActionBy;
                courseViewModel.ActionDone = course.ActionDone;
                courseViewModel.ActionTime = course.ActionTime;

                courseViewModels.Add(courseViewModel);
            }

            return courseViewModels;
        }

        // get all courses by department
        public List<CourseViewModel> GetAllCoursesInfoByDepartment(int departmentId)
        {
            var result = Context.Courses.Include(d => d.Department).Include(s => s.Semester).Where(c=>c.DepartmentId == departmentId);
            List<CourseViewModel> courseViewModels = new List<CourseViewModel>();

            foreach (Course course in result)
            {
                CourseViewModel courseViewModel = new CourseViewModel();

                courseViewModel.Id = course.Id;
                courseViewModel.Code = course.Code;
                courseViewModel.Name = course.Name;
                courseViewModel.Credit = course.Credit;
                courseViewModel.Description = course.Description;
                courseViewModel.DepartmentName = course.Department.Name;
                courseViewModel.SemesterName = course.Semester.SemesterName;
                courseViewModel.ActionBy = course.ActionBy;
                courseViewModel.ActionDone = course.ActionDone;
                courseViewModel.ActionTime = course.ActionTime;

                courseViewModels.Add(courseViewModel);
            }

            return courseViewModels;
        }

        // check by id course is exists
        public bool CheckByIdCourseIsExists(int id)
        {
            return Context.Courses.Any(i=>i.Id == id);
        }

        // get course by id viewModel
        public CourseViewModel GetCourseInfoById(int id)
        {
            var result = Context.Courses.Include(d => d.Department).Include(s => s.Semester).Where(c=>c.Id == id);
            CourseViewModel courseViewModel = new CourseViewModel();

            foreach (Course course in result)
            {
                courseViewModel.Id = course.Id;
                courseViewModel.Code = course.Code;
                courseViewModel.Name = course.Name;
                courseViewModel.Credit = course.Credit;
                courseViewModel.Description = course.Description;
                courseViewModel.DepartmentName = course.Department.Name;
                courseViewModel.SemesterName = course.Semester.SemesterName;
                courseViewModel.ActionBy = course.ActionBy;
                courseViewModel.ActionDone = course.ActionDone;
                courseViewModel.ActionTime = course.ActionTime;
            }

            return courseViewModel;
        }

        // update course
        public int UpdateCourse(Course course)
        {
            Context.Courses.AddOrUpdate(course);
            int rowsAffected = Context.SaveChanges();
            return rowsAffected;
        }

        // get all assigned courses
        public List<CourseAssignViewModel> GetAssignedCourses(int departmentId)
        {
            var query =
                Context.Courses.Include(c => c.Semester)
                    .Include(c => c.CourseAssigned)
                    .Where(c => c.DepartmentId == departmentId)
                    .Select(ca => new
                    {
                        Code = ca.Code,
                        Name = ca.Name,
                        Semester = ca.Semester.SemesterName,
                        CourseAssignedData = ca.CourseAssigned.Where(cas => cas.State == "ACTIVE").Select(s => new
                        {
                            TeacherName = s.Teacher.Name
                        })
                    });

            List<CourseAssignViewModel> coursesAssigneds = new List<CourseAssignViewModel>();

            foreach (var data in query)
            {
                CourseAssignViewModel courseAssignViewModel = new CourseAssignViewModel();

                courseAssignViewModel.Code = data.Code;
                courseAssignViewModel.Name = data.Name;
                courseAssignViewModel.Semester = data.Semester;
                courseAssignViewModel.AssignTo = "Not Assigned Yet";

                foreach (var item in data.CourseAssignedData)
                {
                    courseAssignViewModel.AssignTo = item.TeacherName;
                }
                
                coursesAssigneds.Add(courseAssignViewModel);
            }

            return coursesAssigneds;
        }

        // get courses by teacher id

    }
}