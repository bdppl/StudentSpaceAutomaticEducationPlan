using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using PortalG1;
using System.Data.SqlClient;
using System.Text ;
using GSpaceCommon;
using System.Threading;
using System.IO;
namespace PortalG1
{
	public class AutomaticEducationPlan : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.DataGrid dgrDetails;
		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdSemesterID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdflag;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdSem;
		protected System.Web.UI.WebControls.Label lblrec;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdTopbar;
		protected System.Web.UI.HtmlControls.HtmlTableCell a;
		protected System.Web.UI.WebControls.Label lblError;
		protected System.Web.UI.WebControls.Label lblrowver;
		protected System.Web.UI.WebControls.DropDownList ddlcamp;
		protected System.Web.UI.WebControls.ListBox listroom;
		protected System.Web.UI.WebControls.DropDownList ddlclasstype;
		protected System.Web.UI.WebControls.DropDownList ddlequip;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.DropDownList ddlbuild;
		protected System.Web.UI.WebControls.TextBox txtcap;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.TextBox txtcredithours;
		protected System.Web.UI.WebControls.DropDownList ddlprogram;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.DropDownList ddlcampus;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.DropDownList ddlsem;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Panel Panel1;
		protected System.Web.UI.WebControls.Button btnAutoedplan;
		protected System.Web.UI.WebControls.CheckBoxList chkprogram;
		protected System.Web.UI.WebControls.DropDownList ddlDegree;
		protected System.Web.UI.WebControls.Button BtnView;
		protected System.Web.UI.HtmlControls.HtmlGenericControl divGrid;
		protected System.Web.UI.WebControls.Panel pnlProgress;
		protected const string ModuleID="ADMSEMMGR";
		protected System.Web.UI.WebControls.CheckBox chkAlert;
		protected System.Web.UI.WebControls.Label lblCohort;
		protected System.Web.UI.WebControls.CheckBoxList chkCohort;
		protected System.Web.UI.WebControls.Panel Panel2;
		SqlConnection scon=Dbobject.scon;
        string mailid;
		string mailid1;
		string sub;
		private void Page_Load(object sender, System.EventArgs e)
		{			                               
			if(!IsPostBack)
			{
				scon=Dbobject.OpenConnection();

				if(RightsManager.GetRightsByForm(ModuleID,GSpaceCommon.SessionMgr.snWorkGroupId)==AccessRights.Writable)
				{
					hdSemesterID.Value="-1";
					hdflag.Value="Add";
					FillDegree();					
					Fillsemester();					
					Fillcamp();
					FillCohort();
					btnAutoedplan.Attributes.Add("onclick","return validate()");
				}
				else
				{
					FillGrid();
					btnAutoedplan.Enabled=false;					
				}
			}
			else
			{
				if(hdSemesterID.Value!="-1")
				{
					lblError.Visible=false;
					lblrowver.Visible=false;
				}
			}
		}
		private void BindProgram()
		{
			if(ddlcampus.SelectedIndex==0)
			{
				chkprogram.Items.Clear();
				return;
			}
			
			string DegreeId=ddlDegree.SelectedValue;
			int CampusId=Convert.ToInt32(ddlcampus.SelectedValue);

			//string strQry="select distinct ProgramName,ProgramId from portal_vwprogramname where studentid in(select studentid from tsStudent where schoolid="+CampusId+") and (StartSemesterid<="+SessionMgr.snCurrentSemId+" and GradTermSemesterid>="+SessionMgr.snCurrentSemId+") and PrimaryProgram=1";
			string strQry="select distinct ProgramName,ProgramId from portal_vwprogramname where studentid in(select studentid from tsStudent where schoolid="+CampusId+") and PrimaryProgram=1";
			if(DegreeId!="0")
				strQry+=" and DegreeId='"+DegreeId+"'";
			strQry+=" order by ProgramName";			
						
			chkprogram.DataSource=Dbobject._getDataReader(strQry,CommandType.Text);
			chkprogram.DataTextField="ProgramName";
			chkprogram.DataValueField="ProgramId";
			chkprogram.DataBind();
		}
		public void FillDegree()
		{
			
			string str="select distinct DegreeId,(select Codename from tsmiscdetail where typecode=DegreeId) as DegreeName from tsProgramsInfo order by DegreeName";
			ddlDegree.DataSource=Dbobject._getDataReader(str,CommandType.Text);				
			ddlDegree.DataBind();
			ddlDegree.Items.Insert(0, new ListItem("-- Select Degree --","0"));
		}
		public void FillCohort()
		{
			try
			{			
				string strqry="select TypeCode,CodeName from portal_vwstudentcohort order by CodeName " ;					
				SqlDataReader  oreader=Dbobject._getDataReader(strqry,CommandType.Text);
				chkCohort.DataSource=oreader;
				chkCohort.DataValueField="TypeCode";
				chkCohort.DataTextField="CodeName";				
				chkCohort.DataBind();
				//chkprogram.Items.Insert(0,"-- Select Cohort --");
			}
			catch(Exception ex)
			{
				string ss=ex.Message;
			}
		}			
		# region fill campus dropdown
		private void Fillcamp()
		{
			try
			{
			
				string strqry="SELECT SchoolID,SchoolName FROM portal_vwDistrictschool where Blocked=0 order by schoolName " ;			
				
				SqlDataReader  oreader=Dbobject._getDataReader(strqry,CommandType.Text);
				ddlcampus.DataSource=oreader;
				ddlcampus.DataTextField="schoolName";
				ddlcampus.DataValueField="SchoolID";
				ddlcampus.DataBind();
				ddlcampus.Items.Insert(0,"-- Select Campus --");
			}
			catch(Exception ex)
			{
				string ss=ex.Message;
			}
		}
		# endregion		
		# region fill semester dropdown
		private void Fillsemester()
		{			
			SqlParameter prmCurrSem=new SqlParameter("@CurrentSem",GSpaceCommon.SessionMgr.snCurrentSemId);
				
			ddlsem.DataSource=Dbobject._getDataReader("get_futureSemester1",CommandType.StoredProcedure,prmCurrSem);	
			ddlsem.DataBind();
			ddlsem.Items.Insert(0,"-- Select Semester --");
		}
		# endregion
		
		protected void lnkViewPlan_Click(object sender, System.EventArgs e)
		{
			int semesterId= Convert.ToInt32(ddlsem.SelectedValue);
			string courseId= (sender as LinkButton).CommandArgument;
			string programIDs="";
			foreach(ListItem item in chkprogram.Items)
			{
				if(item.Selected)
				{
					if(programIDs!="")
						programIDs+="|";
					programIDs+=item.Value;
				}
			}
			Session["Programs"]=programIDs;			
			Page.RegisterStartupScript("ViewAutoPlan","<script type='text/javascript'>OpenPlanView('"+semesterId+"','"+courseId+"');</script>");

		}

		private void ShowHide()
		{
			pnlProgress.Visible=true;
			divGrid.Visible=false;
		}
		
		private void btnAutoedplan_Click(object sender, System.EventArgs e)
		{
			
			string programIDs="";
			int semesterId= Convert.ToInt32(ddlsem.SelectedValue);			
			BtnView.Enabled=false;						
			
			// Checks for programs individually if planning has already done for students in selected semester....
			foreach(ListItem item in chkprogram.Items)
			{
				if(item.Selected)
				{			
					if(isCreated(item.Value,semesterId,int.Parse(ddlcampus.SelectedValue)))
					{
						//Page.RegisterStartupScript("Created","<script language='javascript'>alert(\"Automatic Education Plan has already been created in semester for program - "+item.Value+"\");</script>");
						Page.RegisterStartupScript("Created","<script language='javascript'>alert(\"Automatic Education Plan has again created in semester for program - "+item.Value+"\");</script>");
						//BtnView.Enabled=true;						
						//return;
					}
					if(programIDs!="")
						programIDs+="|";
					programIDs+=item.Value;
				}
			}
			     createautoplanforfailed(programIDs);
				CreateAutoPlan(programIDs);	
			
		}






		private bool isPreCreated(string ProgramId,string semName,string year)
		{
			bool isExist=false;			

			string str="select count(*)as TotalRecords from tsEduPlanAhead where programid='"+ProgramId+"' and semesterId=(select semesterId from tssemesterInfo where Semester='"+semName+"' and YearNo='"+year+"')  and isauto=1";
			SqlDataReader dr=Dbobject._getDataReader(str,CommandType.Text);
			dr.Read();
			if(Convert.ToInt32(dr["TotalRecords"])>0)
				isExist=true;
			dr.Close();
			
			return isExist;
		}
		/// <summary>
		/// Check whether Automatic Education Plan created or not for particular program. 
		/// </summary>
		/// <param name="ProgramId"></param>
		/// <param name="semesterId"></param>
		/// <param name="SchoolID"></param>
		/// <returns></returns>
		private bool isCreated(string ProgramId,int semesterId,int SchoolID)
		{
			bool isExist=false;			
			string str="select count(*)as TotalRecords from tsEduPlanAhead where programid='"+ProgramId+"' and semesterId="+semesterId+" and studentid in (select studentid from tsStudent where schoolid="+SchoolID+") and isauto=1";
			SqlDataReader dr=Dbobject._getDataReader(str,CommandType.Text);
			dr.Read();
			if(Convert.ToInt32(dr["TotalRecords"])>0)
				isExist=true;
			dr.Close();
			
			return isExist;
		}
		private DataTable getPTBySemester(string ProgramId,int DayEveStatus)
		{			
			// No SemesterId in tlPartTerm that's why taking for Semester Text....
			SqlParameter prmSemester=new SqlParameter("@SemesterName",ddlsem.SelectedItem.Value); // CodeType for Auto Alert Type (In General Alert)...
			SqlParameter prmProgram=new SqlParameter("@programid",ProgramId);
			SqlParameter prmSchoolId=new SqlParameter("@CampusId",ddlcampus.SelectedItem.Value);
			string str="select distinct PartTerm as PTName,PartTerm+ isnull(case [Time] when 0 then ' [D]' when 1 then ' [E]' else '' end,'') as PartTerm ,PartTermId from vw_getPartTerm where SemesterId="+ddlsem.SelectedItem.Value+" and programid='"+ProgramId+"' and CampusId="+ddlcampus.SelectedItem.Value+" and Blocked=0 and Time="+ DayEveStatus+" order by PartTermId  ";
			//DataTable tbl= Dbobject._getDataset("proc_getSemester_New",CommandType.StoredProcedure,prmSemester,prmProgram,prmSchoolId).Tables[0];								
			DataTable tbl= Dbobject._getDataset(str,CommandType.Text).Tables[0];								
			//DataTable tbl= Dbobject._getDataset("select PartTerm,PartTermId from tlPartTerm where SemesterName='"+ddlsem.SelectedItem.Text+"' order by parttermid ",CommandType.Text).Tables[0];			
			return tbl;
		}			
		private int getSTudentDayorEveningStatus(int Studentid,int CurrentSem)
		{
			//Method to Check The Day and Evening Status of Student 
			SqlConnection sqlnewcon=Dbobject.getconnection();
			sqlnewcon.Open();
			int Status=0;
			try
			{
				SqlParameter prmStudentId=new SqlParameter("@StudentID",Studentid);
				SqlParameter prmSemID=new SqlParameter("@currSemID",CurrentSem);
				SqlParameter prmStatus=new SqlParameter("@Status",SqlDbType.Int);
				prmStatus.Direction=ParameterDirection.Output;

				// return all the students in selected campus taking these programs in current semester with the catalogsemester applicable.
				Dbobject._executenonquery("sp_GetStudentDayEvenStatus",sqlnewcon,CommandType.StoredProcedure,prmStudentId,prmSemID,prmStatus);	
				Status=Convert.ToInt32(prmStatus.Value);
			}
			catch(Exception ex)
			{
				GSpaceCommon.ExceptionHandler.Handle(ex);
			}
			finally
			{

				sqlnewcon.Close();	
				sqlnewcon.Dispose();
			}
			return Status;
		}
		private void fnInsertUpdateStudnetDayEvening(int CurrrentSem)
		{
			//Function to Insert all the student Day and Evening Details of Current Semester
			SqlConnection sqlnewcon=Dbobject.getconnection();
			sqlnewcon.Open();
			try
			{
				SqlParameter prmCurrentSem=new SqlParameter("@SemesterID",SqlDbType.Int);
				SqlParameter prmErrormsg=new SqlParameter("@Errormsg",SqlDbType.VarChar,500);

				prmCurrentSem.Value=CurrrentSem;
                prmErrormsg.Value="";
				prmErrormsg.Direction=ParameterDirection.Output;
				int sucess=Dbobject._executenonquery("uspUpdateSemester",sqlnewcon,CommandType.StoredProcedure,prmCurrentSem,prmErrormsg);	

				string status=prmErrormsg.Value.ToString();
			}
			catch(Exception ex)
			{
				GSpaceCommon.ExceptionHandler.Handle(ex);
			}
			finally
			{
				sqlnewcon.Close();	
				sqlnewcon.Dispose();
			}
		}

