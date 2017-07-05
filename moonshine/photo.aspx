<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="photo.aspx.cs" Inherits="moonshine.photo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script>
        function refreshOpener() {
            opener.location.reload();
            window.close();
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="display: inline-table">
            <asp:FileUpload ToolTip="上传图片" runat="server" ID="FileUpload1" onchange="CheckFile1(this)" />
        </div>
        <div style="display: inline-table">
            <asp:Button ID="btnIMG" runat="server" Text="更改图片(名字一致可以覆盖图片)" CssClass=" btn  btn-danger" Width="300px" OnClick="btnIMG_Click" />
        </div>
        <div class="col-lg-12">
            <img id="img1" src="" class="img-thumbnail" />
        </div>
    </form>
</body>
<script src="Scripts/jquery.js"></script>
<script src="Scripts/myJavaScript.js"></script>
<script>
    getPower();
    function getPower() {
        $.ajax({
            type: 'get',
            url: "AJAX/getPower.ashx",
            contentType: "application/text; charset=utf-8",
            success: function (data) {
                if (data == "guest") {
                    document.getElementById("FileUpload1").style.display = "none";
                    document.getElementById("btnIMG").style.display = "none";
                }
            }
        });
    }
    $("#img1").attr("src", "uploads/<%=Request.QueryString["photo"]%>");
        function CheckFile1(obj)//onchange="CheckFile(this);
        {
            //$('#img1').attr("src",getObjectURL(obj.value));
            CheckFile(obj);
            //alert(getObjectURL(obj.value));
        };
</script>
</html>