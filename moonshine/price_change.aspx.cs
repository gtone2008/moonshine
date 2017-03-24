using moonshine.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace moonshine
{
    public partial class price_change : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            gvband(Request.QueryString["bid"]);
            //Response.Write("<script>alert('" + Request.QueryString["bid"] + "')</script>");

        }
        protected void gvband(string bid)
        {
            string str1 = "select basic_id, price,change_user,change_date,remark from price_change where basic_id='" + bid+ "'";
            gvprice.DataSource = MysqlHelper.ExecuteDataTable(str1);
            gvprice.DataBind();
            if(gvprice.Rows.Count==0)
            {
                Response.Write("No Change");
            }
        }
    }
}