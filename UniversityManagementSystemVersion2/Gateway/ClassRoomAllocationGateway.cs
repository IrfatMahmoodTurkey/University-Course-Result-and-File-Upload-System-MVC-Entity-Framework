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
    public class ClassRoomAllocationGateway:CommonGateway
    {
        // save allocation classroom
        public int AllocateClassRooms(ClassroomAllocate allocate)
        {
            Context.ClassroomAllocates.Add(allocate);
            int rowsAffected = Context.SaveChanges();
            return rowsAffected;
        }

        // get class schedule list by day and room
        public List<ClassroomAllocate> GetSchedule(ClassroomAllocate allocate)
        {
            List<ClassroomAllocate> query =
                Context.ClassroomAllocates.Where(c => c.Day == allocate.Day && c.RoomId == allocate.RoomId && c.State == "ACTIVE").ToList();
            return query;
        }
 
        // get class schedule of same course by day different room
        public List<ClassroomAllocate> GetScheduleForSameCourseAndDay(ClassroomAllocate allocate)
        {
            List<ClassroomAllocate> query =
                Context.ClassroomAllocates.Where(c => c.Day == allocate.Day && c.CourseId == allocate.CourseId && c.State == "ACTIVE").ToList();
            return query;
        }
 
        //check if room is allocated
        public bool IsClassExists(ClassroomAllocate classroomAllocate)
        {
            bool isExists =
                Context.ClassroomAllocates.Any(
                    c => c.Day == classroomAllocate.Day && c.RoomId == classroomAllocate.RoomId && c.State == "ACTIVE");
            return isExists;
        } 

        // check if course exists in the same time in another room
        public bool IsCourseExists(ClassroomAllocate classroomAllocate)
        {
            bool isExists =
                Context.ClassroomAllocates.Any(
                    c => c.Day == classroomAllocate.Day && c.CourseId == classroomAllocate.CourseId && c.State == "ACTIVE");
            return isExists;
        }

        // get class schedule by departmentId
        public List<ClassRoomAllocateViewModel> GetScheduleByDepartmentId(int departmentId)
        {
            var query = Context.Courses.Include(c => c.ClassroomAllocates).Where(c=>c.DepartmentId == departmentId).Select(s => new
            {
                CourseCode = s.Code,
                CourseName = s.Name,
                ClassSchedule = s.ClassroomAllocates.Where(c=> c.State == "ACTIVE").Select(sa => new
                {
                    RoomNo = sa.Room.RoomNo,
                    Day = sa.Day,
                    FromTime = sa.FromTime,
                    ToTime = sa.ToTime
                })
            });

            List<ClassRoomAllocateViewModel> classRoomAllocateViewModels = new List<ClassRoomAllocateViewModel>();

            foreach (var result in query)
            {
                ClassRoomAllocateViewModel model = new ClassRoomAllocateViewModel();

                model.CourseCode = result.CourseCode;
                model.CourseName = result.CourseName;
                model.Schedules = "Not Allocated Yet";

                foreach (var myResult in result.ClassSchedule)
                {
                    if (model.Schedules.Equals("Not Allocated Yet"))
                    {
                        model.Schedules = "R.No: "+myResult.RoomNo+", "+myResult.Day+", "+myResult.FromTime+" - "+myResult.ToTime;
                    }
                    else
                    {
                        model.Schedules = model.Schedules +";"+ "</br>" + "R.No: " + myResult.RoomNo + ", " + myResult.Day + ", " + myResult.FromTime + " - " + myResult.ToTime;
                    }
                }


                classRoomAllocateViewModels.Add(model);
            }

            return classRoomAllocateViewModels;
        }

        // get all information of class schedule
        public List<ClassroomAllocate> GetAllAllocate()
        {
            List<ClassroomAllocate> classroomAllocates =
                Context.ClassroomAllocates.Where(c => c.State == "ACTIVE").ToList();
            return classroomAllocates;
        } 
 
        // unallocate all courses
        public int UnallocateCourse(ClassroomAllocate allocate)
        {
            allocate.State = "DEACTIVE";
            allocate.ActionDone = ActionUtility.ActionDelete;
            allocate.ActionTime = DateTime.Now.ToString();

            Context.ClassroomAllocates.AddOrUpdate(allocate);
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