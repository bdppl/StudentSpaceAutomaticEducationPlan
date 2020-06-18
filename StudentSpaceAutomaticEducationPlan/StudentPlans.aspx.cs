using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace StudentSpaceAutomaticEducationPlan
{
    public partial class StudentPlans : System.Web.UI.Page
    {
        int studentId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["StudentId"] != null)
                {
                    studentId = Convert.ToInt32(Session["StudentId"]);
                    hdnStudentId.Value = studentId.ToString();
                    BindStudentInfo(studentId);
                    BindEduPlanList(studentId);
                }

                pnlCreatePlan.Visible = false;
                pnlPlans.Visible = true;
            }
            
        }

        private void BindStudentInfo(int StudentId)
        {            
            DBCommon db = new DBCommon();            
            SqlCommand cmd = new SqlCommand("select * from tsstudent where StudentId = @StudentId");
            cmd.Parameters.AddWithValue("@StudentId", StudentId); 

            DataTable studInfo = db.GetTable(cmd);
            if (studInfo != null)
            {
                lblSID.Text = studInfo.Rows[0]["SID"].ToString();
                lblName.Text = studInfo.Rows[0]["FirstName"].ToString() + " " + studInfo.Rows[0]["LastName"].ToString();
                lblHeaderName.Text = studInfo.Rows[0]["FirstName"].ToString() + " " + studInfo.Rows[0]["LastName"].ToString(); 
                LblDOB.Text =Convert.ToDateTime(studInfo.Rows[0]["DOB"]).ToString("MMM dd yyyy");
                LblEmailid.Text = studInfo.Rows[0]["Email"].ToString();
            }
        }

        private void BindEduPlanList(int StudentId)
        {
            DBCommon db = new DBCommon();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_GetEduPlantList";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StudentId", StudentId);

            DataTable dtplans = db.GetTable(cmd);
            if (dtplans != null)
            {
                DataList1.DataSource = dtplans;
                DataList1.DataBind();
            }
        }

        protected void DataListItemBound(object sender, DataListItemEventArgs e )
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView drv = e.Item.DataItem as DataRowView;

                string ProgramId = drv["ProgramId"].ToString();
                int TotalCrHr = Convert.ToInt32(drv["TotalCrHr"]);

                int totalPassCredit = GetTotalPassCredit(studentId, ProgramId);
                int remainingCredit = TotalCrHr - totalPassCredit;

                Label lblPassCredits = (Label)e.Item.FindControl("lblPassCredits");
                lblPassCredits.Text = totalPassCredit.ToString();

                Label lblRemCredit = (Label)e.Item.FindControl("lblRemCredit");
                lblRemCredit.Text = remainingCredit.ToString();

            }
        }

        private int GetTotalPassCredit(int StudentId, string ProgramId)
        {
            int Credits = 0; 
            DBCommon db = new DBCommon();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_GetTotalPassCredits";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@studentid", StudentId.ToString());
            cmd.Parameters.AddWithValue("@ProgramId", ProgramId);

            Credits = Convert.ToInt32(db.GetValue(cmd));
                    
            return Credits;
        }

        protected void btnAddPlan_Click(object sender, EventArgs e)
        {
            pnlCreatePlan.Visible = true;
            pnlPlans.Visible = false;

            BindCatalogYear();
        }

        protected void btnClosePlan_Click(object sender, EventArgs e)
        {
            pnlCreatePlan.Visible = false;
            pnlPlans.Visible = true;
        }

        protected void btnCreatePlan_Click(object sender, EventArgs e)
        {
            int yearId = Convert.ToInt32(ddlCatalogYear.SelectedValue);
            String ProgramId = hdnSelectedProgramId.Value;
            int studentId = Convert.ToInt32(hdnStudentId.Value);

            
            BuildPlan(yearId, ProgramId);
        }

        private void BuildPlan(int yearId, string ProgramId)
        {
            DBCommon db = new DBCommon();
            SqlCommand cmd = new SqlCommand();

            //        SqlParameter prmStudentId = new SqlParameter("@StudentId", GSpaceCommon.SessionMgr.snStudentId);
            //        SqlParameter prmProgramId = new SqlParameter("@ProgramId", ddlPrograms.SelectedValue);

            //        SqlDataReader dr = Dbobject._getDataReader("sp_GetPassCoursesCrhs", CommandType.StoredProcedure, prmStudentId, prmProgramId);
            //        while (dr.Read())
            //        {
            //            switch (dr["category"].ToString())
            //            {
            //                case "General":

            //                    lblGenPass.Text = Convert.ToString(dr["sumofcredithours"] != DBNull.Value ? Convert.ToInt32(dr["sumofcredithours"]) : 0);
            //                    lblGenRemn.Text = int.Parse(lblTotGeneralHrs.Text) > int.Parse(lblGenPass.Text) ? Convert.ToString(int.Parse(lblTotGeneralHrs.Text) - int.Parse(lblGenPass.Text)) : "0";
            //                    break;
            //                case "Major":

            //                    lblMajPass.Text = Convert.ToString(dr["sumofcredithours"] != DBNull.Value ? Convert.ToInt32(dr["sumofcredithours"]) : 0);
            //                    lblMajRemn.Text = int.Parse(lblTotMajorHrs.Text) > int.Parse(lblMajPass.Text) ? Convert.ToString(int.Parse(lblTotMajorHrs.Text) - int.Parse(lblMajPass.Text)) : "0";
            //                    break;

            //                case "Major (C)":

            //                    lblMajPassc.Text = Convert.ToString(dr["sumofcredithours"] != DBNull.Value ? Convert.ToInt32(dr["sumofcredithours"]) : 0);
            //                    lblMajRemnc.Text = int.Parse(lblTotMajorHrsc.Text) > int.Parse(lblMajPassc.Text) ? Convert.ToString(int.Parse(lblTotMajorHrsc.Text) - int.Parse(lblMajPassc.Text)) : "0";
            //                    break;
            //                case "Medullar":

            //                    lblMedPass.Text = Convert.ToString(dr["sumofcredithours"] != DBNull.Value ? Convert.ToInt32(dr["sumofcredithours"]) : 0);
            //                    lblMedRemn.Text = int.Parse(lblTotMedullarHrs.Text) > int.Parse(lblMedPass.Text) ? Convert.ToString(int.Parse(lblTotMedullarHrs.Text) - int.Parse(lblMedPass.Text)) : "0";
            //                    break;
            //                case "Free-Elective":

            //                    lblFreeElectPass.Text = Convert.ToString(dr["sumofcredithours"] != DBNull.Value ? Convert.ToInt32(dr["sumofcredithours"]) : 0);
            //                    lblFreeElectRemn.Text = int.Parse(lblTotFreeElctvHrs.Text) > int.Parse(lblFreeElectPass.Text) ? Convert.ToString(int.Parse(lblTotFreeElctvHrs.Text) - int.Parse(lblFreeElectPass.Text)) : "0";
            //                    break;
            //                case "Major-Elective":

            //                    lblMajElectPass.Text = Convert.ToString(dr["sumofcredithours"] != DBNull.Value ? Convert.ToInt32(dr["sumofcredithours"]) : 0);
            //                    lblMajElecRemn.Text = int.Parse(lblTotMajorElctvHrs.Text) > int.Parse(lblMajElectPass.Text) ? Convert.ToString(int.Parse(lblTotMajorElctvHrs.Text) - int.Parse(lblMajElectPass.Text)) : "0";
            //                    break;
            //                case "Social Science":

            //                    lblPsyPass.Text = Convert.ToString(dr["sumofcredithours"] != DBNull.Value ? Convert.ToInt32(dr["sumofcredithours"]) : 0);
            //                    lblSocSciRemn.Text = int.Parse(lblTotSocSciHrs.Text) > int.Parse(lblPsyPass.Text) ? Convert.ToString(int.Parse(lblTotSocSciHrs.Text) - int.Parse(lblPsyPass.Text)) : "0";
            //                    break;
            //                case "Psychology":

            //                    lblPsyPass.Text = Convert.ToString(dr["sumofcredithours"] != DBNull.Value ? Convert.ToInt32(dr["sumofcredithours"]) : 0);
            //                    lblPsyRemn.Text = int.Parse(lblTotPsyHrs.Text) > int.Parse(lblPsyPass.Text) ? Convert.ToString(int.Parse(lblTotPsyHrs.Text) - int.Parse(lblPsyPass.Text)) : "0";
            //                    break;
            //                case "Math and Science":

            //                    lblMathSciPass.Text = Convert.ToString(dr["sumofcredithours"] != DBNull.Value ? Convert.ToInt32(dr["sumofcredithours"]) : 0);
            //                    lblMathSciRemn.Text = int.Parse(lblTotMathSciHrs.Text) > int.Parse(lblMathSciPass.Text) ? Convert.ToString(int.Parse(lblTotMathSciHrs.Text) - int.Parse(lblMathSciPass.Text)) : "0";


            //                    lblTotPassedHrs.Text = Convert.ToString(Convert.ToInt32(lblGenPass.Text) + Convert.ToInt32(lblMajPass.Text) + Convert.ToInt32(lblMajPassc.Text) + Convert.ToInt32(lblMedPass.Text) + Convert.ToInt32(lblFreeElectPass.Text) + Convert.ToInt32(lblMajElectPass.Text) + Convert.ToInt32(lblPsyPass.Text) + Convert.ToInt32(lblPsyPass.Text) + Convert.ToInt32(lblMathSciPass.Text));
            //                    lblTotRemain.Text = Convert.ToString(Convert.ToInt32(lblGenRemn.Text) + Convert.ToInt32(lblMajRemn.Text) + Convert.ToInt32(lblMajRemnc.Text) + Convert.ToInt32(lblMedRemn.Text) + Convert.ToInt32(lblFreeElectRemn.Text) + Convert.ToInt32(lblMajElecRemn.Text) + Convert.ToInt32(lblSocSciRemn.Text) + Convert.ToInt32(lblPsyRemn.Text) + Convert.ToInt32(lblMathSciRemn.Text));
            //                    break;
            //            }
            //        }
            //        scon.Close();
            //    }
            //
        }


        private void BindCatalogYear()
        {
            DateTime currDate = DateTime.Now;
            string yearpart = currDate.Year.ToString();
            yearpart = yearpart.Substring(1);

            DBCommon db = new DBCommon();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT YearID, YearName From tlYearMaster where YearIndicator >= @YearPart";
            
            cmd.Parameters.AddWithValue("@YearPart", yearpart);

            DataTable yearDT = db.GetTable(cmd);
            if (yearDT != null)
            {
                ddlCatalogYear.DataSource = yearDT;
                ddlCatalogYear.DataTextField = "YearName";
                ddlCatalogYear.DataValueField = "YearID";

                ddlCatalogYear.DataBind();
            }
        }

        
        [WebMethod]
        public static List<ProgramList> GetProgramName(string SearchProgramTxt, string StudentId)
        {
            List<ProgramList> empResult = new List<ProgramList>();
           
            {
                DBCommon db = new DBCommon();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "sp_GetEduProgramListAutocom";
                    cmd.CommandType = CommandType.StoredProcedure;
                 
                    cmd.Parameters.AddWithValue("@SearchProgramTxt", SearchProgramTxt);
                    cmd.Parameters.AddWithValue("@StudentId", Convert.ToInt32(StudentId));

                    DataTable dt = db.GetTable(cmd);
                    foreach (DataRow dr in dt.Rows)
                    {
                        ProgramList pr = new ProgramList();
                        pr.ProgramId = dr["ProgramId"].ToString();
                        pr.ProgramName = dr["ProgramName"].ToString();
                        empResult.Add(pr);
                    }
                
                    return empResult;
                }
            }
        }

        protected void btnclick_Click(object sender, EventArgs e)
        {

        }
    }

    public class ProgramList
    {
        public string ProgramId { get; set; }
        public string ProgramName{ get; set; }
    }
}