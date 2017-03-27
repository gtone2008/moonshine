<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShoppingCart.aspx.cs" Inherits="moonshine.ShoppingCart" %>

<%@ Register Src="~/Controls/menu2.ascx" TagName="menu" TagPrefix="uc1" %>
<!DOCTYPE html>

<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Moonshine Management System</title>
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="Scripts/jquery.js"></script>
    <script src="Scripts/jquery.validate.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
    <script src="Scripts/bootstrap-typeahead.js"></script>
    <script src="Scripts/underscore-min.js"></script>
    <script src="laydate/laydate.js"></script>
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
                <li><a href="#">Purchase</a></li>
                <li class="active">ShoppingCart</li>
            </ol>
            <div class="panel panel-default " id="d_base">
                <div class="panel-heading"><a href="Base_List.aspx">常规物料:</a></div>
                <asp:GridView ID="gvshoping0" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-condensed table-hover">
                    <Columns>
                        <asp:BoundField DataField="ps_id" HeaderText="Item_id" />
                        <asp:BoundField DataField="ps_name" HeaderText="Description" />
                        <asp:BoundField DataField="price" HeaderText="Unit Price" />
                        <asp:TemplateField HeaderText="Qty">
                            <ItemTemplate>
                                <asp:TextBox runat="server" ID="textbox0" class="required digits" Text='<%# Eval("count") %>' OnTextChanged="textbox0_TextChanged" AutoPostBack="True" placeholder="请输入整数" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="total" HeaderText="Sub-Total Price" />

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button runat="server" ID="button0" CommandArgument='<%# Eval("ps_id") %>'
                                    Text="delete" OnClick="button0_Click" CssClass="SubmitStyle" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

            <div class="panel panel-default " id="d_standard">
                <div class="panel-heading"><a href="PT_Standard_List.aspx">标准产品:</a></div>
                <asp:GridView ID="gvshoping1" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-condensed table-hover">
                    <Columns>
                        <asp:BoundField DataField="ps_id" HeaderText="Item_id" />
                        <asp:BoundField DataField="ps_name" HeaderText="Description" />
                        <asp:BoundField DataField="price" HeaderText="Unit Price" />
                        <asp:TemplateField HeaderText="Qty">
                            <ItemTemplate>
                                <asp:TextBox runat="server" ID="textbox1" class="required digits" Text='<%# Eval("count") %>' OnTextChanged="textbox1_TextChanged" AutoPostBack="True" placeholder="请输入整数" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="total" HeaderText="Sub-Total Price" />

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button runat="server" ID="button1" CommandArgument='<%# Eval("ps_id") %>'
                                    Text="delete" OnClick="button1_Click" CssClass="SubmitStyle" />
                            </ItemTemplate>

                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>
            </div>

            <div class="panel panel-default " id="d_nstandard">
                <div class="panel-heading"><a href="PT_NStandard_List.aspx">定制产品:</a></div>
                <asp:GridView ID="gvshoping2" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-condensed table-hover">
                    <Columns>
                        <asp:BoundField DataField="ps_id" HeaderText="Item_id" />
                        <asp:BoundField DataField="ps_name" HeaderText="Description" />
                        <asp:BoundField DataField="price" HeaderText="Unit Price" />
                        <asp:TemplateField HeaderText="Qty">
                            <ItemTemplate>
                                <asp:TextBox runat="server" ID="textbox2" class="required digits" Text='<%# Eval("count") %>' OnTextChanged="textbox2_TextChanged" AutoPostBack="True" placeholder="请输入整数" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="total" HeaderText="Sub-Total Price" />

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button runat="server" ID="button2" CommandArgument='<%# Eval("ps_id") %>'
                                    Text="delete" OnClick="button2_Click" CssClass="SubmitStyle" />
                            </ItemTemplate>

                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>
            </div>
            <div style="margin: auto,0; text-align: right">
                <asp:Button ID="ButtonApprove" runat="server" Text="Summit" CssClass="SubmitStyle" Width="300" OnClick="ButtonApprove_Click" OnClientClick="check()" />
            </div>

            <hr />
            <footer>
                <div class="footer" style="height: 2%; background-color: gray; width: 100%; text-align: center"><%: DateTime.Now.Year %>  Developed By Jabil(Wuxi) Mfg Engineering Team</div>
            </footer>
        </div>
    </form>
</body>
<script src="Scripts/myJavaScript.js"></script>
</html>
