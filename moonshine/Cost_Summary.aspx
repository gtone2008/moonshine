<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cost_Summary.aspx.cs" Inherits="moonshine.Cost_Summary" ValidateRequest="false" %>

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
        function check() {
            $("#f1").validate();
            $("#lbDate").text($("#sDate").val() + "~" + $("#eDate").val());

        };
        /////////////
    </script>
</head>
<body style="width: 100%;">
    <uc1:menu ID="menu1" runat="server" />
    <form runat="server" id="f1" class="form-horizontal" defaultbutton="buttonxxx">
        <asp:Button ID="buttonxxx" runat="server" Enabled="False" Style="display: none" />
        <div class="container-fluid body-content">
            <ol class="breadcrumb">
                <li><a href="#">Report</a></li>
                <li class="active">Cost_Summary</li>
            </ol>
            <div class="col-sm-12 panel panel-default ">
                <div class="panel-body">
                    <label>Approval close Date:</label>
                    From:<input class="required " value="<%=Request["sDate"]%>" type="text" name="sDate" id="sDate" placeholder="开始日期" autocomplete="off" />
                    To:<input class="required " value="<%=Request["eDate"]%>" type="text" name="eDate" id="eDate" placeholder="结束日期" autocomplete="off" />
                    <input type="checkbox" name="ckCer" id="ckCer" value='0' />NOT INCLUDED CER/NRE
                    <asp:Button ID="btnQuery" runat="server" Text="Query" CssClass="SubmitStyle" OnClick="btnQuery_Click" OnClientClick="check()" />
                </div>
            </div>
            <div class="col-sm-12 ">

                <asp:GridView runat="server" ID="gvAll" AutoGenerateColumns="False" HorizontalAlign="Center" ViewStateMode="Disabled" Width="100%" OnRowDataBound="gvAll_RowDataBound" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                    <Columns>
                        <asp:BoundField DataField="reqCost" HeaderText="Cost Center成本中心" />
                        <asp:BoundField DataField="new" HeaderText="New Product Quote新产品报价(RMB)" />
                        <asp:BoundField DataField="old" HeaderText="Recycle Product Quote作回收报价(RMB)" />
                        <asp:BoundField DataField="cost" HeaderText="Trolley Spare Part Quote推车配件报价(RMB)" />
                        <asp:TemplateField HeaderText="Cost支付费用(新产品报价-作回收报价)(RMB)">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="Total"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle BackColor="#00b0f0" Font-Bold="True" />
                </asp:GridView>
            </div>

            <hr />
            <footer>
                <div class="footer" style="height: 2%; background-color: gray; width: 100%; text-align: center"><%: DateTime.Now.Year %>  Developed By Jabil(Wuxi) Mfg Engineering Team</div>
            </footer>
        </div>
    </form>
    <script>
        var start = {
            elem: '#sDate',
            format: 'YYYY/MM/DD hh:mm:ss',
            istime: true,
            istoday: false,
            choose: function (datas) {
                end.min = datas; //开始日选好后，重置结束日的最小日期
                end.start = datas //将结束日的初始值设定为开始日
            }
        };
        var end = {
            elem: '#eDate',
            format: 'YYYY/MM/DD hh:mm:ss',
            istime: true,
            istoday: true,
            choose: function (datas) {
                start.max = datas; //结束日选好后，重置开始日的最大日期
            }
        };
        laydate(start);
        laydate(end);
    </script>
</body>
</html>