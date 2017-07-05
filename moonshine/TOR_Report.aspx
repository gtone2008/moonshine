<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TOR_Report.aspx.cs" Inherits="moonshine.TOR_Report" ValidateRequest="false" %>

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
    <link href="Content/bootstrap-table.min.css" rel="stylesheet" />
    <script src="Scripts/jquery.js"></script>
    <script src="Scripts/jquery.validate.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
    <script src="Content/bootstrap-table.min.js"></script>
    <script src="Content/tableExport.js"></script>
    <script src="Content/bootstrap-table-export.js"></script>
    <script src="Scripts/bootstrap-typeahead.js"></script>
    <script src="Scripts/underscore-min.js"></script>
    <script src="laydate/laydate.js"></script>
    <style type="text/css">
        table {
            table-layout: fixed;
            margin: 0px;
        }

            table tr td {
                text-overflow: ellipsis; /* for IE */
                -moz-text-overflow: ellipsis; /* for Firefox,mozilla */
                overflow: hidden;
                white-space: nowrap;
                border: 1px solid;
                text-align: left;
            }
    </style>
    <script>
        function check() {
            $("#f1").validate();
        };
    </script>
</head>
<body style="width: 100%;">
    <uc1:menu ID="menu1" runat="server" />
    <form runat="server" id="f1" class="form-horizontal" defaultbutton="buttonxxx">
        <asp:Button ID="buttonxxx" runat="server" Enabled="False" Style="display: none" />
        <div class="container-fluid body-content">
            <ol class="breadcrumb">
                <li><a href="#">Report</a></li>
                <li class="active">Inventory turn over</li>
            </ol>
            <div id="toolbar">
                <label>Period  Date:</label>
                From:<input class="required input-sm" value="<%=Request["sDate"]%>" type="text" name="sDate" id="sDate" placeholder="开始日期" autocomplete="off" />
                To:<input class="required input-sm" value="<%=Request["eDate"]%>" type="text" name="eDate" id="eDate" placeholder="结束日期" autocomplete="off" />
                <a class="btn btn-default" id="query" data-loading-text="Loading..."><i class="glyphicon  glyphicon-search">Query</i></a>
                <label id="total" class=" label label-success"></label>
            </div>
            <div class="col-sm-12 panel panel-default btn-sm">
                <div class="panel-body">

                    <table id="tabAll"></table>
                </div>
            </div>

            <hr />
            <footer>
                <div class="footer" style="height: 2%; background-color: gray; width: 100%; text-align: center"><%: DateTime.Now.Year %>  Developed By Jabil(Wuxi) Mfg Engineering Team</div>
            </footer>
        </div>
    </form>
    <script type="text/javascript">
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

        var now = new Date();
        var month = now.getMonth() + 1;
        month = month < 10 ? "0" + month : month + 1;
        var firstdate = now.getFullYear() + '-' + month + '-01 00:00:00';
        $('#sDate').val(firstdate);
        $('#eDate').val(laydate.now(now.toDateString(), 'YYYY-MM-DD hh:mm'));

        var sd = $('#sDate').val();
        var ed = $('#eDate').val();
        var ddiff = Math.floor((new Date(ed).getTime() - new Date(sd).getTime()) / (24 * 3600 * 1000));
        var $tab = $('#tabAll');
        var tab = document.getElementById('tabAll');
        $('#tabAll').bootstrapTable(
                      {
                          url: 'ajax/TORHandler.ashx?action=getDataTOR&sdate=' + sd + '&edate=' + ed,
                          dataType: "json",
                          search: true,
                          showExport: true,
                          toolbar: "#toolbar",
                          //clickToSelect: true,
                          //singleSelect: true,
                          idField: "basic_id",
                          columns: [
                              //{
                              //    formatter: function (value, row, index) {
                              //        return index + 1;
                              //    },
                              //    width:'30px'
                              //},
                              {
                                  field: 'basic_id',
                                  title: 'basic_id',

                              }, {
                                  field: 'description',
                                  title: 'description',
                              }, {
                                  field: 'spec',
                                  title: 'spec'
                              }, {
                                  field: 'price',
                                  title: 'price(RMB)',
                              },
                           {
                               formatter: function (value, row) {
                                   qty1 = $.isNumeric(row['IN-OUT1']) ? row['IN-OUT1'] : 0;
                                   return qty1;

                               },
                               title: 'Begin Inventory Qty',
                               titleTooltip: 'Begin Inventory Qty',
                           },
                            {
                                formatter: function (value, row) {
                                    amount1 = (parseInt(qty1) * parseFloat(row['price'])).toFixed(2);
                                    return amount1;

                                },
                                title: 'Begin Inventory Amount',
                                titleTooltip: 'Begin Inventory Amount',
                            },
                            {
                                formatter: function (value, row) {
                                    qty2 = $.isNumeric(row['IN-OUT2']) ? row['IN-OUT2'] : 0;
                                    return qty2;

                                },
                                title: 'End Inventory Qty',
                                titleTooltip: 'End Inventory Qty',
                            },
                            {
                                formatter: function (value, row) {
                                    amount2 = (parseInt(qty2) * parseFloat(row['price'])).toFixed(2);
                                    return amount2;

                                },
                                title: 'End Inventory Amount',
                                titleTooltip: 'End Inventory Amount',
                            },
                            {
                                formatter: function (value, row) {
                                    in_qty = $.isNumeric(row.in_qty) ? row.in_qty : 0;
                                    return in_qty;

                                },
                                title: 'period in qty',
                                titleTooltip: 'period in qty',
                            },
                            {
                                formatter: function (value, row) {
                                    in_amo = (in_qty * parseFloat(row['price'])).toFixed(2);
                                    return in_amo;

                                },
                                title: 'period in amount',
                                titleTooltip: 'period in amount',
                            },
                           {
                               formatter: function (value, row) {
                                   out_qty = $.isNumeric(row.out_qty) ? row.out_qty : 0;
                                   return out_qty;

                               },
                               title: 'period out qty',
                               titleTooltip: 'period out qty',
                           },
                            {
                                formatter: function (value, row) {
                                    out_amo = (out_qty * parseFloat(row['price'])).toFixed(2);
                                    return out_amo;

                                },
                                title: 'period out amount',
                                titleTooltip: 'period out amount',
                            },
                             {
                                 formatter: function (value, row) {
                                     ITO = (parseFloat(out_amo) * 2) / (parseFloat(amount1) + parseFloat(amount2));
                                     return $.isNumeric(ITO) ? ITO.toFixed(2) : 0;

                                 },
                                 title: 'ITO',
                             },
                              {
                                  formatter: function () {
                                      var days = parseInt(ddiff + 1) / ITO
                                      return days.toFixed(0);

                                  },
                                  title: 'days sales of inventory',
                                  titleTooltip: 'days sales of inventory',
                              },

                          ]
                      });

        $('#query').on('click', function () { check(); lodaData(); });

        $tab.on('post-body.bs.table', function () { getdata(); });
        function getdata() {
            //alert(JSON.stringify($('#tabAll').bootstrapTable('getData')));
            var qj = 0, qc = 0, qm = 0;

            for (var i = 1; i < tab.rows.length; i++) {
                qj += parseFloat(tab.rows[i].cells[11].innerHTML);
                qc += parseFloat(tab.rows[i].cells[5].innerHTML);
                qm += parseFloat(tab.rows[i].cells[7].innerHTML);
            }
            var ITOtotal = qj * 2 / (qc + qm);
            var ITOday = (ddiff + 1) / ITOtotal
            $('#total').text("  库存周转率: " + ITOtotal.toFixed(2) + "  库存周转天数: " + parseInt(ITOday));
        }
        function lodaData() {
            //var sel = $('#tabAll').bootstrapTable('getSelections');
            //console.log(JSON.stringify(sel));
            sd = $('#sDate').val();
            ed = $('#eDate').val();
            ddiff = Math.floor((new Date(ed).getTime() - new Date(sd).getTime()) / (24 * 3600 * 1000));
            $tab.bootstrapTable('refresh', { url: 'ajax/TORHandler.ashx?action=getDataTOR&sdate=' + sd + '&edate=' + ed });
        }
    </script>
</body>
</html>