﻿<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="New_Request.aspx.cs" Inherits="moonshine.New_Request" validateRequest="false"%>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <script src="laydate/laydate.js"></script>
    <ol class="breadcrumb">
        <li><a href="#">Request</a></li>
        <li class="active">New_Request</li>
    </ol>
    <fieldset>
            <label class="col-sm-2">Applicant申请人</label>
            <input class="col-sm-4" type="text" name="reqUser"  placeholder="请输入名字" readonly="true"/>
            <label class="col-sm-2" ">Application Date申请日期</label>
            <input class="required col-sm-4" type="text" name="reqDate" placeholder="请输入日期" readonly="true" />
            <label class="col-sm-2">Phone联系方式</label>
            <input class="col-sm-4" type="text" name="reqPhone" placeholder="请输入联系方式" />
            <label class="col-sm-2">Department部门</label>
            <input class="required col-sm-4" type="text" name="reqDep"  placeholder="请输入部门" readonly="true" />
            <label class="col-sm-2">Workcell项目</label>
            <input class="required col-sm-4" type="text" name="reqWC"  placeholder="请输入项目"  />
            <label class="col-sm-2">Cost Center成本中心</label>
            <input class="required digits col-sm-4" type="text" name="reqCost" id="reqCost" placeholder="请输入成本中心" data-provide="typeahead" autocomplete="off" onblur="checkCost();"/>
            <label class="col-sm-2">Required Date需求日期</label>
            <input class="required col-sm-4" type="text" name="reqNeedDate" id="reqNeedDate" placeholder="请选择需求日期(七天后)" autocomplete="off"/>
            <label class="col-sm-2">CER/NRE NO</label>
            <input class="col-sm-4" type="text"  name="reqCER" placeholder="请输入CER/NRE NO" />
            <label for="comment" class="col-sm-2">Required Description/需求描述:</label>
        <div class="col-sm-4">
            <input type="radio" name="newold" value="1" checked="checked"/>新产品报价
            <input type="radio" name="newold" value="0"/>作回收报价
        </div>
           <input class="required col-sm-12" type="text"  name="reqDesc" placeholder="请输入需求描述"  />
        </fieldset>
    <br />
    <div id="tabAll">
        <asp:GridView runat="server" ID="gvAll" CssClass="required table table-condensed table-bordered table-hover"  AutoGenerateColumns="False" OnRowDataBound="gvbom1_RowDataBound" ShowFooter="True">
             <Columns>
                <asp:BoundField DataField="type" HeaderText="type" />
                <asp:BoundField DataField="ps_id" HeaderText="Item_id" />
                <asp:BoundField DataField="ps_name" HeaderText="Description" />
                <asp:BoundField DataField="price" HeaderText="Unit Price" />
                 <asp:BoundField DataField="count" HeaderText="Qty" />
                <asp:BoundField DataField="total" HeaderText="Sub-Total Price" />
            </Columns>
        </asp:GridView>
        </div>
     <div style="margin:auto,0;text-align:right">
         <input id="ButtonApprove" runat="server" type="submit" value="Go To Approval" class=" btn btn-danger"  onclick="checkap();" onserverclick="ButtonApprove_Click"  />
     </div>
    <input type="text" id="txtAll" name="txtAll" class="required" style="height:0.5px;width:0px;padding:0px;margin:0px;"/>
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
                var product = products.find(function (p) {
                    return p.costID == item;
                });
                //max1 = product.current_qty;
                return product.costID + "  [" + product.costName +"老板NTID:"+ product.approverNTID + "]";
            },

            updater: function (item) {
                var product = _.find(products, function (p) {
                    return p.costID == item;
                });
                return product.costID;

            }

        });//typeahead
        function checkCost()
        {
            var product = products.find(function (p) {
                return p.costID == $('#reqCost').val();
            });
            if (!product)
            {
                $('#reqCost').val("");
            }

        }

        function checkap() {
            $("#f1").validate({
                errorPlacement: function (error, element) {
                    error.appendTo(element.parent());
                },
                ignore: "",
                success:function()
                {
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
        alert($("#txtAll").val());
        $('input[name=reqUser]').val('<%=Session["userName"]%>');
        $('input[name=reqDep]').val('<%=moonshine.Util.Common.GetADUserEntity(moonshine.Util.Common.GetCurrentNTID()).Department%>');
        var myDate = new Date();
        $('input[name=reqDate]').val(myDate.getFullYear() + '-' + (myDate.getMonth() + 1) + '-' + myDate.getDate());
        //laydate.skin('molv');
        laydate({
            elem: '#reqNeedDate',
            min: laydate.now(+7), //-1代表昨天，-2代表前天，以此类推
            format: 'YYYY-MM-DD', //日期格式
            isclear: true
        });
</script>
</asp:Content>