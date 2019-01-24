using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementSystemVersion2.Models;

namespace UniversityManagementSystemVersion2.Gateway
{
    public class NoticeGateway:CommonGateway
    {
        // submit notice
        public int UploadNotice(Notice notice)
        {
            Context.Notices.Add(notice);
            int rowsAffected = Context.SaveChanges();
            return rowsAffected;
        }

        // show notice
        public List<Notice> GetAllNotice()
        {
            List<Notice> notices = Context.Notices.OrderByDescending(n=>n.Id).ToList();
            return notices;
        } 
    }
}