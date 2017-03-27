using moonshine.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using moonshine.Util;

namespace moonshine
{
    public partial class photo : System.Web.UI.Page
    {
        string url1, sql1;
        // MySqlDataReader mysdr;

        protected void btnIMG_Click(object sender, EventArgs e)
        {
            if (url1 == "Base_data")
            {

                if (FileUpload1.FileName != "")
                {
                    if (!Common.UploadFile(FileUpload1, FileUpload1.FileName))
                    {
                        Response.Write("<script>alert('图片上传不成功！')</script>");
                        return;
                    }
                    else
                    {
                        sql1 = string.Format("update basic_data set photo='{0}' where basic_id='{1}'", FileUpload1.FileName, Request.QueryString["bid"]);
                        if (MysqlHelper.ExecuteNonQuery(sql1) > 0)
                            ClientScript.RegisterStartupScript(ClientScript.GetType(), "my1", "<script>refreshOpener();</script>");
                    }
                }
                else
                {
                    Response.Write("<script>alert('上传图片不能为空!')</script>");
                    return;
                }
            }

            if (url1 == "PT_Standard")
            {

                if (FileUpload1.FileName != "")
                {
                    if (!Common.UploadFile(FileUpload1, FileUpload1.FileName))
                    {
                        Response.Write("<script>alert('图片上传不成功！')</script>");
                        return;
                    }
                    else
                    {
                        sql1 = string.Format("update product_standard set ps_pic='{0}' where ps_id='{1}'", FileUpload1.FileName, Request.QueryString["bid"]);
                        if (MysqlHelper.ExecuteNonQuery(sql1) > 0)
                         ClientScript.RegisterStartupScript(ClientScript.GetType(), "", "<script>refreshOpener();</script>");
                        //Response.Redirect("photo.aspx?url1=PT_Standard&bid=" + Request.QueryString["bid"] + "&photo=" + FileUpload1.FileName);
                    }
                }
                else
                {
                    Response.Write("<script>alert('上传图片不能为空!')</script>");
                    return;
                }
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {

            //gvband(Request.QueryString["bid"]);
            url1 = Request.QueryString["url1"];
            //id1 = Request.QueryString["id"];
            // sql1 = "";
            //Response.Write("<script>alert('" + Request.QueryString["bid"] + "')</script>");

        }






    }//
}