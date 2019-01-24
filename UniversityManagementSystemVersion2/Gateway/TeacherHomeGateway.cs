using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementSystemVersion2.Models;
using System.Data.Entity;

namespace UniversityManagementSystemVersion2.Gateway
{
    public class TeacherHomeGateway:CommonGateway
    {
        // get no of courses taken by teacher
        public int GetNoOfCoursesTaken(int id)
        {
            int total = Context.CourseAssigneds.Where(c => c.TeacherId == id && c.State == "ACTIVE").Count();
            return total;
        }

        //get credit to be taken
        public decimal GetCreditToBeTaken(int id)
        {
            Teacher teacher = Context.Teachers.Where(t => t.Id == id).FirstOrDefault();
            return teacher.CreditToBeTaken;
        }

        // get taken total course credit
        public decimal GetTotalTakenCoursesCredit(int id)
        {
            decimal total =
                Context.CourseAssigneds.Include(c => c.Course)
                    .Where(ca => ca.TeacherId == id && ca.State == "ACTIVE")
                    .Sum(cas => cas.Course.Credit);

            return total;
        }

        public bool IsTakenCoursesExists(int id)
        {
            return Context.CourseAssigneds.Any(ca => ca.TeacherId == id && ca.State == "ACTIVE");
        }

        // get A+ by course id
        public int FourPoint(int courseId)
        {
            int no =
                Context.StudentResults.Where(
                    s => s.CourseId == courseId && s.State == "ACTIVE" && s.PublishStatus == "Published" && s.Point == 4.00m).Count();
            return no;
        }

        // get A by course id
        public int ThreeFivePoint(int courseId)
        {
            int no =
                Context.StudentResults.Where(
                    s => s.CourseId == courseId && s.State == "ACTIVE" && s.PublishStatus == "Published" && s.Point == 3.50m).Count();
            return no;
        }

        // get A- by course id
        public int ThreePoint(int courseId)
        {
            int no =
                Context.StudentResults.Where(
                    s => s.CourseId == courseId && s.State == "ACTIVE" && s.PublishStatus == "Published" && s.Point == 3.00m).Count();
            return no;
        }

        // get B by course id
        public int TwoPoint(int courseId)
        {
            int no =
                Context.StudentResults.Where(
                    s => s.CourseId == courseId && s.State == "ACTIVE" && s.PublishStatus == "Published" && s.Point == 2.00m).Count();
            return no;
        }

        // get C by course id
        public int OnePoint(int courseId)
        {
            int no =
                Context.StudentResults.Where(
                    s => s.CourseId == courseId && s.State == "ACTIVE" && s.PublishStatus == "Published" && s.Point == 1.00m).Count();
            return no;
        }

        // get F by course id
        public int ZeroPoint(int courseId)
        {
            int no =
                Context.StudentResults.Where(
                    s => s.CourseId == courseId && s.State == "ACTIVE" && s.PublishStatus == "Published" && s.Point == 0.00m).Count();
            return no;
        }

        // get pass by course id
        public int GetPassed(int courseId)
        {
            int no =
                Context.StudentResults.Where(
                    s => s.CourseId == courseId && s.State == "ACTIVE" && s.PublishStatus == "Published" && s.Point != 0.00m).Count();
            return no;
        }

        // get failed
        public int GetFailed(int courseId)
        {
            int no =
                Context.StudentResults.Where(
                    s => s.CourseId == courseId && s.State == "ACTIVE" && s.PublishStatus == "Published" && s.Point == 0.00m).Count();
            return no;
        }

        // get list of taken courses
        public List<Course> GetTakenCourses(int id)
        {
            var query =
                Context.CourseAssigneds.Include(ca => ca.Course).Where(c => c.TeacherId == id && c.State == "ACTIVE");

            List<Course> courses = new List<Course>();

            foreach (var result in query)
            {
                Course course = new Course();

                course.Code = result.Course.Code;
                course.Name = result.Course.Name;
                course.Credit = result.Course.Credit;

                courses.Add(course);
            }

            return courses;
        } 
    }
}