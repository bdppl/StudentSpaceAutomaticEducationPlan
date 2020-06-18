<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProgramList.aspx.cs" Inherits="StudentSpaceAutomaticEducationPlan.ProgramList1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        .pnlHeading {
            width: 80%;
            height: 40px;
            padding: 5px;
            text-decoration: underline;
            float: left;
            color: blue;
            font-size: 23px;
            
        }
        .programName {
            width:33%;
            padding:2px;
            float:left;
            word-wrap: break-word;
        }
        .programList{
            float:left;
            width: 100%;
        }
        .lblAddprog {
            
            margin-left: 369px;
            text-decoration-color: #ab2c34;
        }
        .lblAddprogline{
                margin-left: 404px;
        }
        .divAddprog{
       
        color: blue;
        font-size: 31px;
        }
        .divAlphabet{
        color: blue;
        
        align-items: center;
        margin-left: 151px;
        font-size: 24px;
        }
        .divAlphabet{

        }
    </style>
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script> 
        function setProgramName(control) {
         //   if (window.opener != null && !window.opener.closed) {
                var clickId = control.id;
                var numId = clickId.split('_')[2];
              //  alert(numId);

                var programName = document.getElementById("ProgramList_lblProgramName_" + numId).innerText;
                var programId = document.getElementById("ProgramList_hdnProgramId_" + numId).value;

                var parentProgramName = parent.document.getElementById("txtProgram");
                var parentProgramId = parent.document.getElementById("hdnSelectedProgramId");

                parentProgramId.value = programId;
            parentProgramName.value = programName;

           // parent.document.getElementById("popupdiv").style.display = "none";
            window.parent.$('#popupdiv').dialog('close');

            }

           
            
      //  }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="float:left; width: 100%">
            <div class="divAddprog"><asp:Label Text="Add a Program to your plan." Id="lblAddprog" CssClass="lblAddprog" runat="server" /></div>
            <div class="divAddprogline"><asp:Label Text="Press the program to learn more about it." Id="lblAddprogline" CssClass="lblAddprogline" runat="server" /></div>
            <div class="divAlphabet ">
                <a href="#Target_A"><asp:Label Text="A |" Id="A" CssClass="lblAlphabet" runat="server" /> </a> 
                <a href="#Target_B"><asp:Label Text="B |" Id="B" CssClass="lblAlphabet" runat="server" /> </a>
                <a href="#Target_C"><asp:Label Text="C |" Id="C" CssClass="lblAlphabet" runat="server" /> </a>
                <a href="#Target_D"><asp:Label Text="D |" Id="D" CssClass="lblAlphabet" runat="server" /> </a>
                <a href="#Target_E"><asp:Label Text="E |" Id="E" CssClass="lblAlphabet" runat="server" /> </a>
                <a href="#Target_F"><asp:Label Text="F |" Id="F" CssClass="lblAlphabet" runat="server" /> </a>
                <a href="#Target_G"><asp:Label Text="G |" Id="G" CssClass="lblAlphabet" runat="server" /> </a>
                <a href="#Target_H"><asp:Label Text="H |" Id="H" CssClass="lblAlphabet" runat="server" /> </a>
                <a href="#Target_I"><asp:Label Text="I |" Id="I" CssClass="lblAlphabet" runat="server" /> </a>
                <a href="#Target_J"><asp:Label Text="J |" Id="J" CssClass="lblAlphabet" runat="server" /> </a>
                <a href="#Target_K"><asp:Label Text="K |" Id="K" CssClass="lblAlphabet" runat="server" /> </a>
                <a href="#Target_L"><asp:Label Text="L |" Id="L" CssClass="lblAlphabet" runat="server" /> </a>
                <a href="#Target_M"><asp:Label Text="M |" Id="M" CssClass="lblAlphabet" runat="server" /> </a>
                <a href="#Target_N"><asp:Label Text="N |" Id="N" CssClass="lblAlphabet" runat="server" /> </a>
                <a href="#Target_O"><asp:Label Text="O |" Id="O" CssClass="lblAlphabet" runat="server" /> </a>
                <a href="#Target_P"><asp:Label Text="P |" Id="P" CssClass="lblAlphabet" runat="server" /> </a>
                <a href="#Target_Q"><asp:Label Text="Q |" Id="Q" CssClass="lblAlphabet" runat="server" /> </a>
                <a href="#Target_R"><asp:Label Text="R |" Id="R" CssClass="lblAlphabet" runat="server" /> </a>
                <a href="#Target_S"><asp:Label Text="S |" Id="S" CssClass="lblAlphabet" runat="server" /> </a>
                <a href="#Target_T"><asp:Label Text="T |" Id="T" CssClass="lblAlphabet" runat="server" /> </a>
                <a href="#Target_U"><asp:Label Text="U |" Id="U" CssClass="lblAlphabet" runat="server" /> </a>
                <a href="#Target_V"><asp:Label Text="V |" Id="V" CssClass="lblAlphabet" runat="server" /> </a>
                <a href="#Target_W"><asp:Label Text="W |" Id="W" CssClass="lblAlphabet" runat="server" /> </a>
                <a href="#Target_X"><asp:Label Text="X |" Id="X" CssClass="lblAlphabet" runat="server" /> </a>
                <a href="#Target_Y"><asp:Label Text="Y |" Id="Y" CssClass="lblAlphabet" runat="server" /> </a>
                <a href="#Target_Z"><asp:Label Text="Z |" Id="Z" CssClass="lblAlphabet" runat="server" /> </a>
                

            </div>
            <asp:DataList runat="server" ID="ProgramList" OnItemDataBound="ProgramList_ItemBound" RepeatLayout="Flow" CssClass="programList">
                
                <ItemTemplate>
                    
                    <asp:Panel runat="server" CssClass="pnlHeading" ID="pnlHeading" ClientIDMode="Static" Visible="false">
                        <asp:HyperLink id="aTag" runat="server" ClientIDMode="Static"> <asp:Label Text="" Id="lblHeading" runat="server" ClientIDMode="Static" Visible="false"/> </asp:HyperLink>
                    </asp:Panel>
                    
                    <div class="programName">
                        <%--<asp:LinkButton Text="" Id="lkbtnProgramName" runat="server" OnClientClick="setProgramName(this);" />--%>
                    <a  runat="server" href="#" id="lnkProgram" onclick="setProgramName(this)">    
                        <asp:Label Text="" Id="lblProgramName" runat="server" /> </a>
                        <asp:HiddenField Id="hdnProgramId" runat="server" />
                    </div>
                    
                    
                </ItemTemplate>
            </asp:DataList>
        </div>
    </form>
</body>
</html>
