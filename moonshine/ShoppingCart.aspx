<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ShoppingCart.aspx.cs" Inherits="moonshine.ShoppingCart" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <ol class="breadcrumb">
        <li><a href="#">Purchase</a></li>
        <li class="active">ShoppingCart</li>
    </ol>
    <div class="panel panel-default " id="d_base">
        <div class="panel-heading"><a href="Base_List.aspx">常规物料:</a></div>
            <asp:GridView ID="gvshoping0" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-condensed table-hover">
            <Columns>
                <asp:BoundField DataField="ps_id" HeaderText="Item_id" />
                <asp:BoundField DataField="ps_name" HeaderText="Description" />
                <asp:BoundField DataField="price" HeaderText="Unit Price" />
                <asp:TemplateField HeaderText="Qty">
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="textbox0" class="required digits" Text='<%# Eval("count") %>' OnTextChanged="textbox0_TextChanged" AutoPostBack="True" placeholder="请输入整数" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="total" HeaderText="Sub-Total Price" />

                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button runat="server" ID="button0" CommandArgument='<%# Eval("ps_id") %>'
                            Text="delete" OnClick="button0_Click"  CssClass="SubmitStyle"/>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div> 

    <div class="panel panel-default " id="d_standard">
        <div class="panel-heading"><a href="PT_Standard_List.aspx">标准产品:</a></div>
            <asp:GridView ID="gvshoping1" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-condensed table-hover">
            <Columns>
                <asp:BoundField DataField="ps_id" HeaderText="Item_id" />
                <asp:BoundField DataField="ps_name" HeaderText="Description" />
                <asp:BoundField DataField="price" HeaderText="Unit Price" />
                <asp:TemplateField HeaderText="Qty">
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="textbox1" class="required digits" Text='<%# Eval("count") %>' OnTextChanged="textbox1_TextChanged" AutoPostBack="True" placeholder="请输入整数" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="total" HeaderText="Sub-Total Price" />

                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button runat="server" ID="button1" CommandArgument='<%# Eval("ps_id") %>'
                            Text="delete" OnClick="button1_Click"  CssClass="SubmitStyle"/>
                    </ItemTemplate>

                </asp:TemplateField>

            </Columns>
        </asp:GridView>
    </div> 

        <div class="panel panel-default " id="d_nstandard">
        <div class="panel-heading">定制产品:</div>
            <asp:GridView ID="gvshoping2" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-condensed table-hover">
            <Columns>
                <asp:BoundField DataField="type" HeaderText="type" />
                <asp:BoundField DataField="ps_id" HeaderText="Item_id" />
                <asp:BoundField DataField="ps_name" HeaderText="Description" />
                <asp:BoundField DataField="price" HeaderText="Unit Price" />
                <asp:TemplateField HeaderText="Qty">
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="textbox1" class="required digits" Text='<%# Eval("count") %>' OnTextChanged="textbox1_TextChanged" AutoPostBack="True" placeholder="请输入整数" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="total" HeaderText="Sub-Total Price" />

                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button runat="server" ID="button1" CommandArgument='<%# Eval("ps_id") %>'
                            Text="delete" OnClick="button1_Click"  CssClass="SubmitStyle"/>
                    </ItemTemplate>

                </asp:TemplateField>

            </Columns>
        </asp:GridView>
    </div>
    <div style="margin:auto,0;text-align:right">
        <asp:Button ID="ButtonApprove" runat="server" Text="Summit" CssClass="SubmitStyle"  Width="300" OnClick="ButtonApprove_Click" OnClientClick="check()" />
    </div>
    
</asp:Content>
