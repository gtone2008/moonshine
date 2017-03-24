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
    <fieldset>
            <label class="col-sm-2">Applicant申请人</label>
            <input class="col-sm-4" type="text" name="reqUser"  placeholder="请输入名字"  disabled="disabled"/>
            <label class="col-sm-2" ">Application Date申请日期</label>
            <input class="col-sm-4" type="text" name="reqDate" placeholder="请输入日期" disabled="disabled"/>
            <label class="col-sm-2">Phone联系方式</label>
            <input class="col-sm-4" type="text" name="reqPhone" placeholder="请输入联系方式"  disabled="disabled"/>
            <label class="col-sm-2">Department部门</label>
            <input class="col-sm-4" type="text" name="reqDep"  placeholder="请输入部门"  disabled="disabled"/>
            <label class="col-sm-2">Workcell项目</label>
            <input class="col-sm-4" type="text" name="reqWC"  placeholder="请输入项目"  disabled="disabled"/>
            <label class="col-sm-2">Cost Center成本中心</label>
            <input class="col-sm-4" type="text" name="reqCost"  placeholder="请输入成本中心"  disabled="disabled"/>
            <label class="col-sm-2">Required Date需求日期</label>
            <input class="col-sm-4" type="text" name="reqNeedDate" id="reqNeedDate" placeholder="请选择需求日期(七天后)"  disabled="disabled"/>
            <label class="col-sm-2">CER/NER NO</label>
            <input class="col-sm-4" type="text"  name="reqCER" placeholder="请输入CER NO"  disabled="disabled"/>
            <label for="comment" class="col-sm-2">Required Description/需求描述:</label>
            <div class="col-sm-4">
                <input type="radio" name="newold" id="new1"  disabled="disabled"/>新产品报价
                <input type="radio" name="newold" id="old1"  disabled="disabled"/>作回收报价
            </div>
            <input class="col-sm-12" type="text"  name="reqDesc" placeholder="请输入需求描述" disabled="disabled"/>
        </fieldset>
    <br />
    <div id="requestInfo"  class="col-lg-12" style="padding:0">
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
