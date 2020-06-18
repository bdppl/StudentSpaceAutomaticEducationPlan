<%@ Register TagPrefix="uc1" TagName="topbar" Src="controls/topbar.ascx" %>
<%@ Register TagPrefix="ft1" TagName="footer" Src="controls/footer1.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Admin_leftbar" Src="controls/Admin_leftbar.ascx" %>
<%@ Register TagPrefix="uc1" TagName="leftbarstudentlogin" Src="Controls/leftbarstudentlogin.ascx" %>
<%@ Page language="c#" Codebehind="AutomaticEduPLan.aspx.cs" AutoEventWireup="false" Inherits="PortalG1.AutomaticEducationPlan" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>s t u d e n t | s p a c e - Automatic Education Plan </title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="JavaScript" type="text/JavaScript" src="JScript/calandar.js"> </script>
		<script language="JavaScript" type="text/JavaScript" src="Javascript/search.js"></script>
		<script src="javascript/common.js"></script>
		<script src="menu/mainhead.js"></script>
        
		<LINK runat="server" rel="icon" type="image/x-icon" href="favicon.ico"/>
         <LINK runat="server" rel="shortcut icon" type="image/x-icon" href="favicon.ico" />
		<LINK rel="stylesheet" type="text/css" href="Css/portal_new_css1.css">
		<LINK rel="stylesheet" type="text/css" href="Css/control_style1.css">
		<script type="text/javascript" src="javascript/tooltip1.js"></script>
		<script language="javascript" type="text/javascript">
		
		function OpenPlanView(SemesterId,CourseID)
		{	
			openCenteredWindow('ViewAutoEduPlan.aspx?Sem='+SemesterId+'&CourseId='+CourseID,'Student|Space :: View Plan Details',"toolbar:no;location:no;directories:no;statusbar:no;menubar:no;scrollbars:no;copyhistory:no;resizable:no;",'400','700');			
		}	
		
		function ViewPlanDetail(SemesterId,CampusID)
		{	
			openCenteredWindow('ViewPlanDetail.aspx?Sem='+SemesterId+'&camp='+CampusID,'Student|Space :: View Plan Details','toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=1,resizable=1','450','700');			
		}	
		
         function validate()
		  {
		  		  
		    var ddlcampus=document.getElementById ('ddlcampus');
		     
		   		   if ( ddlcampus.selectedIndex == 0)
		   		   {
		   		       alert("Please select Campus");
		   		       ddlcampus.focus();
		   		       return false;
		   		   }
				
			var chklst = document.getElementById('chkprogram');
		        
			var flag=false;
			var inputs = chklst.getElementsByTagName('input');
			
			for (i=0; i<inputs.length; i++)
				{
					if (inputs[i].type=='checkbox' && inputs[i].checked)
					{
						flag=true;
						break;
					}
				}
		    if(!flag)
		    {
				alert("Please select Program.");		   		
		   		return false;
		    }
		    
		    var ddlsem=document.getElementById ('ddlsem');
		   	   
		   	if ( ddlsem.selectedIndex == 0)
		   	{
		   		alert("Please select Semester");
		   		ddlsem.focus();
		   		return false;
		   	}
		    
		  }		  
		function Check()
		{
			var chklst = document.getElementById('chkprogram');
			
			var inputs = chklst.getElementsByTagName('input');
			
			for (i=0; i<inputs.length; i++)
			{
				if (inputs[i].type=='checkbox')
					inputs[i].checked=true;					
			}
		}
		function Uncheck()
		{
			var chklst = document.getElementById('chkprogram');
			
			var inputs = chklst.getElementsByTagName('input');
			
			for (i=0; i<inputs.length; i++)
			{
				if (inputs[i].type=='checkbox')
					inputs[i].checked=false;					
			}
		}
		
		
		
		         var sessionTimeoutWarning = "<%= System.Configuration.ConfigurationSettings.AppSettings["SessionWarning"].ToString()%>";
        var sessionTimeout = "<%= Session.Timeout %>";
        var timeOnPageLoad = new Date();
        var sessionWarningTimer = null;
        var redirectToWelcomePageTimer = null;
        //For warning
        var sessionWarningTimer = setTimeout('SessionWarning()', 
				parseInt(sessionTimeoutWarning) * 60 * 1000);
        //To redirect to the welcome page
        var redirectToWelcomePageTimer = setTimeout('RedirectToWelcomePage()',
					parseInt(sessionTimeout) * 60 * 1000);
					 //Session Warning
        function SessionWarning() {
            //minutes left for expiry
            var minutesForExpiry =  (parseInt(sessionTimeout) - parseInt(sessionTimeoutWarning));
            var message = "Your session will expire in " + 
		minutesForExpiry + " mins. Do you want to extend the session time to log out? Click OK to continue and Cancel to log out!";

            //Confirm the user if he wants to extend the session
            answer = confirm(message);

            //if yes, extend the session.
            if(answer)
            {
                var img = new Image(1, 1);
                img.src = 'KeepAlive.aspx?date=' + escape(new Date());

                //Clear the RedirectToWelcomePage method
                if (redirectToWelcomePageTimer != null) {
                    clearTimeout(redirectToWelcomePageTimer);
                }
   	       //reset the time on page load
                timeOnPageLoad =  new Date();
                sessionWarningTimer = setTimeout('SessionWarning()', 
				parseInt(sessionTimeoutWarning) * 60 * 1000);
                //To redirect to the welcome page
                redirectToWelcomePageTimer = setTimeout
		('RedirectToWelcomePage()',parseInt(sessionTimeout) * 60 * 1000);
            }

            //*************************
            //Even after clicking ok(extending session) or cancel button, 
	   //if the session time is over. Then exit the session.
            var currentTime = new Date();
            //time for expiry
            var timeForExpiry = timeOnPageLoad.setMinutes(timeOnPageLoad.getMinutes() + 
				parseInt(sessionTimeout)); 

            //Current time is greater than the expiry time
            if(Date.parse(currentTime) > timeForExpiry)
            {
                alert("Session expired. You will be redirected to Login page");
                window.location = "Default.aspx";
            }
            //**************************
        }

        //Session timeout
        function RedirectToWelcomePage(){
            alert("Session expired. You will be redirected to Login page");
            window.location = "Default.aspx";
        }
		
	
		</script>
	</HEAD>
	<body onload="chngbkg('leftbar1_tdAutoEdu');" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<INPUT id="hdSem" type="hidden" name="hdSem" runat="server"><INPUT id="hdSemesterID" type="hidden" name="hdSemesterID" runat="server"><INPUT id="hdflag" type="hidden" name="hdflag" runat="server">
			<table class="tbl_main-1" border="0" cellSpacing="0" cellPadding="0" width="97%" align="center">
				<tbody>
					<tr vAlign="top">
						<td align="center">
							<table border="0" cellSpacing="0" cellPadding="0" width="100%" align="center">
								<tbody>
									<tr vAlign="top">
										<td id="tdTopbar" vAlign="top" runat="server"><uc1:topbar id="Topbar1" runat="server"></uc1:topbar></td>
									</tr>
									<tr>
										<td align="center">
											<table border="0" cellSpacing="0" cellPadding="0" width="100%">
												<tbody>
													<tr vAlign="top">
														<%--<td id="a" class="top_border" bgColor="#7da2ca" vAlign="top" width="19%" runat="server">--%><td id="a" class="top_border" bgColor="#7da2ca" vAlign="top" width="13%" runat="server">
															<div id="leftColumn">
																<div style="BORDER-RIGHT: #c6d0da 1px solid" id="ssp_Left_Nav"><uc1:admin_leftbar id="leftbar1" runat="server"></uc1:admin_leftbar></div>
															</div>
														</td>
														<td vAlign="top" width="81%">
															<table class="left_border" border="0" cellSpacing="0" cellPadding="0" width="100%">
																<tbody>
																	<tr vAlign="top">
																		<td style="HEIGHT: 34px" class="tbl_top_header" vAlign="middle" noWrap align="left">
																			<!--<IMG alt="Student Personal Information" src="Images/two_students.gif" align="absMiddle">--><strong class="lblHeader_white">&nbsp; 
																				Automatic Education Plan</strong></td>
																	</tr>
																	<tr vAlign="top">
																		<td style="HEIGHT: 4px"><asp:label style="Z-INDEX: 0" id="lblError" runat="server" CssClass="err_msg_color"></asp:label></td>
																	</tr>
																	<tr vAlign="top">
																		<td vAlign="top">
																			<table style="Z-INDEX: 0" id="Table5" class="tbl_content50" border="0" cellSpacing="2" cellPadding="1"
																				width="100%">
																				<tbody>
																					<tr vAlign="top">
																						<td>
																							<div class="rbroundbox">
																								<div class="rbtop">
																									<div></div>
																								</div>
																								<div style="Z-INDEX: 0" class="rbcontent">
																									<table style="WIDTH: 100%" border="0" cellSpacing="0" cellPadding="0">
																										<tr vAlign="top">
																											<td width="50%" align="left"><strong class="header_bk_color"></strong></td>
																											<td width="50%" align="right"><%--<span class="header_bk_color"><A href="#"><IMG border="0" alt="Help" align="top" src="Images/HelpIcon01.png"></A></span>--%></td>
																										</tr>
																									</table>
																									<table id="tbl_info2" class="tbl_content_area" border="1" rules="rows" cellSpacing="0"
																										borderColorLight="#f0fff0" cellPadding="2" width="100%">
																										<tr class="back_color_blue" vAlign="top">
																											<td style="WIDTH: 10%; HEIGHT: 30px" noWrap><asp:label id="Label6" onmouseover="ddrivetip('Select the Campus.', 200)" onmouseout="hideddrivetip()"
																													runat="server">Campus<FONT class="smallfontred">*</FONT>&nbsp;:</asp:label></td>
																											<td style="WIDTH: 45%; HEIGHT: 30px" class="right_border"><asp:dropdownlist id="ddlcampus" runat="server" CssClass="textbox" AutoPostBack="True" Width="241px"></asp:dropdownlist></td>
																											<td style="WIDTH: 15%; HEIGHT: 30px" vAlign="top" noWrap><asp:label id="Label3" onmouseover="ddrivetip('Select the Degree.', 200)" onmouseout="hideddrivetip()"
																													runat="server">Degree&nbsp;:</asp:label></td>
																											<td style="WIDTH: 30%; HEIGHT: 30px" class="right_border" vAlign="top"><asp:dropdownlist id="ddlDegree" runat="server" CssClass="textbox" AutoPostBack="True" Width="241px"
																													DataValueField="DegreeId" DataTextField="DegreeName"></asp:dropdownlist></td>
																										</tr>
																										<TR class="back_color_gray">
																											<TD vAlign="top" noWrap><asp:label id="Label4" Font-Size="9pt" onmouseover="ddrivetip('Select the Program(s).', 200)" onmouseout="hideddrivetip()"
																													runat="server">Program<FONT class="smallfontred">*</FONT>&nbsp;:</asp:label><br>
																												<br>
																												<A style="COLOR: red; FONT-SIZE: 11px; FONT-WEIGHT: bold" onclick="Check()" href="#">
																													Select All</A><br>
																												<A style="COLOR: red; FONT-SIZE: 11px; FONT-WEIGHT: bold" onclick="Uncheck()" href="#">
																													UnSelect All</A>
																											</TD>
																											<TD class="right_border"><asp:panel style="Z-INDEX: 0; OVERFLOW: auto" id="Panel1" runat="server" CssClass="textbox"
																													Width="310" Height="85px">
																													<asp:checkboxlist id="chkprogram" runat="server" CssClass="rdbox" ForeColor="Black" Font-Size="10px"
																														CellSpacing="0" CellPadding="0"></asp:checkboxlist>
																												</asp:panel></TD>
																											<td vAlign="top" noWrap><asp:label id="Label5" Font-Size="9pt" onmouseover="ddrivetip('Select the Semester.', 200)" onmouseout="hideddrivetip()"
																													runat="server">Semester<FONT class="smallfontred">*</FONT>&nbsp;:</asp:label></td>
																											<td vAlign="top" noWrap><%--<asp:dropdownlist id="ddlsem" runat="server" CssClass="textbox" Width="237px" DataValueField="SemesterID"
																													DataTextField="Semester" Height="38px"></asp:dropdownlist>--%><asp:dropdownlist id="ddlsem" runat="server" CssClass="textbox" Width="237px" DataValueField="SemesterID"
																													DataTextField="Semester" Height="25px"></asp:dropdownlist><br>
																												<br>
																												<FONT style="FONT-SIZE: 11px" class="smallfontred">Note: List of Semesters,<br>
																													for which PartTerm has been defined.</FONT>
																												<br>
																												<br>
																												<asp:checkbox id="chkAlert" Font-Bold="True" Text="Is automatic alert enabled" Runat="server"></asp:checkbox></td>
																										</TR>
																										<TR class="back_color_blue" vAlign="top">
																											<TD style="WIDTH: 20%" noWrap><asp:label id="lblCohort" onmouseover="ddrivetip('Search by Cohort', 200)" onmouseout="hideddrivetip()"
																													runat="server">Cohort : </asp:label></TD>
																											<TD class="right_border"><asp:panel style="Z-INDEX: 0; OVERFLOW: auto" id="Panel2" runat="server" CssClass="textbox"
																													Width="310" Height="85px">
																													<asp:checkboxlist id="chkCohort" runat="server" CssClass="rdbox" ForeColor="Black" Font-Size="10px"></asp:checkboxlist>
																												</asp:panel></TD>
																										</TR>
																									</table>
																								</div>
																								<div class="rbbot">
																									<div></div>
																								</div>
																							</div>
																						</td>
																					</tr>
																				</tbody>
																			</table>
																		</td>
																	</tr>
																	<tr>
																		<td align="left"><FONT class="smallfont_blue">[Note :&nbsp;<font class="smallfontred">*</font>Mandatory 
																				fields]</FONT>
																		</td>
																	</tr>
																	<tr>
																		<td style="HEIGHT: 38px" align="center"><asp:button style="Z-INDEX: 0" id="btnAutoedplan" runat="server" CssClass="buttons" Width="172px"
																				Height="24px" BackColor="Info" ToolTip="To Automatic Education Plan" text="Automatic Education Plan"></asp:button>&nbsp; 
																			&nbsp; &nbsp;
																			<asp:button id="BtnView" runat="server" CssClass="buttons" Width="120px" Text="View/Print Plan Log"
																				BackColor="Info" ToolTip="Click to view Plan Details." Enabled="False"></asp:button></td>
																	</tr>
																	<tr>
																		<td style="WIDTH: 100%; HEIGHT: 34px" class="tbl_top_header" colSpan="4"><strong class="lblHeader_white">&nbsp;Automatic 
																				Education Plan Details </strong>
																		</td>
																	</tr>
																	<tr>
																		<td width="100%" colSpan="4"><asp:label id="lblrec" runat="server" CssClass="bg_colour" Width="100%" ForeColor="Red"></asp:label></td>
																	</tr>
																	<!--<tr>
																		<td>
																			<asp:label style="Z-INDEX: 0" id="Label1" runat="server">Program<FONT class="smallfontred">
																				</FONT>&nbsp;:</asp:label>&nbsp;
																			<asp:dropdownlist style="Z-INDEX: 0" id="ddlprogram" runat="server" CssClass="textbox" Width="303px"></asp:dropdownlist>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
																			<asp:label style="Z-INDEX: 0" id="Label2" runat="server">Total Number OF Students<FONT class="smallfontred">
																				</FONT>&nbsp;:</asp:label>
																			<asp:textbox style="Z-INDEX: 0" id="txtcredithours" runat="server" CssClass="textbox" Width="117px"
																				Height="18px"></asp:textbox>
																		</td>
																		<td>
																		</td>
																		<td>
																		</td>
																	</tr>-->
																	<tr>
																		<td>
																			<div style="Z-INDEX: 0; WIDTH: 100%; HEIGHT: 300px; OVERFLOW: auto" id="divGrid" runat="server"><asp:datagrid id="dgrDetails" runat="server" Width="735px" CellPadding="1" BackColor="White" ShowFooter="True"
																					HorizontalAlign="Left" PageSize="10" PagerStyle-Visible="False" BorderWidth="1px" BorderColor="#C0C0FF" autogeneratecolumns="False" cssclass="grida" BorderStyle="None" DataKeyField="CourseID">
																					<FooterStyle ForeColor="Red" Font-Bold="True" BackColor="#B5C7DE"></FooterStyle>
																					<SelectedItemStyle Font-Italic="True" ForeColor="#4A3C8C" BackColor="#FFC0C0"></SelectedItemStyle>
																					<AlternatingItemStyle BackColor="#F7F7F7" Height="20px"></AlternatingItemStyle>
																					<ItemStyle ForeColor="#4A3C8C" BackColor="#E7E7FF" Height="20px"></ItemStyle>
																					<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" CssClass="gridheader" BackColor="#36669B"></HeaderStyle>
																					<Columns>
																						<asp:BoundColumn DataField="Total" HeaderText="No.of Students">
																							<ItemStyle Width="150px"></ItemStyle>
																						</asp:BoundColumn>
																						<asp:BoundColumn DataField="CourseNo" HeaderText="Course No">
																							<ItemStyle Width="180px"></ItemStyle>
																						</asp:BoundColumn>
																						<asp:BoundColumn DataField="CourseName" HeaderText="Course Name">
																							<ItemStyle></ItemStyle>
																						</asp:BoundColumn>
																						<asp:TemplateColumn HeaderText="View Details" ItemStyle-HorizontalAlign="Center">
																							<ItemTemplate>
																								<asp:LinkButton ID="lnkViewPlan" Runat="server" Font-Bold="True" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.CourseID") %>' OnClick="lnkViewPlan_Click">View Details</asp:LinkButton>
																							</ItemTemplate>
																						</asp:TemplateColumn>
																					</Columns>
																					<PagerStyle VerticalAlign="Middle" Visible="False" HorizontalAlign="Right" ForeColor="#4A3C8C"
																						BackColor="Silver" Mode="NumericPages"></PagerStyle>
																				</asp:datagrid></div>
																			<asp:panel id="pnlProgress" Runat="server" HorizontalAlign="Center" Visible="False">
																				<IMG src="Images/ProgressBar.gif">
																			</asp:panel></td>
																	</tr>
																	<tr>
																		<td>&nbsp;</td>
																	</tr>
																</tbody>
															</table>
														</td>
													</tr>
												</tbody>
											</table>
										</td>
									</tr>
								</tbody>
							</table>
						</td>
					</tr>
				</tbody>
			</table>
			<div align="center"><ft1:footer id="footer1" runat="server"></ft1:footer></div>
		</form>
	</body>
</HTML>
