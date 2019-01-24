using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityManagementSystemVersion2.Utility
{
    public static class ActionUtility
    {
        public static string ActionBy { get; set; }
        public static string ActionInsert { get; private set; }
        public static string ActionUpdate { get; private set; }
        public static string ActionDelete { get; private set; }
        public static string ActionRecovered { get; private set; }
        public static string ActionTime { get; set; }
        public static string DefaultProfilePicture { get; set; }

        static ActionUtility()
        {
            ActionInsert = "INSERTED";
            ActionUpdate = "UPDATED";
            ActionDelete = "DELETED";
            ActionRecovered = "RECOVERED";
            DefaultProfilePicture = "~/ProfilePictures/avater.jpg";
        }
    }
}