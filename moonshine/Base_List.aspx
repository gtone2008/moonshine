<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Base_List.aspx.cs" Inherits="moonshine.Base_List"  EnableEventValidation ="false" %>

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
        <li><a href="#">Purchase</a></li>
        <li class="active">Base_Material</li>
        <li><span style="float:right"><a href='ShoppingCart.aspx' ><font color="red">My Cart<asp:Label ID="lab1" runat="server"/></font></a></span>           
        </li>
        <asp:CheckBox Text="Below limt" runat="server" ID="checklow" Checked="false"  AutoPostBack="true" OnCheckedChanged="checklow_CheckedChanged"/>
    </ol>
    <div class=" col-lg-12">
        <asp:DataList ID="dlbase" runat="server" RepeatColumns="5" RepeatDirection="Horizontal"
            Width="99%" OnItemCommand="dlProducts_ItemCommand" DataKeyField="basic_id"  EnableViewState="false">
            <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
            <ItemTemplate>
                <ul>
                    <asp:Image ID="imgPic" runat="server" class="img-thumbnail" Height="100" Width="200" ImageUrl='<%#string.Format("~/uploads\\{0}",Eval("photo"))%>' />
                    <li>Item_id:<%# Eval("basic_id") %></li>
                    <li>Desc:<%# Eval("description") %></li>
                    <li>Standard:<%# Eval("description") %></li>
                    <li>current_qty:<%# Eval("current_qty") %></li>
                    <li>Price:<font color="red">¥<%# Eval("price")%></font></li>
                    <asp:Button ID="addcart" runat="server" CommandName="addcart" Text="Add to Cart" CssClass="SubmitStyle" CommandArgument='<%# Eval("basic_id")%>' />
                    <%--    <a href='ShoppingCart.aspx?ID=<%# Eval("ps_id")%>' >Add to cart</a>--%>
                </ul>
            </ItemTemplate>
        </asp:DataList>
    </div>

        <hr />
            <footer>
                <div class="footer" style="height: 2%; background-color: gray; width: 100%; text-align: center"><%: DateTime.Now.Year %>  Developed By Jabil(Wuxi) Mfg Engineering Team</div>
            </footer>
        </div>
    </form>
</body>
</html>
