    <%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentPlans.aspx.cs" Inherits="StudentSpaceAutomaticEducationPlan.StudentPlans" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">



    <meta charset="utf-8">

    <title>StudetSpace</title>
    <meta name="description" content="The HTML5 Herald">
    <meta name="author" content="GeetaZaildar">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link href="//db.onlinewebfonts.com/c/b6968516e7add824ac6ed94c7098bd06?family=CentGothWGL" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css">
    
    <link rel="stylesheet" href="css/bootstrap.min.css">
    <link rel="stylesheet" href="css/bootstrap-grid.min.css">
    <link rel="stylesheet" href="css/bootstrap-reboot.min.css">
    <link rel="stylesheet" href="css/style.css">
     <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
  <link rel="stylesheet" href="/resources/demos/style.css">
  <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
  <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>

    <%--<script src="js/jquery.min.js"></script>--%>
    <script type="text/javascript" src="js/bootstrap.min.js"></script>
    <script type="text/javascript" src="js/bootstrap.bundle.min.js"></script>
<%--        <script src="Scripts/jquery-ui-1.12.1.min.js"></script>--%>

    
    <style>
        .button2 {
            background-color: white;
            color: black;
            border: 2px solid #008CBA;
        }

        .button3 {
            margin-left: 3px;
            margin-right: 1px margin-bottom: 25px;
            margin-right: -75px;
        }
       
.weekDays-selector input {
  display: none!important;
}

.weekDays-selector input[type=checkbox] + label {
  display: inline-block;
  border-radius: 6px;
  background: #dddddd;
  height: 40px;
  width: 30px;
  margin-right: 3px;
  line-height: 40px;
  text-align: center;
  cursor: pointer;
}

.weekDays-selector input[type=checkbox]:checked + label {
  background: #2AD705;
  color: #ffffff;
}


    </style>


        <%--<script src="jquery.min.js" type="text/javascript"></script>  --%>
  


    <script type="text/javascript">  
        var popup;
        function OpenProgramPopUp() {
            popup = window.open("ProgramList.aspx", "Popup", "width=600,height=900");
            popup.focus();
        }

        $(document).ready(function() {  
            SearchText();  
        });  
        function SearchText() {         
           
            $("#txtProgram").autocomplete({  
                source: function(request, response) {  
                    $.ajax({  
                        type: "POST",  
                        contentType: "application/json; charset=utf-8",  
                        url: "StudentPlans.aspx/GetProgramName",  
                       
                      data: '{SearchProgramTxt: "' + document.getElementById('txtProgram').value + '", StudentId: ' + document.getElementById("hdnStudentId").value + '}',


                        dataType: "json",  
                        success: function (data) {                              
                           response($.map(data.d, function (item) {
                               return {
                                   label: item.ProgramName,
                                val: item.ProgramId
                            }
                        }))
                        },  
                        error: function(result) {  
                            console.log(result);
                        }  
                    });  
                }  ,
            select: function (e, i) {
                $("#<%=hdnSelectedProgramId.ClientID %>").val(i.item.val);
            }
            });  


       

            //$( "#txtProgramName" ).autocomplete({
            //      source: function( request, response ) {
            //        $.ajax( {
            //          url: "StudentPlans.aspx/GetProgramName",
            //          dataType: "jsonp",
            //           data: "{'SearchProgramTxt':'" + document.getElementById('txtProgramName').value + "', 'StudentId':'"+ studentId+"'}",  
            //          success: function( data ) {
            //              console.log(data);
            //          }
            //        } );
            //      },
            //      minLength: 2,
            //      select: function( event, ui ) {
            //        console.log( "Selected: " + ui.item.value + " aka " + ui.item.id );
            //      }
            //    } );
        }  
        function myFunction() {
          var x = document.getElementById("myDIV");
          if (x.style.display == "none") {
            x.style.display = "block";
          } else {
            x.style.display = "none";
          }
        }


$(function() {
$('#<%=btnclick.ClientID%>').click(function() {
$("#popupdiv").dialog({
//title: "jQuery Popup from Server Side",
width: 1200,
height: 800,
modal: true,
buttons: {
Close: function() {
$(this).dialog('close');
}
}
});
return false;
});
})

    </script> 
