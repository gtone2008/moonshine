using System;
using moonshine.Util;

namespace Template.Controls
{
    public partial class menu2 : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //this.lbUser.Text = Page.User.Identity.Name;
            //this.lbUser.Text = Common.GetCurrentNTID();
            moonshine.Model.AdUser user = Common.GetADUserEntity(Common.GetCurrentNTID());
            this.lbUser.Text = user.DisplayName;
            Session["userName"] = this.lbUser.Text;
        }
    }
}