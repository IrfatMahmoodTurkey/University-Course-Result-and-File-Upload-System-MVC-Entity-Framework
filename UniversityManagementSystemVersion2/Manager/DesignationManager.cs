using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystemVersion2.Gateway;
using UniversityManagementSystemVersion2.Models;

namespace UniversityManagementSystemVersion2.Manager
{
    public class DesignationManager
    {
        private DesignationGateway designationGateway;

        public DesignationManager()
        {
            designationGateway = new DesignationGateway();
        }

        // save designation
        public int SaveDesignation(Designation designation)
        {
            if (designationGateway.IsExists(designation))
            {
                return 2;
            }
            else
            {
                int rowsAffected = designationGateway.SaveDesignation(designation);

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

        // get all designations
        public List<Designation> GetAllDesignations()
        {
            return designationGateway.GetAllDesignations();
        } 

        // get designation for dropdown
        public List<SelectListItem> GetDesignationsForDropDown()
        {
            List<Designation> designations = designationGateway.GetAllDesignations();

            List<SelectListItem> selectListItems =
                designations.ConvertAll(d => new SelectListItem() {Text = d.DesignationName, Value = d.Id.ToString()});

            List<SelectListItem> mainSelectListItems = new List<SelectListItem>();
            mainSelectListItems.Add(new SelectListItem(){Text = "-- Select Designation --", Value = ""});

            mainSelectListItems.AddRange(selectListItems);

            return mainSelectListItems;
        }
 
        // get designation by id
        public Designation GetDesignationById(int designationId)
        {
            return designationGateway.GetDesignationById(designationId);
        }
    }
}