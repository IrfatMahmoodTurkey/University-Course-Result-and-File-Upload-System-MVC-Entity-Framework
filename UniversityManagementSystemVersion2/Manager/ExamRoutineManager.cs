using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementSystemVersion2.Gateway;
using UniversityManagementSystemVersion2.Models;
using UniversityManagementSystemVersion2.Models.ViewModels;

namespace UniversityManagementSystemVersion2.Manager
{
    public class ExamRoutineManager
    {
        private ExamRoutineGateway routineGateway;

        public ExamRoutineManager()
        {
            routineGateway = new ExamRoutineGateway();
        }

        // upload exam routine
        public int UploadRoutine(ExamRoutine examRoutine)
        {
            if (!routineGateway.IsCourseExists(examRoutine))
            {
                if (!routineGateway.IsExistsCourseByDepartmentAndSession(examRoutine))
                {
                    int rowsAffected = routineGateway.UploadRoutine(examRoutine);

                    if (rowsAffected > 0)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 3;
                }
                
            }
            else
            {
                return 2;
            }
        }

        // get exam routine by department
        public List<ExamRoutineViewModel> GetExamRoutine(int departmentId, string session)
        {
            return routineGateway.GetExamRoutine(departmentId, session);
        }

        // deactivate all exam routine
        public int DeactivateExamRoutine()
        {
            List<ExamRoutine> routines = routineGateway.GetExamRoutine();
            int total = routines.Count;
            int count = 0;

            foreach (ExamRoutine routine in routines)
            {
                int update = routineGateway.DeactivateExamSchedule(routine);

                count = count + update;
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
    }
}