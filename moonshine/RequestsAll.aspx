﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RequestsAll.aspx.cs" Inherits="moonshine.RequestsAll" ValidateRequest="false" %>

<%@ Register Src="~/Controls/menu2.ascx" TagName="menu" TagPrefix="uc1" %>
<!DOCTYPE html>

<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Moonshine Management System</title>
    <link rel="shortcut icon" href="favicon.ico" />
    <link rel="bookmark" href="favicon.ico" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="Scripts/jquery.js"></script>
    <script src="Scripts/jquery.validate.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
    <script src="Scripts/bootstrap-typeahead.js"></script>
    <script src="Scripts/underscore-min.js"></script>
    <script src="laydate/laydate.js"></script>
    <style type="text/css">
        table {
            table-layout: fixed;
            margin: 0px;
        }

            table tr th, td {
                text-overflow: ellipsis; /* for IE */
                -moz-text-overflow: ellipsis; /* for Firefox,mozilla */
                overflow: hidden;
                white-space: nowrap;
                text-align: left;
            }
    </style>
    <script>
        //$(document).ready(function () {
        //    //document.onkeydown = function (e) {
        //    //    if (e.keyCode == 13) {

        //    //        e.keyCode = 0;

        //    //    }
        //    //}
        //});
        function check() {
            $("#f1").validate();
        };
        /////////////
        $(function () {
            $("#f1").validate(
                {
                    onfocusout: function (element) { $(element).valid(); },
                    onkeyup: true
                }
                );
        }
        );
    </script>
</head>
<body style="width: 100%;">
    <uc1:menu ID="menu1" runat="server" />
    <form runat="server" id="f1" class="form-horizontal" defaultbutton="buttonxxx">
        <asp:Button ID="buttonxxx" runat="server" Enabled="False" Style="display: none" />
        <div class="container-fluid body-content">
            <ol class="breadcrumb">
                <li><a href="#">Requests</a></li>
                <li class="active">RequestsAll</li>
            </ol>
            <div class="col-sm-12 panel panel-default ">
                <div class="panel-body">
                    reqID:<asp:TextBox ID="reqID" runat="server" />
                    reqUser:<asp:TextBox ID="reqUser" runat="server" />
                    CostCenter:<asp:TextBox ID="coCter" runat="server" />
                    <asp:Button ID="btnQuery" runat="server" Text="Query" CssClass="SubmitStyle" OnClick="btnQuery_Click" />
                    <asp:CheckBox ID="ckMyReq" runat="server" AutoPostBack="true" OnCheckedChanged="ckMyReq_CheckedChanged" />My Request
                    <asp:CheckBox ID="ckMyAppr" runat="server" AutoPostBack="true" OnCheckedChanged="ckMyAppr_CheckedChanged" />My Approval
                    <asp:CheckBox ID="ckClose" runat="server" AutoPostBack="true" OnCheckedChanged="ckMyAppr_CheckedChanged" />Closed Request
                </div>
            </div>
            <div class="col-sm-12  table-responsive" style="overflow: scroll">
                <asp:GridView runat="server" ID="gvAll" DataKeyNames="reqID" AutoGenerateColumns="False" HorizontalAlign="Center" Width="100%" OnRowDataBound="gvAll_RowDataBound" OnRowEditing="gvAll_RowEditing" OnRowCancelingEdit="gvAll_RowCancelingEdit" OnRowUpdating="gvAll_RowUpdating" CssClass="table table-hover  table-condensed" AllowPaging="True" OnPageIndexChanging="gvAll_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField HeaderText="reqID" HeaderStyle-Width="50px">
                            <ItemTemplate>
                                <asp:HyperLink runat="server" ID="linkReqID" Target="_blank" Text='<%# Bind("reqID") %>' Font-Underline="false" />
                            </ItemTemplate>

                            <HeaderStyle Width="50px"></HeaderStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="reqUser" HeaderText="reqUser" ReadOnly="true" />
                        <asp:BoundField DataField="reqDate" HeaderText="reqDate" ReadOnly="true" />
                        <asp:BoundField DataField="reqNeedDate" HeaderText="reqNeedDate" ReadOnly="true" />
                        <asp:BoundField DataField="lastApproval" HeaderText="lastApprovalDate" ReadOnly="true" />
                        <asp:BoundField DataField="reqCost" HeaderText="CostCenter" ReadOnly="true" />
                        <asp:BoundField DataField="reqCER" HeaderText="CER/NRE" ReadOnly="true" HeaderStyle-Width="80px">
                            <HeaderStyle Width="80px"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="reqDesc" HeaderText="reqDescription" ReadOnly="true" />
                        <asp:BoundField DataField="status" HeaderText="reqStatus" ReadOnly="true" />
                        <asp:BoundField DataField="newold" HeaderText="quotationType" ReadOnly="true" />
                        <asp:TemplateField HeaderText="TaskStatus">
                            <ItemTemplate>
                                <asp:Label ID="lbTaskStatus" runat="server" Text='<%#Eval("taskStatus")%>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlTaskStatus" runat="server" AutoPostBack="false" Enabled="false" />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="taskRemark">
                            <ItemTemplate>
                                <asp:Label ID="lbRemark" runat="server" Text='<%#Eval("taskRemark")%>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="tbRemark" runat="server" Text='<%#Eval("taskRemark")%>' Enabled="false" />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ApplicantConfirms">
                            <ItemTemplate>
                                <asp:Label ID="lbApp" runat="server" Text='<%#Eval("confirms")%>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlApp" runat="server" AutoPostBack="false" Enabled="false"></asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowEditButton="True" />
                    </Columns>
                    <AlternatingRowStyle BackColor="White" CssClass="table" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <RowStyle BackColor="#EFF3FB" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>
            </div>

            <hr />
            <footer>
                <div class="footer" style="height: 2%; background-color: gray; width: 100%; text-align: center"><%: DateTime.Now.Year %>  Developed By Jabil(Wuxi) Mfg Engineering Team</div>
            </footer>
        </div>
    </form>
</body>
</html>
