<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="menu1.ascx.cs" Inherits="Template.Controls.menu1" %>
<%--<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">--%>
<%--<meta name="viewport" content="width=device-width, initial-scale=1.0">--%>
<style>
    *, *:after, *:before {
        -moz-box-sizing: border-box;
        -webkit-box-sizing: border-box;
        box-sizing: border-box;
    }

    .animenu__toggle {
        display: none;
        cursor: pointer;
        background-color: #111;
        border: 0;
        padding: 10px;
        height: 40px;
        width: 40px;
    }

        .animenu__toggle:hover {
            background-color: #005288;
        }

    .animenu__toggle__bar {
        display: block;
        width: 20px;
        height: 2px;
        background-color: #fff;
        -webkit-transition: 0.15s cubic-bezier(0.75, -0.55, 0.25, 1.55);
        -o-transition: 0.15s cubic-bezier(0.75, -0.55, 0.25, 1.55);
        transition: 0.15s cubic-bezier(0.75, -0.55, 0.25, 1.55);
    }

        .animenu__toggle__bar + .animenu__toggle__bar {
            margin-top: 4px;
        }

    .animenu__toggle--active .animenu__toggle__bar {
        margin: 0;
        position: absolute;
    }

        .animenu__toggle--active .animenu__toggle__bar:nth-child(1) {
            -webkit-transform: rotate(45deg);
            -ms-transform: rotate(45deg);
            -o-transform: rotate(45deg);
            transform: rotate(45deg);
        }

        .animenu__toggle--active .animenu__toggle__bar:nth-child(2) {
            opacity: 0;
        }

        .animenu__toggle--active .animenu__toggle__bar:nth-child(3) {
            -webkit-transform: rotate(-45deg);
            -ms-transform: rotate(-45deg);
            -o-transform: rotate(-45deg);
            transform: rotate(-45deg);
        }

    .animenu {
        display: block;
    }

        .animenu ul {
            padding: 0;
            list-style: none;
            font: 0px 'Open Sans', Arial, Helvetica;
        }

        .animenu li, .animenu a {
            display: inline-block;
            font-size: 15px;
        }

        .animenu a {
            color: #aaaaaa;
            text-decoration: none;
        }

    .animenu__nav {
        background-color: #005288;
    }

        .animenu__nav > li {
            position: relative;
            border-right: 1px solid #444444;
        }

            .animenu__nav > li > a {
                padding: 10px 30px;
                text-transform: uppercase;
            }

                .animenu__nav > li > a:first-child:nth-last-child(2):before {
                    content: "";
                    position: absolute;
                    border: 4px solid transparent;
                    border-bottom: 0;
                    border-top-color: currentColor;
                    top: 50%;
                    margin-top: -2px;
                    right: 10px;
                }

            .animenu__nav > li:hover > ul {
                opacity: 1;
                visibility: visible;
                margin: 0;
            }

            .animenu__nav > li:hover > a {
                color: #fff;
            }

    .animenu__nav__child {
        min-width: 100%;
        position: absolute;
        top: 100%;
        left: 0;
        z-index: 1;
        opacity: 0;
        visibility: hidden;
        margin: 20px 0 0 0;
        background-color: #005288;
        transition: margin .15s, opacity .15s;
    }

        .animenu__nav__child > li {
            width: 100%;
            border-bottom: 1px solid #515151;
        }

            .animenu__nav__child > li:first-child > a:after {
                content: '';
                position: absolute;
                height: 0;
                width: 0;
                left: 1em;
                top: -6px;
                border: 6px solid transparent;
                border-top: 0;
                border-bottom-color: inherit;
            }

            .animenu__nav__child > li:last-child {
                border: 0;
            }

        .animenu__nav__child a {
            padding: 10px;
            width: 100%;
            border-color: #373737;
        }

            .animenu__nav__child a:hover {
                background-color: #0186ba;
                border-color: #0186ba;
                color: #fff;
            }

    @media screen and (max-width: 767px) {
        .animenu__toggle {
            display: inline-block;
        }

        .animenu__nav,
        .animenu__nav__child {
            display: none;
        }

        .animenu__nav {
            margin: 10px 0;
        }

            .animenu__nav > li {
                width: 100%;
                border-right: 0;
                border-bottom: 1px solid #515151;
            }

                .animenu__nav > li:last-child {
                    border: 0;
                }

                .animenu__nav > li:first-child > a:after {
                    content: '';
                    position: absolute;
                    height: 0;
                    width: 0;
                    left: 1em;
                    top: -6px;
                    border: 6px solid transparent;
                    border-top: 0;
                    border-bottom-color: inherit;
                }

                .animenu__nav > li > a {
                    width: 100%;
                    padding: 10px;
                    border-color: #111;
                    position: relative;
                }

            .animenu__nav a:hover {
                background-color: #0186ba;
                border-color: #0186ba;
                color: #fff;
            }

        .animenu__nav__child {
            position: static;
            background-color: #373737;
            margin: 0;
            transition: none;
            visibility: visible;
            opacity: 1;
        }

            .animenu__nav__child > li:first-child > a:after {
                content: none;
            }

            .animenu__nav__child a {
                padding-left: 20px;
                width: 100%;
            }
    }

    .animenu__nav--open {
        display: block !important;
    }

        .animenu__nav--open .animenu__nav__child {
            display: block;
        }

    .userInfo {
        color: #ffffff;
        display: block;
        font-size: 16px;
        font-weight: bold;
        float: right;
        margin-top: -30px;
        margin-right: 10px;
    }

    #topHR {
        color: #008a5e;
        background: #008a5e;
        height: 8px;
        top: 0px;
        width: 100%;
    }

    #logoDiv {
        height: 48px;
        display: inline; /*margin: -12px auto;*/
    }

    #appName {
        height: 48px;
        vertical-align: bottom;
        line-height: 48px;
        font-size: 32px;
        font-family: Arial,sans-serif;
        font-weight: bold;
        float: right;
        margin-right: 15px;
    }

    .error {
        color: red;
    }

    .SubmitStyle {
        font-family: Arial;
        color: #ffffff;
        text-decoration: none;
        -webkit-border-radius: 28px;
        -moz-border-radius: 28px;
        border-radius: 28px;
        -webkit-box-shadow: 0px 1px 3px #666666;
        -moz-box-shadow: 0px 1px 3px #666666;
        box-shadow: 0px 1px 3px #FFFFFF;
        border: solid #FFFFFF 2px;
        background: #cccccc;
        cursor: pointer;
    }

        .SubmitStyle:hover {
            background: #e62097;
        }
