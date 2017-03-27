using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using moonshine.DAL;
namespace moonshine
{
    public partial class bom_Nstandard : System.Web.UI.Page
    {
        string str1 = "SELECT bom_no_standard.bs_name, bom_no_standard.basic_id, description,bom_no_standard.bs_qty,basic_data.price,bs_qty * price as amount from bom_no_standard left join basic_data on bom_no_standard.basic_id=basic_data.basic_id ";
        string str11 = "insert into bom_no_standard(bs_name, basic_id, bs_qty) values ('{0}','{1}',{2})";//加入BOM
        string str2 = "SELECT description,spec FROM basic_data where 1=1 and basic_id='{0}' ";// //下拉菜单PN号取描述
        string str21 = "SELECT ps_name FROM product_no_standard where 1=1 and ps_id='{0}' ";// //下拉菜单Item号取name
        string str3 = "SELECT basic_id FROM basic_data  "; //下拉菜单PN号
        string str4 = "SELECT ps_id FROM product_no_standard  "; //下拉菜单item号
        string str5 = "delete from bom_no_standard ";
        private decimal sum = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //str1 = str1 + string.Format("and in_date ='{0}'", DateTime.Now);
                ddlimdband();
                ddlimd0pband();
                ddlimd.Items.Insert(0, new ListItem("", ""));
                ddlimd0.Items.Insert(0, new ListItem("", ""));
                //str1 = str1 + "order by in_date desc limit 20 ";
                //dvband();
            }
        }

        //gv取数据
        private void dvband(string ss)
        {
            str1 = str1+string.Format("where bs_name ='{0}' ",ss);
            gvbom1.DataSource=MysqlHelper.ExecuteDataTable(str1);
            gvbom1.DataBind();
        }

        //下拉菜单PN号
        private void ddlimd0pband() {
            ddlimd0.DataSource = MysqlHelper.ExecuteDataTable(str3).DefaultView;
            ddlimd0.DataTextField = "basic_id";
            ddlimd0.DataValueField = "basic_id";
            ddlimd0.DataBind();
        }

        //下拉菜单item号
        private void ddlimdband()
        {
            ddlimd.DataSource = MysqlHelper.ExecuteDataTable(str4).DefaultView;
            ddlimd.DataTextField = "ps_id";
            ddlimd.DataValueField = "ps_id";
            ddlimd.DataBind();
        }


        //点击一下加入BOM，然后GV展示
        protected void btnInsert_Click(object sender, EventArgs e)
        {
            //Response.Write(txtspec.Text);
            str11 = string.Format(str11, ddlimd.SelectedValue, ddlimd0.SelectedValue,int.Parse(txtqty.Text.Trim()));
            if (MysqlHelper.ExecuteNonQuery(str11) > 0)
            {
                dvband(ddlimd.SelectedValue);
                Response.Write("<script>alert('成功！')</script>");
                //txtname.Text = null;
                ddlimd0.Text = null;
                txtdesc.Text = null;
                txtspec.Text = null;
                txtqty.Text = null;
                
            }
            else
            {
                return;
            }
        }

        //下拉菜单Item号
        protected void ddlimd_SelectedIndexChanged(object sender, EventArgs e)
        {
            str21 =string.Format(str21, ddlimd.SelectedValue);
            if (ddlimd.SelectedValue != "")
            {
                DataTable dt21 = MysqlHelper.ExecuteDataTable(str21);
                if (dt21.Rows.Count > 0)
                {
                    txtname.Text = dt21.Rows[0]["ps_name"].ToString();
                    dvband(ddlimd.SelectedValue);
                    ddlimd0.Focus();
                }
            }
            else {
                ddlimd0.Text = null;
                txtname.Text = null;
                txtdesc.Text = null;
                txtspec.Text = null;
                txtqty.Text = null;
            }

        }
        //下拉菜单PN号
        protected void ddlimd0_SelectedIndexChanged(object sender, EventArgs e)
        {
            str2 = string.Format(str2,ddlimd0.SelectedValue);
            DataTable dt2 = MysqlHelper.ExecuteDataTable(str2);
            if (dt2.Rows.Count > 0)
            {
                txtdesc.Text = dt2.Rows[0]["description"].ToString();
                txtspec.Text = dt2.Rows[0]["spec"].ToString();
            }
            txtqty.Focus();
        }

        //金额汇总
        protected void gvbom1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                sum += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "amount"));

                if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
                {
                    LinkButton lb = e.Row.FindControl("LinkButton1") as LinkButton;
                    lb.Attributes.Add("onclick", "javascript:return confirm('你确认要删除\"" + e.Row.Cells[2].Text + "\"吗?')");
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[5].Text = "合计：";
                e.Row.Cells[6].Text = sum.ToString();
            }

 
        }

        //BOM中删除
        protected void gvbom1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //str5 = str5 + string.Format("where bs_name='{0}' and basic_id='{1}'", ((TextBox)(gvbom1.Rows[e.RowIndex].Cells[1].Controls[0])).Text.ToString(), gvbom1.DataKeys[e.RowIndex].Value.ToString());
            str5 = str5 + string.Format("where bs_name='{0}' and basic_id='{1}'", gvbom1.Rows[e.RowIndex].Cells[1].Text, gvbom1.DataKeys[e.RowIndex].Value.ToString());
            if (MysqlHelper.ExecuteNonQuery(str5) > 0)
            {
              Response.Write("<script>alert('成功！')</script>");
              dvband(ddlimd.SelectedValue);
            }
        }
    }
}