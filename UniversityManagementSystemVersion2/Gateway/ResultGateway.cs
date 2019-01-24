using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using UniversityManagementSystemVersion2.Models;
using System.Data.Entity;
using UniversityManagementSystemVersion2.Models.ViewModels;

namespace UniversityManagementSystemVersion2.Gateway
{
    public class ResultGateway:CommonGateway
    {
        // save on result
        public int SaveResult(StudentResult result)
        {
            Context.StudentResults.Add(result);
            return Context.SaveChanges();
        }

        // update result
        public int UpdateResult(StudentResult result)
        {
            Context.StudentResults.AddOrUpdate(result);
            return Context.SaveChanges();
        }

        // get result by studentId, courseId, session, state
        public StudentResult GetResultByStudentIdCourseId(StudentResult result)
        {
            StudentResult studentResult =
                Context.StudentResults.Where(
                    r =>
                        r.StudentId == result.StudentId && r.CourseId == result.CourseId && r.Session == result.Session &&
                        r.State == "ACTIVE").FirstOrDefault();

            return studentResult;
        }

        // is result entry prevoiusly exists
        public bool IsResultEntryExists(StudentResult result)
        {
            bool isExists =
                Context.StudentResults.Any(
                    r =>
                        r.StudentId == result.StudentId && r.CourseId == result.CourseId && r.Session == result.Session &&
                        r.State == "ACTIVE");
            return isExists;
        }

        // get published result by student id
        public List<ResultSheetViewModel> GetResultByStudentId(int studentId)
        {
            var query = Context.StudentResults.Include(r => r.Course).Where(r=>r.PublishStatus == "Published" && r.StudentId == studentId && r.State == "ACTIVE").Select(s => new
            {
                Id = s.Id,
                Code = s.Course.Code,
                Title = s.Course.Name,
                Att = s.Attendance,
                Ass = s.Assignement,
                Ct = s.ClassTest,
                Mid = s.MidTerm,
                Final = s.FinalExam,
                Point = s.Point,
                Session = s.Session
            });

            List<ResultSheetViewModel> resultSheetViewModels = new List<ResultSheetViewModel>();

            foreach (var result in query)
            {
                ResultSheetViewModel model = new ResultSheetViewModel();

                model.Id = result.Id;
                model.Code = result.Code;
                model.Title = result.Title;
                model.Assignment = result.Ass;
                model.Attendance = result.Att;
                model.ClassTest = result.Ct;
                model.MidTerm = result.Mid;
                model.FinalExam = result.Final;
                model.Point = result.Point;
                model.Session = result.Session;

                resultSheetViewModels.Add(model);
            }

            return resultSheetViewModels;
        }


        // get details result by student id
        public ResultSheetViewModel GetDetailsResult(int id)
        {
            var query = Context.StudentResults.Include(r => r.Course).Where(r=>r.Id == id && r.PublishStatus == "Published" && r.State == "ACTIVE").Select(s => new
            {
                Id = s.Id,
                Code = s.Course.Code,
                Title = s.Course.Name,
                Att = s.Attendance,
                Ass = s.Assignement,
                Ct = s.ClassTest,
                Mid = s.MidTerm,
                Final = s.FinalExam,
                Point = s.Point,
                Session = s.Session
            }).First();

            ResultSheetViewModel model = new ResultSheetViewModel();

            model.Id = query.Id;
            model.Code = query.Code;
            model.Title = query.Title;
            model.Assignment = query.Ass;
            model.Attendance = query.Att;
            model.ClassTest = query.Ct;
            model.MidTerm = query.Mid;
            model.FinalExam = query.Final;
            model.Point = query.Point;
            model.Session = query.Session;


            return model;
        }
 
        // check result is exists
        public bool IsResultExists(int id)
        {
            bool isExists =
                Context.StudentResults.Any(r => r.Id == id && r.PublishStatus == "Published" && r.State == "ACTIVE");
            return isExists;
        }

        // get all not published result for view
        public List<ResultSheetViewModel> GetAllUnPublishedResultForView()
        {
            var query = Context.StudentResults.Include(r => r.Course).Where(r => r.PublishStatus == "NotPublished" && r.State == "ACTIVE").Select(s => new
            {
                Id = s.Id,
                Code = s.Course.Code,
                RegNo = s.Student.RegNo,
                Title = s.Course.Name,
                Att = s.Attendance,
                Ass = s.Assignement,
                Ct = s.ClassTest,
                Mid = s.MidTerm,
                Final = s.FinalExam,
                Point = s.Point,
                Session = s.Session
            });

            List<ResultSheetViewModel> resultSheetViewModels = new List<ResultSheetViewModel>();

            foreach (var result in query)
            {
                ResultSheetViewModel model = new ResultSheetViewModel();

                model.Id = result.Id;
                model.Code = result.Code;
                model.StudentRegNo = result.RegNo;
                model.Title = result.Title;
                model.Assignment = result.Ass;
                model.Attendance = result.Att;
                model.ClassTest = result.Ct;
                model.MidTerm = result.Mid;
                model.FinalExam = result.Final;
                model.Point = result.Point;
                model.Session = result.Session;

                resultSheetViewModels.Add(model);
            }

            return resultSheetViewModels;
        }

        // get unpublished
        public List<StudentResult> GetAllUnpublishedResult()
        {
            return Context.StudentResults.Where(s => s.PublishStatus == "NotPublished" && s.State == "ACTIVE").ToList();
        }
 
        // publish unpublish result
        public int PublishUnPublishResult(StudentResult result)
        {
            result.PublishStatus = "Published";
            Context.StudentResults.AddOrUpdate(result);
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

        // get all published result
        public List<ResultSheetViewModel> GetAllPublishedResultForAdmin()
        {
            var query = Context.StudentResults.Include(r => r.Course).Where(r => r.PublishStatus == "Published" && r.State == "ACTIVE").Select(s => new
            {
                Id = s.Id,
                Code = s.Course.Code,
                RegNo = s.Student.RegNo,
                Title = s.Course.Name,
                Att = s.Attendance,
                Ass = s.Assignement,
                Ct = s.ClassTest,
                Mid = s.MidTerm,
                Final = s.FinalExam,
                Point = s.Point,
                Session = s.Session
            });

            List<ResultSheetViewModel> resultSheetViewModels = new List<ResultSheetViewModel>();

            foreach (var result in query)
            {
                ResultSheetViewModel model = new ResultSheetViewModel();

                model.Id = result.Id;
                model.Code = result.Code;
                model.Title = result.Title;
                model.StudentRegNo = result.RegNo;
                model.Assignment = result.Ass;
                model.Attendance = result.Att;
                model.ClassTest = result.Ct;
                model.MidTerm = result.Mid;
                model.FinalExam = result.Final;
                model.Point = result.Point;
                model.Session = result.Session;

                resultSheetViewModels.Add(model);
            }

            return resultSheetViewModels;
        } 
    }
}