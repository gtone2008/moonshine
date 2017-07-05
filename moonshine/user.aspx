<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="user.aspx.cs" Inherits="moonshine.user" ValidateRequest="false" %>

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
    <script src="Content/echarts.common.min.js"></script>
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
                <li><a href="#">Manage</a></li>
                <li class="active">User (action please right-click on cell if admin role)</li>
            </ol>
            <div class="col-sm-12 panel panel-default table-responsive">
                <table id="tabAll" class="table table-condensed"></table>
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
                                <div class="col-sm-6">
                                    <label class="col-sm-4 text-right" for="location">NTID</label>
                                    <input class="col-sm-8  required" type="text" id="NTID" name="NTID" />
                                </div>
                                <div class="col-sm-6">
                                    <label class="col-sm-4 text-right">power</label>
                                    <input class="col-sm-8 required" type="text" id="power" name="power" />
                                </div>
                            </div>
                            <br />
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
        })

        function getPower() {
            var res = "";
            $.ajax({
                type: 'get',
                url: "AJAX/getPower.ashx",
                contentType: "application/text; charset=utf-8",
                async: false,
                success: function (data) {
                    if (data != "admin") {
                        res = 0;
                    }
                    else
                    {                        
                        res = 1;
                    }
                    document.cookie = "admin="+res;
                    getTable(res);

                }
            });
        }

        function getTable(role)
        {
            $('#tabAll').bootstrapTable({
                url: 'ajax/ActionHandler.ashx?action=getDataUser',
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
                    if (role) {
                        if ($el.data("item") == "edit") {
                            EditInfo(row);
                        } else if ($el.data("item") == "delete") {
                            DeleteInfo(row.NTID);
                        } else if ($el.data("item") == "add") {
                            AddInfo();
                        }
                    }
                    else {
                        alert('You do not have permission');
                    }
                },
                columns: [
                    {
                        title: "ID",
                        formatter: function (value, row, index) {
                            return index + 1;
                        },
                        width: '30px'
                    },
                    {
                        field: 'NTID',
                        title: 'NTID',

                    },
                    {
                        field: 'power',
                        title: 'power',

                    }, {
                        field: 'date',
                        title: 'Createdate',
                    },
                ]
            });
        }

        function EditInfo(el) {
            $('#myModal').modal({ backdrop: 'static', keyboard: false });
            $('#myModal').on('shown.bs.modal', function () {
                $('#mhead').html("EDIT");
                $('#NTID').val(el.NTID);
                $('#NTID').attr("readonly", "true");
                $('#power').val(el.power);
            });
        }
        function AddInfo() {
            $('#myModal').modal({ backdrop: 'static', keyboard: false });
            $('#myModal').on('shown.bs.modal', function (e) {
                $('#costID').removeAttr("readonly");
                $('#mhead').html("ADD");
                $(":input").val('');
            });
        }

        function DeleteInfo(NTID) {
            $.ajax({
                type: "get",
                url: "ajax/ActionHandler.ashx?action=delDataUser&NTID=" + NTID,
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
                url += (action == "EDIT") ? ("=editDataUser&") : ("=addDataUser&");
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