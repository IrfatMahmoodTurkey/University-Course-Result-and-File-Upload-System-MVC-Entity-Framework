using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementSystemVersion2.Gateway;
using UniversityManagementSystemVersion2.Models;

namespace UniversityManagementSystemVersion2.Manager
{
    public class SupplymentaryManager
    {
        private SupplymentaryGateway supplymentaryGateway;

        public SupplymentaryManager()
        {
            supplymentaryGateway = new SupplymentaryGateway();
        }

        // upload suplymentary
        public int UploadSupplymentary(Supplimentary supplimentary)
        {
            int rowsAffected = supplymentaryGateway.UploadSupplymentary(supplimentary);

            if (rowsAffected > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        // get files by session and courseId
        public List<Supplimentary> GetFiles(string session, int courseId)
        {
            return supplymentaryGateway.GetFiles(session, courseId);
        }

        // deactivate all files
        public int DeactiveFiles()
        {
            List<Supplimentary> supplimentaries = supplymentaryGateway.GetAllFiles();
            int total = supplimentaries.Count;
            int count = 0;

            foreach (Supplimentary supplimentary in supplimentaries)
            {
                int update = supplymentaryGateway.DeactiveFiles(supplimentary);

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

        // get all files by course id
        public List<Supplimentary> GetAllFilesByCourseId(int courseId)
        {
            return supplymentaryGateway.GetAllFilesByCourseId(courseId);
        }
    }
}