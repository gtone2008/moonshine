using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using moonshine.DAL;
using moonshine.Util;
namespace moonshine
{
    public partial class Base_data : System.Web.UI.Page
    {
        public string str1 = "select basic_id,description,spec,price,LowLimit,UpperLimit,location,current_qty,photo from basic_data where 1=1 ";
        string str2 = "INSERT INTO basic_data(basic_id, description, spec, price, LowLimit, UpperLimit, location, photo, current_qty) VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}');";
        //string str3 = "select basic_id from basic_data where basic_id ='{0}'";
        string str4 = "delete from basic_data where basic_id ='{0}'";
        string str5 = "update basic_data set description='{0}',spec='{1}',price='{2}',LowLimit='{3}',UpperLimit='{4}',location='{5}',current_qty='{6}' where basic_id='{7}'";
        string str6 = "SELECT basic_id FROM bom_standard where basic_id ='{0}' ";//删除前判断BOM中是否存在此料，否则不给删除;
        string str7 = "INSERT INTO price_change(basic_id, price,change_user,remark)VALUES('{0}','{1}','{2}','{3}')";//单价更改记录
        //string price="";
        //string remark="";
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Write("<script>alert('"+ System.IO.Path.GetFileName(Request.Path).ToString()+ "')</script>");
            if (txtbase.Text != "")
            {
                str1 = str1 + " and basic_id like '%" + txtbase.Text.Trim() + "%'";
            }

            if (txtdesc.Text != "")
            {
                str1 = str1 + " and description like '%" + txtdesc.Text.Trim() + "%'";
            }           
            if (!IsPostBack)
            {             
                dvband(str1);
                
                //gvbase.Attributes.Add("style", "word-break:break-all;word-wrap:break-word");//自动换行
                //this.Form.Attributes.Add("defaultbutton", "buttonxxx");
            }
        }
        private void dvband(string  str)
        {
            str1 = str1 + " order by description desc";
            gvbase.DataSource = MysqlHelper.ExecuteDataTable(str1);
            gvbase.DataBind();

        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
           
            dvband(str1);
        }

