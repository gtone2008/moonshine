using System;
using System.Web.UI.WebControls;
using moonshine.DAL;
using moonshine.Util;

namespace moonshine
{
    public partial class Out : System.Web.UI.Page
    {
        private string str1 = "SELECT out_id,description,spec,out_qty,out_type,reques_by as request_by,remark,out_date,operator FROM out_data left join basic_data on out_data.out_id=basic_data.basic_id where 1=1 ";
        private string str4 = "INSERT INTO  out_data(out_id, out_qty, out_type, reques_by, remark,operator) values('{0}',{1},'{2}','{3}','{4}','{5}');";
        private string str5 = "update basic_data set current_qty=current_qty-{0} where basic_id='{1}' ";//更新basic_data中current_qty数量

        //string str6 = "SELECT current_qty-{0} FROM basic_data WHERE basic_id='{1}' ";//out不能大于basic_data中current_qty数量
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //str1 = str1 + string.Format("and in_date ='{0}'", DateTime.Now);
                ddltype.Items.Add(new ListItem("Out-normal", "Out-normal"));
                ddltype.Items.Add(new ListItem("Out-scrap", "Out-scrap"));
                dvband();
            }
        }

        private void dvband()
        {
            str1 = str1 + "order by out_date desc limit 200 ";
            gvin.DataSource = MysqlHelper.ExecuteDataTable(str1);
            gvin.DataBind();
        }

        protected void btnOut_Click(object sender, EventArgs e)
        {
            str4 = string.Format(str4, Request.Form["pn1"].Trim(), int.Parse(Request.Form["txtqty1"].Trim()), ddltype.SelectedValue, txtreq.Text.Trim(), txtrem.Text.Trim(), Common.GetADUserEntity(Common.GetCurrentNTID()).DisplayName);
            str5 = string.Format(str5, int.Parse(Request.Form["txtqty1"].Trim()), Request.Form["pn1"].Trim());
            if (MysqlHelper.ExecuteNonQuery(str4) > 0)
            {
                if (MysqlHelper.ExecuteNonQuery(str5) > 0)
                {
                    Response.Write("<script>alert('成功！')</script>");
                    Response.Redirect("Out.aspx");
                }
            }
        }
    }//class
}