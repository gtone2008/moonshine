using System;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using moonshine.DAL;
using moonshine.Util;
using MySql.Data.MySqlClient;

namespace moonshine
{
    public partial class Base_data : System.Web.UI.Page
    {
        private string str1 = @"select basic_id,description,spec,price,LowLimit,UpperLimit,location,current_qty,photo from basic_data where 1=1 ";
        private string str2 = "INSERT INTO basic_data(basic_id, description, spec, price, LowLimit, UpperLimit, location, photo, current_qty) VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}');";

        //string str3 = "select basic_id from basic_data where basic_id ='{0}'";
        private string str4 = "delete from basic_data where basic_id ='{0}'";

        private string str5 = "update basic_data set description='{0}',spec='{1}',price='{2}',LowLimit='{3}',UpperLimit='{4}',location='{5}' where basic_id='{6}'";
        private string str6 = "SELECT basic_id FROM bom_standard where basic_id ='{0}' ";//删除前判断BOM中是否存在此料，否则不给删除;
        private string str7 = "INSERT INTO price_change(basic_id, price,change_user,remark)VALUES('{0}','{1}','{2}','{3}')";//单价更改记录

        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Write("<script>alert('"+ System.IO.Path.GetFileName(Request.Path).ToString()+ "')</script>");

            if (!IsPostBack)
            {
                dvband(str1);
                getTotal();
            }
        }

        private void getTotal()
        {
            string sql = "SELECT count(*),sum(price*current_qty) FROM basic_data ";
            MySqlDataReader mySdr = MysqlHelper.ExecuteReader(sql);
            while (mySdr.Read())
            {
                this.lbTotal.Text = "共计 " + mySdr[0].ToString() + " 种物料 总金额 " + mySdr[1].ToString() + " 元";
            }
            mySdr.Close();
        }

        private void dvband(string str)
        {
            str = str + " order by description desc";
            gvbase.DataSource = MysqlHelper.ExecuteDataTable(str);
            gvbase.DataBind();
        }

        private void txtInput()
        {
            dvband(sqlSplice(str1));
        }

