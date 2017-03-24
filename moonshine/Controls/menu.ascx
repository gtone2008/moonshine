<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="menu.ascx.cs" Inherits="Template.Controls.menu" %>
<div id="Header">
    <hr id="topHR" />
    <div id="logoDiv">
        <a href="http://jabilweb.corp.jabil.org/" style="cursor: pointer; float: left">
            <img src="images/jabil_log.jpg" style="width: 179px; height: 48px; border: none" alt="JABIL" />
        </a><span id="appName"><font color="Blue">Moonshine System</font> <font size="3">v1.0</font></span>
        <div style="clear: both">
        </div>
    </div>
    <div id="nav1">
        <ul>
            <li><a href="#">Inventory</a>
                <ul>
                    
                    <li><a href="./Base_data.aspx">Base</a></li>
                    <li><a href="./In.aspx">In</a></li>
                    <li><a href="./Out.aspx">Out</a></li>
                </ul>
            </li>     
            <li><a href="#">Product</a>
                <ul>                  
                    <li> <a href="./PT_Standard.aspx">Standard_Product</a></li>
                    <li><a href="./bom_standard.aspx">Bom_Standard</a></li>
                </ul>
            </li>
             <li><a href="#">Purchase</a>
                <ul>                  
                    <li> <a href="./PT_Standard_List.aspx">Standard_Product</a></li>
                    <%--<li><a href="./AddModelItems.aspx">Model Setting</a></li>--%>
                </ul>
            </li>                                           
        </ul>
        <asp:Label ID="lbUser" CssClass="userInfo" runat="server">XXX</asp:Label>
    </div>
</div>