﻿<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="department.aspx.cs" Inherits="Lesson_6.department" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Department Details</h1>
    <h5>All Fields Required</h5>
    <fieldset>
        <label for="txtDepartmentName" class="col-sm-2">Department Name: </label>
        <asp:TextBox ID="txtDepartmentName" runat="server" required MaxLength="50" />
    </fieldset>
    <fieldset>
        <label for="txtBudget" class="col-sm-2">Budget: </label>
        <asp:TextBox ID="txtBudget" runat="server" required MaxLength="50" />
        <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Must be Greater than 0"
            ControlToValidate="txtBudget" CssClass="alert alert-danger"
            MinimumValue="0" MaximumValue="9999999999"></asp:RangeValidator>
    </fieldset>

    <div class="col-sm-offset-2">
        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_Click" />
    </div>
</asp:Content>