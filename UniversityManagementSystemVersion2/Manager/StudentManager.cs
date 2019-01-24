using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystemVersion2.Gateway;
using UniversityManagementSystemVersion2.Models;
using UniversityManagementSystemVersion2.Models.ViewModels;
using UniversityManagementSystemVersion2.Utility;

namespace UniversityManagementSystemVersion2.Manager
{
    public class StudentManager
    {
        private StudentGateway studentGateway;
        private DepartmentGateway departmentGateway;
        private ResultGateway resultGateway;

        public StudentManager()
        {
            studentGateway = new StudentGateway();
            departmentGateway = new DepartmentGateway();
            resultGateway = new ResultGateway();
        }

        // save student
        public int SaveStudent(Student student)
        {
            if (studentGateway.IsEmailExists(student))
            {
                return 2;
            }
            else
            {
                
                int rowsAffected = studentGateway.SaveStudent(student);

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

        // get student by departemtnid
        public Department GetDepartmentById(int departmentId)
        {
            return departmentGateway.GetDepartmentById(departmentId);
        }

        public int GetStudentNumberByDepartmentAndYear(Student student)
        {
            return studentGateway.GetStudentNumberByDepartmentAndYear(student);
        }

        

        // get all student details
        public List<StudentViewModel> GetAllStudentDetails()
        {
            return studentGateway.GetAllStudentDetails();
        }

        // get student details by student id
        public StudentViewModel GetStudentDetailsById(int id)
        {
            return studentGateway.GetStudentDetailsById(id);
        }

        // is student exists
        public bool IsStudentExists(int id)
        {
            return studentGateway.IsStudentExists(id);
        }

        // get departmentId by student RegNo
        public Student GetDepartmentIdByStudentRegNo(string regNo)
        {
            return studentGateway.GetDepartmentIdByStudentRegNo(regNo);
        }

        // Enroll Course
        public int EnrollCourse(EnrollCourses enrollCourse)
        {
            EnrollState enrollState = studentGateway.GetEnrollStateSingle();

            enrollCourse.Session = DateTime.Now.Month + "-" + DateTime.Now.Year;

            if (enrollState != null && enrollState.State == "Enabled")
            {
                // check regular
                if (enrollCourse.Type.Equals("Regular"))
                {
                    if (studentGateway.IsAnyCourseExists(enrollCourse))
                    {
                        return 2;
                    }
                    else
                    {
                        int rowsAffected = studentGateway.EnrollCourse(enrollCourse);

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
                // check retake
                else if (enrollCourse.Type.Equals("Retake"))
                {
                    if (studentGateway.IsRegularCourseExists(enrollCourse))
                    {
                        // update enroll entry by retake
                        EnrollCourses result = studentGateway.GetEnrollCourseByStudentAndCourse(enrollCourse);

                        // take string for session
                        string sessionCourse = result.Session;

                        result.Session = enrollCourse.Session;
                        result.Type = "Retake";

                        int rowsAffected = studentGateway.UpdateType(result);

                        StudentResult studentResult = new StudentResult();

                        studentResult.StudentId = enrollCourse.StudentId;
                        studentResult.CourseId = enrollCourse.CourseId;
                        studentResult.Session = sessionCourse;

                        StudentResult studentResult2 = resultGateway.GetResultByStudentIdCourseId(studentResult);

                        studentResult2.Session = enrollCourse.Session;
                        studentResult2.PublishStatus = "NotPublished";

                        int updateResult = resultGateway.UpdateResult(studentResult2);

                        if (rowsAffected > 0 && updateResult > 0)
                        {
                            return 3;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                    else
                    {
                        return 4;
                    }
                }
                // check recourse
                else
                {
                    if (studentGateway.IsRetakeOrRecourseIsExists(enrollCourse))
                    {
                        // update retake or regular entry by recourse
                        EnrollCourses result = studentGateway.GetEnrollCourseByStudentAndCourse(enrollCourse);

                        //string check session for result
                        string sessionCourse = result.Session;

                        result.Session = enrollCourse.Session;
                        result.Type = "Recourse";

                        int rowsAffected = studentGateway.UpdateType(result);

                        bool returnValue = false;

                        StudentResult studentResult = new StudentResult();
                        studentResult.StudentId = result.StudentId;
                        studentResult.CourseId = result.CourseId;
                        studentResult.Attendance = 0;
                        studentResult.Assignement = 0.0m;
                        studentResult.ClassTest = 0.0m;
                        studentResult.MidTerm = 0.0m;
                        studentResult.FinalExam = 0.0m;
                        studentResult.Point = 0.0m;
                        studentResult.Session = sessionCourse;
                        studentResult.State = "ACTIVE";
                        studentResult.ActionBy = result.ActionBy;
                        studentResult.ActionDone = ActionUtility.ActionUpdate;
                        studentResult.ActionTime = DateTime.Now.ToString();
                        studentResult.PublishStatus = "NotPublished";

                        // update recourse result with initial state
                        if (resultGateway.IsResultEntryExists(studentResult))
                        {
                            StudentResult studentResult2 = resultGateway.GetResultByStudentIdCourseId(studentResult);
                            studentResult.Id = studentResult2.Id;
                            studentResult.Session = enrollCourse.Session;

                            int updateResult = resultGateway.UpdateResult(studentResult);

                            if (updateResult > 0)
                            {
                                returnValue = true;
                            }
                            else
                            {
                                returnValue = false;
                            }
                        }

                        if (rowsAffected > 0 && returnValue)
                        {
                            return 5;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                    else
                    {
                        return 6;
                    }
                }
            }
            else
            {
                return 7;
            }            
        }

        // sessions for drop down
        public List<SelectListItem> GetAllSessionsForDropDown()
        {
            List<string> sessions = studentGateway.GetAllSessions();

            List<SelectListItem> selectListItems =
                sessions.ConvertAll(s => new SelectListItem() {Text = s.ToString(), Value = s.ToString()});

            List<SelectListItem> mainSelectListItems = new List<SelectListItem>();
            mainSelectListItems.Add(new SelectListItem()
            {
                Text = "-- Select Session --",
                Value = ""
            });

            mainSelectListItems.AddRange(selectListItems);

            return mainSelectListItems;
        }
 
        // get enrolled student list by course and session
        public List<Student> GetEnrolledStudentByCourseAndSession(int courseId, string session)
        {
            return studentGateway.GetEnrolledStudentByCourseAndSession(courseId, session);
        }

        // get enrolled student list by course and session (with retake)
        public List<Student> GetAllEnrolledStudentByCourseAndSession(int courseId, string session)
        {
            return studentGateway.GetAllEnrolledStudentByCourseAndSession(courseId, session);
        }

        // get enroll courses by student id and session
        public List<Course> GetCoursesByStudentIdAndSession(int studentId, string session)
        {
            return studentGateway.GetCoursesByStudentIdAndSession(studentId, session);
        }

        // get all courses by  and session
        public List<Course> GetAllCoursesBySession(string session)
        {
            return studentGateway.GetAllCoursesBySession(session);
        }

        // update student
        public int UpdateStudent(Student student)
        {
            int rowsAffected = studentGateway.UpdateStudent(student);

            if (rowsAffected > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        // get student by student id
        public Student GetStudentById(int id)
        {
            return studentGateway.GetStudentById(id);
        }

        // update student information
        public int UpdateStudentInformation(Student student)
        {
            if (studentGateway.IsEmailExistsForUpdate(student))
            {
                return 2;
            }
            else
            {
                int rowsAffected = studentGateway.UpdateStudent(student);

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

        // change enroll state
        public int ChangeEnrollState(EnrollState state)
        {
            if (studentGateway.IsExistsEnrollState(state))
            {
                EnrollState enrollState = studentGateway.GetEnrollStateSingle();

                enrollState.State = state.State;
                enrollState.ActionDone = ActionUtility.ActionUpdate;
                enrollState.ActionTime = DateTime.Now.ToString();

                int update = studentGateway.UpdateState(enrollState);

                if (update > 0)
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
                state.ActionDone = ActionUtility.ActionUpdate;
                state.ActionTime = DateTime.Now.ToString();

                int update = studentGateway.ChangeState(state);

                if (update > 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }

        // check student exists for auth
        public bool IsStudentExistsForAuth(string regNo)
        {
            return studentGateway.IsStudentExistsForAuth(regNo);
        }

        // get enroll state
        public EnrollState GetEnrollStateSingle()
        {
            return studentGateway.GetEnrollStateSingle();
        }

        // get enrolled courses by student id
        public List<EnrollCourseViewModel> GetEnrollCourseByOnlyStudentId(int studentId)
        {
            return studentGateway.GetEnrollCourseByOnlyStudentId(studentId);
        }

        // get student by action time
        public List<Student> GetStudentBySessionDateForAdmit(string actionDate)
        {
            return studentGateway.GetStudentBySessionDateForAdmit(actionDate);
        }
 
        // get enroll courses by actionDate and studentId
        public List<EnrollCourseViewModel> GetCoursesByActionDateAndStudentId(string actionDate, int studentId)
        {
            return studentGateway.GetCoursesByActionDateAndStudentId(actionDate, studentId);
        }
    }
}