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
    public partial class department : System.Web.UI.Page
    {
        Department s = new Department();
        Int32 DepartmentID = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            //if save wasnt clicked AND we have a StudentID in the url
            if ((!IsPostBack) && (Request.QueryString.Count > 0))
            {
                GetDepartments();
            }
        }

        protected void GetDepartments()
        {
            //populate form wih existing student record
            Int32 DepartmentID = Convert.ToInt32(Request.QueryString["DepartmentID"]);

            //connect to db via EF
            using (comp2007Entities db = new comp2007Entities())
            {
                Department s = (from objs in db.Departments
                             where objs.DepartmentID == DepartmentID
                             select objs).FirstOrDefault();

                //map the student properties to the form controls
                txtDepartmentName.Text = s.Name;
                txtBudget.Text = s.Budget.ToString();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //use EG to connect to SQL Server
            using (comp2007Entities db = new comp2007Entities())
            {
                //use the Student Model to save the new record


                //check the querystring for an id so we can determine add/update
                if (Request.QueryString["DepartmentID"] != null)
                {
                    //get the id from the url
                    DepartmentID = Convert.ToInt32(Request.QueryString["DepartmentID"]);

                    //get the current student from EF
                    s = (from objs in db.Departments
                         where objs.DepartmentID == DepartmentID
                         select objs).FirstOrDefault();
                }

                s.Name = txtDepartmentName.Text;
                s.Budget = Convert.ToDecimal(txtBudget.Text);

                //call add only if we have no student ID
                if (DepartmentID == 0)
                {
                    db.Departments.Add(s);
                }

                db.SaveChanges();

                //redirect
                Response.Redirect("departments.aspx");
            }

        }
    }
}