<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Base_data.aspx.cs" Inherits="moonshine.Base_data" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <script src="Scripts/table2excel.js"></script>
    <ol class="breadcrumb">
        <li><a href="#">Inventory</a></li>
        <li class="active">Base</li>
    </ol>
    <div class="form-group" id="add1" style="display: none">
        <div class="col-sm-12">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;PN:<input type="text" id="pn1" name="pn1" onblur="checkPn()" class="required" />
            Description:<asp:TextBox ID="txtdescription" runat="server" class="required" />
            Spec:<asp:TextBox ID="txtspec" runat="server" class="required" />
            Price<asp:TextBox ID="txtprice" runat="server" class="required number" />
            LowLimit:<asp:TextBox ID="txtlow" runat="server" class="digits" />
        </div>
        <div class="col-sm-12">
            UpperLimit:<asp:TextBox ID="txtupper" runat="server" class="digits" />
            Location:<asp:TextBox ID="txtloc" runat="server" />
            Current_qty:<asp:TextBox ID="txtcqty" runat="server" class="required number" />
            <div style="display: inline-table">
                <asp:FileUpload ToolTip="上传图片" runat="server" ID="FileUpload1" onchange="CheckFile(this)" />
            </div>
            <div style="display: inline-table">
                <asp:Button ID="btnInsert" runat="server" Text="Add" CssClass="SubmitStyle" OnClientClick="check()" Width="100px" OnClick="btnInsert_Click" />
            </div>
        </div>
    </div>
    <div class="col-sm-12 panel panel-default ">
        <div class="panel-body">
            P/N:<asp:TextBox ID="txtbase" runat="server" data-provide="typeahead" autocomplete="off" placeholder="请输入PN号" />
            <%--P/N:<asp:TextBox ID="TextBox3" runat="server"  onkeyup="mySearch()" />--%>
            Description:<asp:TextBox ID="txtdesc" runat="server" CssClass="td1" />
            <asp:Button ID="btnQuery" runat="server" Text="Search" OnClick="btnQuery_Click" CssClass="SubmitStyle" />
            <asp:CheckBox Text="Below limt" runat="server" ID="checklow" Checked="false" OnCheckedChanged="checklow_CheckedChanged" AutoPostBack="true" />
            <input type="checkbox" id="c11" onchange="checkAdd()" />Add
            <asp:Label runat="server" ID="lbTotal" CssClass=" label label-success" />
            <%--<img id="imgPic1"  style="display:none" />--%>
            <asp:LinkButton runat="server" ID="toExcel" OnClick="toExcel_Click" CssClass="btn btn-sm btn-success  pull-right">TO Excel</asp:LinkButton>
        </div>
    </div>
    <div class="col-sm-12 table-responsive">
        <asp:GridView runat="server" ID="gvbase" DataKeyNames="basic_id,price" AutoGenerateColumns="False" OnPageIndexChanging="gvbase_PageIndexChanging" AllowPaging="True" AllowSorting="True" OnRowDataBound="gvbase_RowDataBound" OnRowDeleting="gvbase_RowDeleting" OnRowCancelingEdit="gvbase_RowCancelingEdit" OnRowEditing="gvbase_RowEditing" OnRowUpdating="gvbase_RowUpdating" Width="100%" PageSize="100" CssClass=" table table-hover table-condensed">
            <Columns>
                <asp:BoundField DataField="basic_id" HeaderText="P/N" />
                <asp:BoundField DataField="description" HeaderText="description" />
                <asp:BoundField DataField="spec" HeaderText="spec" />
                <asp:TemplateField HeaderText="price(RMB)">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("price") %>' placeholder="价格"></asp:TextBox>
                        <asp:TextBox ID="TextBox2" runat="server" placeholder="更改价格必须填写备注" class="required"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <%--<asp:Label ID="Label1" runat="server" Text='<%# Bind("price") %>' ><a href='#'>111</a></asp:Label>--%>
                        <%-- <a href='#' onclick='openwin(<%# Eval("basic_id") %>)'><%# Eval("price") %></a>--%>
                        <asp:HyperLink runat="server" ID="h1" Text='<%# Bind("price") %>' Font-Underline="False"></asp:HyperLink>
                        <%--<asp:LinkButton ID="lbprice" runat="server" CausesValidation="False" Text='<%# Eval("price") %>'></asp:LinkButton>--%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="LowLimit" HeaderText="LowLimit" />
                <asp:BoundField DataField="UpperLimit" HeaderText="UpperLimit" />
                <asp:BoundField DataField="location" HeaderText="location" />
                <asp:BoundField DataField="current_qty" HeaderText="current_qty" ReadOnly="true" />
                <asp:TemplateField HeaderText="PhotoName">
                    <ItemTemplate>
                        <%-- <asp:Label ID="Label1" runat="server" Text='<%#string.Format("/uploads/{0}",Eval("photo"))%>' Visible="true" />--%>
                        <asp:Label ID="Label1" runat="server" Text='<%#string.Format("{0}",Eval("photo"))%>' Visible="true" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <EditItemTemplate>
                        <asp:LinkButton ID="LinkButton11" runat="server" CausesValidation="True" CommandName="Update" Text="Update"></asp:LinkButton>
                        &nbsp;<asp:LinkButton ID="LinkButton21" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton31" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle Wrap="true" />
        </asp:GridView>
    </div>
    <table>
        <tr>
        </tr>
    </table>
    <script type="text/javascript">
        getPower();
        function getPower()
        {
            $.ajax({
                type: 'get',
                url: "AJAX/getPower.ashx",
                contentType: "application/text; charset=utf-8",
                success: function (data) {
                    if (data != "admin") {
                        var tb = document.getElementById("MainContent_gvbase");
                        document.getElementById("c11").style.display = "none";
                        for (i = 0; i < tb.rows.length; i++) {
                            tb.rows[i].cells[9].style.display = "none";
                            tb.rows[i].cells[10].style.display = "none";
                        }
                    }
                }
            });
        }
        function openwin(a) {
            var newwin = window.open("price_change.aspx?bid=" + a, "price", "top=nInt,left=nInt,width=600,height=400,location=yes,menubar=no,resizable=yes,scrollbars=yes,status=no,toolbar=no");
            newwin.focus();
        };
        function openphoto(photo, bid) {
            window.open("photo.aspx?url1=Base_data&bid=" + bid + "&photo=" + photo, "photo", "top=nInt,left=nInt,width=600,height=600,location=yes,menubar=no,resizable=yes,scrollbars=yes,status=no,toolbar=no")
        };
        function checkAdd() {

            $("#add1").toggle(200);
        };
        function checkPn() {
            var aa = $("#pn1").val().toString();
            $.ajax({
                type: "get",
                url: "getData1.ashx?action=getPN&pn1=" + $("#pn1").val(),
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    if (data == "True") {
                        alert($("#pn1").val() + "已经存在此料号");
                        $("#pn1").val("");
                        $("#pn1").focus();
                    }
                },
                error: function (err) {
                    alert(err);
                }
            }
        )
        };

        $('#MainContent_txtbase').typeahead({
            source: function (query, process) {
                $.ajax({
                    url: "getData1.ashx?action=getPNS",
                    type: 'GET',
                    dataType: 'JSON',
                    async: true,
                    data: 'basic_id=' + query,
                    success: function (data) {
                        var arr = [];
                        for (i in data) {
                            arr.push(data[i]['basic_id'] + "");
                            //document.write(data[i]['basic_id']);
                        }
                        process(arr);
                    }//success
                });
            }//souce

        });//typeahead

        function ToExcel() {
            CellAreaExcel()
        };

        //fiter
        function mySearch() {
            var input, filter, table, tr, td, i;
            input = document.getElementById("MainContent_txtbase");
            filter = input.value.toUpperCase();
            table = document.getElementById("MainContent_gvbase");
            tr = table.getElementsByTagName("tr");
            for (i = 0; i < tr.length; i++) {
                td = tr[i].getElementsByTagName("td")[0];
                if (td) {
                    if (td.innerHTML.toUpperCase().indexOf(filter) > -1) {
                        tr[i].style.display = "";
                    } else {
                        tr[i].style.display = "none";
                    }
                }
            }
        };
        function mySearch2() {
            var input, filter, table, tr, td, i;
            input = document.getElementById("MainContent_txtdesc");
            filter = input.value.toUpperCase();
            table = document.getElementById("MainContent_gvbase");
            tr = table.getElementsByTagName("tr");
            for (i = 0; i < tr.length; i++) {
                td = tr[i].getElementsByTagName("td")[1];
                if (td) {
                    if (td.innerHTML.toUpperCase().indexOf(filter) > -1) {
                        tr[i].style.display = "";
                    } else {
                        tr[i].style.display = "none";
                    }
                }
            }
        };
    </script>
</asp:Content>