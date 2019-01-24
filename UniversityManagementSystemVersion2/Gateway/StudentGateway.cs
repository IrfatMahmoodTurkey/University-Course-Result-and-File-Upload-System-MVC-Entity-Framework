using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementSystemVersion2.Models;
using UniversityManagementSystemVersion2.Models.ViewModels;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using Microsoft.Ajax.Utilities;

namespace UniversityManagementSystemVersion2.Gateway
{
    public class StudentGateway:CommonGateway
    {
        // save student
        public int SaveStudent(Student student)
        {
            Context.Students.Add(student);
            int rowsAffected = Context.SaveChanges();
            return rowsAffected;
        }

        // check is email exists
        public bool IsEmailExists(Student student)
        {
            bool isExists = Context.Students.Any(s => s.Email == student.Email);
            return isExists;
        }

        // check is email exists for update
        public bool IsEmailExistsForUpdate(Student student)
        {
            bool isExists = Context.Students.Any(s => s.Email == student.Email && s.Id != student.Id);
            return isExists;
        }

        // get all students by department
        public List<Student> GetAllStudentsByDepartment(Student student)
        {
            return Context.Students.Where(s=>s.DepartmentId == student.DepartmentId).ToList();
        }

        // get total count by year and department
        public int GetStudentNumberByDepartmentAndYear(Student student)
        {
            string year = student.Date.Substring(6, 4);
            List<Student> students= Context.Students.Where(s => s.DepartmentId == student.DepartmentId && s.Date.EndsWith(year)).ToList();
            return students.Count;
        }

        // get all student details
        public List<StudentViewModel> GetAllStudentDetails()
        {
            var query = Context.Students.Include(sd => sd.Department).OrderByDescending(s => s.Id).ToList();

            List<StudentViewModel> studentViewModels = new List<StudentViewModel>();

            foreach (var result in query)
            {
                StudentViewModel studentViewModel = new StudentViewModel();

                studentViewModel.Id = result.Id;
                studentViewModel.RegNo = result.RegNo;
                studentViewModel.Name = result.Name;
                studentViewModel.Email = result.Email;
                studentViewModel.DepartmentName = result.Department.Name;
                string profilePicture = result.ProfilePicture;
                int length = profilePicture.Length;
                studentViewModel.ProfilePicture = profilePicture.Substring(1, length - 1);


                studentViewModels.Add(studentViewModel);
            }

            return studentViewModels;
        }
 
        // get student details by student id
        public StudentViewModel GetStudentDetailsById(int id)
        {
            var query = Context.Students.Include(sa => sa.Department).Where(sa => sa.Id == id).FirstOrDefault();

            StudentViewModel studentViewModel = new StudentViewModel();

            studentViewModel.RegNo = query.RegNo;
            studentViewModel.Name = query.Name;
            studentViewModel.Email = query.Email;
            studentViewModel.ContactNo = query.ContactNo;
            studentViewModel.Address = query.Address;
            studentViewModel.Date = query.Date;
            studentViewModel.DepartmentName = query.Department.Name;
            string profilePicture = query.ProfilePicture;
            int length = profilePicture.Length;
            studentViewModel.ProfilePicture = profilePicture.Substring(1, length - 1);
            studentViewModel.ActionBy = query.ActionBy;
            studentViewModel.ActionDone = query.ActionDone;
            studentViewModel.ActionTime = query.ActionTime;

            return studentViewModel;
        }

        // is student exists
        public bool IsStudentExists(int id)
        {
            bool isExists = Context.Students.Any(s => s.Id == id);
            return isExists;
        }

        // get departmentId by student RegNo
        public Student GetDepartmentIdByStudentRegNo(string regNo)
        {
            Student student = Context.Students.Where(s => s.RegNo == regNo).FirstOrDefault();
            return student;
        }

        // get student by student id
        public Student GetStudentById(int id)
        {
            Student student = Context.Students.Where(s => s.Id == id).FirstOrDefault();
            return student;
        }
        
        // Enroll Course
        public int EnrollCourse(EnrollCourses enrollCourse)
        {
            Context.EnrollCourseses.Add(enrollCourse);
            int rowsAffected = Context.SaveChanges();
            return rowsAffected;
        }

