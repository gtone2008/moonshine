<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PT_Standard.aspx.cs" Inherits="moonshine.PT_Standard" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <script src="Scripts/myJavaScript.js"></script>
        <ol class="breadcrumb">
        <li><a href="#">Product</a></li>
        <li class="active">Standard(amount空因为未维护BOM)</li>
    </ol>
    <table>
        <tr>
            <td>
                <table style="background-color: #EEEEEE; float: left;">
                    <tr>
                        <td>|Item_id:<asp:TextBox ID="txt_id" runat="server" AutoPostBack="True" OnTextChanged="txt_id_TextChanged" class="required"/>

                        </td>
                        <td style="text-align: right;">|Name:<asp:TextBox ID="txt_name" runat="server" class="required"/>

                        </td>
                        <td style="text-align: right;">|Moonshine Standard:<asp:TextBox ID="txtms" runat="server" class="required"/></td>
                        <td style="text-align: right;">|Use Area<asp:TextBox ID="txtuar" runat="server" class="required"/>
                        </td>
                    </tr>
                    <tr>
                        <td>|Picture:<asp:FileUpload runat="server" ID="FileUpload1" onchange="CheckFile(this)" class="required"/></td>
                        <td style="text-align: right;" colspan="3">
                            <asp:Button ID="btninsertPTS" runat="server" Text="Add" CssClass="btn btn-danger" OnClick="btninsertPTS_Click" /></td>
                    </tr>
                </table>
            </td>
            <td></td>
        </tr>
    </table>
    <div class="table-responsive col-sm-12">
        <asp:GridView runat="server" ID="gvin" CssClass="table table-condensed table-bordered table-hover" DataKeyNames="ps_id" AllowPaging="True" AutoGenerateColumns="False" OnPageIndexChanging="gvin_PageIndexChanging" PageSize="30" OnRowCancelingEdit="gvin_RowCancelingEdit" OnRowDeleting="gvin_RowDeleting" OnRowEditing="gvin_RowEditing" OnRowDataBound="gvin_RowDataBound" OnRowUpdating="gvin_RowUpdating1">
            <Columns>
                <asp:BoundField DataField="ps_id" HeaderText="item_id" ReadOnly="True" />
                <asp:BoundField DataField="ps_name" HeaderText="name" />
                <asp:BoundField DataField="ps_standard" HeaderText="standard" />
                <asp:BoundField DataField="ps_useArea" HeaderText="useArea" />
                <asp:BoundField DataField="v_amount" HeaderText="amount" ReadOnly="True" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Image ID="imgPic" runat="server" Height="50" Width="200" ImageUrl='<%#string.Format("~/uploads\\{0}",Eval("ps_pic"))%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="PhotoName">
                    <ItemTemplate>
                        <asp:Label ID="LabelPhoto" runat="server" Text='<%#string.Format("{0}",Eval("ps_pic"))%>' Visible="true" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <EditItemTemplate>
                        <asp:LinkButton ID="LinkButtonUpdate" runat="server" CausesValidation="True" CommandName="Update" Text="Update"></asp:LinkButton>
                        &nbsp;<asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButtonDelete" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <script>
        function openphoto(photo, bid) {
            window.open("photo.aspx?url1=PT_Standard&bid=" + bid + "&photo=" + photo, "photo", "top=nInt,left=nInt,width=600,height=600,location=yes,menubar=no,resizable=yes,scrollbars=yes,status=no,toolbar=no")
        };
    </script>
</asp:Content>
