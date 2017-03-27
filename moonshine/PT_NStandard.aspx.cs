using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using moonshine.DAL;
using moonshine.Util;
using MySql.Data.MySqlClient;

namespace moonshine
{
    public partial class PT_NStandard : System.Web.UI.Page
    {
        string str1 = "SELECT ps_id, ps_name, ps_standard, ps_pic, ps_useArea ,v_amount FROM product_no_standard left join v_no_product_standard on ps_id=v_bs_name where 1=1 ";
        string str2 = "insert into product_no_standard(ps_id,ps_name,ps_standard,ps_pic, ps_useArea) values('{0}', '{1}','{2}','{3}','{4}')";
        string str3 = "SELECT ps_id FROM product_no_standard where  ps_id ='{0}' ";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //str1 = str1 + string.Format("and in_date ='{0}'", DateTime.Now);              
                dvband();
            }
        }
        private void dvband()
        {
            //Label l1 = (Label)PreviousPage.FindControl("lbUser");
            //Response.Write(l1.Text);
            str1 = str1 + "order by ps_id desc   ";
            gvin.DataSource = MysqlHelper.ExecuteDataTable(str1);
            gvin.DataBind();

        }


        //插入数据库
        protected void btninsertPTS_Click(object sender, EventArgs e)
        {

            if (!Common.isImage(FileUpload1))
            {
                Response.Write("<script>alert('请上传图片格式文件！')</script>");
                return;
            }

            if (!Common.IsFileSize(FileUpload1))
            {
                Response.Write("<script>alert('请上传小于1M的图片！')</script>");
                return;
            }

            if (!Common.UploadFile(FileUpload1, FileUpload1.FileName))
            {
                Response.Write("<script>alert('图片上传不成功！')</script>");
                return;
            }

            else
            {
                str2 = string.Format(str2, txt_id.Text.Trim(), txt_name.Text.Trim(), txtms.Text.Trim(), FileUpload1.FileName, txtuar.Text.Trim());
                if (MysqlHelper.ExecuteNonQuery(str2) > 0)
                {
                    Response.Write("<script>alert('成功！')</script>");
                    txt_id.Text = null;
                    txt_name.Text = null;
                    txtms.Text = null;
                    txtuar.Text = null;
                    dvband();
                }
                else
                {
                    return;
                }
            }
        }

        protected void txt_id_TextChanged(object sender, EventArgs e)
        {
            if (MysqlHelper.Exists(string.Format(str3, txt_id.Text.Trim())))
            {
                Response.Write("<script>alert('该料号已经存在！')</script>");
                txt_id.Text = null;
                return;
            }
        }

        protected void gvin_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvin.PageIndex = e.NewPageIndex;
            dvband();
        }

        protected void gvin_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string sqlcheck = "SELECT bs_name FROM bom_standard where bs_name ='{0}' ";//删除前判断BOM是否存在料，否则不给删除;
            string sqldel = "delete from product_no_standard where ps_id='{0}'";
            sqlcheck = string.Format(sqlcheck, gvin.Rows[e.RowIndex].Cells[0].Text);
            if (MysqlHelper.Exists(sqlcheck))
            {
                Response.Write("<script>alert('该标准品已经存在BOM，请先删除BOM！');document.location=document.location;</script>");//放在有write的下一句防止字体变大
                return;
            }
            else
            {
                sqldel = string.Format(sqldel, gvin.Rows[e.RowIndex].Cells[0].Text);
                if (MysqlHelper.ExecuteNonQuery(sqldel) > 0)
                {
                    Response.Write("<script>alert('成功！')</script>");
                    dvband();
                }
            }

        }

        protected void gvin_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvin.EditIndex = e.NewEditIndex;
            dvband();
        }

        protected void gvin_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvin.EditIndex = -1;
            dvband();
        }

        protected void gvin_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
                {
                    LinkButton lb = e.Row.FindControl("LinkButtonDelete") as LinkButton;
                    lb.Attributes.Add("onclick", "javascript:return confirm('你确认要删除标准品:[ " + e.Row.Cells[0].Text + e.Row.Cells[1].Text + " ]吗?')");
                    Label ll = e.Row.FindControl("LabelPhoto") as Label;
                    ll.Attributes.Add("onclick", "openphoto(" + string.Format("'{0}','{1}'", ll.Text, e.Row.Cells[0].Text) + ")");//弹窗 
                }
            }
        }

        protected void gvin_RowUpdating1(object sender, GridViewUpdateEventArgs e)
        {
            string sqlUpdate = "update product_no_standard set ps_name=@ps_name,ps_standard=@ps_standard,ps_useArea=@ps_useArea where ps_id=@ps_id";
            //string sqlUpdateBom = "update bom_standard set bs_name=@bs_name";
            Response.Write(str1);
            MySqlParameter[] p1 = new MySqlParameter[]
            {
                new MySqlParameter("@ps_id",gvin.Rows[e.RowIndex].Cells[0].Text),
                 new MySqlParameter("@ps_name",((TextBox)(gvin.Rows[e.RowIndex].Cells[1].Controls[0])).Text.Trim()),
                  new MySqlParameter("@ps_standard",((TextBox)(gvin.Rows[e.RowIndex].Cells[2].Controls[0])).Text.Trim()),
                   new MySqlParameter("@ps_useArea",((TextBox)(gvin.Rows[e.RowIndex].Cells[3].Controls[0])).Text.Trim())
            };
            if (MysqlHelper.ExecuteNonQuery(sqlUpdate,p1) > 0)
            {
                Response.Write("<script>alert('成功！');document.location=document.location;</script>");
                dvband();
            }

        }
    }//class
}