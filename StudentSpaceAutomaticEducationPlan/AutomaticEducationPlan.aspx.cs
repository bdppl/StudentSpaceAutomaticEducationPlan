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
    public partial class AutomaticEducationPlan : System.Web.UI.Page
    {
        public int _studentId = 123;

        protected void Page_Load(object sender, EventArgs e)
        {
            BindStudentInfo(_studentId);   
            //BindProgramInfo();
            //BIndRequirements();
        }


        // Bind Student Information using studentid
        private void  BindStudentInfo(int studentId)
        {
            DBCommon db = new DBCommon();
            SqlCommand cmd = new SqlCommand();

            DataTable studInfo = db.GetTable(cmd);
            if(studInfo != null)
            {

            }
        }
    }
}