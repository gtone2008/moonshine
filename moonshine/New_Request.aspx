<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="New_Request.aspx.cs" Inherits="moonshine.New_Request" validateRequest="false"%>
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
    <script src="laydate/laydate.js"></script>
    <ol class="breadcrumb">
        <li><a href="#">Request</a></li>
        <li class="active">New_Request</li>
    </ol>
    <fieldset>
            <div class="col-sm-6">
            <label class="col-sm-4">Applicant申请人</label>
            <input class="col-sm-8" type="text" name="reqUser"  placeholder="请输入名字" readonly="true" />
            </div>
        <div class="col-sm-6">
            <label class="col-sm-4" ">Application Date申请日期</label>
            <input class="required col-sm-8" type="text" name="reqDate" placeholder="请输入日期" readonly="true" />
            </div>
        <div class="col-sm-6">
            <label class="col-sm-4">Phone联系方式</label>
            <input class="required col-sm-8" type="text" name="reqPhone" placeholder="请输入联系方式" />
            </div>
        <div class="col-sm-6">
            <label class="col-sm-4">Department部门</label>
            <input class="required col-sm-8" type="text" name="reqDep"  placeholder="请输入部门" readonly="true" />
            </div>
        <div class="col-sm-6">
            <label class="col-sm-4">Workcell项目</label>
            <input class="required col-sm-8" type="text" name="reqWC"  placeholder="请输入项目" />
            </div>
        <div class="col-sm-6">
            <label class="col-sm-4">Cost Center成本中心</label>
            <input class="required digits col-sm-8" type="text" name="reqCost" id="reqCost" placeholder="请输入成本中心" data-provide="typeahead" autocomplete="off" onblur="checkCost();" />
            </div>
        <div class="col-sm-6">
            <label class="col-sm-4">Required Date需求日期</label>
            <input class="required col-sm-8" type="text" name="reqNeedDate" id="reqNeedDate" placeholder="请选择需求日期" autocomplete="off" />
            </div>
        <div class="col-sm-6">
            <label class="col-sm-4">CER/NRE NO</label>
            <input class="col-sm-8 digits" type="text"  name="reqCER" placeholder="请输入CER/NRE NO(可选项)" />
            </div>
        <div class="col-sm-6">
            <label  class="col-sm-4">Required Description/需求描述:</label>
            <div class="col-sm-8">
            <input type="radio" name="newold" value="1" checked="checked" />新产品报价
            <input type="radio" name="newold" value="0" />作回收报价
            </div>
          </div>

           <input class="required col-sm-12" type="text"  name="reqDesc" placeholder="请输入需求描述" />

        </fieldset>
    <br />
    <div id="tabAll">
        <asp:GridView runat="server" ID="gvAll" CssClass="required table table-condensed table-bordered table-hover"  AutoGenerateColumns="False" OnRowDataBound="gvbom1_RowDataBound" ShowFooter="True">
             <Columns>
                <asp:BoundField DataField="type" HeaderText="type" />
                <asp:BoundField DataField="ps_id" HeaderText="Item_id" />
                <asp:BoundField DataField="ps_name" HeaderText="Description" />
                <asp:BoundField DataField="price" HeaderText="Unit Price(RMB)" />
                 <asp:BoundField DataField="count" HeaderText="Qty" />
                <asp:BoundField DataField="total" HeaderText="Sub-Total Price(RMB)" />
            </Columns>
        </asp:GridView>
        </div>
     <div style="margin:auto,0;text-align:right">
         <input id="ButtonApprove" runat="server" type="submit" value="Go To Approval" class=" btn btn-danger"  onclick="checkap();" onserverclick="ButtonApprove_Click" />
     </div>
    <input type="text" id="txtAll" name="txtAll" class="required" style="height:0.5px;width:0px;padding:0px;margin:0px;" />
    <script>
        var products;
        $('#reqCost').typeahead({
            source: function (query, process) {
                $.ajax({
                    url: "getData1.ashx?action=getCCS",
                    type: 'GET',
                    dataType: 'JSON',
                    async: true,
                    data: 'costID=' + query,
                    success: function (data) {
                        products = data;
                    }//success
                });//ajax
                var results = _.map(products, function (product) {
                    return product.costID + "";
                });
                process(results);
            },//souce
            highlighter: function (item) {
                //var product = products.find(function (p) {
                //    return p.costID == item;
                //});//IE低版本不兼容啊
                var product = _.find(products, function (p) {
                    return p.costID == item;
                });
                //max1 = product.current_qty;
                return product.costID + "  [" + product.costName + "Owner:" + product.approverName + "]";
            },

            updater: function (item) {
                var product = _.find(products, function (p) {
                    return p.costID == item;
                });
                return product.costID;

            }

        });//typeahead

        function checkCost() {
            var product = _.find(products, function (p) {
                return p.costID == $('#reqCost').val();
            });
            if (!product) {
                $('#reqCost').val("");
            }

        }

        function checkap() {
            $("#f1").validate({
                errorPlacement: function (error, element) {
                    error.appendTo(element.parent());
                },
                ignore: "",
                success: function () {
                    aaa();
                },
                submitHandler: function (form) {
                    form.submit();
                }
            });
        };
        function aaa() {
            $('#ButtonApprove').attr('disabled', 'disabled');
            $('#ButtonApprove').val('请等待...');
            //__doPostBack('ButtonApprove', '');
        };
        var aa = $("#tabAll").html().replace('<div>', "").trim();
        aa = aa.replace('</div>', "").trim();
        $("#txtAll").val(aa);
        //alert($("#txtAll").val());
        $('input[name=reqUser]').val('<%=Session["userName"]%>');
        $('input[name=reqDep]').val('<%=moonshine.Util.Common.GetADUserEntity(moonshine.Util.Common.GetCurrentNTID()).Department%>');
        var myDate = new Date();
        $('input[name=reqDate]').val(myDate.getFullYear() + '-' + (myDate.getMonth() + 1) + '-' + myDate.getDate());
        //laydate.skin('molv');
        laydate({
            elem: '#reqNeedDate',
            min: laydate.now(+1), //-1代表昨天，-2代表前天，以此类推
            format: 'YYYY-MM-DD', //日期格式
            isclear: true
        });
</script>
  <hr />
            <footer>
                <div class="footer" style="height: 2%; background-color: gray; width: 100%; text-align: center"><%: DateTime.Now.Year %>  Developed By Jabil(Wuxi) Mfg Engineering Team</div>
            </footer>
        </div>
    </form>
</body>
<script src="Scripts/myJavaScript.js"></script>
</html>