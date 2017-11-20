<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="ScissorsTest._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Scissors Validation Framework</h2>
    <div class="form-group row">
        <label for="<%= firstNameInput.ClientID %>" class="col-md-2 control-label">
            First Name</label>
        <div class="col-md-4">
            <asp:TextBox ID="firstNameInput" runat="server" CssClass="form-control" />
        </div>
    </div>
    <div class="form-group row">
        <label for="<%= middleNameInput.ClientID %>" class="col-md-2 control-label">
            Middle Name</label>
        <div class="col-md-4">
            <asp:TextBox ID="middleNameInput" runat="server" CssClass="form-control" />
        </div>
    </div>
    <div class="form-group row">
        <label for="<%= lastNameInput.ClientID %>" class="col-md-2 control-label">
            Last Name</label>
        <div class="col-md-4">
            <asp:TextBox ID="lastNameInput" runat="server" CssClass="form-control" />
        </div>
    </div>
    <div class="form-group row">
        <label for="<%= emailInput.ClientID %>" class="col-md-2 control-label">
            Email</label>
        <div class="col-md-4">
            <asp:TextBox ID="emailInput" runat="server" CssClass="form-control" />
        </div>
    </div>
    <div class="form-group row">
        <label for="<%= yearsExperienceInput.ClientID %>" class="col-md-2 control-label">
            Years of Experience</label>
        <div class="col-md-4">
            <asp:TextBox ID="yearsExperienceInput" runat="server" CssClass="form-control" />
        </div>
    </div>
    <div class="form-group row">
        <label for="<%= ratingInput.ClientID %>" class="col-md-2 control-label">
            Rating</label>
        <div class="col-md-4">
            <asp:TextBox ID="ratingInput" runat="server" CssClass="form-control" />
        </div>
    </div>
    <div class="form-group row">
        <label for="<%= employmentDateInput.ClientID %>" class="col-md-2 control-label">
            Date of Employment</label>
        <div class="col-md-4">
            <asp:TextBox ID="employmentDateInput" runat="server" CssClass="form-control" />
        </div>
    </div>
    <div class="row">
        <asp:Repeater ID="errorsList" runat="server">
            <HeaderTemplate>
                <ul>
            </HeaderTemplate>
            <ItemTemplate>
                <li>
                    <%# Eval("Message") %></li>
            </ItemTemplate>
            <FooterTemplate>
                </ul>
            </FooterTemplate>
        </asp:Repeater>
    </div>
    <div class="form-group">
        <div class="col-sm-offset-2 col-sm-10">
            <asp:Button ID="submitButton" runat="server" Text="Submit" CssClass="btn btn-default"
                OnClick="submitButton_Click" />
        </div>
    </div>
</asp:Content>
