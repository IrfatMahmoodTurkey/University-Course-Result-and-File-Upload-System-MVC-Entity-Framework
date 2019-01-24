using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementSystemVersion2.Gateway;
using UniversityManagementSystemVersion2.Models;
using UniversityManagementSystemVersion2.Models.ViewModels;
using UniversityManagementSystemVersion2.Utility;

namespace UniversityManagementSystemVersion2.Manager
{
    public class ResultManager
    {
        private ResultGateway resultGateway;

        public ResultManager()
        {
            resultGateway = new ResultGateway();
        }

        // upload first fifty result
        public int UploadFirstFiftyNumber(StudentResult result)
        {
            if (resultGateway.IsResultEntryExists(result))
            {

                StudentResult prevResult = resultGateway.GetResultByStudentIdCourseId(result);

                prevResult.Attendance = result.Attendance;
                prevResult.Assignement = result.Assignement;
                prevResult.ClassTest = result.ClassTest;
                prevResult.MidTerm = result.MidTerm;
                prevResult.FinalExam = result.FinalExam;
                prevResult.Point = GradePoints(prevResult);
                prevResult.ActionTime = result.ActionTime;
                prevResult.ActionBy = result.ActionBy;
                prevResult.ActionDone = ActionUtility.ActionUpdate;

                int rowsAffected = resultGateway.UpdateResult(prevResult);

                if (rowsAffected > 0)
                {
                    return 2;
                }
                else
                {
                    return 3;
                }
            }
            else
            {
                result.ActionDone = ActionUtility.ActionInsert;
                int rowsAffected = resultGateway.SaveResult(result);

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

        // update last fifty number
        public int UploadLastFiftyResult(StudentResult result)
        {
            if (resultGateway.IsResultEntryExists(result))
            {
                StudentResult studentResult = resultGateway.GetResultByStudentIdCourseId(result);
                studentResult.FinalExam = result.FinalExam;
                studentResult.ActionDone = ActionUtility.ActionUpdate;
                studentResult.ActionTime = DateTime.Now.ToString();
                studentResult.Point = GradePoints(studentResult);

                int rowsAffected = resultGateway.UpdateResult(studentResult);

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
                return 2;
            }
        }

        // compute grade points
        public decimal GradePoints(StudentResult studentResult)
        {
            decimal attendance = studentResult.Attendance;
            decimal assignment = studentResult.Assignement;
            decimal ct = studentResult.ClassTest;
            decimal mid = studentResult.MidTerm;

            decimal totalFirstFifty = attendance + assignment + ct + mid+studentResult.FinalExam;

            if (totalFirstFifty <= 100 && totalFirstFifty >= 80)
            {
                return 4.00m;
            }
            else if (totalFirstFifty <= 79 && totalFirstFifty >= 70)
            {
                return 3.50m;
            }
            else if (totalFirstFifty <= 69 && totalFirstFifty >= 60)
            {
                return 3.00m;
            }
            else if (totalFirstFifty <= 59 && totalFirstFifty >= 50)
            {
                return 2.00m;
            }
            else if (totalFirstFifty <= 49 && totalFirstFifty >= 40)
            {
                return 1.00m;
            }
            else
            {
                return 0.00m;
            }
        }

        // get published result
        public List<ResultSheetViewModel> GetPublishedResult(int studentId)
        {
            return resultGateway.GetResultByStudentId(studentId);
        }
 
        // get details result
        public ResultSheetViewModel GetDetailsResult(int id)
        {
            return resultGateway.GetDetailsResult(id);
        }

        // check result is exists or not
        public bool IsResultExists(int id)
        {
            return resultGateway.IsResultExists(id);
        }

        // get unpublished result for view
        public List<ResultSheetViewModel> GetUnpublishedResultForView()
        {
            return resultGateway.GetAllUnPublishedResultForView();
        } 

        // publish unpublish result
        public int PublishUnpublishResult()
        {
            List<StudentResult> results = resultGateway.GetAllUnpublishedResult();
            int total = results.Count;

            int count = 0;

            foreach (StudentResult result in results)
            {
                int update = resultGateway.PublishUnPublishResult(result);

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

        // get all published result
        public List<ResultSheetViewModel> GetAllPublishedResultForAdmin()
        {
            return resultGateway.GetAllPublishedResultForAdmin();
        }
    }
}