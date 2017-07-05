using System;
using moonshine.Util;

namespace Template.Controls
{
    public partial class menu1 : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //string url = System.IO.Path.GetFileName(Request.Path).ToString();
                //if (url != "Default")
                //{
                //    if (!role.role_check(url))
                //    {
                //        //Response.Write("<script>alert('没有当前页面权限!')</script>");
                //        //Response.Write("<script language=javascript>history.back(-1);</script>");
                //        //Response.Redirect("Default.aspx");
                //        Response.Write("<script>alert('无此页面权限!');location.href='Default.aspx';</script>");
                //    }
                //}
            }
            //this.lbUser.Text = Page.User.Identity.Name;
            //this.lbUser.Text = Common.GetCurrentNTID();
            moonshine.Model.AdUser user = Common.GetADUserEntity(Common.GetCurrentNTID());
            this.lbUser.Text = user.DisplayName;
            Session["userName"] = this.lbUser.Text;
        }
    }
}