</head>
<body>
    <div class="container_fluid ">

        <div class="MainMenu">
            <div class="container">
                <div class="col-lg-12">
                    <div class="pos-f-t">
                        <div class="collapse" id="navbarToggleExternalContent">
                            <div class="menu_inner">
                                <ul>
                                    <li><a href="#" class="menu_link">Home</a></li>
                                    <li><a href="#" class="menu_link">Menu Link</a></li>
                                    <li><a href="#" class="menu_link">Menu Link</a></li>
                                    <li><a href="#" class="menu_link">Menu Link</a></li>
                                    <li><a href="#" class="menu_link">Menu Link</a></li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-4 text-left">
                        <nav class="navbar navbar-dark ">
                            <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarToggleExternalContent" aria-controls="navbarToggleExternalContent" aria-expanded="false" aria-label="Toggle navigation">
                                <span class="navbar-toggler-icon"></span>
                            </button>
                        </nav>
                    </div>
                    <div class="col-lg-4 text-center">
                        <a href="#">
                            <img src="images/main_logo.png"/></a>
                    </div>
                    <div class="col-lg-4 text-right header_top_right">
                        <p>
                            <span>Logged in as :</span><br>support admin (AdminStaff)
                        </p>
                        <a href="#">
                            <img src="images/logout.png"/></a>
                    </div>
                </div>
            </div>
        </div>
        <div class="page_title">
            <h1>Education Plan</h1>
        </div>
    </div>
    <div class="container">
        <div class="main_box">
            <div class="orange_bar row">
                <div class="col-md-6 text-left">
                    <h2>
                        <asp:Label Text="" ID="lblHeaderName" runat="server" />
                        <span class="line_hor"></span></h2>
                    <h2>Education Plan</h2>
                </div>
                <div class="col-md-6 text-right main_right_orange">
                    <a href="#">
                        <p>Print Education Plan </p>
                        <img src="images/printer.png"/></a>
                </div>

            </div>

            <div class="row">
                <div class="left_main col-md-3 text-center">
                    <a href="#">
                        <img src="images/defaultimage.png"/>
                        Upload Photo
                    </a>
                    <ul>
                        <li>
                            <p>
                                <span>ID :</span>
                                <asp:Label Text="" ID="lblSID" runat="server" />
                            </p>
                        </li>
                        <li>
                            <p>
                                <span>Name :</span>
                                <asp:Label Text="" ID="lblName" runat="server" />
                            </p>
                        </li>
                        <li>
                            <%--  <p><span>DOB :</span> Aug 12 1978</p>--%>
                            <p>
                                <span>DOB :</span>
                                <asp:Label Text="" ID="LblDOB" runat="server" />
                            </p>
                        </li>
                        <li>
                            <%--<p><span>Email :</span> support@studentspace.com</p>--%>
                            <p>
                                <span>Email :</span>
                                <asp:Label Text="" ID="LblEmailid" runat="server" />
                            </p>
                        </li>
                        <ul>

                            <div class="shortlinks">
                                <h2>Other Info </h2>
                                <hr>
                                <ul class="shorlistlinks">
                                    <li><a href="#">
                                        <img src="images/logo1.png">Multiple Address</a></li>
                                    <li><a href="#">
                                        <img src="images/logo2.png">Employment On-Campus</a></li>
                                    <li><a href="#">
                                        <img src="images/logo3.png">Employment Off-Campus</a></li>
                                    <li><a href="#">
                                        <img src="images/logo4.png">High School</a></li>
                                    <li><a href="#">
                                        <img src="images/logo5.png">Time Based Cohort</a></li>
                                    <li><a href="#">
                                        <img src="images/logo6.png">Financial Aid</a></li>
                                </ul>
                            </div>
                </div>
               
                <div class="right_main Syl-White col-md-9 justify-content-center">
                     <form  method="post" runat="server">
               <asp:HiddenField ID="hdnStudentId" runat="server" ClientIDMode="Static" />    
                         <asp:HiddenField ID="hdnSelectedProgramId" runat="server" ClientIDMode="Static" />    
                    <asp:Panel runat="server" ID="pnlPlans" >

                    <div class="col-md-12 ">

                         
                        <asp:DataList ID="DataList1" OnItemDataBound="DataListItemBound" runat="server" CssClass="row">
                            <ItemTemplate>
                                <div class="edu_plan_box_main ">
                                    <div class="row ">
                                        <div class="progressbar text-center">
                                            <h1>In Progress</h1>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="row productList">

                                                <div class="col-10">
                                                    <h2><%#Eval("ProgramName")%></h2>
                                                    <h2><i><%#Eval("SemesterName")%></i></h2>


                                                </div>
                                                <div class="col-md-2">
                                                    <h3><%#Eval("termcode")%></h3>
                                                    <h3>
                                                        <button class="button button2">Check Program Status</button></h3>
                                                </div>

                                            </div>
                                        </div>

                                        <hr>
                                        <div class="col-md-4">
                                            <h4>Total Credit Hours : <span>
                                                <%#Convert.ToInt32( Eval("TotalCrHr"))%></span></h4>
                                        </div>
                                        <div class="col-md-4">
                                            <h4>Total Passed :  <span>
                                                <asp:Label Text="" ClientIDMode="Static" ID="lblPassCredits" runat="server" /></span></h4>
                                        </div>
                                        <div class="col-md-4">
                                            <h4>Total Remaining : <span>
                                                <asp:Label Text="" ClientIDMode="Static" ID="lblRemCredit" runat="server" /></span></h4>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>


                        </asp:DataList>

                        <a href="#">
                            <div class="edu_plan_box_main addnew align-items-center">
                                <img src="images/addbox.png">
                                <%--<button class="button button3">Create Program </button>--%>
                                <asp:Button Text="Create New Plan" runat="server" Id="btnAddProgram" OnClick="btnAddPlan_Click"/>
                            </div>

                        </a>
                               
                    </div>

                    </asp:Panel>

                   

                    <asp:Panel runat="server" ID="pnlCreatePlan">
                        <asp:LinkButton Text="Back to Plans" runat="server" class="top_float_btn" OnClick="btnClosePlan_Click"/>
                       <%-- <a class="top_float_btn" href="#"> < Back to Plans </a>--%>
                        <div class="row">
                            <div class="col-md-12 text-center main_box_con">
                               <h1> <asp:Label Text="Create New Plan" runat="server" /></h1>                            
                                <p><asp:Label Text="Chose a program and adjust your plan setting as needed." runat="server" /></p>
                           
                                  <asp:Label Text="Catalog year" runat="server" />
                                  <asp:DropDownList runat="server" ID ="ddlCatalogYear">
                                      <asp:ListItem Text="text1" />
                                      <asp:ListItem Text="text2" />
                                  </asp:DropDownList>

                           
                                  <p> <div class="insertbox"> <asp:Label Text="Program" runat="server" /><asp:Button ID="btnclick" runat="server" Text="Browse" OnClick="btnclick_Click" /></p> <%--<a href="#" onclick="OpenProgramPopUp();"> <asp:Label ID="btnPopup" runat="server" Text="Browse" /></a></p>--%>
                               <asp:TextBox runat="server" Id="txtProgram" ClientIDMode ="Static"/>

                                  </div> 
                           
                               <a href="#"> <asp:Label Text="Advanced Setting" onclick="myFunction(); return false ;" runat="server" /></a> <br>
                          <%-- <button onclick="myFunction(); return false ;">Try it</button>--%>

                            <div id="myDIV" class="advanceform" style="display:none;">
                               <a href="#" class="box-close-mark"> <asp:Label Text="X " onclick="myFunction(); return false ;" runat="server" /></a> 
                                <div class="full_box"><asp:Label Text="How many semesters you want to keep in a year?" runat="server" />
                                    <select id="SemReq" name="SemReq">
                                        <option value="3">3</option>
                                        <option value="4">4</option>
                                        
                                    </select>
                                </div>
                                <div class="full_box"><asp:Label Text="What term does your plan start?" runat="server" />
                                    <select id="TermPlanStart" name="TermPlanStart">
                                        <option value="Spring">Spring</option>
                                        <option value="Summer">Summer</option>
                                        <option value="Fall">Fall</option>
                                    </select>
                                </div>                        
                                
                                <div class="full_box">
                                    <asp:Label Text="About how many credit do you want to take per term?" runat="server" />
                                    
                                        <input type="text" id="CreditTakeTerm" name="CreditTakeTerm"/>

                                    

                                </div>
                                <div class="full_box">
                                    <asp:Label Text="Do you want to take Summer classes?" runat="server" />
                                    <div id="group1">
                                       <asp:RadioButton id="SummerClassesYes" Text="Yes" Checked="True" GroupName="SummerClasses" runat="server"/>
                                        <asp:RadioButton id="SummerClassesNo" Text="No" Checked="True" GroupName="SummerClasses" runat="server"/>

                                    </div>

                                </div>       
        
    
                                <div class="full_box">
                                    <asp:Label Text="Do you want to take online classes?" runat="server" />
                                    <div id="group2">
                                 
                                        <asp:RadioButton id="OnlineClsYes" Text="Yes" Checked="True" GroupName="OnlineCls" runat="server"/>
                                        <asp:RadioButton id="OnlineClsNo" Text="No" Checked="True" GroupName="OnlineCls" runat="server"/>

                                    </div>

                                </div>
                                <div class="full_box">
                                    <asp:Label Text="What type of schedule do you prefer?" runat="server" />
                                    <div id="group3">
                                        <asp:RadioButton id="LessTimeBetCls" Text="Less time between classes" Checked="True" GroupName="LessTimeBetCls" runat="server"/>
                                        <asp:RadioButton id="FewerTimeBetCls" Text="Fewer days with classes" Checked="True" GroupName="LessTimeBetCls" runat="server"/>

                                    </div>

                                </div>

                            
                                <div class="full_box">
                                    <asp:Label Text="When do you prefer to go to class?" runat="server" />
                                    <div id="group4">
                                        <asp:RadioButton id="RadioBtnAnytime" Text="Anytime" Checked="True" GroupName="Anytime" runat="server"/>
                                        <asp:RadioButton id="RadioBtnAfternoon" Text="Afternoon" Checked="True" GroupName="Anytime" runat="server"/>
                                        <asp:RadioButton id="RadioBtnMorning" Text="Morning" Checked="True" GroupName="Anytime" runat="server"/>
                                        <asp:RadioButton id="RadioBtnEvening" Text="Evening" Checked="True" GroupName="Anytime" runat="server"/>

                                    </div>

                                </div>
                                <div class="full_box"><asp:Label Text="What day are you available?" runat="server" /><div class="weekDays-selector">
                                  <input type="checkbox" id="weekday-mon" class="weekday" />
                                  <label for="weekday-mon">M</label>
                                  <input type="checkbox" id="weekday-tue" class="weekday" />
                                  <label for="weekday-tue">T</label>
                                  <input type="checkbox" id="weekday-wed" class="weekday" />
                                  <label for="weekday-wed">W</label>
                                  <input type="checkbox" id="weekday-thu" class="weekday" />
                                  <label for="weekday-thu">T</label>
                                  <input type="checkbox" id="weekday-fri" class="weekday" />
                                  <label for="weekday-fri">F</label>
                                  <input type="checkbox" id="weekday-sat" class="weekday" />
                                  <label for="weekday-sat">S</label>
                                  <input type="checkbox" id="weekday-sun" class="weekday" />
                                  <label for="weekday-sun">S</label>
                                </div></div>
                                <div class="full_box">
                                    <asp:Label Text="What is the earliest class you can attend?" runat="server" /> 
                                    <asp:DropDownList ID="ddlTimeFrom" runat="server"></asp:DropDownList>

                                </div>
                                <div class="full_box">
                                    <asp:Label Text="What is the latest class you can attend?" runat="server" />
                                    <asp:DropDownList ID="ddlTimeTo" runat="server"></asp:DropDownList>

                                </div>
                         
                        </div> 
                            
                            <asp:Button Text="Cancel" runat="server" class="cancel-btn" Id="btnCancelPlan" OnClick ="btnClosePlan_Click"/>
                            <asp:Button Text="Build Plan" runat="server" Id="btnBuildPlan" OnClick ="btnCreatePlan_Click" />
                            
                               <%--<input type="button" class="cancel-btn" value="Cancel"/>--%>
                            <%--<input type="button"  value="Build Plan"/>--%>
                            </div>
                        
                         </asp:Panel>
                        

                        <div>
                        <div id="popupdiv" title="Select Program From List" style="display: none">
                        
                            <iframe src="ProgramList.aspx" width="100%" height="100%"></iframe>
                        </div>
                      <%--  <table align="center" style="margin-top:200px">
                        <tr>
                        <td>
                        <asp:Button ID="btnclick" runat="server" Text="Show Modal Popup" OnClick="btnclick_Click" />
                        </td>
                        </tr>
                        </table>--%>

                        </div>
                       
                   
                      </form>

                    </div>
                </div>

                   
            </div>
        </div>
    </div>
     


    <div class="footer text-center">
        <h6>Copyright © 2020.   s t u d e n t | s p a c e  Corporation. All Rights Reserved.  >
    <script>
        $(document).ready(function () {
            // Add minus icon for collapse element which is open by default
            $(".collapse.show").each(function () {
                $(this).prev(".card-header").find(".fa").addClass("fa-minus").removeClass("fa-plus");
            });

            // Toggle plus minus icon on show hide of collapse element
            $(".collapse").on('show.bs.collapse', function () {
                $(this).prev(".card-header").find(".fa").removeClass("fa-plus").addClass("fa-minus");
            }).on('hide.bs.collapse', function () {
                $(this).prev(".card-header").find(".fa").removeClass("fa-minus").addClass("fa-plus");
            });
        });
    </script>

    


    <script>
        var closebtns = document.getElementsByClassName("close");
        var i;

        for (i = 0; i < closebtns.length; i++) {
            closebtns[i].addEventListener("click", function () {
                this.parentElement.style.display = 'none';
            });
        }
    </script>

    </footer>

</body>
</html>