        protected void gvbase_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvbase.PageIndex = e.NewPageIndex;
            dvband(str1);

        }



        protected void btnInsert_Click(object sender, EventArgs e)
        {
            string img1 = FileUpload1.FileName;
            if (txtlow.Text.Trim() == "") txtlow.Text = "0";
            if (txtupper.Text.Trim() == "") txtlow.Text = "0";
            if (FileUpload1.FileName != "")
            {
                if (!Common.UploadFile(FileUpload1, FileUpload1.FileName))
                {
                    Response.Write("<script>alert('图片上传不成功！')</script>");
                    return;
                }
            }
            else
            {
                img1 = ".jpg";
            }
            str2 = string.Format(str2, Request.Form["pn1"].Trim(), txtdescription.Text.Trim(), txtspec.Text.Trim(), decimal.Parse(txtprice.Text.Trim()), int.Parse(txtlow.Text.Trim()), int.Parse(txtupper.Text.Trim()), txtloc.Text.Trim(), img1, int.Parse(txtcqty.Text.Trim()));
                if (MysqlHelper.ExecuteNonQuery(str2) > 0)
                {

                    Response.Write("<script>alert('成功！')</script>");
                    Response.Redirect("Base_data.aspx");//防止重复提交
                }
                else
                {
                Response.Write("<script>alert('失败！')</script>");
                return;
                }         
        }



        protected void gvbase_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
                {
                    LinkButton lb = e.Row.FindControl("LinkButton1") as LinkButton;
                    lb.Attributes.Add("onclick", "javascript:return confirm('你确认要删除[ " + e.Row.Cells[0].Text + e.Row.Cells[1].Text + " ]吗?')");
                    HyperLink h1 = e.Row.FindControl("h1") as HyperLink;
                    h1.Attributes.Add("onclick", "openwin('"+ e.Row.Cells[0].Text + "')");//弹窗
                    h1.NavigateUrl = "#";
                    Label labl1= e.Row.Cells[8].FindControl("Label1") as Label;
                    labl1.Attributes.Add("onclick", "openphoto(" + string.Format("'{0}','{1}'", ((Label)e.Row.Cells[8].FindControl("Label1")).Text, e.Row.Cells[0].Text) + ")");//弹窗
                    //e.Row.Attributes.Add("onclick", "javascript:$('#imgPic1').attr('src','"+ ((Label)e.Row.Cells[8].FindControl("Label1")).Text + "')");
                    //int low = int.Parse((e.Row.Cells[4].Text));
                    //int qty = int.Parse((e.Row.Cells[7].Text));
                    //if(qty<=low)
                    //{
                    //    e.Row.Style.Add("background", "red");
                    //}
                    //e.Row.Attributes.Add("onclick", "alert('" + ((Label)e.Row.Cells[8].FindControl("Label1")).Text + "')");//弹窗
                    //e.Row.Attributes.Add("onclick", "alert('" + e.Row.Cells[4].Text + "')");//弹窗
                    //e.Row.Attributes.Add("onclick", "javascript:$('#imgPic1').attr('src','/uploads/ME003335气管.JPG')");
                    //e.Row.Attributes.Add("onclick", "javascript:$('#imgPic1').css({'src':'"+ ((Label)e.Row.Cells[8].FindControl("Label1")).Text + "','display':'block'})");
                    //e.Row.Attributes.Add("onmouseout", "javascript:$('#imgPic1').css({'display':'none'})");
                    //e.Row.Attributes.Add("onMouseOver", string.Format("javascript:{0}.css{1}", "$('#aa1')", "({'display':'true'})"));
                    //e.Row.Attributes.Add("onclick", "javascript:$('#aa1').css({'display':'true'})");

                }
            }
        }

        protected void gvbase_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            str6 = string.Format(str6, gvbase.Rows[e.RowIndex].Cells[0].Text);
            if (MysqlHelper.Exists(str6))
            {
                Response.Write("<script>alert('该料在BOM中用到，请检查BOM！')</script>");
                return;
            }
            else
            {
                str4 = string.Format(str4, gvbase.Rows[e.RowIndex].Cells[0].Text);
                if (MysqlHelper.ExecuteNonQuery(str4) > 0)
                {
                    Response.Write("<script>alert('成功！')</script>");
                    dvband(str1);
                }
            }
        }


        protected void gvbase_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvbase.EditIndex = e.NewEditIndex;
            gvbase.Columns[3].HeaderText = "price|remark";
            gvbase.Columns[8].Visible = false;//把图片隐藏了，占地方
            dvband(str1);
        }

        protected void gvbase_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            
            string str1 = ((TextBox)(gvbase.Rows[e.RowIndex].Cells[1].Controls[0])).Text.Trim();
            string str2 = ((TextBox)(gvbase.Rows[e.RowIndex].Cells[2].Controls[0])).Text.Trim();
            //decimal dc3 = decimal.Parse((gvbase.Rows[e.RowIndex].Cells[3].Controls[0]).ToString());
            TextBox t3 = gvbase.Rows[e.RowIndex].FindControl("TextBox1") as TextBox;
            TextBox t31 = gvbase.Rows[e.RowIndex].FindControl("TextBox2") as TextBox;//价格改变的备注
            //Response.Write("<script>alert('" + t3.Text + "')</script>");
            //Response.Write("<script>alert('" + price + "')</script>");
            decimal dc3 = decimal.Parse(t3.Text);
            int int4 = int.Parse(((TextBox)(gvbase.Rows[e.RowIndex].Cells[4].Controls[0])).Text.Trim());
            int int5 = int.Parse(((TextBox)(gvbase.Rows[e.RowIndex].Cells[5].Controls[0])).Text.Trim());
            string str6 = ((TextBox)(gvbase.Rows[e.RowIndex].Cells[6].Controls[0])).Text.Trim();
            int int7 = int.Parse(((TextBox)(gvbase.Rows[e.RowIndex].Cells[7].Controls[0])).Text.Trim());
            //string str8 = gvbase.Rows[e.RowIndex].Cells[0].Text;//basic_id(已经改为可编辑模式)
            //string str8 = ((TextBox)(gvbase.Rows[e.RowIndex].Cells[0].Controls[0])).Text.Trim();//可编辑模式
            string str8 = gvbase.DataKeys[e.RowIndex].Values["basic_id"].ToString();//老的值
            str5 = string.Format(str5, str1, str2, dc3, int4, int5, str6, int7, str8);
            if (MysqlHelper.ExecuteNonQuery(str5) > 0)
            {

                Response.Write("<script>alert('成功！')</script>");
                if (e.NewValues["basic_id"].ToString().Trim() != gvbase.DataKeys[e.RowIndex].Values[0].ToString())//basic_id改变
                {

                    string sql1 = string.Format("update basic_data set basic_id='{0}' where basic_id='{1}'", e.NewValues["basic_id"].ToString().Trim(), gvbase.DataKeys[e.RowIndex].Values[0].ToString());
                    string sql2 = string.Format("update bom_standard set basic_id='{0}' where basic_id='{1}'", e.NewValues["basic_id"].ToString().Trim(), gvbase.DataKeys[e.RowIndex].Values[0].ToString());
                    string sql3  = string.Format("update in_data set in_id='{0}' where in_id='{1}'", e.NewValues["basic_id"].ToString().Trim(), gvbase.DataKeys[e.RowIndex].Values[0].ToString());
                    string sql4 = string.Format("update out_data set out_id='{0}' where out_id='{1}'", e.NewValues["basic_id"].ToString().Trim(), gvbase.DataKeys[e.RowIndex].Values[0].ToString());
                    string sql5 = string.Format("update price_change set basic_id='{0}' where basic_id='{1}'", e.NewValues["basic_id"].ToString().Trim(), gvbase.DataKeys[e.RowIndex].Values[0].ToString());
                   
                    MysqlHelper.ExecuteNonQuery(sql1);
                    MysqlHelper.ExecuteNonQuery(sql2);
                    MysqlHelper.ExecuteNonQuery(sql3);
                    MysqlHelper.ExecuteNonQuery(sql4);
                    MysqlHelper.ExecuteNonQuery(sql5);
                    Response.Write("<script>alert('PN号更改成功！')</script>");
                }
                //if(price!="")//如果值有改变就插入记录
                //Response.Write("<script>alert('" + price + "')</script>");

                //Response.Write("<script>alert('" + e.NewValues["price"] + "')</script>");
                // Response.Write("<script>alert('" + e.OldValues["price"] + "')</script>");
                //Response.Write("<script>alert('" + gvbase.DataKeys[e.RowIndex].Values[1] + "')</script>");
                if (e.NewValues["price"].ToString() != gvbase.DataKeys[e.RowIndex].Values[1].ToString())//价格改变
                {

                    //decimal pr3 = 0; //decimal.Parse(price);
                    string change_user = Session["userName"].ToString();    
                    str7 = string.Format(str7, str8, dc3, change_user, t31.Text.Trim());
                    //Response.Write("<script>alert('" + str7 + "')</script>");
                    Response.Write("<script>alert('价格更改成功！')</script>");
                    MysqlHelper.ExecuteNonQuery(str7);
                }
                
                gvbase.EditIndex = -1;
                gvbase.Columns[3].HeaderText = "price";
                gvbase.Columns[8].Visible = true;//把图片隐藏了，占地方
                dvband(str1);
               
                //Response.Redirect("Base_data.aspx");//防止重复提交
            }
        }

        protected void gvbase_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvbase.EditIndex = -1;
            gvbase.Columns[3].HeaderText = "price";
            gvbase.Columns[8].Visible = true;//把图片隐藏了，占地方
            dvband(str1);
        }

        protected void checklow_CheckedChanged(object sender, EventArgs e)
        {
            if (checklow.Checked)
            {
                str1 = str1 + " and LowLimit>=current_qty ";
                
                dvband(str1);
            }
            else
            {
                dvband(str1);
            }
        }

        //protected void TextBox1_TextChanged(object sender, EventArgs e)
        //{
        //    //GridViewUpdateEventArgs e =gvbase.
        //    //string change_user = Session["userName"].ToString();
        //    price = ((TextBox)sender).Text;
        //    //Response.Write("<script>alert('" + price + "')</script>");
        //}

        //protected void TextBox2_TextChanged(object sender, EventArgs e)
        //{
        //    remark = ((TextBox)sender).Text;
        //}








    }
}