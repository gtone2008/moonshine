<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Help.aspx.cs" Inherits="moonshine.Help" EnableEventValidation="false" %>

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
                <li><a href="#">Help</a></li>
                <li class="active">Work Instruction </li>
            </ol>

            <span><a href='uploads/Moonshine.docx'>Moonshine Management System v1.0 服务申请操作说明-20170502</a></span>
            <hr />
            <footer>
                <div class="footer" style="height: 2%; background-color: gray; width: 100%; text-align: center"><%: DateTime.Now.Year %>  Developed By Jabil(Wuxi) Mfg Engineering Team</div>
            </footer>
        </div>
    </form>
</body>
</html>