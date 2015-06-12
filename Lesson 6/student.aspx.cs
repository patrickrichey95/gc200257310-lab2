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
    public partial class student : System.Web.UI.Page
    {
        Student s = new Student();
        Int32 StudentID = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            //if save wasnt clicked AND we have a StudentID in the url
            if ((!IsPostBack) && (Request.QueryString.Count > 0))
            {
                GetStudent();
            }
        }

        protected void GetStudent()
        {
            //populate form wih existing student record
            Int32 StudentID = Convert.ToInt32(Request.QueryString["StudentID"]);

            //connect to db via EF
            using (comp2007Entities db = new comp2007Entities())
            {
                Student s = (from objs in db.Students
                             where objs.StudentID == StudentID
                             select objs).FirstOrDefault();

                //map the student properties to the form controls
                txtLastName.Text = s.LastName;
                txtFirstMidName.Text = s.FirstMidName;
                txtEnrollmentDate.Text = s.EnrollmentDate.ToShortDateString();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //use EG to connect to SQL Server
            using (comp2007Entities db = new comp2007Entities())
            {
                //use the Student Model to save the new record
                

                //check the querystring for an id so we can determine add/update
                if(Request.QueryString["StudentID"] != null)
                {
                    //get the id from the url
                    StudentID = Convert.ToInt32(Request.QueryString["StudentID"]);

                    //get the current student from EF
                    s = (from objs in db.Students
                                 where objs.StudentID == StudentID
                                 select objs).FirstOrDefault();
                }

                s.LastName = txtLastName.Text;
                s.FirstMidName = txtFirstMidName.Text;
                s.EnrollmentDate = Convert.ToDateTime(txtEnrollmentDate.Text);

                //call add only if we have no student ID
                if(StudentID == 0)
                {
                    db.Students.Add(s);
                }

                db.SaveChanges();

                //redirect
                Response.Redirect("students.aspx");
            }

        }
    }
}