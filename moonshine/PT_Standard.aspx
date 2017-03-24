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
                        <td>|Item_id:<asp:TextBox ID="txt_id" runat="server" AutoPostBack="True" OnTextChanged="txt_id_TextChanged" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_id" ErrorMessage="Not Null" ForeColor="Red" />
                        </td>
                        <td style="text-align: right;">|Name:<asp:TextBox ID="txt_name" runat="server" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_name" ErrorMessage="Not Null" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                        <td style="text-align: right;">|Moonshine Standard:<asp:TextBox ID="txtms" runat="server" /></td>
                        <td style="text-align: right;">|Use Area<asp:TextBox ID="txtuar" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>|Picture:<asp:FileUpload runat="server" ID="FileUpload1" onchange="CheckFile(this)" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="FileUpload1" ErrorMessage="Not Null" ForeColor="Red"></asp:RequiredFieldValidator></td>
                        <td style="text-align: right;" colspan="3">
                            <asp:Button ID="btninsertPTS" runat="server" Text="Add" CssClass="SubmitStyle" OnClick="btninsertPTS_Click" /></td>
                    </tr>
                </table>
            </td>
            <td></td>
        </tr>
    </table>
    <div class="table-responsive col-sm-12">
        <asp:GridView runat="server" ID="gvin" CssClass="table table-condensed table-bordered table-hover" DataKeyNames="ps_id" AllowPaging="True" AutoGenerateColumns="False" OnPageIndexChanging="gvin_PageIndexChanging" PageSize="20">
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
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
