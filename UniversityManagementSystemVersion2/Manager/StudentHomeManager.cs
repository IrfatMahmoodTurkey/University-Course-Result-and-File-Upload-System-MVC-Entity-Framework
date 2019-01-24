using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementSystemVersion2.Gateway;
using UniversityManagementSystemVersion2.Models;
using UniversityManagementSystemVersion2.Models.ViewModels;

namespace UniversityManagementSystemVersion2.Manager
{
    public class StudentHomeManager
    {
        private StudentHomeGateway studentHomeGateway;

        public StudentHomeManager()
        {
            studentHomeGateway = new StudentHomeGateway();
        }

        // get no of enrolled courses
        public int GetTotalEnrolledCourses(int studentId)
        {
            return studentHomeGateway.GetTotalEnrolledCourses(studentId);
        }

        // get no of regular courses
        public int NoOfRegularCourses(int studentId)
        {
            return studentHomeGateway.NoOfRegularCourses(studentId);
        }

        // get no of regular courses
        public int NoOfRecourseCourses(int studentId)
        {
            return studentHomeGateway.NoOfRecourseCourses(studentId);
        }

        // get average gpa point
        public decimal GetAverageGpaPoint(int studentId)
        {
            if (studentHomeGateway.IsResultExists(studentId))
            {
                return studentHomeGateway.GetAverageGpaPoint(studentId);
            }
            else
            {
                return 0.00m;
            }
            
        }

        // get no of 4.00 subject
        public int FourPointSubjects(int studentId)
        {
            return studentHomeGateway.FourPointSubjects(studentId);
        }

        // get no of 3.50 subject
        public int ThreeFivePointSubjects(int studentId)
        {
            return studentHomeGateway.ThreeFivePointSubjects(studentId);
        }

        // get no of 3.00 subject
        public int ThreePointSubjects(int studentId)
        {
            return studentHomeGateway.ThreePointSubjects(studentId);
        }

        // get no of 2.00 subject
        public int TwoPointSubjects(int studentId)
        {
            return studentHomeGateway.TwoPointSubjects(studentId);
        }

        // get no of 1.00 subject
        public int OnePointSubjects(int studentId)
        {
            return studentHomeGateway.OnePointSubjects(studentId);
        }

        // get no of 0.00 subject
        public int ZeroPointSubjects(int studentId)
        {
            return studentHomeGateway.ZeroPointSubjects(studentId);
        }

        // get passed subjects
        public int NoOfPassedSubjects(int studentId)
        {
            return studentHomeGateway.NoOfPassedSubjects(studentId);
        }

        // get failed subjects
        public int NoOfFailedSubjects(int studentId)
        {
            return studentHomeGateway.NoOfFailedSubjects(studentId);
        }

        // get result of A+ A A-
        public List<ResultSheetViewModel> StudentResult(int studentId)
        {
            return studentHomeGateway.StudentResult(studentId);
        }
    }
}