		private void createautoplanforfailed(string programIDs)
		{
			fnInsertUpdateStudnetDayEvening(Convert.ToInt32(SessionMgr.snCurrentSemId));
			string strXml="";
			string strCohort="";
			/*swat*/
			foreach(ListItem lst in chkCohort.Items)
			{
				if(lst.Selected)
				{
					strCohort+=lst.Value+",";                   
				}              			     
			}
			if(strCohort.Length>1)
			{
				strCohort = strCohort.Substring(0,strCohort.Length -1);
				strCohort = strCohort.Replace(",","#,#");
				strCohort= "#"+strCohort+"#";
			}			
			string strStudent="";
			string NotCreated="";
			string StudentCohort="";
			SqlParameter prmProgram=new SqlParameter("@ProgramIDs",programIDs);
			SqlParameter prmSemID=new SqlParameter("@currSemID",SessionMgr.snCurrentSemId);
			SqlParameter prmSchoolID=new SqlParameter("@SchoolID",ddlcampus.SelectedValue);
			SqlParameter prmCohort=new SqlParameter("@Cohort",strCohort);
			/*----------------------  made by swat ---------------------*/

			

			DataTable tbl= Dbobject._getDataset("sp_getStudentsByProgram",CommandType.StoredProcedure,prmProgram,prmSemID,prmSchoolID,prmCohort).Tables[0];	

			// if Catalogue has not created or Students not available for selected program and semester
			// Then It exit with message display....
			if(tbl.Rows.Count==0)
			{
				Page.RegisterStartupScript("AutoError","<script language='javascript'>alert('Program Configuration has not been created.\\n\\rOr Students not found for this program(s) and Campus.');</script>");
				return;
			}
			DataTable tblSummary=new DataTable();
			tblSummary.Columns.Add("StudentID");
			tblSummary.Columns.Add("Total");
			tblSummary.Columns.Add("CourseNo");
			tblSummary.Columns.Add("CourseName");
			tblSummary.Columns.Add("CourseId");
			tblSummary.Columns.Add("CRS");
			tblSummary.Columns.Add("PartTermId");
			tblSummary.Columns.Add("SemesterId");
			tblSummary.Columns.Add("IsPassingGrade");
			tblSummary.Columns.Add("TotalStudent");
			tblSummary.Columns.Add("PartTerm");
			tblSummary.Columns.Add("CategoryId");
			tblSummary.Columns.Add("progCatalogId");
			DataTable tblPrgCtlgCourses=null;

			int RowsAffected=0;
			bool Send=false;	// If Alert sent or not........

			string PrevProgramId="";
			int PrevCatlogSemesterID=0;			
			strXml="<ROOT>";
			string str1="";
			foreach(DataRow row in tbl.Rows)
			{
					int CheckGraduatetStudent=0;
				string Xml="<ROOT>";
				int PTNo=0;
				int Studentid= Convert.ToInt32(row["Studentid"]);
				//int Studentid=54047;

				//if(Studentid!=40628)	// Only for testing....
				//					continue;
				
				string ProgramId=row["ProgramID"].ToString();			
				string Pid=row["PID"].ToString();

				// Get Cohort detail for particular student				
				StudentCohort=fnGetStudentCohortdetail(Studentid);

				//getSTudentDayorEveningStatus() Method to Check The Day and Evening Status of Student 
				int DayEveStudentStatus=getSTudentDayorEveningStatus(Studentid,Convert.ToInt32(SessionMgr.snCurrentSemId));
				DataTable tblPT=getPTBySemester(ProgramId,DayEveStudentStatus);
				if(tblPT.Rows.Count!=0)
				{
					int CatlogSemesterID=0;
					if(row["CatalogSemesterID"].ToString()!="")
					{
						CatlogSemesterID=Convert.ToInt32(row["CatalogSemesterID"]);
					}				    
					// Multiple students may have in same Semester catalogue
					// so don't need to retrieve catalogue courses each time individually.
					// Thus Checks with PrevProgramId and PrevCatlogSemesterID...

					if(PrevProgramId!=ProgramId || PrevCatlogSemesterID!=CatlogSemesterID)
					{	
						// Get Program Catalogue Detail
						tblPrgCtlgCourses=GetPrgCtlgCourses(ProgramId,CatlogSemesterID,Studentid);


						//tblPrgCtlgCourses=GetPrgCtlgCourses(ProgramId,CatlogSemesterID);
						PrevProgramId=ProgramId;
						PrevCatlogSemesterID=CatlogSemesterID;
					}

					int PreprogCatalogId=0;
					int TotalCRS=0;
					int TotalConsumedCRS=0;
				
					// Get the Pre-Planned courses for Student and Program.
					DataTable tblPlannedCourses=GetPlannedCourses(Studentid,ProgramId);
					DataTable tblfailedcourse=Getfailedcourse(ProgramId,CatlogSemesterID,Studentid);
					DataRow[] TotalCRSinSemester=tblPlannedCourses.Select("semesterid="+ddlsem.SelectedValue);

					for(int i=0;i<TotalCRSinSemester.Length;i++)
					{
						TotalConsumedCRS+= Convert.ToInt32(TotalCRSinSemester[i]["CreditHrsE"]);
					}
					foreach(DataRow CtlgRow in tblfailedcourse.Rows)
					{
						if(StudentCohort=="Special Cohort")
						{
							if(TotalCRS>=(15-TotalConsumedCRS))
								break;
						}
						else
						{
							if(TotalCRS>=(12-TotalConsumedCRS) || TotalCRS>=(14-TotalConsumedCRS))
								break;
						}
						// Total Credit Hours of courses taken for each student should not be greater than 12 or 14...
						int a=Convert.ToInt32(CtlgRow["courseid"]);
						DataRow[] failed=tblPrgCtlgCourses.Select("courseid="+a);

                          int progCatalogId=Convert.ToInt32(failed[0]["ProgramCatDtlId"].ToString());

						if(progCatalogId!=PreprogCatalogId )//tblSummary.Select("progCatalogId='"+progCatalogId+"'")
						{

						
							
							//int progCatalogId= Convert.ToInt32(CtlgRow["ProgramCatDtlId"]);


							
							
							string categoryId=failed[0]["categoryId"].ToString();
							if(categoryId!="CRSCAT00004")
							{
								// Always will take the first course from list...
								int RowIndex=0;
								string courseNo="";
								int courseId=0;
								string categoryName="";													 

								// check for each course in group if already taken or not
								// if not taken any one then take the first one
								// if any found in group already taken then skip to next programcatalogid courses.
								bool isfound=false;
								if(categoryId.CompareTo("CRSCAT00005")<0)
								{
									courseNo=failed[0]["CourseNo"].ToString();										
									categoryName=failed[0]["category"].ToString();
									isfound=false;
							
								}
								if(!isfound)
								{									
									courseNo=failed[0]["CourseNo"].ToString();
									courseId= a;
									categoryName=failed[0]["category"].ToString();
									CheckGraduatetStudent=1;
								}
								else
								{
									PreprogCatalogId=progCatalogId;
									continue;
								}
								int TotalMEHours=Convert.ToInt32(failed[0]["FreeMajorElecHrs"]);	
								if(categoryId.CompareTo("CRSCAT00005")>=0)
								{
									DataTable dtMECourses=tblfailedcourse.Clone();

									DataRow[] MERows=tblfailedcourse.Select("categoryId='"+categoryId+"'");
									
									if(MERows.Length==0)
									{
									
										courseId= Convert.ToInt32(failed[0]["CourseId"]);
										//DataRow[] PlannedRows=tblPlannedCourses.Select("CourseId="+courseId+" and courseScheduleId is not null");	
									 
										DataRow[] MECourseWithoutCategory=tblfailedcourse.Select("CourseId="+courseId);
										if(MECourseWithoutCategory.Length>0)
										{
											dtMECourses.ImportRow(MECourseWithoutCategory[MECourseWithoutCategory.Length-1]);
											//Array.Copy(MECourseWithoutCategory,MECourseWithoutCategory.Length-1,MERows,MERows.Length-1,1);
											//MECourseWithoutCategory.CopyTo(MERows,MERows.Length);
											
										}
									}
									else
									{
										for(int i=0;i<MERows.Length;i++)
										{
											dtMECourses.ImportRow(MERows[MERows.Length-1]);
										}
									}
									int TotalPlannedME=0;
									int flagCheckCoursePlannedForME=0;
									for(int k=0;k<dtMECourses.Rows.Count;k++)
									{
										//										string courseID= dtMECourses.Rows[k]["CourseId"].ToString();
										//
										//										DataRow[] coursesInCatalog=tblPrgCtlgCourses.Select("CourseId="+courseID);
										//
										//										if(drCourses.Length>0)
										TotalPlannedME+=Convert.ToInt32(dtMECourses.Rows[k]["CreditHrsE"]);
										
										if(Convert.ToInt32(dtMECourses.Rows[k]["ProgramCatDtlId"]==DBNull.Value?0:dtMECourses.Rows[k]["ProgramCatDtlId"])== progCatalogId)
										{
											flagCheckCoursePlannedForME=1;
										}
									}
									if(flagCheckCoursePlannedForME==1)
										continue;
									flagCheckCoursePlannedForME=0;
									DataRow[] MESTudentRows=tblSummary.Select("StudentId='"+Studentid+"' and CategoryId='"+categoryId+"'");

										
									for(int l=0;l<MESTudentRows.Length;l++)
									{
										TotalPlannedME+=Convert.ToInt32(MESTudentRows[l]["CRS"].ToString());
										if(Convert.ToInt32(MESTudentRows[l]["progCatalogId"])== progCatalogId)
										{
											flagCheckCoursePlannedForME=1;
										}
									}
									if(flagCheckCoursePlannedForME==1)
										continue;
									int MEHrsToBeTaken=TotalMEHours-TotalPlannedME;

									if(MEHrsToBeTaken>0 )
									{

										CheckGraduatetStudent=1;
										// If Major Elective already Planned then check for next courses in Major List.
										if(dtMECourses.Rows.Count>0)
										{
											string METaken="";
											for(int j=0;j<dtMECourses.Rows.Count;j++)
											{
												if(METaken!="")
													METaken+=",";
												METaken+=dtMECourses.Rows[j]["CourseNo"].ToString();
											}
											for(int i=0;i<failed.Length;i++)
											{	
												string majorCourse=failed[i]["CourseNo"].ToString();
													
												//Check here for same course and Program catlog id if matched the skip other wise taken
												//if(Convert.ToInt32(MERows[k]["ProgramCatDtlId"])!= progCatalogId ||Convert.ToInt32(MESTudentRows[k]["ProgramCatDtlId"])!= progCatalogId)
												if(METaken.IndexOf(majorCourse)<0)
												{
													courseNo=majorCourse;
													courseId=Convert.ToInt32(failed[i]["CourseId"]);
													categoryName=failed[0]["category"].ToString();
													RowIndex=i;	// RowIndex is used to read table data...
													break;
												}											
											}
										}
										else
										{
											string METaken="";
											for(int j=0;j<dtMECourses.Rows.Count;j++)
											{
												if(METaken!="")
													METaken+=",";
												METaken+=dtMECourses.Rows[j]["CourseNo"].ToString();
											}
											//Check here for same course and Program catalog id if matched the bhaag other wise taken

											// ME courses may be in current Planning Table..So check for duplicacy...
											for(int i=0;i<failed.Length;i++)
											{	
												string majorCourse=failed[i]["CourseNo"].ToString();
												if(tblSummary.Select("StudentId='"+Studentid+"' and CourseNo='"+majorCourse+"'").Length==0)
												{
													courseNo=majorCourse;
													courseId=Convert.ToInt32(failed[i]["CourseId"]);
													categoryName=failed[0]["category"].ToString();
													RowIndex=i;	// RowIndex is used to read table data...
													break;
												}											
											}
										}
									}
									else
									{
										continue;
									}	
							
								}
								if(PTNo==tblPT.Rows.Count)
									PTNo=0;
									
								int PTID= Convert.ToInt32(tblPT.Rows[PTNo]["PartTermId"]);
								string PTName=tblPT.Rows[PTNo]["PartTerm"].ToString();		

								bool isValid=true;
								string PreCoGroupId="";
								if(failed[0]["PreCoGroupId"]!=DBNull.Value)
								{
									isValid=false;
									//int ReqGroupID= Convert.ToInt32(drCourses[RowIndex]["PreCoGroupId"]);
									DataTable dt=new DataTable();
									SqlParameter prmCourse=new SqlParameter("@CouseNos",SqlDbType.VarChar,100);
									prmCourse.Value=courseNo;
									SqlParameter prmprogramid=new SqlParameter("@ProgramID",SqlDbType.VarChar,100);
									prmprogramid.Value=ProgramId;
									dt=Dbobject._getDataset("sp_getPreCoDetailsByCourse",CommandType.StoredProcedure,prmCourse,prmprogramid).Tables[0];
									/*Add condition by Sandeep sir for check for fail course on 27Dec2012*/
									
									// Added by Lalit on 20-07-2013
									// if precogroupid exist but restricted for this program then above dt return nothing...
									// and this course should be planned by ignoring precogroup details...
									if(dt.Rows.Count==0)
										isValid=true;

									int groupidp=0;
									int groupidc=0;	
									foreach(DataRow dr in dt.Rows)
									{
										groupidc=Convert.ToInt32(dr["GroupId"]);
										if (isValid==true && groupidc!=groupidp)
										{
											break;	
										}
										else if((isValid==true && groupidp==groupidc) || groupidp==0 )
										{
											string reqCourse=dr["GroupCourse"].ToString();		
											// Whether Pre-Co Match or not....
											if(!IsRequisiteMatch(Studentid, reqCourse, Convert.ToBoolean(dr["IsPre"]), int.Parse(ddlsem.SelectedValue), ref PTID,tblfailedcourse,tblSummary,ref PTName,tblPT))
											{	
												isValid=false;
												groupidp=groupidc;
												// break;
											}
											else
											{
												PreCoGroupId=dr["GroupId"]!=DBNull.Value? string.Format("{0:N0}",dr["GroupId"]):"0";
												isValid=true;
												groupidp=groupidc;
											}
										}
										else if (groupidc!=groupidp)
										{
											groupidp=groupidc;
											string reqCourse=dr["GroupCourse"].ToString();		
											// Whether Pre-Co Match or not....
											if(!IsRequisiteMatch(Studentid, reqCourse, Convert.ToBoolean(dr["IsPre"]), int.Parse(ddlsem.SelectedValue), ref PTID,tblPlannedCourses,tblSummary,ref PTName,tblPT))
											{	
												isValid=false;
												groupidp=groupidc;
												// break;  
											}
											else
											{
												PreCoGroupId=dr["GroupId"]!=DBNull.Value? string.Format("{0:N0}",dr["GroupId"]):"0";
												isValid=true;							
											}
										}
										/*-------------End Condition-------------*/										 
									}		
								}

								if(isValid)
								{											
									if(!IsCourseTakenForOtherPreGroup(courseNo,tblfailedcourse,tblSummary,tblPrgCtlgCourses))
									{
										string CRS= failed[0]["CRS"]!=DBNull.Value? string.Format("{0:N0}",failed[0]["CRS"]):"0";			
										TotalCRS+=int.Parse(CRS);
										if(StudentCohort=="Special Cohort")
										{
											if((TotalCRS<=(15-TotalConsumedCRS)))
											{
												string courseName=failed[0]["CourseName"].ToString();												

												string s1= "<tsEduPlanAhead StudentID=#@StudentID# CourseNo=#@CourseNo# SemesterID=#@SemesterID# ProgramID=#@ProgramID# PartTermID=#@PartTermID# CdtHrs=#@CdtHrs# ReqGroupID=#@ReqGroupID# ProgramCatDtlId=#@ProgramCatDtlId# isAuto=#@isAuto#  />";
												s1=s1.Replace("@StudentID",Studentid.ToString());
												s1=s1.Replace("@CourseNo",courseNo);
												s1=s1.Replace("@SemesterID",ddlsem.SelectedValue);
												s1=s1.Replace("@ProgramID",ProgramId);							
												//s1=s1.Replace("@PartTermID",tblPT.Rows[PTNo]["PartTermId"].ToString());
												s1=s1.Replace("@PartTermID",PTID.ToString());
												//s1=s1.Replace("@PartTermID","656");													
												s1=s1.Replace("@CdtHrs",CRS);
												s1=s1.Replace("@ReqGroupID",PreCoGroupId);
												s1=s1.Replace("@ProgramCatDtlId",progCatalogId.ToString());
												s1=s1.Replace("@isAuto","1");
												Xml+=s1;
												DataRow[] drSummary=tblSummary.Select("CourseNo='"+courseNo+"'");
													
												DataRow newRow=tblSummary.NewRow();
												newRow["StudentID"]=Studentid.ToString();
												newRow["Total"]="1";
												newRow["CourseNo"]=courseNo;
												newRow["CourseName"]=courseName;
												newRow["CourseId"]=courseId;
												newRow["CRS"]=CRS;
												//newRow["PartTermId"]=tblPT.Rows[PTNo]["PartTermId"].ToString();
												newRow["PartTermId"]=PTID.ToString();
												//newRow["PartTermId"]="656";
												newRow["SemesterId"]=ddlsem.SelectedValue;
												newRow["IsPassingGrade"]="";
												newRow["TotalStudent"]=tbl.Rows.Count;
												//newRow["PartTerm"]="1";
												//newRow["PartTerm"]=tblPT.Rows[PTNo]["PartTerm"].ToString();
												newRow["PartTerm"]=PTName;
												newRow["CategoryId"]=categoryId;
												newRow["progCatalogId"]=progCatalogId;
												tblSummary.Rows.Add(newRow);
												//}	
												PTNo++;
												 
											}
											else
											{
												TotalCRS=TotalCRS-int.Parse(CRS);
												continue;
											}													
										}
										else
										{
											if((TotalCRS<=(14-TotalConsumedCRS)))
											{
												string courseName=failed[0]["CourseName"].ToString();												

												string s1= "<tsEduPlanAhead StudentID=#@StudentID# CourseNo=#@CourseNo# SemesterID=#@SemesterID# ProgramID=#@ProgramID# PartTermID=#@PartTermID# CdtHrs=#@CdtHrs# ReqGroupID=#@ReqGroupID# ProgramCatDtlId=#@ProgramCatDtlId# isAuto=#@isAuto#  />";
												s1=s1.Replace("@StudentID",Studentid.ToString());
												s1=s1.Replace("@CourseNo",courseNo);
												s1=s1.Replace("@SemesterID",ddlsem.SelectedValue);
												s1=s1.Replace("@ProgramID",ProgramId);												 
												s1=s1.Replace("@PartTermID",PTID.ToString());
												//s1=s1.Replace("@PartTermID","656");													
												s1=s1.Replace("@CdtHrs",CRS);
												s1=s1.Replace("@ReqGroupID",PreCoGroupId);
												s1=s1.Replace("@ProgramCatDtlId",progCatalogId.ToString());
												s1=s1.Replace("@isAuto","1");
												Xml+=s1;										 
												DataRow[] drSummary=tblSummary.Select("CourseNo='"+courseNo+"'");
													
												DataRow newRow=tblSummary.NewRow();
												newRow["StudentID"]=Studentid.ToString();
												newRow["Total"]="1";
												newRow["CourseNo"]=courseNo;
												newRow["CourseName"]=courseName;
												newRow["CourseId"]=courseId;
												newRow["CRS"]=CRS;
												//newRow["PartTermId"]=tblPT.Rows[PTNo]["PartTermId"].ToString();
												newRow["PartTermId"]=PTID.ToString();
												//newRow["PartTermId"]="656";
												newRow["SemesterId"]=ddlsem.SelectedValue;
												newRow["IsPassingGrade"]="";
												newRow["TotalStudent"]=tbl.Rows.Count;
												newRow["PartTerm"]=PTName;
												newRow["CategoryId"]=categoryId;
												newRow["progCatalogId"]=progCatalogId;
												tblSummary.Rows.Add(newRow);											
												//}	
												 
											
												PTNo++;
												
											}
											else
											{
												TotalCRS=TotalCRS-int.Parse(CRS);
												continue;
											}
										}
									}
								}
							}
							PreprogCatalogId=progCatalogId;
						}
					}
					Xml+="</ROOT>";			
					// If any coused added to be inserted...
					if(Xml.IndexOf("tsEduPlanAhead")> 0)
					{
						SqlConnection sqlnewcon=Dbobject.getconnection();
						sqlnewcon.Open();					
						SqlParameter prmXml=new SqlParameter("@XmlEduPlanAhead",Xml);					
						RowsAffected= Dbobject._executenonquery("sp_InsertAutoEduPlanAhead",sqlnewcon,CommandType.StoredProcedure,prmXml);								
						sqlnewcon.Close();	
						sqlnewcon.Dispose();					
					}
				}
				else
				{
					if(NotCreated=="")
						NotCreated=Pid+",";
					int c=NotCreated.IndexOf(Pid);
					if(NotCreated.IndexOf(Pid)==-1)
						NotCreated+=Pid+",";
				}
			}
			// End of main loop for single semester assignment...
			/*Create xml and procedure sp_InsertAEPRejectedRecords by Avinash on 16/12/2012*/
			if(strXml!="<ROOT>")
			{
				strXml+="</ROOT>";	
				SqlConnection sqlnewcon=Dbobject.getconnection();
				sqlnewcon.Open();					
				SqlParameter prmXml=new SqlParameter("@XmlAEPRejectedRecords",strXml);					
				int RowsEffected= Dbobject._executenonquery("sp_InsertAEPRejectedRecords",sqlnewcon,CommandType.StoredProcedure,prmXml);								
				sqlnewcon.Close();	
				sqlnewcon.Dispose();
			}
			/*-------------- End -------------------*/							
					
			if(RowsAffected>0)
			{		
				// This Function plannned the Labs of Currently Planned Courses   
				// If Labs are exist in Program Catalogue then it will planned the Labs other wise not.
				fnInsertCoursesLabDetails(Convert.ToInt32(ddlsem.SelectedItem.Value));

				SqlParameter prmProgramIDs=new SqlParameter("@ProgramIDs",programIDs); // CodeType for Auto Alert Type (In General Alert)...
				SqlParameter prmSemesterID=new SqlParameter("@SemesterID",ddlsem.SelectedItem.Value);			
				SqlParameter prmCampusID=new SqlParameter("@CampusID",ddlcampus.SelectedValue);		
				
				//string strQuery="select COunt(studentid) as Total,e.courseid,c.CourseNo,C.courseName from tseduplanAhead as e inner join tscourse as c on e.courseid=c.courseid	where semesterid="+ddlsem.SelectedItem.Value+" and isauto=1 group by e.courseid,c.courseno,C.courseName order by courseno";
				DataSet ds=Dbobject._getDataset("ppGetAEPPlannedSummary",CommandType.StoredProcedure,prmProgramIDs,prmSemesterID,prmCampusID);
				
				dgrDetails.DataSource=ds.Tables[0];
				dgrDetails.DataBind();

				//Page.RegisterStartupScript("AutoSaved","<script language='javascript'>alert('Automatic Education Plan created successfully.');</script>");
				if(NotCreated!="")
					//Page.RegisterStartupScript("AutoSaved","<script language='javascript'>alert(\"Automatic Education Plan created successfully.\\n\\rProgram's whose partterm not defined- "+NotCreated.Trim(',')+"\");</script>"); swat
				//else
					//Page.RegisterStartupScript("AutoSaved","<script language='javascript'>alert('Automatic Education Plan created successfully.');</script>");
				//lblError.Text="Program's whose partterm not defined- "+NotCreated.Trim(',');
				//Page.RegisterStartupScript("Created","<script language='javascript'>alert(\"Automatic Education Plan has not been created for Previous semester - "+semName+" "+year+" for program - "+item.Value+"\");</script>");
				
				//lblrec.Text= "Total Students= "+ ds.Tables[1].Rows[0][0].ToString();
				BtnView.Enabled=true;

				//if(Send)
				//{
					//Page.RegisterStartupScript("LessCredit","<script language='javascript'>alert('An Alert has been sent to Admin, Advisior and Counselor for students who has Less than 12 Credits.');</script>");
				//}
			}
			else
			{
				BtnView.Enabled=false;
				//if(NotCreated!="")
					//Page.RegisterStartupScript("AutoSaved","<script language='javascript'>alert(\"Program's whose partterm not defined- "+NotCreated.Trim(',')+"\");</script>");	
				//else
					//Page.RegisterStartupScript("AutoError","<script language='javascript'>alert('Error Occured in Automatic Education Plan Creation.');</script>");
				//Page.RegisterStartupScript("AutoError","<script language='javascript'>alert('No New Students are Avaliable To Create Automatic Education Plan.');</script>");
			}
	
		}
		private void CreateAutoPlan(string programIDs)
		{		
			//Function is used to Insert all the students Day and Evening Details of Current Semester
			fnInsertUpdateStudnetDayEvening(Convert.ToInt32(SessionMgr.snCurrentSemId));
			string strXml="";
			string strCohort="";
			/*Add New parameter Cohort by Avinash on 13/12/12 and change in proc sp_getStudentsByProgram also*/
			foreach(ListItem lst in chkCohort.Items)
            {
				if(lst.Selected)
				{
				  strCohort+=lst.Value+",";                   
				}              			     
			}
			if(strCohort.Length>1)
			{
				strCohort = strCohort.Substring(0,strCohort.Length -1);
				strCohort = strCohort.Replace(",","#,#");
				strCohort= "#"+strCohort+"#";
			}			
			string strStudent="";
			string NotCreated="";
			string StudentCohort="";
			SqlParameter prmProgram=new SqlParameter("@ProgramIDs",programIDs);
			SqlParameter prmSemID=new SqlParameter("@currSemID",SessionMgr.snCurrentSemId);
			SqlParameter prmSchoolID=new SqlParameter("@SchoolID",ddlcampus.SelectedValue);
			SqlParameter prmCohort=new SqlParameter("@Cohort",strCohort);
            /*---------------------- Eng Changes by Avinash Singh ---------------------*/

			/*return all the students in selected campus taking these programs in current semester with the catalogsemester applicable.*/

			DataTable tbl= Dbobject._getDataset("sp_getStudentsByProgram",CommandType.StoredProcedure,prmProgram,prmSemID,prmSchoolID,prmCohort).Tables[0];	

			// if Catalogue has not created or Students not available for selected program and semester
			// Then It exit with message display....
			if(tbl.Rows.Count==0)
			{
				Page.RegisterStartupScript("AutoError","<script language='javascript'>alert('Program Configuration has not been created.\\n\\rOr Students not found for this program(s) and Campus.');</script>");
				return;
			}
			DataTable tblSummary=new DataTable();
			tblSummary.Columns.Add("StudentID");
			tblSummary.Columns.Add("Total");
			tblSummary.Columns.Add("CourseNo");
			tblSummary.Columns.Add("CourseName");
			tblSummary.Columns.Add("CourseId");
			tblSummary.Columns.Add("CRS");
			tblSummary.Columns.Add("PartTermId");
			tblSummary.Columns.Add("SemesterId");
			tblSummary.Columns.Add("IsPassingGrade");
			tblSummary.Columns.Add("TotalStudent");
			tblSummary.Columns.Add("PartTerm");
			tblSummary.Columns.Add("CategoryId");
			tblSummary.Columns.Add("progCatalogId");
			DataTable tblPrgCtlgCourses=null;

			int RowsAffected=0;
			bool Send=false;	// If Alert sent or not........

			string PrevProgramId="";
			int PrevCatlogSemesterID=0;			
			strXml="<ROOT>";
			string str1="";
			foreach(DataRow row in tbl.Rows)
			{
				int CheckGraduatetStudent=0;
				string Xml="<ROOT>";
				int PTNo=0;
				int Studentid= Convert.ToInt32(row["Studentid"]);
				//int Studentid=9418;

				//if(Studentid!=40628)	// Only for testing....
//					continue;
				
				string ProgramId=row["ProgramID"].ToString();			
				string Pid=row["PID"].ToString();

				// Get Cohort detail for particular student				
				StudentCohort=fnGetStudentCohortdetail(Studentid);

				//getSTudentDayorEveningStatus() Method to Check The Day and Evening Status of Student 
				int DayEveStudentStatus=getSTudentDayorEveningStatus(Studentid,Convert.ToInt32(SessionMgr.snCurrentSemId));
				DataTable tblPT=getPTBySemester(ProgramId,DayEveStudentStatus);
				if(tblPT.Rows.Count!=0)
				{
					int CatlogSemesterID=0;
					if(row["CatalogSemesterID"].ToString()!="")
					{
						CatlogSemesterID=Convert.ToInt32(row["CatalogSemesterID"]);
					}				    
					// Multiple students may have in same Semester catalogue
					// so don't need to retrieve catalogue courses each time individually.
					// Thus Checks with PrevProgramId and PrevCatlogSemesterID...

					if(PrevProgramId!=ProgramId || PrevCatlogSemesterID!=CatlogSemesterID)
					{	
						// Get Program Catalogue Detail
						tblPrgCtlgCourses=GetPrgCtlgCourses(ProgramId,CatlogSemesterID,Studentid);


						//tblPrgCtlgCourses=GetPrgCtlgCourses(ProgramId,CatlogSemesterID);
						PrevProgramId=ProgramId;
						PrevCatlogSemesterID=CatlogSemesterID;
					}

					int PreprogCatalogId=0;
					int TotalCRS=0;
					int TotalConsumedCRS=0;
				
					// Get the Pre-Planned courses for Student and Program.
					DataTable tblPlannedCourses=GetPlannedCourses(Studentid,ProgramId);
					//DataTable tblfailedcourse=Getfailedcourse(ProgramId,CatlogSemesterID,Studentid);
					
//					for(int p1=0;p1<tblPlannedCourses.Rows.Count;p1++)
//					{
//						for(int pt1=0;pt1<tblPrgCtlgCourses.Rows.Count;pt1++ )
//						{
//							if(tblPlannedCourses.Rows[p1]["courseid"].ToString()==tblPrgCtlgCourses.Rows[pt1]["courseid"].ToString())
//							{
//								string strGradeId1="";
//									 strGradeId1=Convert.ToString(tblPlannedCourses.Rows[p1]["gradeid"].ToString());
//
//								string courseNo1=tblPlannedCourses.Rows[p1]["CourseNo"].ToString();										
//								string categoryName1=tblPrgCtlgCourses.Rows[pt1]["category"].ToString();
//
//								if( strGradeId1!="")
//								{
//									Object IsPassed1=PassingGradeInThisCategory(Convert.ToInt32(strGradeId1),categoryName1);
//
//									if(IsPassed1!=null)
//									{							
//										if(!Convert.ToBoolean(IsPassed1))
//
//										{
//												isfound=false;
//											     RowIndex=i;
//
//											      break;
//
//
//										}
//										else
//										{
//											break;
//										}
//									}
//								}
//
//
//
//							}
//						}
//					}

				
					// Get Planned course rows in selected semester to calculate total consumed hours......
					DataRow[] TotalCRSinSemester=tblPlannedCourses.Select("semesterid="+ddlsem.SelectedValue);

					for(int i=0;i<TotalCRSinSemester.Length;i++)
					{
						TotalConsumedCRS+= Convert.ToInt32(TotalCRSinSemester[i]["CreditHrsE"]);
					}
					
					


				
						foreach(DataRow CtlgRow in tblPrgCtlgCourses.Rows)
						{
							if(StudentCohort=="Special Cohort")
							{
								if(TotalCRS>=(15-TotalConsumedCRS))
									break;
							}
							else
							{
								if(TotalCRS>=(12-TotalConsumedCRS) || TotalCRS>=(14-TotalConsumedCRS))
									break;
							}
							// Total Credit Hours of courses taken for each student should not be greater than 12 or 14...

							int progCatalogId= Convert.ToInt32(CtlgRow["ProgramCatDtlId"]);

							//To check the Or Combination 
							//if(progCatalogId!=PreprogCatalogId)//tblSummary.Select("progCatalogId='"+progCatalogId+"'")

							if(progCatalogId!=PreprogCatalogId )//tblSummary.Select("progCatalogId='"+progCatalogId+"'")
							{
								DataRow[] drCourses=tblPrgCtlgCourses.Select("ProgramCatDtlId="+progCatalogId);
								string categoryId=drCourses[0]["categoryId"].ToString();						
							
								// if Not free Elective
								if(categoryId!="CRSCAT00004")
								{
									// Always will take the first course from list...
									int RowIndex=0;
									string courseNo="";
									int courseId=0;
									string categoryName="";													 

									// check for each course in group if already taken or not
									// if not taken any one then take the first one
									// if any found in group already taken then skip to next programcatalogid courses.
									bool isfound=false;
									if(categoryId.CompareTo("CRSCAT00005")<0)
									{
										for(int i=0;i<drCourses.Length;i++)
										{
											courseId= Convert.ToInt32(drCourses[i]["CourseId"]);
											// DataRow[] PlannedRows=tblPlannedCourses.Select("CourseId="+courseId+" and courseScheduleId is not null");	
											DataRow[] PlannedRows=tblPlannedCourses.Select("CourseId="+courseId+"");	
											// Added by Lalit.. (Previous Planned course not have coursescheduled in eduplanahead)
											if(PlannedRows.Length==0)
											{
												PlannedRows=tblPlannedCourses.Select("CourseId="+courseId+" and ProgramCatDtlId="+progCatalogId);	
											}
																		
											if(PlannedRows.Length>0)
											{
												// May have multiple rows. so get the latest gradeid to find grade...
												string strGradeId="";
												int presem=0;
												for(int l=0; l<PlannedRows.Length;l++)
												{
													int seme=Convert.ToInt32(PlannedRows[l]["abssemorder"]);
													if(presem<seme)
														presem=seme;
												}
												DataRow[] PlannedRowsNew=tblPlannedCourses.Select("CourseId="+courseId+" and abssemorder="+presem);
												for(int j=0; j<PlannedRowsNew.Length;j++)
												{
													if( Convert.ToString(PlannedRowsNew[j]["GradeId"])!="" || PlannedRowsNew[j]["GradeId"]!=DBNull.Value)
														strGradeId=Convert.ToString(PlannedRowsNew[j]["GradeId"]);
												}

												courseNo=drCourses[i]["CourseNo"].ToString();										
												categoryName=drCourses[i]["category"].ToString();
										
												if( strGradeId!="")
												{
													Object IsPassed=PassingGradeInThisCategory(Convert.ToInt32(strGradeId),categoryName);

													if(IsPassed!=null)
													{							
														if(!Convert.ToBoolean(IsPassed))
														{
															// if failed get the course with rowindex no from row array...
															isfound=false;
															//isfound=true;

															RowIndex=i;
															// Lalit on 28-02-2012....
															break;	// It should skip the loop for further courses...
														}
														else
														{
															isfound=true;
															break;
														}
													}
												}
												else
												{
													isfound=true; 
													break;
												}										
											}
										}
									}
									if(!isfound)
									{									
										courseNo=drCourses[RowIndex]["CourseNo"].ToString();
										courseId= Convert.ToInt32(drCourses[RowIndex]["CourseId"]);
										categoryName=drCourses[RowIndex]["category"].ToString();
										CheckGraduatetStudent=1;
									}
									else
									{
										PreprogCatalogId=progCatalogId;
										continue;
									}								
									int TotalMEHours=Convert.ToInt32(drCourses[RowIndex]["FreeMajorElecHrs"]);	
									if(categoryId.CompareTo("CRSCAT00005")>=0)
									{	
										DataTable dtMECourses=tblPlannedCourses.Clone();

										DataRow[] MERows=tblPlannedCourses.Select("categoryId='"+categoryId+"'");
									
										if(MERows.Length==0)
										{
											// changed by lalit.. 28-07-2013
											// some ME courses have no categoryid even planned in any previous program.
											for(int i=0;i<drCourses.Length;i++)
											{
												courseId= Convert.ToInt32(drCourses[i]["CourseId"]);
												//DataRow[] PlannedRows=tblPlannedCourses.Select("CourseId="+courseId+" and courseScheduleId is not null");	
									 
												DataRow[] MECourseWithoutCategory=tblPlannedCourses.Select("CourseId="+courseId);
												if(MECourseWithoutCategory.Length>0)
												{
													dtMECourses.ImportRow(MECourseWithoutCategory[MECourseWithoutCategory.Length-1]);
													//Array.Copy(MECourseWithoutCategory,MECourseWithoutCategory.Length-1,MERows,MERows.Length-1,1);
													//MECourseWithoutCategory.CopyTo(MERows,MERows.Length);
											
												}
											}
										
										}
										else
										{
											for(int i=0;i<MERows.Length;i++)
											{
												dtMECourses.ImportRow(MERows[MERows.Length-1]);
											}
										}
										
										
										int TotalPlannedME=0;
										int flagCheckCoursePlannedForME=0;
										for(int k=0;k<dtMECourses.Rows.Count;k++)
										{
											//										string courseID= dtMECourses.Rows[k]["CourseId"].ToString();
											//
											//										DataRow[] coursesInCatalog=tblPrgCtlgCourses.Select("CourseId="+courseID);
											//
											//										if(drCourses.Length>0)
											TotalPlannedME+=Convert.ToInt32(dtMECourses.Rows[k]["CreditHrsE"]);
										
											if(Convert.ToInt32(dtMECourses.Rows[k]["ProgramCatDtlId"]==DBNull.Value?0:dtMECourses.Rows[k]["ProgramCatDtlId"])== progCatalogId)
											{
												flagCheckCoursePlannedForME=1;
											}
										}
										if(flagCheckCoursePlannedForME==1)
											continue;
										flagCheckCoursePlannedForME=0;
										DataRow[] MESTudentRows=tblSummary.Select("StudentId='"+Studentid+"' and CategoryId='"+categoryId+"'");

										
										for(int l=0;l<MESTudentRows.Length;l++)
										{
											TotalPlannedME+=Convert.ToInt32(MESTudentRows[l]["CRS"].ToString());
											if(Convert.ToInt32(MESTudentRows[l]["progCatalogId"])== progCatalogId)
											{
												flagCheckCoursePlannedForME=1;
											}
										}
										if(flagCheckCoursePlannedForME==1)
											continue;
										int MEHrsToBeTaken=TotalMEHours-TotalPlannedME;

										if(MEHrsToBeTaken>0 )
										{

											CheckGraduatetStudent=1;
											// If Major Elective already Planned then check for next courses in Major List.
											if(dtMECourses.Rows.Count>0)
											{
												string METaken="";
												for(int j=0;j<dtMECourses.Rows.Count;j++)
												{
													if(METaken!="")
														METaken+=",";
													METaken+=dtMECourses.Rows[j]["CourseNo"].ToString();
												}
												for(int i=0;i<drCourses.Length;i++)
												{	
													string majorCourse=drCourses[i]["CourseNo"].ToString();
													
													//Check here for same course and Program catlog id if matched the skip other wise taken
													//if(Convert.ToInt32(MERows[k]["ProgramCatDtlId"])!= progCatalogId ||Convert.ToInt32(MESTudentRows[k]["ProgramCatDtlId"])!= progCatalogId)
													if(METaken.IndexOf(majorCourse)<0)
													{
														courseNo=majorCourse;
														courseId=Convert.ToInt32(drCourses[i]["CourseId"]);
														categoryName=drCourses[RowIndex]["category"].ToString();
														RowIndex=i;	// RowIndex is used to read table data...
														break;
													}											
												}
											}
											else
											{
												string METaken="";
												for(int j=0;j<dtMECourses.Rows.Count;j++)
												{
													if(METaken!="")
														METaken+=",";
													METaken+=dtMECourses.Rows[j]["CourseNo"].ToString();
												}
												//Check here for same course and Program catalog id if matched the bhaag other wise taken

												// ME courses may be in current Planning Table..So check for duplicacy...
												for(int i=0;i<drCourses.Length;i++)
												{	
													string majorCourse=drCourses[i]["CourseNo"].ToString();
													if(tblSummary.Select("StudentId='"+Studentid+"' and CourseNo='"+majorCourse+"'").Length==0)
													{
														courseNo=majorCourse;
														courseId=Convert.ToInt32(drCourses[i]["CourseId"]);
														categoryName=drCourses[RowIndex]["category"].ToString();
														RowIndex=i;	// RowIndex is used to read table data...
														break;
													}											
												}
											}
										}
										else
										{
											continue;
										}
										
									}
									// Problem is here for id 11983 for courseid 357 cois 101 this is failed but not checked by method...

									// It's not necessay but has been commented for future use...

									// Check for course If student is failed then we can take this again for current semester...
									//if(validCourseforStudent(Studentid,courseId,categoryName))
									//{		
									if(PTNo==tblPT.Rows.Count)
										PTNo=0;
									
									int PTID= Convert.ToInt32(tblPT.Rows[PTNo]["PartTermId"]);
									string PTName=tblPT.Rows[PTNo]["PartTerm"].ToString();		

									bool isValid=true;
									string PreCoGroupId="";
									if(drCourses[RowIndex]["PreCoGroupId"]!=DBNull.Value)
									{
										isValid=false;
										//int ReqGroupID= Convert.ToInt32(drCourses[RowIndex]["PreCoGroupId"]);
										DataTable dt=new DataTable();
										SqlParameter prmCourse=new SqlParameter("@CouseNos",SqlDbType.VarChar,100);
										prmCourse.Value=courseNo;
										SqlParameter prmprogramid=new SqlParameter("@ProgramID",SqlDbType.VarChar,100);
										prmprogramid.Value=ProgramId;
										dt=Dbobject._getDataset("sp_getPreCoDetailsByCourse",CommandType.StoredProcedure,prmCourse,prmprogramid).Tables[0];
										/*Add condition by Sandeep sir for check for fail course on 27Dec2012*/
									
										// Added by Lalit on 20-07-2013
										// if precogroupid exist but restricted for this program then above dt return nothing...
										// and this course should be planned by ignoring precogroup details...
										if(dt.Rows.Count==0)
											isValid=true;

										int groupidp=0;
										int groupidc=0;	
										foreach(DataRow dr in dt.Rows)
										{
											groupidc=Convert.ToInt32(dr["GroupId"]);
											if (isValid==true && groupidc!=groupidp)
											{
												break;	
											}
											else if((isValid==true && groupidp==groupidc) || groupidp==0 )
											{
												string reqCourse=dr["GroupCourse"].ToString();		
												// Whether Pre-Co Match or not....
												if(!IsRequisiteMatch(Studentid, reqCourse, Convert.ToBoolean(dr["IsPre"]), int.Parse(ddlsem.SelectedValue), ref PTID,tblPlannedCourses,tblSummary,ref PTName,tblPT))
												{	
													isValid=false;
													groupidp=groupidc;
													// break;
												}
												else
												{
													PreCoGroupId=dr["GroupId"]!=DBNull.Value? string.Format("{0:N0}",dr["GroupId"]):"0";
													isValid=true;
													groupidp=groupidc;
												}
											}
											else if (groupidc!=groupidp)
											{
												groupidp=groupidc;
												string reqCourse=dr["GroupCourse"].ToString();		
												// Whether Pre-Co Match or not....
												if(!IsRequisiteMatch(Studentid, reqCourse, Convert.ToBoolean(dr["IsPre"]), int.Parse(ddlsem.SelectedValue), ref PTID,tblPlannedCourses,tblSummary,ref PTName,tblPT))
												{	
													isValid=false;
													groupidp=groupidc;
													// break;  
												}
												else
												{
													PreCoGroupId=dr["GroupId"]!=DBNull.Value? string.Format("{0:N0}",dr["GroupId"]):"0";
													isValid=true;							
												}
											}
											/*-------------End Condition-------------*/										 
										}											
									}								 	
									if(isValid)
									{											
										if(!IsCourseTakenForOtherPreGroup(courseNo,tblPlannedCourses,tblSummary,tblPrgCtlgCourses))
										{
											string CRS= drCourses[RowIndex]["CRS"]!=DBNull.Value? string.Format("{0:N0}",drCourses[RowIndex]["CRS"]):"0";			
											TotalCRS+=int.Parse(CRS);
											if(StudentCohort=="Special Cohort")
											{
												if((TotalCRS<=(15-TotalConsumedCRS)))
												{
													string courseName=drCourses[RowIndex]["CourseName"].ToString();												

													string s1= "<tsEduPlanAhead StudentID=#@StudentID# CourseNo=#@CourseNo# SemesterID=#@SemesterID# ProgramID=#@ProgramID# PartTermID=#@PartTermID# CdtHrs=#@CdtHrs# ReqGroupID=#@ReqGroupID# ProgramCatDtlId=#@ProgramCatDtlId# isAuto=#@isAuto#  />";
													s1=s1.Replace("@StudentID",Studentid.ToString());
													s1=s1.Replace("@CourseNo",courseNo);
													s1=s1.Replace("@SemesterID",ddlsem.SelectedValue);
													s1=s1.Replace("@ProgramID",ProgramId);							
													//s1=s1.Replace("@PartTermID",tblPT.Rows[PTNo]["PartTermId"].ToString());
													s1=s1.Replace("@PartTermID",PTID.ToString());
													//s1=s1.Replace("@PartTermID","656");													
													s1=s1.Replace("@CdtHrs",CRS);
													s1=s1.Replace("@ReqGroupID",PreCoGroupId);
													s1=s1.Replace("@ProgramCatDtlId",progCatalogId.ToString());
													s1=s1.Replace("@isAuto","1");
													Xml+=s1;
													DataRow[] drSummary=tblSummary.Select("CourseNo='"+courseNo+"'");
													
													DataRow newRow=tblSummary.NewRow();
													newRow["StudentID"]=Studentid.ToString();
													newRow["Total"]="1";
													newRow["CourseNo"]=courseNo;
													newRow["CourseName"]=courseName;
													newRow["CourseId"]=courseId;
													newRow["CRS"]=CRS;
													//newRow["PartTermId"]=tblPT.Rows[PTNo]["PartTermId"].ToString();
													newRow["PartTermId"]=PTID.ToString();
													//newRow["PartTermId"]="656";
													newRow["SemesterId"]=ddlsem.SelectedValue;
													newRow["IsPassingGrade"]="";
													newRow["TotalStudent"]=tbl.Rows.Count;
													//newRow["PartTerm"]="1";
													//newRow["PartTerm"]=tblPT.Rows[PTNo]["PartTerm"].ToString();
													newRow["PartTerm"]=PTName;
													newRow["CategoryId"]=categoryId;
													newRow["progCatalogId"]=progCatalogId;
													tblSummary.Rows.Add(newRow);
													//}	
													PTNo++;
												 
												}
												else
												{
													TotalCRS=TotalCRS-int.Parse(CRS);
													continue;
												}													
											}
											else
											{
												if((TotalCRS<=(14-TotalConsumedCRS)))
												{
													string courseName=drCourses[RowIndex]["CourseName"].ToString();												

													string s1= "<tsEduPlanAhead StudentID=#@StudentID# CourseNo=#@CourseNo# SemesterID=#@SemesterID# ProgramID=#@ProgramID# PartTermID=#@PartTermID# CdtHrs=#@CdtHrs# ReqGroupID=#@ReqGroupID# ProgramCatDtlId=#@ProgramCatDtlId# isAuto=#@isAuto#  />";
													s1=s1.Replace("@StudentID",Studentid.ToString());
													s1=s1.Replace("@CourseNo",courseNo);
													s1=s1.Replace("@SemesterID",ddlsem.SelectedValue);
													s1=s1.Replace("@ProgramID",ProgramId);												 
													s1=s1.Replace("@PartTermID",PTID.ToString());
													//s1=s1.Replace("@PartTermID","656");													
													s1=s1.Replace("@CdtHrs",CRS);
													s1=s1.Replace("@ReqGroupID",PreCoGroupId);
													s1=s1.Replace("@ProgramCatDtlId",progCatalogId.ToString());
													s1=s1.Replace("@isAuto","1");
													Xml+=s1;										 
													DataRow[] drSummary=tblSummary.Select("CourseNo='"+courseNo+"'");
													
													DataRow newRow=tblSummary.NewRow();
													newRow["StudentID"]=Studentid.ToString();
													newRow["Total"]="1";
													newRow["CourseNo"]=courseNo;
													newRow["CourseName"]=courseName;
													newRow["CourseId"]=courseId;
													newRow["CRS"]=CRS;
													//newRow["PartTermId"]=tblPT.Rows[PTNo]["PartTermId"].ToString();
													newRow["PartTermId"]=PTID.ToString();
													//newRow["PartTermId"]="656";
													newRow["SemesterId"]=ddlsem.SelectedValue;
													newRow["IsPassingGrade"]="";
													newRow["TotalStudent"]=tbl.Rows.Count;
													newRow["PartTerm"]=PTName;
													newRow["CategoryId"]=categoryId;
													newRow["progCatalogId"]=progCatalogId;
													tblSummary.Rows.Add(newRow);											
													//}	
												 
											
													PTNo++;
												
												}
												else
												{
													TotalCRS=TotalCRS-int.Parse(CRS);
													continue;
												}
											}
										}
									}	// if any pre defined and Match for Course...
								
									////}	// if End for course valid for student condition check..

									/////}	// End of if course not planned..

								}	// If end for category check..........

								PreprogCatalogId=progCatalogId;
							}

						}	// End of PrgCtlg course Table for Loop..(for Each student)
					
								
					if(TotalCRS<(12-TotalConsumedCRS))
					{
						// Send mail to Student and Advisor.....(If Checkbox is checked)
						if(chkAlert.Checked)
						{
							if(CheckGraduatetStudent==1)
							{
								/*Create by Avinash on 16/12/2012*/
								#region Create XMl of Day Time Customization
								str1= "<AEPRejectedRecords StudentID=#@StudentID# ProgramID=#@ProgramID#  CourseNo=#@CourseNo# Cdt_Hrs=#@Cdt_Hrs# SemesterID=#@SemesterID# PartTermID=#@PartTermID# AlertBy=#@AlertBy#  />";
								str1=str1.Replace("@StudentID",row["Studentid"].ToString());
								str1=str1.Replace("@ProgramID",row["ProgramID"].ToString());							

								DataRow[] rows= tblSummary.Select("StudentId='"+row["Studentid"]+"'");
								foreach(DataRow row1 in	rows)
								{
									str1=str1.Replace("@CourseNo",row1["CourseNo"].ToString());
									str1=str1.Replace("@Cdt_Hrs",row1["CRS"].ToString());
									str1=str1.Replace("@SemesterID",ddlsem.SelectedItem.Text);
									str1=str1.Replace("@PartTermID",row1["PartTermId"].ToString());
								}
								str1=str1.Replace("@AlertBy",SessionMgr.snUserId);
								strXml+=str1;						
								#endregion
								strStudent+=Studentid.ToString()+",";
								Send=SendAlertMail(Studentid.ToString(),tblSummary);
								//Send=SendAlertMail1(Studentid.ToString(),tblSummary);
								//Send=SendAlertMail2(Studentid.ToString(),tblSummary);
							}
						}
					}
					Xml+="</ROOT>";			
					// If any coused added to be inserted...
					if(Xml.IndexOf("tsEduPlanAhead")> 0)
					{
						SqlConnection sqlnewcon=Dbobject.getconnection();
						sqlnewcon.Open();					
						SqlParameter prmXml=new SqlParameter("@XmlEduPlanAhead",Xml);					
						RowsAffected= Dbobject._executenonquery("sp_InsertAutoEduPlanAhead",sqlnewcon,CommandType.StoredProcedure,prmXml);								
						sqlnewcon.Close();	
						sqlnewcon.Dispose();					
					}

				}//End of If GetPartterm function
				else
				{
					if(NotCreated=="")
						NotCreated=Pid+",";
					int c=NotCreated.IndexOf(Pid);
					if(NotCreated.IndexOf(Pid)==-1)
						NotCreated+=Pid+",";
				}
				
			}	// End of main loop for single semester assignment...
			/*Create xml and procedure sp_InsertAEPRejectedRecords by Avinash on 16/12/2012*/
			if(strXml!="<ROOT>")
			{
                strXml+="</ROOT>";	
				SqlConnection sqlnewcon=Dbobject.getconnection();
				sqlnewcon.Open();					
				SqlParameter prmXml=new SqlParameter("@XmlAEPRejectedRecords",strXml);					
				int RowsEffected= Dbobject._executenonquery("sp_InsertAEPRejectedRecords",sqlnewcon,CommandType.StoredProcedure,prmXml);								
				sqlnewcon.Close();	
				sqlnewcon.Dispose();
			}
			/*-------------- End -------------------*/							
					
			if(RowsAffected>0)
			{		
				// This Function plannned the Labs of Currently Planned Courses   
				// If Labs are exist in Program Catalogue then it will planned the Labs other wise not.
				fnInsertCoursesLabDetails(Convert.ToInt32(ddlsem.SelectedItem.Value));

				SqlParameter prmProgramIDs=new SqlParameter("@ProgramIDs",programIDs); // CodeType for Auto Alert Type (In General Alert)...
				SqlParameter prmSemesterID=new SqlParameter("@SemesterID",ddlsem.SelectedItem.Value);			
				SqlParameter prmCampusID=new SqlParameter("@CampusID",ddlcampus.SelectedValue);		
				
				//string strQuery="select COunt(studentid) as Total,e.courseid,c.CourseNo,C.courseName from tseduplanAhead as e inner join tscourse as c on e.courseid=c.courseid	where semesterid="+ddlsem.SelectedItem.Value+" and isauto=1 group by e.courseid,c.courseno,C.courseName order by courseno";
				DataSet ds=Dbobject._getDataset("ppGetAEPPlannedSummary",CommandType.StoredProcedure,prmProgramIDs,prmSemesterID,prmCampusID);
				
				dgrDetails.DataSource=ds.Tables[0];
				dgrDetails.DataBind();

				//Page.RegisterStartupScript("AutoSaved","<script language='javascript'>alert('Automatic Education Plan created successfully.');</script>");
				if(NotCreated!="")
					Page.RegisterStartupScript("AutoSaved","<script language='javascript'>alert(\"Automatic Education Plan created successfully.\\n\\rProgram's whose partterm not defined- "+NotCreated.Trim(',')+"\");</script>");
				else
					Page.RegisterStartupScript("AutoSaved","<script language='javascript'>alert('Automatic Education Plan created successfully.');</script>");
				//lblError.Text="Program's whose partterm not defined- "+NotCreated.Trim(',');
				//Page.RegisterStartupScript("Created","<script language='javascript'>alert(\"Automatic Education Plan has not been created for Previous semester - "+semName+" "+year+" for program - "+item.Value+"\");</script>");
				
				lblrec.Text= "Total Students= "+ ds.Tables[1].Rows[0][0].ToString();
				BtnView.Enabled=true;

				if(Send)
				{
					Page.RegisterStartupScript("LessCredit","<script language='javascript'>alert('An Alert has been sent to Admin, Advisior and Counselor for students who has Less than 12 Credits.');</script>");
				}
			}
			else
			{
				BtnView.Enabled=false;
				if(NotCreated!="")
					Page.RegisterStartupScript("AutoSaved","<script language='javascript'>alert(\"Program's whose partterm not defined- "+NotCreated.Trim(',')+"\");</script>");	
				else
					Page.RegisterStartupScript("AutoError","<script language='javascript'>alert('Error Occured in Automatic Education Plan Creation.');</script>");
				//Page.RegisterStartupScript("AutoError","<script language='javascript'>alert('No New Students are Avaliable To Create Automatic Education Plan.');</script>");
			}
		}

		
		private void fnInsertCoursesLabDetails(int intSemester)
		{
			int RowsAffected=0;
			//Function to Insert all the student Day and Evening Details of Current Semester
			try
			{
				SqlConnection sqlnewcon=Dbobject.getconnection();
				sqlnewcon.Open();
				SqlCommand cmd=new SqlCommand("usp_AddCourseLabs",sqlnewcon);
				cmd.CommandType=CommandType.StoredProcedure;
				cmd.Parameters.Add("@SemesterId",SqlDbType.Int).Value=intSemester;
				cmd.CommandTimeout = 100;
				RowsAffected= cmd.ExecuteNonQuery();
				sqlnewcon.Close();	
				sqlnewcon.Dispose();

			}
			catch(Exception ex)
			{
				GSpaceCommon.ExceptionHandler.Handle(ex);
			}
			finally
			{
			}
		}
		private void SaveAlert(int StudentId,int SemesterId,int ReportedBy,int workgroupid)
		{
			SqlConnection sqlnewcon=Dbobject.getconnection();
			sqlnewcon.Open();					
			SqlParameter prmAlertType=new SqlParameter("@AlertType","EALT010017"); // CodeType for Auto Alert Type (In General Alert)...
			SqlParameter prmStudentId=new SqlParameter("@Studentid",StudentId);			
			SqlParameter prmSemId=new SqlParameter("@SemesterId",SemesterId);					
			SqlParameter prmReportedBy=new SqlParameter("@ReportedBy",ReportedBy);
			SqlParameter prmwrkgrpid=new SqlParameter("@workgroupid",workgroupid);
			int RowsAffected= Dbobject._executenonquery("sp_SaveAlertAEP",sqlnewcon,CommandType.StoredProcedure,prmAlertType,prmStudentId,prmSemId,prmReportedBy,prmwrkgrpid);								
			sqlnewcon.Close();	
			sqlnewcon.Dispose();
		}

