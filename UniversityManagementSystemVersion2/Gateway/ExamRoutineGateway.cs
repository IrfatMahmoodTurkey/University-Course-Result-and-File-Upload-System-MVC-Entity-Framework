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
    public class ExamRoutineGateway:CommonGateway
    {
        // upload routine
        public int UploadRoutine(ExamRoutine routine)
        {
            Context.ExamRoutines.Add(routine);
            int rowsAffected = Context.SaveChanges();
            return rowsAffected;
        }

        // check course exists by session and course id
        public bool IsCourseExists(ExamRoutine routine)
        {
            bool isExists = Context.ExamRoutines.Any(e => e.CourseId == routine.CourseId && e.Session == routine.Session && e.State == "ACTIVE");
            return isExists;
        }

        // get department wise course time by department date and session
        public bool IsExistsCourseByDepartmentAndSession(ExamRoutine examRoutine)
        {
            bool isExists =
                Context.ExamRoutines.Any(
                    e =>
                        e.DepartmentId == examRoutine.DepartmentId && e.Session == examRoutine.Session &&
                        e.Date == examRoutine.Date && e.State == "ACTIVE");

            return isExists;
        }
 
        // get exam routine by department
        public List<ExamRoutineViewModel> GetExamRoutine(int departmentId, string session)
        {
            var query = Context.ExamRoutines.Include(e => e.Course).Where(e=>e.DepartmentId == departmentId && e.Session == session && e.State == "ACTIVE").OrderBy(e=>e.Date).Select(s => new
            {
                Code = s.Course.Code,
                Name = s.Course.Name,
                Date = s.Date,
                FromTime = s.FromTime,
                ToTime = s.ToTime
            });

            List<ExamRoutineViewModel> examRoutines = new List<ExamRoutineViewModel>();

            foreach (var result in query)
            {
                ExamRoutineViewModel model = new ExamRoutineViewModel();

                model.Code = result.Code;
                model.Name = result.Name;
                model.Date = result.Date;
                model.Schedule = result.FromTime + " - " + result.ToTime;

                examRoutines.Add(model);
            }

            return examRoutines;
        }

        // get exam routines
        public List<ExamRoutine> GetExamRoutine()
        {
            List<ExamRoutine> routines = Context.ExamRoutines.Where(e => e.State == "ACTIVE").ToList();
            return routines;
        }
 
        // deactivate exam routine
        public int DeactivateExamSchedule(ExamRoutine examRoutine)
        {
            examRoutine.State = "DEACTIVE";
            examRoutine.ActionDone = ActionUtility.ActionDelete;
            examRoutine.ActionTime = DateTime.Now.ToString();

            Context.ExamRoutines.AddOrUpdate(examRoutine);
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
    }
}