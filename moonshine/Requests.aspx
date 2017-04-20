<%@ Page Language="C#"  AutoEventWireup="true" CodeBehind="Requests.aspx.cs" Inherits="moonshine.Requests" validateRequest="false" ViewStateMode="Disabled"%>
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
    <style>
        .ribbon {
            display:none;
            background-color: #a00;
            overflow: hidden;
            white-space: nowrap;
            opacity: 0.6;
            /* top left corner */
            position: fixed;
            right: 100px;
            top: 200px;
            /* 45 deg ccw rotation */
            -webkit-transform: rotate(45deg);
            -moz-transform: rotate(45deg);
            -ms-transform: rotate(45deg);
            -o-transform: rotate(45deg);
            transform: rotate(45deg);
            /* shadow */
            -webkit-box-shadow: 0 0 10px #888;
            -moz-box-shadow: 0 0 10px #888;
            box-shadow: 0 0 10px #888;
            z-index: 999;
        }

            .ribbon a {
                border: 1px solid #faa;
                color: #fff;
                display: block;
                font: bold 81.25% 'Helvetica Neue', Helvetica, Arial, sans-serif;
                margin: 1px 0;
                padding: 10px 50px;
                text-align: center;
                text-decoration: none;
                /* shadow */
                text-shadow: 0 0 5px #444;
            }
    </style>
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
    <form runat="server" id="f1" class="form-horizontal" defaultbutton="buttonxxx">
        <asp:Button ID="buttonxxx" runat="server" Enabled="False" Style="display: none" />
         <div class="container-fluid body-content">
    <ol class="breadcrumb">
        <li><a href="#">Requests</a></li>
        <li class="active">Approval</li>
    </ol>
<div class="ribbon">
    <a href="#" ></a>
</div>
    <fieldset>
    
            <div class="col-sm-6">
            <label class="col-sm-4">Applicant申请人</label>
            <input class="col-sm-8" type="text" name="reqUser"  placeholder="请输入名字" disabled="disabled"/>
            </div>
        <div class="col-sm-6">
            <label class="col-sm-4" ">Application Date申请日期</label>
            <input class="required col-sm-8" type="text" name="reqDate" placeholder="请输入日期" disabled="disabled" />
            </div>
        <div class="col-sm-6">
            <label class="col-sm-4">Phone联系方式</label>
            <input class="col-sm-8" type="text" name="reqPhone" disabled="disabled" />
            </div>
        <div class="col-sm-6">
            <label class="col-sm-4">Department部门</label>
            <input class="required col-sm-8" type="text" name="reqDep"  disabled="disabled" />
            </div>
        <div class="col-sm-6">
            <label class="col-sm-4">Workcell项目</label>
            <input class="required col-sm-8" type="text" name="reqWC"  disabled="disabled"  />
            </div>
        <div class="col-sm-6">
            <label class="col-sm-4">Cost Center成本中心</label>
            <input class="required digits col-sm-8" type="text" name="reqCost" id="reqCost" disabled="disabled"/>
            </div>
        <div class="col-sm-6">
            <label class="col-sm-4">Required Date需求日期</label>
            <input class="required col-sm-8" type="text" name="reqNeedDate" id="reqNeedDate"  disabled="disabled"/>
            </div>
        <div class="col-sm-6">
            <label class="col-sm-4">CER/NRE NO</label>
            <input class="col-sm-8" type="text"  name="reqCER" disabled="disabled" />
            </div>
        <div class="col-sm-6">
            <label  class="col-sm-4">Required Description/需求描述:</label>
            <div class="col-sm-8">
            <input type="radio" name="newold" value="1" checked="checked" disabled="disabled"/>新产品报价
            <input type="radio" name="newold" value="0" disabled="disabled"/>作回收报价
            </div>
          </div>

           <input class="required col-sm-12" type="text"  name="reqDesc" disabled="disabled"  />
        </fieldset>
    <br />
    <div id="requestInfo"  class="col-lg-12 disabled" style="padding:0;" >
    </div>

        <div class="col-sm-12 panel panel-default ">
        <div class="panel-body">
            <div id="remarkDiv" runat="server" visible="false">
            <label class="col-sm-2">Remark:</label>
            <textarea class="col-sm-10" rows="2" id="txtRemark"  name="txtRemark" placeholder="请输入需求审批意见"></textarea>
            </div>
            <input id="btnApprove" runat="server" type="submit" value="Approve" class="col-sm-2 col-lg-push-2 btn btn-success" visible="false" onclick="return aaa();" onserverclick="btnApprove_Click"  />
            <input id="btnReject" runat="server" type="submit" value="Reject" class="col-sm-2 col-lg-push-2 btn btn-warning" visible="false" onclick="return bbb();" onserverclick="btnReject_Click"  />
            <input id="btnCancel" runat="server" type="submit" value="Cancel" class="col-sm-2 col-lg-push-2 btn btn-danger" visible="false" onclick="return ccc();" onserverclick="btnCancel_Click"  />
        </div>
    </div>
  <div class="col-sm-12 ">
        <asp:GridView runat="server" ID="gvTask"   AutoGenerateColumns="true" HorizontalAlign="Center"  ViewStateMode="Disabled" Width="100%">
        </asp:GridView>
