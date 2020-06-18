using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StudentSpaceAutomaticEducationPlan
{
    public partial class ProgramList1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GetProgramList();
        }

        public void GetProgramList()
        {
            int studentId = 0;

            DBCommon db = new DBCommon();
            using (SqlCommand cmd = new SqlCommand())
            {
                studentId = Convert.ToInt32(Session["StudentId"]);
                //cmd.CommandText = "SELECT ProgramId, ProgramName from tsProgram order by ProgramName Asc";

                cmd.CommandText = "sp_GetEduProgramListBrowse";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StudentID", studentId);

                DataTable dt = db.GetTable(cmd);
                ProgramList.DataSource = dt;
                ProgramList.DataBind();
            }           
            
        }

        string PrevHeadingAlpha = "";
        string HeadingAlpha = "";
        protected void ProgramList_ItemBound(object sender , DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView drv = e.Item.DataItem as DataRowView;

                Label lblHeading = (Label)e.Item.FindControl("lblHeading");                
                Label lblProgramName = (Label)e.Item.FindControl("lblProgramName");
              //  LinkButton lkbtnProgramName = (LinkButton)e.Item.FindControl("lkbtnProgramName");
                
                HiddenField hdnProgramId = (HiddenField)e.Item.FindControl("hdnProgramId");
                Panel pnlHeading = (Panel)e.Item.FindControl("pnlHeading");
                HyperLink aTag = (HyperLink)e.Item.FindControl("aTag");

                string programName = drv["ProgramName"].ToString();
                HeadingAlpha = programName.Substring(1, 1);

                if(PrevHeadingAlpha != HeadingAlpha)
                {
                    pnlHeading.Visible = true;
                    lblHeading.Text = HeadingAlpha;
                    lblHeading.Visible = true;
                    aTag.ID = "Target_"+HeadingAlpha;
                    aTag.Attributes.Add("name", HeadingAlpha);
                   // pnlHeading.ID = HeadingAlpha;
                }

                lblProgramName.Text = programName;
                //lkbtnProgramName.Text = programName;
                //lkbtnProgramName.Attributes.Add("onClick", "return false;");
                hdnProgramId.Value = drv["ProgramId"].ToString();

                PrevHeadingAlpha = HeadingAlpha;
            }
        }
    }
}