using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//reference the EF Models
using Lesson_6.Models;
using System.Web.ModelBinding;

namespace Lesson_6
{
    public partial class departments : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if loading the page for the first time, populate the student grid
            if (!IsPostBack)
            {
                GetDepartments();
            }
        }


        protected void GetDepartments()
        {
            //connect to EF
            using (comp2007Entities db = new comp2007Entities())
            {
                //query the students table using EF and LINQ
                var Departments = from s in db.Departments
                               select s;

                //bind the result to the gridview
                grdDepartments.DataSource = Departments.ToList();
                grdDepartments.DataBind();
            }
        }

        protected void grdDepartments_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //store which row was clicked
            Int32 selectedRow = e.RowIndex;

            //get the selected StudentID using the grid's Data Key collection
            Int32 DepartmentID = Convert.ToInt32(grdDepartments.DataKeys[selectedRow].Values["DepartmentID"]);

            //use EF to remove the selected student from the db
            using (comp2007Entities db = new comp2007Entities())
            {
                Department s = (from objs in db.Departments
                             where objs.DepartmentID == DepartmentID
                             select objs).FirstOrDefault();

                //do the delete
                db.Departments.Remove(s);
                db.SaveChanges();
            }

            //refresh the grid
            GetDepartments();
        }
    }
}