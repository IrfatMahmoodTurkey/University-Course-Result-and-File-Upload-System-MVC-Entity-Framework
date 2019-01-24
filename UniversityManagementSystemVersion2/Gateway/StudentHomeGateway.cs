using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementSystemVersion2.Models.ViewModels;
using System.Data.Entity;

namespace UniversityManagementSystemVersion2.Gateway
{
    public class StudentHomeGateway:CommonGateway
    {
        // get total enrolled courses
        public int GetTotalEnrolledCourses(int studentId)
        {
            int totalCourses =
                Context.EnrollCourseses.Where(e => e.StudentId == studentId && e.State == "ACTIVE").Count();
            return totalCourses;
        }

        // get no of regular courses
        public int NoOfRegularCourses(int studentId)
        {
            int totalCourses =
                Context.EnrollCourseses.Where(e => e.StudentId == studentId && e.Type == "Regular" && e.State == "ACTIVE").Count();
            return totalCourses;
        }

        // get no of regular courses
        public int NoOfRecourseCourses(int studentId)
        {
            int totalCourses =
                Context.EnrollCourseses.Where(e => e.StudentId == studentId && e.Type == "Recourse" && e.State == "ACTIVE").Count();
            return totalCourses;
        }

        // get average gpa point
        public decimal GetAverageGpaPoint(int studentId)
        {
            decimal total =
                Context.StudentResults.Where(
                    r => r.StudentId == studentId && r.State == "ACTIVE" && r.PublishStatus == "Published")
                    .Average(r => r.Point);

            return System.Math.Round(total,2);
        }

        // check result is exists or not
        public bool IsResultExists(int studentId)
        {
            bool isExists =
                Context.StudentResults.Any(
                    r => r.StudentId == studentId && r.State == "ACTIVE" && r.PublishStatus == "Published");
            return isExists;
        }

        // get no of 4.00 subject
        public int FourPointSubjects(int studentId)
        {
            int total =
                Context.StudentResults.Where(
                    r =>
                        r.StudentId == studentId && r.State == "ACTIVE" && r.PublishStatus == "Published" &&
                        r.Point == 4.00m).Count();
            return total;
        }

        // get no of 3.50 subject
        public int ThreeFivePointSubjects(int studentId)
        {
            int total =
                Context.StudentResults.Where(
                    r =>
                        r.StudentId == studentId && r.State == "ACTIVE" && r.PublishStatus == "Published" &&
                        r.Point == 3.50m).Count();
            return total;
        }

        // get no of 3.00 subject
        public int ThreePointSubjects(int studentId)
        {
            int total =
                Context.StudentResults.Where(
                    r =>
                        r.StudentId == studentId && r.State == "ACTIVE" && r.PublishStatus == "Published" &&
                        r.Point == 3.00m).Count();
            return total;
        }

        // get no of 2.00 subject
        public int TwoPointSubjects(int studentId)
        {
            int total =
                Context.StudentResults.Where(
                    r =>
                        r.StudentId == studentId && r.State == "ACTIVE" && r.PublishStatus == "Published" &&
                        r.Point == 2.00m).Count();
            return total;
        }

        // get no of 1.00 subject
        public int OnePointSubjects(int studentId)
        {
            int total =
                Context.StudentResults.Where(
                    r =>
                        r.StudentId == studentId && r.State == "ACTIVE" && r.PublishStatus == "Published" &&
                        r.Point == 1.00m).Count();
            return total;
        }

        // get no of 0.00 subject
        public int ZeroPointSubjects(int studentId)
        {
            int total =
                Context.StudentResults.Where(
                    r =>
                        r.StudentId == studentId && r.State == "ACTIVE" && r.PublishStatus == "Published" &&
                        r.Point == 0.00m).Count();
            return total;
        }

        // get passed subjects
        public int NoOfPassedSubjects(int studentId)
        {
            int total =
                Context.StudentResults.Where(
                    r =>
                        r.StudentId == studentId && r.State == "ACTIVE" && r.PublishStatus == "Published" &&
                        r.Point != 0.00m).Count();
            return total;
        }

        // get failed subjects
        public int NoOfFailedSubjects(int studentId)
        {
            int total =
                Context.StudentResults.Where(
                    r =>
                        r.StudentId == studentId && r.State == "ACTIVE" && r.PublishStatus == "Published" &&
                        r.Point == 0.00m).Count();
            return total;
        }

        // get result of A+ A A-
        public List<ResultSheetViewModel> StudentResult(int studentId)
        {
            var query =
                Context.StudentResults.Include(s => s.Course)
                    .Where(
                        s =>
                            s.StudentId == studentId && s.State == "ACTIVE" && s.PublishStatus == "Published" &&
                            (s.Point == 4.00m || s.Point == 3.50m || s.Point == 3.00m));

            List<ResultSheetViewModel> resultSheet = new List<ResultSheetViewModel>();

            foreach (var result in query)
            {
                ResultSheetViewModel model = new ResultSheetViewModel();

                model.Code = result.Course.Code;
                model.Title = result.Course.Name;
                model.Point = result.Point;

                resultSheet.Add(model);
            }

            return resultSheet;
        } 
    }
}