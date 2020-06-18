using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace StudentSpaceAutomaticEducationPlan
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void Search_Click1(object sender, EventArgs e)
        {
            DBCommon db = new DBCommon();
            SqlCommand cmd = new SqlCommand("select StudentId from tsstudent where SID=@SID ");
            cmd.Parameters.AddWithValue("@SID", TextBox1.Text);

            string StudentId = db.GetValue(cmd);
            Session.RemoveAll();
            if (StudentId != "")
            {
                Session["StudentId"] = Convert.ToInt32(StudentId);
                Response.Redirect("StudentPlans.aspx");
            }
            else
            {
                Label1.Text = "Your SID is incorrect";
                Label1.ForeColor = System.Drawing.Color.Red;

            }
        }
    }
}