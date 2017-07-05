<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="bom_Nstandard.aspx.cs" Inherits="moonshine.bom_Nstandard" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <ol class="breadcrumb">
        <li><a href="#">Product</a></li>
        <li class="active">bom_Nstandard(非标件)</li>
    </ol>
    <div class="col-sm-12 panel panel-default ">
        <div class="panel-body">
            Item_id:<asp:DropDownList ID="ddlimd" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlimd_SelectedIndexChanged" class="required" />
            Name:<asp:TextBox ID="txtname" runat="server" class="required" />
            Base_PN:<asp:DropDownList ID="ddlimd0" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlimd0_SelectedIndexChanged" class="required" />
            Desc:<asp:TextBox ID="txtdesc" runat="server" class="required" />
            Spec:<asp:TextBox ID="txtspec" runat="server" class="required" />
            Qty:<asp:TextBox ID="txtqty" runat="server" class="required digits" />
            <asp:Button ID="btnInsert" runat="server" Text="Add" OnClick="btnInsert_Click" CssClass="SubmitStyle" OnClientClick="check()" />
        </div>
    </div>

    <div class="table-responsive col-sm-12">
        <asp:GridView runat="server" ID="gvbom1" CssClass="table table-condensed table-bordered table-hover" DataKeyNames="basic_id" OnRowDataBound="gvbom1_RowDataBound" OnRowDeleting="gvbom1_RowDeleting" ShowFooter="True">
            <Columns>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <script>
        getPower();
        function getPower() {
            $.ajax({
                type: 'get',
                url: "AJAX/getPower.ashx",
                contentType: "application/text; charset=utf-8",
                success: function (data) {
                    if (data == "guest") {
                        var tb = document.getElementById("MainContent_gvbom1");
                        document.getElementById("MainContent_btnInsert").style.display = "none";
                        for (i = 0; i < tb.rows.length; i++) {
                            tb.rows[i].cells[0].style.display = "none";
                        }
                    }
                }
            });
        }
    </script>
</asp:Content>