using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace UniversityManagementSystemVersion2.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("name=UniversityDbConString")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<CourseAssigned> CourseAssigneds { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<ClassroomAllocate> ClassroomAllocates { get; set; }
        public DbSet<EnrollCourses> EnrollCourseses { get; set; }
        public DbSet<StudentResult> StudentResults { get; set; }
        public DbSet<ExamRoutine> ExamRoutines { get; set; }
        public DbSet<AssignmentOrReport> AssignmentOrReports { get; set; }
        public DbSet<Supplimentary> Supplimentaries { get; set; }
        public DbSet<Notice> Notices { get; set; }
        public DbSet<EnrollState> EnrollStates { get; set; }
    }
}