<%@ Page Language="C#"  AutoEventWireup="true" CodeBehind="RequestsAll.aspx.cs" Inherits="moonshine.RequestsAll" validateRequest="false"%>
<%@ Register Src="~/Controls/menu2.ascx" TagName="menu" TagPrefix="uc1" %>
<!DOCTYPE html>

<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Moonshine System</title>
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
                    onkeyup:true
                }
                );
        }
        );
    </script>
</head>
    <body style="width: 100%;">
    <uc1:menu ID="menu1" runat="server" />
    <form runat="server" id="f1" class="form-horizontal" defaultbutton="buttonxxx" >
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
            <asp:Button ID="btnQuery" runat="server"  Text="Query" CssClass="SubmitStyle" OnClick="btnQuery_Click" />       
        </div>
    </div>
  <div class="col-sm-12 ">
        <asp:GridView runat="server" ID="gvAll"  DataKeyNames="reqID"  AutoGenerateColumns="False" HorizontalAlign="Center"  ViewStateMode="Disabled" Width="100%" OnRowDataBound="gvAll_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="reqID">
                    <ItemTemplate>
                        <asp:HyperLink runat="server" ID="linkReqID" Text='<%# Bind("reqID") %>' Font-Underline="false" />
                        
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField  DataField="reqUser" HeaderText="reqUser"/>
                <asp:BoundField DataField="reqDate" HeaderText="reqDate"/>
                <asp:BoundField DataField="reqNeedDate" HeaderText="reqNeedDate"/>
                <asp:BoundField DataField="reqCost" HeaderText="CostCenter"/>
                <asp:BoundField DataField="reqCER" HeaderText="reqCER"/>
                <asp:BoundField DataField="reqDesc" HeaderText="reqDesciption"/>
                <asp:BoundField DataField="status" HeaderText="status"/>
                <asp:BoundField DataField="newold" HeaderText="报价类型"/>
            </Columns>
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
