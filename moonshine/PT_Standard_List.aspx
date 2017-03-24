<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PT_Standard_List.aspx.cs" Inherits="moonshine.PT_Standard_List"  EnableEventValidation ="false" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <ol class="breadcrumb">
        <li><a href="#">Purchase</a></li>
        <li class="active">Standard_Product</li>
        <li><span style="float:right"><a href='ShoppingCart.aspx' ><font color="red">My Cart<asp:Label ID="lab1" runat="server"/></font></a></span></li>
    </ol>
    <div class="divBorder">
        <asp:DataList ID="dlProducts" runat="server" RepeatColumns="5" RepeatDirection="Horizontal"
            Width="99%" OnItemCommand="dlProducts_ItemCommand" DataKeyField="ps_id" >
            <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
            <ItemTemplate>
                <ul>
                    <asp:Image ID="imgPic" runat="server" class="img-thumbnail" Height="100" Width="200" ImageUrl='<%#string.Format("~/uploads\\{0}",Eval("ps_pic"))%>' />
                    <li>Item_id:<%# Eval("ps_id") %></li>
                    <li>Name:<%# Eval("ps_name") %></li>
                    <li>Standard:<%# Eval("ps_standard") %></li>
                    <li>UseArea:<%# Eval("ps_useArea") %></li>
                    <li>Price:<font color="red">¥<%# Eval("v_amount")%></font></li>
                    <asp:Button ID="addcart" runat="server" CommandName="addcart" Text="Add to Cart" CssClass="SubmitStyle" CommandArgument='<%# Eval("ps_id")%>' />
                    <%--    <a href='ShoppingCart.aspx?ID=<%# Eval("ps_id")%>' >Add to cart</a>--%>
                </ul>
            </ItemTemplate>
        </asp:DataList>
    </div>

</asp:Content>
