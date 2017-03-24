using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using moonshine.Model;
using moonshine.BLL;
using moonshine.Util;
namespace Template.Controls
{
    public partial class menu : System.Web.UI.UserControl
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (!role.role_check(System.IO.Path.GetFileName(Request.Path).ToString()))
                {
                    //Response.Write("<script>alert('没有当前页面权限!')</script>");
                    //Response.Write("<script language=javascript>history.back(-1);</script>");
                    //Response.Redirect("Default.aspx");
                    Response.Write("<script>alert('无此页面权限!');location.href='Default.aspx';</script>");
                }
            }
            try { 
                moonshine.Model.AdUser user = bll_aduser.GetCurrentAdUser();
                this.lbUser.Text = user.DisplayName;
                Session["userName"] = user.DisplayName;
            }
            catch 
            {
                //this.lbUser.Text = Page.User.Identity.Name;
                this.lbUser.Text = Common.GetCurrentNTID();
                Session["userName"] = this.lbUser.Text;
            }
        }
    }
}