using System;
using System.Web.UI.WebControls;
using moonshine.DAL;
using moonshine.Util;

namespace moonshine
{
    public partial class In : System.Web.UI.Page
    {
        private string str1 = "SELECT in_id,description,spec,in_qty,type,reques_by as request_by,remark,in_date,operator FROM in_data left join basic_data on in_data.in_id=basic_data.basic_id where 1=1 ";

        //string str2 = "SELECT description,spec,current_qty FROM basic_data ";
        //string str3 = "SELECT basic_id FROM basic_data  ";
        private string str4 = "INSERT INTO  in_data(in_id, in_qty, type, reques_by, remark,operator) values('{0}',{1},'{2}','{3}','{4}','{5}');";

        private string str5 = "update basic_data set current_qty=current_qty+{0} where basic_id='{1}' ";//更新basic_data中current_qty数量

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //str1 = str1 + string.Format("and in_date ='{0}'", DateTime.Now);
                ddltype.Items.Add(new ListItem("In-normal", "In-normal"));
                ddltype.Items.Add(new ListItem("In-recycle", "In-recycle"));

                dvband();
            }
        }

        private void dvband()
        {
            str1 = str1 + "order by in_date desc limit 100 ";
            gvin.DataSource = MysqlHelper.ExecuteDataTable(str1);
            gvin.DataBind();
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            // Response.Write(txtspec.Text);
            str4 = string.Format(str4, Request.Form["pn1"].Trim(), int.Parse(Request.Form["txtqty"].Trim()), ddltype.SelectedValue, txtreq.Text.Trim(), txtrem.Text.Trim(), Common.GetADUserEntity(Common.GetCurrentNTID()).DisplayName);
            str5 = string.Format(str5, int.Parse(Request.Form["txtqty"].Trim()), Request.Form["pn1"].Trim());
            if (MysqlHelper.ExecuteNonQuery(str4) > 0)
            {
                if (MysqlHelper.ExecuteNonQuery(str5) > 0)
                {
                    Response.Write("<script>alert('成功！')</script>");
                    Response.Redirect("In.aspx");
                    //dvband();
                }
            }
        }
    }
}