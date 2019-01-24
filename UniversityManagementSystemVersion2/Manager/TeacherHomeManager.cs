using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementSystemVersion2.Gateway;
using UniversityManagementSystemVersion2.Models;

namespace UniversityManagementSystemVersion2.Manager
{
    public class TeacherHomeManager
    {
        private TeacherHomeGateway teacherHomeGateway;

        public TeacherHomeManager()
        {
            teacherHomeGateway = new TeacherHomeGateway();
        }

        // get no of courses taken by teacher
        public int GetNoOfCoursesTaken(int id)
        {
            return teacherHomeGateway.GetNoOfCoursesTaken(id);
        }

        //get credit to be taken
        public decimal GetCreditToBeTaken(int id)
        {
            return teacherHomeGateway.GetCreditToBeTaken(id);
        }

        // get taken total course credit
        public decimal GetTotalTakenCoursesCredit(int id)
        {
            if (teacherHomeGateway.IsTakenCoursesExists(id))
            {
                return teacherHomeGateway.GetTotalTakenCoursesCredit(id);
            }
            else
            {
                return 0.0m;
            }
        }

        // get A+ by course id
        public int FourPoint(int courseId)
        {
            return teacherHomeGateway.FourPoint(courseId);
        }

        // get A by course id
        public int ThreeFivePoint(int courseId)
        {
            return teacherHomeGateway.ThreeFivePoint(courseId);
        }

        // get A- by course id
        public int ThreePoint(int courseId)
        {
            return teacherHomeGateway.ThreePoint(courseId);
        }

        // get B by course id
        public int TwoPoint(int courseId)
        {
            return teacherHomeGateway.TwoPoint(courseId);
        }

        // get C by course id
        public int OnePoint(int courseId)
        {
            return teacherHomeGateway.OnePoint(courseId);
        }

        // get F by course id
        public int ZeroPoint(int courseId)
        {
            return teacherHomeGateway.ZeroPoint(courseId);
        }

        // get pass by course id
        public int GetPassed(int courseId)
        {
            return teacherHomeGateway.GetPassed(courseId);
        }

        // get failed
        public int GetFailed(int courseId)
        {
            return teacherHomeGateway.GetFailed(courseId);
        }

        // get list of taken courses
        public List<Course> GetTakenCourses(int id)
        {
            return teacherHomeGateway.GetTakenCourses(id);
        }
    }
}