</div>
    <script>
        var reqid=<%=Request.QueryString["reqid"]%>; 
        function aaa(){
            $('#btnApprove').attr('disabled', 'disabled');
            $('#btnReject').attr('disabled', 'disabled');
            $('#btnCancel').attr('disabled', 'disabled');
            $('#btnApprove').val('Approving...');
            __doPostBack('btnApprove','');
                               
        };
        function bbb(){
            var a= $('#txtRemark').val();   
            if(!a)
            {
                alert('if Reject , you should input remark');
                $('#txtRemark').focus();
                return false;
            }
            else
            {
                $('#btnApprove').attr('disabled', 'disabled');
                $('#btnReject').attr('disabled', 'disabled');
                $('#btnCancel').attr('disabled', 'disabled');
                $('#btnReject').val('Rejecting...');
                __doPostBack('btnReject','');
            }                      
        };
        function ccc(){
            var a= $('#txtRemark').val();   
            if(!a)
            {
                alert('if cancle , you should input remark');
                $('#txtRemark').focus();
                return false;
            }
            else
            {
                $('#btnApprove').attr('disabled', 'disabled');
                $('#btnReject').attr('disabled', 'disabled');
                $('#btnCancel').attr('disabled', 'disabled');
                $('#btnCancel').val('Canceling...');
                __doPostBack('btnCancel','');
            }                      
        };

        ribbon();
        function ribbon() {
            // Declare variables 
            var table, tr, td, i;
            table = document.getElementById("gvTask");
            tr = table.getElementsByTagName("tr");
            if(tr.length>4)
            {
                td = tr[4].getElementsByTagName("td")[3].innerHTML
                if(td=="Approve")
                {
                    $(".ribbon a").text("Closed");
                    $(".ribbon").show();
                }
            }           
                
        }

        $.ajax({
            type: "get",
            url: "getData1.ashx?action=getRequestByReqID&reqid="+reqid,
            dataType: "json",
            success: function (data) { 
                //data[0].reqUser
                $('input[name=reqUser]').val(data[0].reqUser);
                $('input[name=reqDate]').val(data[0].reqDate);
                $('input[name=reqPhone]').val(data[0].reqPhone);
                $('input[name=reqDep]').val(data[0].reqDep);
                $('input[name=reqWC]').val(data[0].reqWC);
                $('input[name=reqCost]').val(data[0].reqCost);
                $('input[name=reqNeedDate]').val(data[0].reqNeedDate);
                $('input[name=reqDesc]').val(data[0].reqDesc);
                if(data[0].newold=='1') $("#new1").attr("checked","checked");else $("#old1").attr("checked","checked");
                $('input[name=reqDesc]').val(data[0].reqDesc);
                document.getElementById("requestInfo").innerHTML=data[0].reqInfo;
            },
            error: function (err) {
                alert(err);
            }
        });
    </script>

             <%--<script>
                 $('#f1').submit(function(){
                     var $btn = $('#f1').find('input[type="submit"]');
                     $btn.attr('disabled', 'disabled');
                     $btn.val('表单提交中');
                 });
</script>--%>
        <hr />
            <footer>
                <div class="footer" style="height: 2%; background-color: gray; width: 100%; text-align: center"><%: DateTime.Now.Year %>  Developed By Jabil(Wuxi) Mfg Engineering Team</div>
            </footer>
        </div>
    </form>
</body>
</html>
