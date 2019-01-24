using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementSystemVersion2.Models;

namespace UniversityManagementSystemVersion2.Gateway
{
    public class DesignationGateway:CommonGateway
    {
        // save designation
        public int SaveDesignation(Designation designation)
        {
            Context.Designations.Add(designation);
            int rowsAffected = Context.SaveChanges();
            return rowsAffected;
        }

        // designation exists
        public bool IsExists(Designation designation)
        {
            return Context.Designations.Any(d => d.DesignationName == designation.DesignationName);
        }

        // get all designations
        public List<Designation> GetAllDesignations()
        {
            return Context.Designations.ToList();
        }
 
        // get designation by id
        public Designation GetDesignationById(int designationId)
        {
            Designation designation = Context.Designations.Where(d => d.Id == designationId).FirstOrDefault();
            return designation;
        }
    }
}