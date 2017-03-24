using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using moonshine.DAL;
using moonshine.Util;
namespace moonshine
{
    public partial class PT_Standard : System.Web.UI.Page
    {
        string str1 = "SELECT ps_id, ps_name, ps_standard, ps_pic, ps_useArea ,v_amount FROM product_standard left join v_product_standard on ps_id=v_bs_name where 1=1 ";
        string str2 = "insert into product_standard(ps_id,ps_name,ps_standard,ps_pic, ps_useArea) values('{0}', '{1}','{2}','{3}','{4}')";
        string str3 = "SELECT ps_id FROM product_standard where  ps_id ='{0}' ";
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

            if (!Common.isImage(FileUpload1)){
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
                str2 = string.Format(str2, txt_id.Text.Trim(), txt_name.Text.Trim(), txtms.Text.Trim(), FileUpload1.FileName,txtuar.Text.Trim());
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
    }
}