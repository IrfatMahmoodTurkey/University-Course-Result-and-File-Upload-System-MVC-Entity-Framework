using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementSystemVersion2.Models;

namespace UniversityManagementSystemVersion2.Gateway
{
    public class CommonGateway
    {
        public ApplicationDbContext Context { get; set; }

        public CommonGateway()
        {
            Context = new ApplicationDbContext();
        }
    }
}