        private string sqlSplice(string sql)
        {
            if (txtbase.Text != "")
            {
                sql = sql + " and basic_id like '%" + txtbase.Text.Trim() + "%'";
            }

            if (txtdesc.Text != "")
            {
                sql = sql + " and description like '%" + txtdesc.Text.Trim() + "%'";
            }
            if (checklow.Checked)
            {
                sql = sql + " and LowLimit>=current_qty ";
            }
            return sql;
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            txtInput();
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
                Response.Redirect("Base_data.aspx");
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
                    h1.Attributes.Add("onclick", "openwin('" + e.Row.Cells[0].Text + "')");//弹窗
                    h1.NavigateUrl = "#";
                    Label labl1 = e.Row.Cells[8].FindControl("Label1") as Label;
                    labl1.Attributes.Add("onclick", "openphoto(" + string.Format("'{0}','{1}'", ((Label)e.Row.Cells[8].FindControl("Label1")).Text, e.Row.Cells[0].Text) + ")");//弹窗
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
                    txtInput();
                }
            }
        }

        protected void gvbase_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvbase.EditIndex = e.NewEditIndex;
            gvbase.Columns[3].HeaderText = "price|remark";
            gvbase.Columns[8].Visible = false;//把图片隐藏了，占地方
            txtInput();
        }

        protected void gvbase_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string str1 = ((TextBox)(gvbase.Rows[e.RowIndex].Cells[1].Controls[0])).Text.Trim();
            string str2 = ((TextBox)(gvbase.Rows[e.RowIndex].Cells[2].Controls[0])).Text.Trim();
            TextBox t3 = gvbase.Rows[e.RowIndex].FindControl("TextBox1") as TextBox;
            TextBox t31 = gvbase.Rows[e.RowIndex].FindControl("TextBox2") as TextBox;//价格改变的备注

            decimal dc3 = decimal.Parse(t3.Text);
            int int4 = int.Parse(((TextBox)(gvbase.Rows[e.RowIndex].Cells[4].Controls[0])).Text.Trim());
            int int5 = int.Parse(((TextBox)(gvbase.Rows[e.RowIndex].Cells[5].Controls[0])).Text.Trim());
            string str6 = ((TextBox)(gvbase.Rows[e.RowIndex].Cells[6].Controls[0])).Text.Trim();
            //int int7 = int.Parse(((TextBox)(gvbase.Rows[e.RowIndex].Cells[7].Controls[0])).Text.Trim());//

            string str8 = gvbase.DataKeys[e.RowIndex].Values["basic_id"].ToString();//老的值
            str5 = string.Format(str5, str1, str2, dc3, int4, int5, str6, str8); //current_qty不再要求修改
            if (MysqlHelper.ExecuteNonQuery(str5) > 0)
            {
                Response.Write("<script>alert('成功！')</script>");
                if (e.NewValues["basic_id"].ToString().Trim() != gvbase.DataKeys[e.RowIndex].Values[0].ToString())//basic_id改变
                {
                    string sql1 = string.Format("update basic_data set basic_id='{0}' where basic_id='{1}'", e.NewValues["basic_id"].ToString().Trim(), gvbase.DataKeys[e.RowIndex].Values[0].ToString());
                    string sql2 = string.Format("update bom_standard set basic_id='{0}' where basic_id='{1}'", e.NewValues["basic_id"].ToString().Trim(), gvbase.DataKeys[e.RowIndex].Values[0].ToString());
                    string sql3 = string.Format("update in_data set in_id='{0}' where in_id='{1}'", e.NewValues["basic_id"].ToString().Trim(), gvbase.DataKeys[e.RowIndex].Values[0].ToString());
                    string sql4 = string.Format("update out_data set out_id='{0}' where out_id='{1}'", e.NewValues["basic_id"].ToString().Trim(), gvbase.DataKeys[e.RowIndex].Values[0].ToString());
                    string sql5 = string.Format("update price_change set basic_id='{0}' where basic_id='{1}'", e.NewValues["basic_id"].ToString().Trim(), gvbase.DataKeys[e.RowIndex].Values[0].ToString());

                    MysqlHelper.ExecuteNonQuery(sql1);
                    MysqlHelper.ExecuteNonQuery(sql2);
                    MysqlHelper.ExecuteNonQuery(sql3);
                    MysqlHelper.ExecuteNonQuery(sql4);
                    MysqlHelper.ExecuteNonQuery(sql5);
                    Response.Write("<script>alert('PN号更改成功！')</script>");
                }

                if (e.NewValues["price"].ToString() != gvbase.DataKeys[e.RowIndex].Values[1].ToString())//价格改变
                {
                    string change_user = Session["userName"].ToString();
                    str7 = string.Format(str7, str8, dc3, change_user, t31.Text.Trim());

                    Response.Write("<script>alert('价格更改成功！')</script>");
                    MysqlHelper.ExecuteNonQuery(str7);
                }

                gvbase.EditIndex = -1;
                gvbase.Columns[3].HeaderText = "price";
                gvbase.Columns[8].Visible = true;//
                txtInput();

                Response.Write("<script>document.location=document.location;</script>");
            }
        }

        protected void gvbase_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvbase.EditIndex = -1;
            gvbase.Columns[3].HeaderText = "price";
            gvbase.Columns[8].Visible = true;//
            txtInput();
        }

        protected void checklow_CheckedChanged(object sender, EventArgs e)
        {
            txtInput();
        }

        private void ExportDataTable(DataTable grd)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=basedata " + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls");
            HttpContext.Current.Response.Write("<table border=1><tr bgcolor=#AAAAAA >");

            foreach (DataColumn item in grd.Columns)
            {
                HttpContext.Current.Response.Write("<th>" + item.ColumnName + "</th>");
            }

            HttpContext.Current.Response.Write("</tr>");

            foreach (DataRow row in grd.Rows)
            {
                string htmltable = "<tr>";
                for (int i = 0; i < grd.Columns.Count; i++)
                {
                    htmltable += "<td style='vnd.ms-excel.numberformat:@'>" + row[i].ToString() + "</td>";
                }
                htmltable += "</tr>";
                HttpContext.Current.Response.Write(htmltable);
            }
            HttpContext.Current.Response.Write("</table>");
            HttpContext.Current.Response.End();
        }

        protected void toExcel_Click(object sender, EventArgs e)
        {
            DataTable dt = MysqlHelper.ExecuteDataTable(sqlSplice(str1));
            ExportDataTable(dt);
        }
    }
}