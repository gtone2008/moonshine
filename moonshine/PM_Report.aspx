<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PM_Report.aspx.cs" Inherits="moonshine.PM_Report" ValidateRequest="false" %>

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
    <script src="Content/bootstrap-table-editable.js"></script>
    <script src="Content/bootstrap-editable.js"></script>
    <script src="Content/bootstrap-table-contextmenu.js"></script>
    <style type="text/css">
        .row {
            margin-left: 0;
            margin-right: 0;
        }

            .row * {
                padding-left: 0 !important;
                padding-right: 10px !important;
            }

        table {
            table-layout: fixed;
            margin: 0px;
        }
    </style>
</head>
<body style="width: 100%;">
    <uc1:menu ID="menu1" runat="server" />
    <form runat="server" id="f1" class="form-horizontal" defaultbutton="buttonxxx">
        <asp:Button ID="buttonxxx" runat="server" Enabled="False" Style="display: none" />
        <div class="container-fluid body-content">
            <ol class="breadcrumb">
                <li><a href="#">Request</a></li>
                <li class="active">Trolley Spare PM/Repair (action please right-click on cell if admin role)</li>
            </ol>
            <div class="col-sm-12 panel panel-default table-responsive">
                <table id="tabAll" class="table  table-condensed"></table>
            </div>

            <hr />
            <footer>
                <div class="footer" style="height: 2%; background-color: gray; width: 100%; text-align: center"><%: DateTime.Now.Year %>  Developed By Jabil(Wuxi) Mfg Engineering Team</div>
            </footer>
        </div>
    </form>
    <ul id="example1-context-menu" class="dropdown-menu">
        <li data-item="add"><a>Add</a></li>
        <li data-item="edit"><a>Edit</a></li>
        <li data-item="delete"><a>Delete</a></li>
    </ul>
    <div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg" style="height: 800px; width: 1000px">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 id="mhead" class="modal-title">Modal Header</h4>
                </div>
                <div class="modal-body">
                    <form class="form-horizontal" id="saveadmin_form">
                        <div class="container-fluid">
                            <div class="row">
                                <div class="col-sm-4">
                                    <label class="col-sm-4 text-right" for="occur_date">occur_date</label>
                                    <input class="col-sm-8 laydate-icon" type="text" id="occur_date" name="occur_date" />
                                </div>
                                <div class="col-sm-4">
                                    <label class="col-sm-4 text-right" for="location">location</label>
                                    <input class="col-sm-8" type="text" id="location" name="location" />
                                </div>
                                <div class="col-sm-4">
                                    <label class="col-sm-4 text-right">machine_type</label>
                                    <input class="col-sm-8" type="text" id="machine_type" name="machine_type" />
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-sm-4">
                                    <label class="col-sm-4 text-right">machine_ID</label>
                                    <input class="col-sm-8" type="text" id="machine_ID" name="machine_ID" />
                                </div>
                                <div class="col-sm-4">
                                    <label class="col-sm-4 text-right" for="costCenter">costCenter</label>
                                    <input class="col-sm-8 required" type="text" id="costCenter" name="costCenter" data-provide="typeahead" autocomplete="off" onblur="checkCost();" />
                                </div>
                                <div class="col-sm-4">
                                    <label class="col-sm-4 text-right">type</label>
                                    <select class="col-sm-8 combobox" id="type" name="type">
                                        <option>PM</option>
                                        <option>Repair</option>
                                    </select>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-sm-4">
                                    <label class="col-sm-4 text-right">rootCause</label>
                                    <input class="col-sm-8" type="text" id="rootCause" name="rootCause" />
                                </div>
                                <div class="col-sm-4">
                                    <label class="col-sm-4 text-right">solution</label>
                                    <input class="col-sm-8" type="text" id="solution" name="solution" />
                                </div>
                                <div class="col-sm-4">
                                    <label class="col-sm-4 text-right">cost</label>
                                    <input class="col-sm-8 required number" type="text" id="cost" name="cost" />
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-sm-4">
                                    <label class="col-sm-4 text-right">operator</label>
                                    <input class="col-sm-8" type="text" id="operator" name="operator" />
                                </div>
                                <div class="col-sm-4">
                                    <label class="col-sm-4 text-right">solve_date</label>
                                    <input class="col-sm-8 laydate-icon" type="text" id="solve_date" name="solve_date" />
                                </div>
                                <div class="col-sm-4">
                                    <label class="col-sm-4 text-right">condition</label>
                                    <select class="col-sm-8 combobox" id="condition" name="condition">
                                        <option>CLOSED</option>
                                        <option>OPEN</option>
                                    </select>
                                </div>
                            </div>
                        </div>
                    </form>
                </div>
                <div class="modal-footer">
                    <button id="btnSave" type="button" class="btn btn-default" onclick="SaveInfo()">Save</button>
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            getPower();
            $("#saveadmin_form").validate({
                onfocusout: function (element) { $(element).valid(); },
                onkeyup: true,
                errorPlacement: function (error, element) {
                    error.appendTo(element.parent());
                    //$(element).closest("form").find("label[for='" + element.attr("id") + "']").append(error);
                    //$(element).closest("form").find("input[id='" + element.attr("id") + "']").attr("placeholder", error);
                }
            });
            laydate({
                elem: '#occur_date',
                istime: true,
                event: 'click',
                max: laydate.now(+1),
                format: 'YYYY-MM-DD',
                issure: true
            });
            laydate({
                elem: '#solve_date',
                istime: true,
                event: 'click',
                max: laydate.now(+1),
                format: 'YYYY-MM-DD',
                issure: true
            });
        })        var products,ID="";
        $('#costCenter').typeahead({
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

                var product = _.find(products, function (p) {
                    return p.costID == item;
                });
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
                return p.costID == $('#costCenter').val();
            });
            if (!product) {
                $('#costCenter').val("");
            }

        }
        function getPower() {
            var res = false;
            $.ajax({
                type: 'get',
                url: "AJAX/getPower.ashx",
                contentType: "application/text; charset=utf-8",
                async: false,
                success: function (data) {
                    if (data != "admin") {
                        res = 0;
                    }
                    else {
                        res = 1;
                    }
                    document.cookie = "admin=" + res;
                    getTable(res);
                }
            });
            return res;
        }
        function getTable(res)
        {
            $('#tabAll').bootstrapTable({
                url: 'ajax/ActionHandler.ashx?action=getDataPM',
                dataType: "json",
                search: true,
                showExport: true,
                pagination: true,
                pageSize: 15,
                pageList: [15, 20, 40],
                //toolbar: "#toolbar",
                //clickToSelect: true,
                //singleSelect: true,
                contextMenu: '#example1-context-menu',
                onClickRow: function (row, $el) {
                    $('#tabAll').find('.error').removeClass('error');
                    $el.addClass('error');
                },
                onContextMenuItem: function (row, $el) {
                    if (res) {
                        if ($el.data("item") == "edit") {
                            EditInfo(row);
                        } else if ($el.data("item") == "delete") {
                            DeleteInfo(row.ID);
                        } else if ($el.data("item") == "add") {
                            AddInfo();
                        }
                    }
                    else {
                        alert('You do not have permission');
                    }
                },
                columns: [
                    //{
                    //    formatter: function (value, row, index) {
                    //        return index + 1;
                    //    },
                    //    width:'30px'
                    //},
                    {
                        field: 'occur_date',
                        title: 'occur_date',

                    }, {
                        field: 'location',
                        title: 'location',
                    }, {
                        field: 'machine_type',
                        title: 'machine_type'
                    }, {
                        field: 'machine_ID',
                        title: 'machine_ID',
                    },
                     {
                         field: 'costCenter',
                         title: 'costCenter',
                     },
                      {
                          field: 'type',
                          title: 'type',
                      },
                      {
                          field: 'rootCause',
                          title: 'rootCause',
                      },
                       {
                           field: 'solution',
                           title: 'solution',
                       },
                        {
                            field: 'cost',
                            title: 'cost(RMB)',
                        },
                        {
                            field: 'operator',
                            title: 'operator',
                        },
                         {
                             field: 'solve_date',
                             title: 'solve_date',
                         },
                          {
                              field: 'condition',
                              title: 'condition',
                          },
                ]
            });
        }

        function EditInfo(el) {
            $('#myModal').modal({ backdrop: 'static', keyboard: false });
            $('#myModal').on('shown.bs.modal', function () {
                $('#mhead').html("EDIT");
                $('#occur_date').val(laydate.now(el.occur_date, 'YYYY-MM-DD'));
                $('#location').val(el.location);
                $('#machine_type').val(el.machine_type);
                $('#machine_ID').val(el.machine_ID);
                $('#costCenter').val(el.costCenter);
                $('#type').val(el.type);
                $('#rootCause').val(el.rootCause);
                $('#solution').val(el.solution);
                $('#cost').val(el.cost);
                $('#operator').val(el.operator);
                $('#solve_date').val(laydate.now(el.solve_date, 'YYYY-MM-DD'));
                $('#condition').val(el.condition);
                ID=el.ID
            });
        }
        function AddInfo() {
            $('#myModal').modal({ backdrop: 'static', keyboard: false });
            $('#myModal').on('shown.bs.modal', function (e) {
                $('#mhead').html("ADD");
                $(":input").val('');
            });
        }

        function DeleteInfo(ID) {
            $.ajax({
                type: "get",
                url: "ajax/ActionHandler.ashx?action=delDataPM&ID=" + ID,
                cache: false,
                dataType: "text",
                success: function (data) {
                    $("#tabAll").bootstrapTable('refresh');
                },
                error: function () {
                    document.writeln("havn't delete data from database!");
                }
            });
        }
        function SaveInfo() {
            if ($('#saveadmin_form').valid()) {
                var action = $("#mhead").html();
                var url = "ajax/ActionHandler.ashx?action";
                var formParam = $("#saveadmin_form").serialize();
                formParam = decodeURIComponent(formParam, true);
                url += (action == "EDIT") ? ("=editDataPM&ID=" + ID + "&") : ("=addDataPM&");
                url += formParam;
                console.log("URL:" + url);
                $.ajax({
                    type: "POST",
                    url: url,
                    cache: false,
                    success: function (data) {
                        console.log(data);
                        $('#myModal').modal('hide');
                        $("#tabAll").bootstrapTable('refresh');
                    },
                    error: function () {
                        document.writeln("havn't save data into database!");
                    }
                });
            }
            else {
                return;
            }
        }

        $('#myModal').on('hidden.bs.modal', function () {
            //$(".error").remove();
            $("#saveadmin_form").validate().resetForm();
        });
    </script>
</body>
</html>