</style>
<hr id="topHR" style="margin-top: 0px;" />
<div id="logoDiv">
    <a href="http://jabilweb.corp.jabil.org/" style="cursor: pointer; float: left">
        <img src="images/jabil_log.jpg" style="width: 179px; height: 48px; border: none" alt="JABIL" />
    </a><span id="appName"><font color="Blue">Moonshine Management System</font><font size="3">v2.0</font></span>
    <div style="clear: both">
    </div>
</div>
<div class="htmleaf-container">
    <nav class="animenu">
        <button class="animenu__toggle">
            <span class="animenu__toggle__bar"></span>
        </button>
        <ul class="animenu__nav">
            <li>
                <a href="./Default.aspx">Home</a>
            </li>
            <li>
                <a href="#">Inventory</a>
                <ul class="animenu__nav__child">
                    <li><a href="./Base_data.aspx">Base</a></li>
                    <li><a href="./In.aspx">In</a></li>
                    <li><a href="./Out.aspx">Out</a></li>
                </ul>
            </li>
            <li>
                <a href="#">Product</a>
                <ul class="animenu__nav__child">
                    <li><a href="./PT_Standard.aspx">Standard_Product</a></li>
                    <li><a href="./bom_standard.aspx">Bom_Standard</a></li>
                    <li><a href="./PT_NStandard.aspx">NStandard_Product</a></li>
                    <li><a href="./bom_Nstandard.aspx">Bom_NStandard</a></li>
                </ul>
            </li>

            <li>
                <a href="#">Purchase</a>
                <ul class="animenu__nav__child">
                    <%--<li><a href="./Base_List.aspx">Base_Material</a></li>
                    <li><a href="./PT_Standard_List.aspx">Standard_Product</a></li>
                    <li><a href="./PT_NStandard_List.aspx">NStandard_Product</a></li>--%>
                    <li><a href="./ShoppingCart.aspx">ShoppingCart</a></li>
                </ul>
            </li>
            <li>
                <a href="#">Request</a>
                <ul class="animenu__nav__child">
                    <%--<li><a href="./New_Request.aspx">New_Request</a></li>--%>
                    <li><a href="./RequestsAll.aspx">All_Request</a></li>
                </ul>
            </li>
            <li>
                <a href="#">Report</a>
                <ul class="animenu__nav__child">
                    <li><a href="./Cost_Summary.aspx">Cost_Summary</a></li>
                </ul>
            </li>
        </ul>
    </nav>
    <asp:Label ID="lbUser" CssClass="userInfo" runat="server">XXX</asp:Label>
</div>
<script type="text/javascript">
    (function () {
        var animenuToggle = document.querySelector('.animenu__toggle'),
            animenuNav = document.querySelector('.animenu__nav'),
            hasClass = function (elem, className) {
                return new RegExp(' ' + className + ' ').test(' ' + elem.className + ' ');
            },
            toggleClass = function (elem, className) {
                var newClass = ' ' + elem.className.replace(/[\t\r\n]/g, ' ') + ' ';
                if (hasClass(elem, className)) {
                    while (newClass.indexOf(' ' + className + ' ') >= 0) {
                        newClass = newClass.replace(' ' + className + ' ', ' ');
                    }
                    elem.className = newClass.replace(/^\s+|\s+$/g, '');
                } else {
                    elem.className += ' ' + className;
                }
            },
            animenuToggleNav = function () {
                toggleClass(animenuToggle, "animenu__toggle--active");
                toggleClass(animenuNav, "animenu__nav--open");
            }

        if (!animenuToggle.addEventListener) {
            animenuToggle.attachEvent("onclick", animenuToggleNav);
        }
        else {
            animenuToggle.addEventListener('click', animenuToggleNav);
        }
    })()
</script>