        // check any course exists (for regular)
        public bool IsAnyCourseExists(EnrollCourses enrollCourses)
        {
            bool isExists =
                Context.EnrollCourseses.Any(
                    e =>
                        e.StudentId == enrollCourses.StudentId && e.CourseId == enrollCourses.CourseId &&
                        e.State == "ACTIVE");
            return isExists;
        }

        // check regular course exists (for retake)
        public bool IsRegularCourseExists(EnrollCourses enrollCourses)
        {
            bool isExists =
                Context.EnrollCourseses.Any(
                    e =>
                        e.StudentId == enrollCourses.StudentId && e.CourseId == enrollCourses.CourseId &&
                        e.State == "ACTIVE" && (e.Type == "Regular" || e.Type == "Recourse"));
            return isExists;
        }

        // check retake or recourse is exists (for recourse)
        public bool IsRetakeOrRecourseIsExists(EnrollCourses enrollCourses)
        {
            bool isExists =
                Context.EnrollCourseses.Any(
                    e =>
                        e.StudentId == enrollCourses.StudentId && e.CourseId == enrollCourses.CourseId &&
                        e.State == "ACTIVE" && (e.Type == "Retake" || e.Type == "Recourse"));
            return isExists;
        }

        // get entry for update retake or recourse
        public EnrollCourses GetEnrollCourseByStudentAndCourse(EnrollCourses enrollCourses)
        {
            EnrollCourses result =
                Context.EnrollCourseses.Where(
                    e =>
                        e.StudentId == enrollCourses.StudentId && e.CourseId == enrollCourses.CourseId &&
                        e.State == "ACTIVE").FirstOrDefault();
            return result;
        }

        // update course type for retake and course
        public int UpdateType(EnrollCourses enrollCourses)
        {
            Context.EnrollCourseses.AddOrUpdate(enrollCourses);
            int rowsAffected = Context.SaveChanges();
            return rowsAffected;
        }

        // get sessions for selection
        public List<string> GetAllSessions()
        {
            var query = Context.EnrollCourseses.DistinctBy(e => e.Session).ToList();
            List<string> sessions = new List<string>();

            foreach (var result in query)
            {
                sessions.Add(result.Session);
            }

            return sessions;;
        }
 
        // get enrolled student list by course and session
        public List<Student> GetEnrolledStudentByCourseAndSession(int courseId, string session)
        {
            var query =
                Context.EnrollCourseses.Include(e => e.Student)
                    .Where(e => e.CourseId == courseId && e.Session == session && e.State == "ACTIVE" && e.Type != "Retake")
                    .Select(s => new
                    {
                        Id = s.Student.Id,
                        StudentRegNo = s.Student.RegNo,
                        StudentName = s.Student.Name
                    });

            List<Student> students = new List<Student>();

            foreach (var result in query)
            {
                Student student = new Student();

                student.Id = result.Id;
                student.RegNo = result.StudentRegNo;
                student.Name = result.StudentName;

                students.Add(student);
            }

            return students;
        }

        // get enrolled student list by course and session (with retake)
        public List<Student> GetAllEnrolledStudentByCourseAndSession(int courseId, string session)
        {
            var query =
                Context.EnrollCourseses.Include(e => e.Student)
                    .Where(e => e.CourseId == courseId && e.Session == session && e.State == "ACTIVE")
                    .Select(s => new
                    {
                        Id = s.Student.Id,
                        StudentRegNo = s.Student.RegNo,
                        StudentName = s.Student.Name
                    });

            List<Student> students = new List<Student>();

            foreach (var result in query)
            {
                Student student = new Student();

                student.Id = result.Id;
                student.RegNo = result.StudentRegNo;
                student.Name = result.StudentName;

                students.Add(student);
            }

            return students;
        }
 
        // get enroll courses by student id and session
        public List<Course> GetCoursesByStudentIdAndSession(int studentId, string session)
        {
            var query =
                Context.EnrollCourseses.Include(e => e.Course)
                    .Where(e => e.StudentId == studentId && e.Session == session && e.State == "ACTIVE")
                    .Select(sa => new
                    {
                        Id = sa.Course.Id,
                        Code = sa.Course.Code,
                        Name = sa.Course.Name
                    });

            List<Course> courses = new List<Course>();

            foreach (var result in query)
            {
                Course course = new Course();

                course.Id = result.Id;
                course.Code = result.Code;
                course.Name = result.Name;

                courses.Add(course);
            }

            return courses;
        }

