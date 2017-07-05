<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Out.aspx.cs" Inherits="moonshine.Out" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <script src="Scripts/jquery.validate.min.js"></script>
    <ol class="breadcrumb">
        <li><a href="#">Inventory</a></li>
        <li class="active">Out</li>
    </ol>
    <div class="col-sm-12 panel panel-default ">
        <div class="panel-body">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;PN:<input id="pn1" name="pn1" type="text" data-provide="typeahead" autocomplete="off" placeholder="请输入PN号" class="required" />
            Description:<asp:TextBox ID="txtdesc" runat="server" ReadOnly="true" CssClass="label-default" class="required" />
            Spec:<asp:TextBox ID="txtspec" runat="server" ReadOnly="true" CssClass="label-default" />
            Current_qty:<asp:TextBox ID="txtcqty1" runat="server" ReadOnly="true" CssClass="label-default" /><br />
            Out_Qty:<input type="text" id="txtqty1" name="txtqty1" placeholder="请输入数字" class="required digits" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Type:<asp:DropDownList ID="ddltype" runat="server" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Request_by:<asp:TextBox ID="txtreq" runat="server" class="required" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Remark:<asp:TextBox ID="txtrem" runat="server" autocomplete="off" class="required" />
            <asp:Button ID="btnOut" runat="server" Text="Out" CssClass="SubmitStyle" OnClick="btnOut_Click" OnClientClick="return checkRules()" />
        </div>
    </div>

    <div class=" table-responsive col-sm-12">
        <asp:GridView runat="server" ID="gvin" CssClass=" table table-bordered table-condensed table-hover" CellPadding="4" ForeColor="#333333" GridLines="Vertical">
            <AlternatingRowStyle BackColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <%--<HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="#ffff" />--%>
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
    </div>
    <script>
        var products;
        //function getDesc() {

        //}
        //$(document).ready(function () {
        //   //alert('a');
        //});
        getPower();
        function getPower() {
            $.ajax({
                type: 'get',
                url: "AJAX/getPower.ashx",
                contentType: "application/text; charset=utf-8",
                success: function (data) {
                    if (data == "guest") {
                        var tb = document.getElementById("MainContent_btnOut");
                        tb.style.display = "none";
                    }
                }
            });
        }
        $("#pn1").blur(function () {
                checkPn();
                var product = _.find(products, function (p) {
                    return p.basic_id == $("#pn1").val();
                });
                $("#<%=txtdesc.ClientID%>").val(product.description);
                $("#<%=txtspec.ClientID%>").val(product.spec);
                $("#<%=txtcqty1.ClientID%>").val(product.current_qty);

        });
        function checkRules() {

            if (parseInt($("#txtqty1").val()) >parseInt($("#MainContent_txtcqty1").val())) {
                alert("库存不足");
                $("#txtqty1").focus();
                return false;
            };
        }
        function checkPn() {
            $.ajax({
                type: "get",
                url: "getData1.ashx?action=getPN&pn1=" + $("#pn1").val(),
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    if (data != "True") {
                        //$("#pn1").focus();
                        $("#pn1").val("");
                        $("#<%=txtdesc.ClientID%>").val("");
                        $("#<%=txtspec.ClientID%>").val("");
                        $("#<%=txtcqty1.ClientID%>").val("");

                        return false;

                    }
                    else return true;
                },
                error: function (err) {
                    alert(err);
                }
            }
        )
        };

        $('#pn1').typeahead({
            source: function (query, process) {
                $.ajax({
                    url: "getData1.ashx?action=getPNS",
                    type: 'GET',
                    dataType: 'JSON',
                    async: true,
                    data: 'basic_id=' + query,
                    success: function (data) {
                        //var arr = [];
                        //for (i in data) {
                        //    arr.push(data[i]['basic_id'] + "");
                        //    //document.write(data[i]['basic_id']);
                        //}
                        //process(arr);
                        products = data;
                    }//success
                });//ajax
                var results = _.map(products, function (product) {
                    return product.basic_id + "";
                });
                process(results);
            },//souce
            highlighter: function (item) {

                //var product = _.find(products, function (p) {
                //    return p.basic_id == item;
                //});
                var product = products.find(function (p) {
                    return p.basic_id == item;
                });
                //max1 = product.current_qty;
                return product.basic_id + " (" + product.description + ")";
            },

            updater: function (item) {
                var product = _.find(products, function (p) {
                    return p.basic_id == item;
                });
                return product.basic_id;

            }

        });//typeahead

        //$('#pn1').typeahead({
        //    source: function (query, process) {
        //        return ["Deluxe Bicycle", "Super Deluxe Trampoline", "Super Duper Scooter"];
        //    }
        //})
    </script>
</asp:Content>