		private bool SendAlertMail(string StudentId, DataTable tblSummary)
		{

			bool Send=false;
			int status=0;
			SqlDataReader dr= Dbobject._getDataReader("select SSNo,rtrim(FirstName)+' '+LastName as Name,Email from tsStudent where studentid="+StudentId,CommandType.Text);
			dr.Read();
			string SSNo=dr["SSNo"].ToString();
			string Name=dr["name"].ToString();
			string Email=dr["Email"].ToString();
			dr.Close();

			string path = Server.MapPath("InstructAlert.htm");
			StreamReader sr = File.OpenText(path);
			string strContent = "<pre>" + sr.ReadToEnd() + "</pre>";
			strContent = strContent.Replace("#date", DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
			strContent = strContent.Replace("#alertBy", SessionMgr.snUserName + " (" + SessionMgr.snWorkGroupName + ")" );			
			strContent = strContent.Replace("#SID", SSNo);			
			strContent = strContent.Replace("#name", Name);					
			sr.Close();
			
			string InstructorName="";
			SqlDataReader drInstructor= Dbobject._getDataReader("select rtrim(firstname)+' '+lastname as [name],Email from tsInstructor where instructorid=(select max(InstructorId) from tsAdvisinglog where semesterid=(select max(Semesterid) from tsAdvisinglog where studentid="+ StudentId +") and studentid="+ StudentId +")",CommandType.Text);
			if(drInstructor.HasRows)
			{
				drInstructor.Read();				
				InstructorName=drInstructor["name"].ToString();
				string InstructorEmail=drInstructor["Email"].ToString();				
			}
			drInstructor.Close();
			strContent = strContent.Replace("#InstructorName", InstructorName);

			string StudentTableData="<table cellpadding=\"0\" cellspacing=\"0\" border=\"1\"><tr><td align=\"left\" width=\"100px\" ><b>CourseNo</b></td><td align=\"left\" width=\"50px\" ><b>CRS</b></td><td align=\"left\" width=\"100px\" ><b>Semester</b></td><td align=\"left\" width=\"80px\" ><b>PartTerm</b></td></tr>";
			DataRow[] rows=tblSummary.Select("StudentId='"+StudentId+"'");
			foreach(DataRow row in	rows)
			{
				StudentTableData+="<tr><td align=\"left\">"+ row["CourseNo"].ToString() +"</td><td align=\"left\">"+row["CRS"].ToString()+"</td><td align=\"left\">"+ ddlsem.SelectedItem.Text +"</td><td align=\"left\">"+ row["PartTerm"].ToString() +"</td></tr>";			
			}
			StudentTableData+="</table>";
			strContent = strContent.Replace("#TableData", StudentTableData);				
	
			// Save Alert in Table......(to be implemented later in intebayamon)
			SaveAlert(int.Parse(StudentId),int.Parse(SessionMgr.snCurrentSemId), int.Parse(SessionMgr.snUserId),int.Parse(SessionMgr.snWorkGroupId));

			//PortalG1.MailService.SendMailServices objMail=new PortalG1.MailService.SendMailServices ();
			//Send= objMail.SendMail("armendez@suagm.edu,testearlyalert@gmail.com", strContent, "Less than 12 credits", "1111");            
            sub="Less than 12 credits";
			mailid= "testearlyalert@gmail.com";
			SqlConnection sqlCon = Dbobject.getconnection();			
			sqlCon.Open();				
			SqlParameter prm1=new SqlParameter("@EmailID",mailid);				
			SqlParameter prm2=new SqlParameter("@MsgBody",strContent);
			SqlParameter prm3=new SqlParameter("@Subject",sub);							
			status = Dbobject._executenonquery("sp_Send_Mail",sqlCon,CommandType.StoredProcedure,prm1,prm2,prm3);
			sqlCon.Close();		
						
			return Send;		
		}	
	


		private bool SendAlertMail1(string StudentId, DataTable tblSummary)
		{

			bool Send=false;
			int status=0;
			SqlDataReader dr= Dbobject._getDataReader("select SSNo,rtrim(FirstName)+' '+LastName as Name,Email from tsStudent where studentid="+StudentId,CommandType.Text);
			dr.Read();
			string SSNo=dr["SSNo"].ToString();
			string Name=dr["name"].ToString();
			string Email=dr["Email"].ToString();
			dr.Close();

			string path = Server.MapPath("InstructAlert.htm");
			StreamReader sr = File.OpenText(path);
			string strContent = "<pre>" + sr.ReadToEnd() + "</pre>";
			strContent = strContent.Replace("#date", DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
			strContent = strContent.Replace("#alertBy", SessionMgr.snUserName + " (" + SessionMgr.snWorkGroupName + ")" );			
			strContent = strContent.Replace("#SID", SSNo);			
			strContent = strContent.Replace("#name", Name);					
			sr.Close();
			
			string InstructorName="";
			SqlDataReader drInstructor= Dbobject._getDataReader("select rtrim(firstname)+' '+lastname as [name],Email from tsInstructor where instructorid=(select max(InstructorId) from tsAdvisinglog where semesterid=(select max(Semesterid) from tsAdvisinglog where studentid="+ StudentId +") and studentid="+ StudentId +")",CommandType.Text);
			if(drInstructor.HasRows)
			{
				drInstructor.Read();				
				InstructorName=drInstructor["name"].ToString();
				string InstructorEmail=drInstructor["Email"].ToString();				
			}
			drInstructor.Close();
			strContent = strContent.Replace("#InstructorName", InstructorName);

			string StudentTableData="<table cellpadding=\"0\" cellspacing=\"0\" border=\"1\"><tr><td align=\"left\" width=\"100px\" ><b>CourseNo</b></td><td align=\"left\" width=\"50px\" ><b>CRS</b></td><td align=\"left\" width=\"100px\" ><b>Semester</b></td><td align=\"left\" width=\"80px\" ><b>PartTerm</b></td></tr>";
			DataRow[] rows=tblSummary.Select("StudentId='"+StudentId+"'");
			foreach(DataRow row in	rows)
			{
				StudentTableData+="<tr><td align=\"left\">"+ row["CourseNo"].ToString() +"</td><td align=\"left\">"+row["CRS"].ToString()+"</td><td align=\"left\">"+ ddlsem.SelectedItem.Text +"</td><td align=\"left\">"+ row["PartTerm"].ToString() +"</td></tr>";			
			}
			StudentTableData+="</table>";
			strContent = strContent.Replace("#TableData", StudentTableData);				
	
			// Save Alert in Table......(to be implemented later in intebayamon)
			SaveAlert(int.Parse(StudentId),int.Parse(SessionMgr.snCurrentSemId), int.Parse(SessionMgr.snUserId),int.Parse(SessionMgr.snWorkGroupId));

			//PortalG1.MailService.SendMailServices objMail=new PortalG1.MailService.SendMailServices ();
			//Send= objMail.SendMail("armendez@suagm.edu,testearlyalert@gmail.com", strContent, "Less than 12 credits", "1111");      

			SqlParameter prmStudentid1 =new SqlParameter("@StudentId",SqlDbType.VarChar,100);
			prmStudentid1.Value=StudentId.ToString();
		
			DataSet ds =new DataSet();
			ds=Dbobject._getDataset("ad_cons_aep",CommandType.StoredProcedure,prmStudentid1);
			if ( ds.Tables[0].Rows.Count >0)
			{
			mailid1 = ds.Tables[0].Rows[0]["Email"].ToString();
			
			
			}

			sub="Less than 12 credits";
			//mailid= "testearlyalert@gmail.com";
			SqlConnection sqlCon = Dbobject.getconnection();			
			sqlCon.Open();				
			SqlParameter prm1=new SqlParameter("@EmailID",mailid1);				
			SqlParameter prm2=new SqlParameter("@MsgBody",strContent);
			SqlParameter prm3=new SqlParameter("@Subject",sub);							
			status = Dbobject._executenonquery("sp_Send_Mail",sqlCon,CommandType.StoredProcedure,prm1,prm2,prm3);
			sqlCon.Close();		
						
			return Send;		
		}		

		private bool SendAlertMail2(string StudentId, DataTable tblSummary)
		{

			bool Send=false;
			int status=0;
			SqlDataReader dr= Dbobject._getDataReader("select SSNo,rtrim(FirstName)+' '+LastName as Name,Email from tsStudent where studentid="+StudentId,CommandType.Text);
			dr.Read();
			string SSNo=dr["SSNo"].ToString();
			string Name=dr["name"].ToString();
			string Email=dr["Email"].ToString();
			dr.Close();

			string path = Server.MapPath("InstructAlert.htm");
			StreamReader sr = File.OpenText(path);
			string strContent = "<pre>" + sr.ReadToEnd() + "</pre>";
			strContent = strContent.Replace("#date", DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
			strContent = strContent.Replace("#alertBy", SessionMgr.snUserName + " (" + SessionMgr.snWorkGroupName + ")" );			
			strContent = strContent.Replace("#SID", SSNo);			
			strContent = strContent.Replace("#name", Name);					
			sr.Close();
			
			string InstructorName="";
			SqlDataReader drInstructor= Dbobject._getDataReader("select rtrim(firstname)+' '+lastname as [name],Email from tsInstructor where instructorid=(select max(InstructorId) from tsAdvisinglog where semesterid=(select max(Semesterid) from tsAdvisinglog where studentid="+ StudentId +") and studentid="+ StudentId +")",CommandType.Text);
			if(drInstructor.HasRows)
			{
				drInstructor.Read();				
				InstructorName=drInstructor["name"].ToString();
				string InstructorEmail=drInstructor["Email"].ToString();				
			}
			drInstructor.Close();
			strContent = strContent.Replace("#InstructorName", InstructorName);

			string StudentTableData="<table cellpadding=\"0\" cellspacing=\"0\" border=\"1\"><tr><td align=\"left\" width=\"100px\" ><b>CourseNo</b></td><td align=\"left\" width=\"50px\" ><b>CRS</b></td><td align=\"left\" width=\"100px\" ><b>Semester</b></td><td align=\"left\" width=\"80px\" ><b>PartTerm</b></td></tr>";
			DataRow[] rows=tblSummary.Select("StudentId='"+StudentId+"'");
			foreach(DataRow row in	rows)
			{
				StudentTableData+="<tr><td align=\"left\">"+ row["CourseNo"].ToString() +"</td><td align=\"left\">"+row["CRS"].ToString()+"</td><td align=\"left\">"+ ddlsem.SelectedItem.Text +"</td><td align=\"left\">"+ row["PartTerm"].ToString() +"</td></tr>";			
			}
			StudentTableData+="</table>";
			strContent = strContent.Replace("#TableData", StudentTableData);				
	
			// Save Alert in Table......(to be implemented later in intebayamon)
			SaveAlert(int.Parse(StudentId),int.Parse(SessionMgr.snCurrentSemId), int.Parse(SessionMgr.snUserId),int.Parse(SessionMgr.snWorkGroupId));

			//PortalG1.MailService.SendMailServices objMail=new PortalG1.MailService.SendMailServices ();
			//Send= objMail.SendMail("armendez@suagm.edu,testearlyalert@gmail.com", strContent, "Less than 12 credits", "1111");      

			SqlParameter prmStudentid1 =new SqlParameter("@StudentId",SqlDbType.VarChar,100);
			prmStudentid1.Value=StudentId.ToString();
		
			DataSet ds =new DataSet();
			ds=Dbobject._getDataset("ad_cons_aep",CommandType.StoredProcedure,prmStudentid1);
			//if ( ds.Tables[0].Rows.Count >0)
			//{
			//	mailid1 = ds.Tables[0].Rows[0]["Email"].ToString();
			
			
			//}

			sub="Less than 12 credits";
			mailid= "nitinsharma5052@gmail.com";
			SqlConnection sqlCon = Dbobject.getconnection();			
			sqlCon.Open();				
			SqlParameter prm1=new SqlParameter("@EmailID",mailid1);				
			SqlParameter prm2=new SqlParameter("@MsgBody",strContent);
			SqlParameter prm3=new SqlParameter("@Subject",sub);							
			status = Dbobject._executenonquery("sp_Send_Mail",sqlCon,CommandType.StoredProcedure,prm1,prm2,prm3);
			sqlCon.Close();		
						
			return Send;		
		}		


		// SemesterId and PTID is of the main course taken
		private bool IsRequisiteMatch(int Studentid, string reqCourse,bool isPre,int SemesterID, ref int PTID,DataTable tblPlannedCourses,DataTable tblSummary,ref string PTName,DataTable tblPT )
		{	
			bool returnValue=false;
			DataRow[] PreCourses=tblSummary.Select("StudentId='"+Studentid+"'and CourseNo='"+reqCourse+"'");			
			int reqcourseSemId=0;
			int reqPTID=0;
			string reqPTName="";
			string isPassingGrade="";

			if(PreCourses.Length==0)			
				PreCourses=tblPlannedCourses.Select("CourseNo='"+reqCourse+"'");
			// PreCourses=tblSummary.Select("CourseNo='"+reqCourse+"'"); Old Line replace with new...
			
			if(PreCourses.Length>0)
			{	
				reqcourseSemId= Convert.ToInt32(PreCourses[PreCourses.Length-1]["SemesterId"]);
				if(PreCourses[PreCourses.Length-1]["PartTermId"]!=DBNull.Value)
					if(PreCourses[PreCourses.Length-1]["PartTermId"]!=null)
						if(Convert.ToString(PreCourses[PreCourses.Length-1]["PartTermId"])!="")
						{
							reqPTID= Convert.ToInt32(PreCourses[PreCourses.Length-1]["PartTermId"]);
							reqPTName=PreCourses[PreCourses.Length-1]["PartTerm"].ToString();
						}
						else
							reqPTID=0;
				isPassingGrade=PreCourses[PreCourses.Length-1]["IsPassingGrade"].ToString();

				if(reqcourseSemId==0 || reqPTID==0)
				{
					returnValue=false;					
				}
				else if(isPassingGrade!="")
				{
					if(Convert.ToBoolean(isPassingGrade))
						returnValue=true;
					else
						returnValue=false;
				}					
				else if(isPre)
				{
					#region Added By Naresh
					if(!issemestervalid(reqcourseSemId,SemesterID,1))
					{
						if(reqcourseSemId==SemesterID)
						{
							if(reqPTID<PTID)
								return true;
							else
							{
								foreach(DataRow dr in tblPT.Rows)
								{
									if(Convert.ToInt32(dr["PartTermId"])>reqPTID)
									{
										PTID=Convert.ToInt32(dr["PartTermId"].ToString());
										PTName=dr["PTName"].ToString();
										returnValue=true;
										break;
									}
								}
							}
						}	
						else
						{
							return false;
						}
					}
					else
					{
						returnValue=true;
					}			
					#endregion		
				}
				else
				{
					if(reqcourseSemId!=0)
					{
					
						if(reqcourseSemId!=SemesterID )
							returnValue=false;
						else 
						{
							PTID=reqPTID;
							PTName=reqPTName;
							returnValue=true;
						}
					}
					else
					{
						returnValue=true;
					}
				}
			}
			else
				returnValue=false;
		
			return returnValue;
		}		
		private bool issemestervalid(int currsemester, int selectedSemester,int isPre)
		{
			SqlParameter prmCurrSem=new SqlParameter("@currentSem",currsemester );
			SqlParameter prmSelectSem=new SqlParameter("@selectedSem",selectedSemester );
			SqlParameter prmisPre=new SqlParameter("@isPre",isPre );
			SqlParameter prmvalue=new SqlParameter("@returnValue", SqlDbType.Int );
			prmvalue.Direction=ParameterDirection.Output;
			Dbobject._executenonquery("proc_checksemester",scon,CommandType.StoredProcedure,prmCurrSem,prmSelectSem,prmisPre,prmvalue);

			return Convert.ToBoolean(prmvalue.Value);

		}
		private bool IsCourseTakenForOtherPreGroup( string courseNo,DataTable tblPlannedCourses, DataTable tblSummary,DataTable tblPrgCtlgCourses )
		{			
			SqlParameter prmCourseNo=new SqlParameter("@CourseNo",courseNo);
			SqlDataReader dr =Dbobject._getDataReader("sp_getAllMainCourseOfPre",CommandType.StoredProcedure,prmCourseNo);
			
			//bool isPTSelected=false;

			bool isOtherCourseTaken=false;

			while(dr.Read())
			{
				//string AssignToCourse=dr["AssignToCourse"].ToString();
				//string GroupId=dr["GroupId"].ToString();
				string maincourse=dr["CourseNo"].ToString();
				int mainCourseID= Convert.ToInt32(dr["CourseId"]);
				int groupID= Convert.ToInt32(dr["GroupId"]);

				if(tblPrgCtlgCourses.Select("courseid="+mainCourseID).Length>0)
				{
					SqlParameter prmCourNo=new SqlParameter("@CourseId",mainCourseID);
					SqlParameter prmGroupId=new SqlParameter("@groupId",groupID);
					SqlDataReader dr1 =Dbobject._getDataReader("sp_getOtherPreForMainCourse",CommandType.StoredProcedure,prmCourNo,prmGroupId);
					while (dr1.Read())
					{
						string othergroupcourse=dr1["CourseNo"].ToString();

						if(tblPlannedCourses.Select("CourseNo='"+othergroupcourse+"'").Length>0 || tblSummary.Select("CourseNo='"+othergroupcourse+"'").Length>0)
						{
							//isOtherCourseTaken=true;
							isOtherCourseTaken=false;
							break;
						}			
					}
					dr1.Close();
					
				}
				if(isOtherCourseTaken==true)
					break;
				else
					break;	
			}	// End of While loop...

			dr.Close();			

			return isOtherCourseTaken;
		}
		private object PassingGradeInThisCategory(int GradeId,string Category)
		{
			object passValue=null;
			
			if(scon.State==ConnectionState.Closed)
				scon=Dbobject.OpenConnection();
			
			SqlDataReader dr= Dbobject._getDataReader("select IsPassingGrade from ttgradescale where scaleid=(select scaleid from tsgradescale where scalename='"+Category+"') and Gradeid="+GradeId,CommandType.Text);
			if(dr.HasRows)
			{
				dr.Read();
				passValue= dr["IsPassingGrade"];
			}
			dr.Close();
			return passValue;
			
		}	
		// Check whether the student is passed or not in this course....
		private bool validCourseforStudent(int StudentId,int CourseId,string CategoryName)
		{
			bool isValid=false;
			
			SqlParameter prmStudent=new SqlParameter("@studentid",StudentId);
			SqlParameter prmCourse=new SqlParameter("@CourseId",CourseId);
			SqlParameter prmCategory=new SqlParameter("@CategoryName",CategoryName);
			// all the students taking these programs in all semester with the catalogsemester applicable.
			SqlDataReader dr= Dbobject._getDataReader("sp_GetPreviousPassCourseInAuto",CommandType.StoredProcedure,prmStudent,prmCourse,prmCategory);

			if(dr.HasRows)
			{
				dr.Read();
				if(dr["ReturnValue"]!=DBNull.Value)
				{
					if(Convert.ToInt32(dr["ReturnValue"])==2 || Convert.ToInt32(dr["ReturnValue"])==0)
						isValid=true;
				}
			}
			dr.Close();			
			return isValid;
		}	
		private DataTable GetPlannedCourses(int studentId,string ProgramId)
		{
			SqlParameter prmStudent=new SqlParameter("@studentid",studentId);
			SqlParameter prmProgram=new SqlParameter("@ProgramID",ProgramId);
			// all the students taking these programs in all semester with the catalogsemester applicable.
			DataTable tbl=Dbobject._getDataset("sp_GetEduPlanAheadDetails_Auto2",CommandType.StoredProcedure,prmStudent,prmProgram).Tables[0];	
			
			return tbl;
		}
		private DataTable GetPrgCtlgCourses(string ProgramId,int SemesterId,int studentId )
		{						
			SqlParameter prmProgram=new SqlParameter("@ProgramID",ProgramId);
			SqlParameter prmSemester=new SqlParameter("@SemesterId",SemesterId);
			SqlParameter prmStudent=new SqlParameter("@studentid",studentId);
			// all the students taking these programs in all semester with the catalogsemester applicable.
			//DataTable tbl= Dbobject._getDataset("sp_getprgCtlgCourses",CommandType.StoredProcedure,prmProgram,prmSemester,prmStudent).Tables[0];
			DataTable tbl= Dbobject._getDataset("sp_getprgCtlgCourses",CommandType.StoredProcedure,prmProgram,prmSemester).Tables[0];					
			
			return tbl;

		}	
		private DataTable Getfailedcourse(string ProgramId,int SemesterId,int studentId)
		{						
			SqlParameter prmProgram=new SqlParameter("@ProgramID",ProgramId);
			SqlParameter prmSemester=new SqlParameter("@SemesterId",SemesterId);
			SqlParameter prmStudent=new SqlParameter("@studentid",studentId);
			// all the students taking these programs in all semester with the catalogsemester applicable.
			//DataTable tbl= Dbobject._getDataset("sp_getprgCtlgCourses",CommandType.StoredProcedure,prmProgram,prmSemester,prmStudent).Tables[0];
			DataTable tbl= Dbobject._getDataset("failed_course_of_student",CommandType.StoredProcedure,prmStudent,prmProgram,prmSemester).Tables[0];					
			
			return tbl;

		}	
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.ddlcampus.SelectedIndexChanged += new System.EventHandler(this.ddlcampus_SelectedIndexChanged_1);
			this.ddlDegree.SelectedIndexChanged += new System.EventHandler(this.ddlDegree_SelectedIndexChanged);
			this.chkAlert.CheckedChanged += new System.EventHandler(this.chkAlert_CheckedChanged);
			this.btnAutoedplan.Click += new System.EventHandler(this.btnAutoedplan_Click);
			this.BtnView.Click += new System.EventHandler(this.BtnView_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
		# region fill grid
		public void FillGrid()
		{		
			try
			{		
				string strqry="";

				
				DataSet ds=Dbobject._getDataset(strqry,CommandType.Text);
   
                int count=  ds.Tables [0].Rows .Count;

				dgrDetails.DataSource=ds;
				dgrDetails.DataBind();				
	
				if(dgrDetails.PageCount==dgrDetails.CurrentPageIndex+1)
				{
					lblrec.Text="Total number of records: " + count + "";			
				}
				

				if(dgrDetails.Items.Count==0)
				{
					lblrec.Visible=true;
				}
			}

			catch(Exception ex)
			{
				GSpaceCommon.ExceptionHandler.Handle(ex);
			}

		}
		# endregion 
		private void ddlDegree_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			dgrDetails.DataSource=null;
			dgrDetails.DataBind();
			lblrec.Text="";
			BindProgram();
			BtnView.Enabled=false;
		}

		private void BtnView_Click(object sender, System.EventArgs e)
		{
			int semesterId= Convert.ToInt32(ddlsem.SelectedValue);
			
			string programIDs="";
			foreach(ListItem item in chkprogram.Items)
			{
				if(item.Selected)
				{
					if(programIDs!="")
						programIDs+="|";
					programIDs+=item.Value;
				}
			}
			Session["Programs"]=programIDs;			
			Page.RegisterStartupScript("ViewPlanDetail","<script type='text/javascript'>ViewPlanDetail('"+semesterId+"','"+ddlcampus.SelectedValue+"');</script>");
		}

		private void ddlcampus_SelectedIndexChanged_1(object sender, System.EventArgs e)
		{
			dgrDetails.DataSource=null;
			dgrDetails.DataBind();
			lblrec.Text="";
			BtnView.Enabled=false;
			BindProgram();
		}

		private string fnGetStudentCohortdetail(int Studentid)
		{
			string studentCohort="";
			DataTable dt=new DataTable();
			try
			{
				SqlParameter prmStudentId=new SqlParameter("@StudentID",Studentid);
				dt=Dbobject._getDataset("sp_GetStudentCohort",CommandType.StoredProcedure,prmStudentId).Tables[0];	
				if(dt.Rows.Count>0)
				{
					for(int i=0;i<dt.Rows.Count;i++)
					{
						//Check Cohort is Special Cohort or not
						if(dt.Rows[i]["StudentCohort"].ToString()=="PCOHG008440")
						{
							studentCohort="Special Cohort";
						}
						else
						{
							studentCohort="";
						}
					}
				}
			}
			catch(Exception ex)
			{
				GSpaceCommon.ExceptionHandler.Handle(ex);
			}
			finally
			{
			}
			return studentCohort;
		}

		private void chkAlert_CheckedChanged(object sender, System.EventArgs e)
		{
		
		}

	}
}