        // get all courses by  and session
        public List<Course> GetAllCoursesBySession(string session)
        {
            var query =
                Context.EnrollCourseses.Include(e => e.Course)
                    .Where(e=>e.Session == session && e.State == "ACTIVE")
                    .Select(sa => new
                    {
                        Id = sa.Course.Id,
                        Code = sa.Course.Code,
                        Name = sa.Course.Name
                    });

            List<Course> courses = new List<Course>();

            foreach (var result in query)
            {
                Course course = new Course();

                course.Id = result.Id;
                course.Code = result.Code;
                course.Name = result.Name;

                courses.Add(course);
            }

            return courses;
        }

        // update student
        public int UpdateStudent(Student student)
        {
            Context.Students.AddOrUpdate(student);
            int rowsAffected = Context.SaveChanges();
            return rowsAffected;
        }

        // change enroll state
        public int ChangeState(EnrollState state)
        {
            Context.EnrollStates.Add(state);
            int rowsAffected = Context.SaveChanges();
            return rowsAffected;
        }

        // check rowsExists
        public bool IsExistsEnrollState(EnrollState state)
        {
            var query = Context.EnrollStates.ToList();

            if (query.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // update enroll state
        public int UpdateState(EnrollState state)
        {
            Context.EnrollStates.AddOrUpdate(state);
            int rowsAffected = Context.SaveChanges();
            return rowsAffected;
        }

        // get enroll state single
        public EnrollState GetEnrollStateSingle()
        {
            EnrollState enrollState = Context.EnrollStates.FirstOrDefault();
            return enrollState;
        }

        // check student exists for auth
        public bool IsStudentExistsForAuth(string regNo)
        {
            bool isExists = Context.Students.Any(s => s.RegNo == regNo);
            return isExists;
        }

        // get enrolled courses by student id
        public List<EnrollCourseViewModel> GetEnrollCourseByOnlyStudentId(int studentId)
        {
            var query =
                Context.EnrollCourseses.Include(e => e.Course)
                    .Where(e => e.StudentId == studentId && e.State == "ACTIVE").OrderByDescending(e=>e.Session)
                    .Select(sa => new
                    {
                        Id = sa.Course.Id,
                        Code = sa.Course.Code,
                        Name = sa.Course.Name,
                        Credit = sa.Course.Credit,
                        Session = sa.Session,
                        Type = sa.Type
                    });

            List<EnrollCourseViewModel> courses = new List<EnrollCourseViewModel>();

            foreach (var result in query)
            {
                EnrollCourseViewModel course = new EnrollCourseViewModel();

                course.Id = result.Id;
                course.CourseCode = result.Code;
                course.CourseName = result.Name;
                course.CourseCredit = result.Credit;
                course.Session = result.Session;
                course.Type = result.Type;

                courses.Add(course);
            }

            return courses;
        }
 
        // get student id by action date
        public List<Student> GetStudentBySessionDateForAdmit(string actionDate)
        {
            var query =
                Context.EnrollCourseses.Include(e => e.Student)
                    .Where(e => e.ActionTime.Contains(actionDate) && e.State == "ACTIVE").DistinctBy(en => en.StudentId).Select(sa => new
                    {
                        Id = sa.Student.Id,
                        RegNo = sa.Student.RegNo
                    });
                    
            List<Student> students = new List<Student>();

            foreach (var result in query)
            {
                Student student = new Student();

                student.Id = result.Id;
                student.RegNo = result.RegNo;

                students.Add(student);
            }

            return students;
        }
 
        // get enroll courses by actionDate and studentId
        public List<EnrollCourseViewModel> GetCoursesByActionDateAndStudentId(string actionDate, int studentId)
        {
            var query =
                Context.EnrollCourseses.Include(e => e.Course)
                    .Where(
                        e =>
                            e.ActionTime.Contains(actionDate) && e.StudentId == studentId &&
                            e.State == "ACTIVE")
                    .Select(sa => new
                    {
                        CourseCode = sa.Course.Code,
                        CourseName = sa.Course.Name,
                        Type = sa.Type
                    });

            List<EnrollCourseViewModel> courses = new List<EnrollCourseViewModel>();

            foreach (var result in query)
            {
                EnrollCourseViewModel course = new EnrollCourseViewModel();

                course.CourseCode = result.CourseCode;
                course.CourseName = result.CourseName;
                course.Type = result.Type;

                courses.Add(course);
            }

            return courses;
        } 
    }
}