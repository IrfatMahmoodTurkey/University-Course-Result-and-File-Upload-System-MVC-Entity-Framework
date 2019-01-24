using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementSystemVersion2.Gateway;
using UniversityManagementSystemVersion2.Models;
using UniversityManagementSystemVersion2.Models.ViewModels;

namespace UniversityManagementSystemVersion2.Manager
{
    public class ClassRoomAllocationManager
    {
        private ClassRoomAllocationGateway classRoomAllocationGateway;

        public ClassRoomAllocationManager()
        {
            classRoomAllocationGateway = new ClassRoomAllocationGateway();
        }

        // check overlap and partial overlap
        public int CheckOverlap(ClassroomAllocate allocate)
        {
            List<ClassroomAllocate> allocates = classRoomAllocationGateway.GetSchedule(allocate);
            int firstCount = 0;

            // convert string time to TimeSpan class
            TimeSpan fromTime = DateTime.Parse(allocate.FromTime).TimeOfDay;
            TimeSpan toTime = DateTime.Parse(allocate.ToTime).TimeOfDay;

            foreach (ClassroomAllocate result in allocates)
            {
                // convert string time to TimeSpan class
                TimeSpan mainFromTime = DateTime.Parse(result.FromTime).TimeOfDay;
                TimeSpan mainToTime = DateTime.Parse(result.ToTime).TimeOfDay;

                if (mainFromTime == fromTime)
                {
                    firstCount++;
                }
                else if (mainFromTime <= fromTime && fromTime < mainToTime)
                {
                    firstCount++;
                }
                else if (mainFromTime < toTime && toTime <= mainToTime)
                {
                    firstCount++;
                }
                else if (fromTime < mainFromTime && toTime > mainToTime)
                {
                    firstCount++;
                }
            }

            if (firstCount == 0)
            {
                // no overlap with room and day
                if (classRoomAllocationGateway.IsCourseExists(allocate))
                {
                    int overlap = CheckOverlapForSameCourseInDifferentRoom(allocate);

                    if (overlap == 0)
                    {
                        // overlap with same course in diff room
                        return 2;
                    }
                    else
                    {
                        // no overlap
                        return 1;
                    }
                }
                else
                {
                    // no overlap
                    return 1;
                }
            }
            else
            {
                // overlap with room and day
                return 0;
            }
        }

        // check overlap for same course in different room
        public int CheckOverlapForSameCourseInDifferentRoom(ClassroomAllocate allocate)
        {
            List<ClassroomAllocate> allocates = classRoomAllocationGateway.GetScheduleForSameCourseAndDay(allocate);
            int firstCount = 0;

            // convert string time to TimeSpan class
            TimeSpan fromTime = DateTime.Parse(allocate.FromTime).TimeOfDay;
            TimeSpan toTime = DateTime.Parse(allocate.ToTime).TimeOfDay;

            foreach (ClassroomAllocate result in allocates)
            {
                // convert string time to TimeSpan class
                TimeSpan mainFromTime = DateTime.Parse(result.FromTime).TimeOfDay;
                TimeSpan mainToTime = DateTime.Parse(result.ToTime).TimeOfDay;

                if (mainFromTime == fromTime)
                {
                    firstCount++;
                }
                else if (mainFromTime <= fromTime && fromTime < mainToTime)
                {
                    firstCount++;
                }
                else if (mainFromTime < toTime && toTime <= mainToTime)
                {
                    firstCount++;
                }
                else if (fromTime < mainFromTime && toTime > mainToTime)
                {
                    firstCount++;
                }
            }

            if (firstCount == 0)
            {
                // no overlap with day and course
                return 1;
            }
            else
            {
                // overlap with day and course
                return 0;
            }
        }

        // allocate classroom
        public int Allocate(ClassroomAllocate allocate)
        {
            if (classRoomAllocationGateway.IsClassExists(allocate))
            {
                int check = CheckOverlap(allocate);

                if (check == 1)
                {
                    int rowsAffected = classRoomAllocationGateway.AllocateClassRooms(allocate);

                    if (rowsAffected > 0)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else if (check == 2)
                {
                    // overlap same course day
                    return 10;
                }
                else
                {
                    return 5;
                }
            }
            else
            {
                int check = CheckOverlapForSameCourseInDifferentRoom(allocate);

                if (check == 1)
                {
                    int rowsAffected = classRoomAllocationGateway.AllocateClassRooms(allocate);

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
                    // overlap with same course diff room
                    return 10;
                }
               
            }
        }

        // get class schedule by departmentId
        public List<ClassRoomAllocateViewModel> GetScheduleByDepartmentId(int departmentId)
        {
            return classRoomAllocationGateway.GetScheduleByDepartmentId(departmentId);
        }

        // unallocate all classes
        public int UnallocateAllCourses()
        {
            List<ClassroomAllocate> classroomAllocates = classRoomAllocationGateway.GetAllAllocate();
            int total = classroomAllocates.Count;
            int count = 0;

            foreach (ClassroomAllocate allocate in classroomAllocates)
            {
                int update = classRoomAllocationGateway.UnallocateCourse(allocate);

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