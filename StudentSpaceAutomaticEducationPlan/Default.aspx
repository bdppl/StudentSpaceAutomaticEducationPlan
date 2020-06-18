<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="StudentSpaceAutomaticEducationPlan._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

<html>
<head>
<style>
body {background-color: powderblue;}
h1   {color: blue;}
p    {color: red;}

  .abc{
          margin-top: 206px;
    margin-left: 470px;
    margin-bottom: 212px;
  }
   
  
</style>
</head>
<body>
<div class="abc">

<asp:TextBox ID="TextBox1" PlaceHolder="Enter Student SID" runat="server"></asp:TextBox>
        <asp:Button ID="Search" runat="server" Text="Search" OnClick="Search_Click1" />
    <asp:Label id="Label1" Font-Names="Label1" runat="server"></asp:Label>
 
    </div>
</body>
</html>


</asp:Content>
