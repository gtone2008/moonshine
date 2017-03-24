<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Base_List.aspx.cs" Inherits="moonshine.Base_List"  EnableEventValidation ="false" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <ol class="breadcrumb">
        <li><a href="#">Purchase</a></li>
        <li class="active">Base_Material</li>
        <li><span style="float:right"><a href='ShoppingCart.aspx' ><font color="red">My Cart<asp:Label ID="lab1" runat="server"/></font></a></span>           
        </li>
        <asp:CheckBox Text="Below limt" runat="server" ID="checklow" Checked="false"  AutoPostBack="true" OnCheckedChanged="checklow_CheckedChanged"/>
    </ol>
    <div class=" col-lg-12">
        <asp:DataList ID="dlbase" runat="server" RepeatColumns="5" RepeatDirection="Horizontal"
            Width="99%" OnItemCommand="dlProducts_ItemCommand" DataKeyField="basic_id"  EnableViewState="false">
            <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
            <ItemTemplate>
                <ul>
                    <asp:Image ID="imgPic" runat="server" class="img-thumbnail" Height="100" Width="200" ImageUrl='<%#string.Format("~/uploads\\{0}",Eval("photo"))%>' />
                    <li>Item_id:<%# Eval("basic_id") %></li>
                    <li>Desc:<%# Eval("description") %></li>
                    <li>Standard:<%# Eval("description") %></li>
                    <li>current_qty:<%# Eval("current_qty") %></li>
                    <li>Price:<font color="red">¥<%# Eval("price")%></font></li>
                    <asp:Button ID="addcart" runat="server" CommandName="addcart" Text="Add to Cart" CssClass="SubmitStyle" CommandArgument='<%# Eval("basic_id")%>' />
                    <%--    <a href='ShoppingCart.aspx?ID=<%# Eval("ps_id")%>' >Add to cart</a>--%>
                </ul>
            </ItemTemplate>
        </asp:DataList>
    </div>

</asp:Content>
