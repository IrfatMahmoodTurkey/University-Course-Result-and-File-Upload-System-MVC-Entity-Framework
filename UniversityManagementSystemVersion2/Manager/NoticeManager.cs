using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementSystemVersion2.Gateway;
using UniversityManagementSystemVersion2.Models;

namespace UniversityManagementSystemVersion2.Manager
{
    public class NoticeManager
    {
        private NoticeGateway noticeGateway;

        public NoticeManager()
        {
            noticeGateway = new NoticeGateway();
        }

        // submit notice
        public int UploadNotice(Notice notice)
        {
            int rowsAffected = noticeGateway.UploadNotice(notice);

            if (rowsAffected > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        // get all notice
        public List<Notice> GetAllNotice()
        {
            return noticeGateway.GetAllNotice();
        } 
    }
}