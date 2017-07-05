<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="In.aspx.cs" Inherits="moonshine.In" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <ol class="breadcrumb">
        <li><a href="#">Inventory</a></li>
        <li class="active">In</li>
    </ol>
    <div class="col-sm-12 panel panel-default ">
        <div class="panel-body">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;PN:<input id="pn1" name="pn1" type="text" data-provide="typeahead" autocomplete="off" placeholder="请输入PN号" onblur="getDesc()" onchange="cleanDesc()" class="required" />
            Description:<asp:TextBox ID="txtdesc" runat="server" ReadOnly="true" CssClass="label-default" class="required" />
            Spec:<asp:TextBox ID="txtspec" runat="server" ReadOnly="true" CssClass="label-default" />
            Current_qty:<asp:TextBox ID="txtcqty" runat="server" ReadOnly="true" CssClass="label-default" /><br />
            IN_Qty:<input type="text" id="txtqty" name="txtqty" class="required digits" placeholder="请输入数字" onfocus="checkPn()" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Type:<asp:DropDownList ID="ddltype" runat="server" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Request_by:<asp:TextBox ID="txtreq" runat="server" class="required" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Remark:<asp:TextBox ID="txtrem" runat="server" autocomplete="off" class="required" />
            <asp:Button ID="btnInsert" runat="server" Text="In" CssClass="SubmitStyle" OnClientClick="check()" Width="50px" OnClick="btnInsert_Click" />
        </div>
    </div>

    <div class=" table-responsive">
        <asp:GridView runat="server" ID="gvin" CssClass=" table table-bordered table-condensed table-hover" DataKeyNames="in_id" CellPadding="4" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" CssClass="table" />
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
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
        //$(document).ready(function () {
        //    //document.write("aa");

        //});
        getPower();
        function getPower() {
            $.ajax({
                type: 'get',
                url: "AJAX/getPower.ashx",
                contentType: "application/text; charset=utf-8",
                success: function (data) {
                    if (data == "guest") {
                        var tb = document.getElementById("MainContent_btnInsert");
                        tb.style.display = "none";
                    }
                }
            });
        }
        function checkPn() {
            $.ajax({
                type: "get",
                url: "getData1.ashx?action=getPN&pn1=" + $("#pn1").val(),
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    if (data != "True") {
                        $("#pn1").focus();
                        $("#pn1").val("");
                        alert($("#pn1").val() + "不存在此PN号,请检查!");
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

        var products;
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

                var product = _.find(products, function (p) {
                    return p.basic_id == item;
                });
                return product.basic_id + " (" + product.description + ")";
            },

            updater: function (item) {
                var product = _.find(products, function (p) {
                    return p.basic_id == item;
                });
                return product.basic_id;

            }

        })//typeahead
        //$("#<%=txtcqty.ClientID%>").val("1000");
        //$('#pn1').typeahead({
        //    source: function (query, process) {
        //        return ["Deluxe Bicycle", "Super Deluxe Trampoline", "Super Duper Scooter"];
        //    }
        //})
        function cleanDesc() {
            $("#<%=txtdesc.ClientID%>").val("");
            $("#<%=txtspec.ClientID%>").val("");
            $("#<%=txtcqty.ClientID%>").val("");

        }
        function getDesc() {
            //alert($("#pn1").val());

                if ($("#pn1").val() != "") {
                    var product = _.find(products, function (p) {
                        return p.basic_id == $("#pn1").val();
                    });
                    $("#<%=txtdesc.ClientID%>").val(product.description);
                    $("#<%=txtspec.ClientID%>").val(product.spec);
                    $("#<%=txtcqty.ClientID%>").val(product.current_qty);

        }
    }
    </script>
</asp:Content>