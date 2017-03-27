using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MySql.Data.MySqlClient;
using moonshine.DAL;
using System.Collections;

namespace moonshine
{
    public partial class ShoppingCart : Page
    {

        Hashtable ht;
        DataTable dt0, dt, dt1;
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Write(Request.CurrentExecutionFilePath.Substring(1));
            //Response.Write(HttpContext.Current.Request.Url);
            //if (Session["shopcar"] != null)
            //{
            //    Hashtable ht = (Hashtable)Session["shopcar"];

            //    foreach (object item in ht.Keys)
            //    {
            //        Response.Write(item + ht[item].ToString() + "|");
            //    }
            //}

            //ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script>MyFun1();</script>");

            if (Session["shopcar_base"] != null)
            {
                if (dt0 == null)
                { creattb0(); }
                if (!IsPostBack)
                { gvband0(); }
            }
            if (Session["shopcar"] != null)
            {
                if (dt == null)
                { creattb(); }
                if (!IsPostBack)
                { gvband1(); }
            }
            if (Session["Nshopcar"] != null)
            {
                if (dt1 == null)
                { creattb1(); }
                if (!IsPostBack)
                { gvband2(); }
            }

        }
        protected void creattb0()
        {
            dt0 = new DataTable();
            DataColumn col = new DataColumn();
            col.ColumnName = "type";
            col.DataType = System.Type.GetType("System.String");
            dt0.Columns.Add(col);
            col = new DataColumn();
            col.ColumnName = "ps_id";
            col.DataType = System.Type.GetType("System.String");
            dt0.Columns.Add(col);
            col = new DataColumn();
            col.ColumnName = "ps_name";
            col.DataType = System.Type.GetType("System.String");
            dt0.Columns.Add(col);
            col = new DataColumn();
            col.ColumnName = "count";
            col.DataType = System.Type.GetType("System.Int32");
            dt0.Columns.Add(col);
            col = new DataColumn();
            col.ColumnName = "price";
            col.DataType = System.Type.GetType("System.String");
            dt0.Columns.Add(col);
            col = new DataColumn();
            col.ColumnName = "Total";
            col.DataType = System.Type.GetType("System.Decimal");
            dt0.Columns.Add(col);
        }
        protected void creattb()
        {
            dt = new DataTable();
            DataColumn col = new DataColumn();
            col.ColumnName = "type";
            col.DataType = System.Type.GetType("System.String");
            dt.Columns.Add(col);
            col = new DataColumn();
            col.ColumnName = "ps_id";
            col.DataType = System.Type.GetType("System.String");
            dt.Columns.Add(col);
            col = new DataColumn();
            col.ColumnName = "ps_name";
            col.DataType = System.Type.GetType("System.String");
            dt.Columns.Add(col);
            col = new DataColumn();
            col.ColumnName = "count";
            col.DataType = System.Type.GetType("System.Int32");
            dt.Columns.Add(col);
            col = new DataColumn();
            col.ColumnName = "price";
            col.DataType = System.Type.GetType("System.String");
            dt.Columns.Add(col);
            col = new DataColumn();
            col.ColumnName = "Total";
            col.DataType = System.Type.GetType("System.Decimal");
            dt.Columns.Add(col);
        }
        protected void creattb1()
        {
            dt1 = new DataTable();
            DataColumn col = new DataColumn();
            col.ColumnName = "type";
            col.DataType = System.Type.GetType("System.String");
            dt1.Columns.Add(col);
            col = new DataColumn();
            col.ColumnName = "ps_id";
            col.DataType = System.Type.GetType("System.String");
            dt1.Columns.Add(col);
            col = new DataColumn();
            col.ColumnName = "ps_name";
            col.DataType = System.Type.GetType("System.String");
            dt1.Columns.Add(col);
            col = new DataColumn();
            col.ColumnName = "count";
            col.DataType = System.Type.GetType("System.Int32");
            dt1.Columns.Add(col);
            col = new DataColumn();
            col.ColumnName = "price";
            col.DataType = System.Type.GetType("System.String");
            dt1.Columns.Add(col);
            col = new DataColumn();
            col.ColumnName = "Total";
            col.DataType = System.Type.GetType("System.Decimal");
            dt1.Columns.Add(col);
        }
        protected void gvband0()
        {
            MySqlDataReader mysdr;
            if (Session["shopcar_base"] != null)
            {

                ht = (Hashtable)Session["shopcar_base"];
                foreach (object item in ht.Keys)
                {
                    string str1 = "SELECT  description, price FROM basic_data where basic_id='{0}' ";
                    //Response.Write(item);
                    str1 = string.Format(str1, item);
                    mysdr = MysqlHelper.ExecuteReader(str1);
                    if (mysdr.Read())
                    {

                        DataRow row = dt0.NewRow();
                        row["type"] = "常规物料";
                        row["ps_id"] = item;
                        row["ps_name"] = mysdr.GetValue(0);
                        row["price"] = mysdr.GetValue(1);
                        try
                        {
                            row["count"] = int.Parse(ht[item].ToString());
                            row["total"] = int.Parse(ht[item].ToString()) * (Decimal.Parse(mysdr[1].ToString()));
                        }
                        catch
                        {
                            row["count"] = 0;
                            row["total"] = 0;
                            return;
                        }

                        dt0.Rows.Add(row);
                    }
                    mysdr.Close();

                }
                gvshoping0.DataSource = dt0.DefaultView;
                gvshoping0.DataBind();
                Session["dt0"] = dt0;
            }

        }

        protected void gvband1()
        {
            MySqlDataReader mysdr;
            if (Session["shopcar"] != null)
            {
                ht = (Hashtable)Session["shopcar"];
                foreach (object item in ht.Keys)
                {
                    string str1 = "SELECT  ps_name, v_amount FROM product_standard join v_product_standard on ps_id=v_bs_name where ps_id='{0}' ";
                    //Response.Write(item);
                    str1 = string.Format(str1, item);
                    mysdr = MysqlHelper.ExecuteReader(str1);
                    if (mysdr.Read())
                    {

                        DataRow row = dt.NewRow();
                        row["type"] = "标准产品";
                        row["ps_id"] = item;
                        row["ps_name"] = mysdr.GetValue(0);
                        row["price"] = mysdr.GetValue(1);
                        try
                        {
                            row["count"] = int.Parse(ht[item].ToString());
                            row["total"] = int.Parse(ht[item].ToString()) * (Decimal.Parse(mysdr[1].ToString()));
                        }
                        catch
                        {
                            //Response.Write("<script>alert('请输入整数！')</script>");
                            return;
                        }

                        dt.Rows.Add(row);
                    }
                    mysdr.Close();
                }
                gvshoping1.DataSource = dt.DefaultView;
                gvshoping1.DataBind();
                Session["dt"] = dt;
            }

        }
        protected void gvband2()
        {
            MySqlDataReader mysdr;
            if (Session["Nshopcar"] != null)
            {
                ht = (Hashtable)Session["Nshopcar"];
                foreach (object item in ht.Keys)
                {
                    string str1 = "SELECT  ps_name, v_amount FROM product_no_standard join v_no_product_standard on ps_id=v_bs_name where ps_id='{0}' ";
                    //Response.Write(item);
                    str1 = string.Format(str1, item);
                    mysdr = MysqlHelper.ExecuteReader(str1);
                    if (mysdr.Read())
                    {

                        DataRow row = dt1.NewRow();
                        row["type"] = "定制产品";
                        row["ps_id"] = item;
                        row["ps_name"] = mysdr.GetValue(0);
                        row["price"] = mysdr.GetValue(1);
                        try
                        {
                            row["count"] = int.Parse(ht[item].ToString());
                            row["total"] = int.Parse(ht[item].ToString()) * (Decimal.Parse(mysdr[1].ToString()));
                        }
                        catch
                        {
                            //Response.Write("<script>alert('请输入整数！')</script>");
                            return;
                        }

                        dt1.Rows.Add(row);
                    }
                    mysdr.Close();
                }
                gvshoping2.DataSource = dt1.DefaultView;
                gvshoping2.DataBind();
                Session["dt1"] = dt1;
            }

        }
        protected void textbox0_TextChanged(object sender, EventArgs e)
        {

            Hashtable ht = (Hashtable)Session["shopcar_base"];
            if (ht == null) return;
            for (int i = 0; i < gvshoping0.Rows.Count; i++)
            {
                string id = gvshoping0.Rows[i].Cells[0].Text.ToString();
                //Response.Write(id);
                string num = ((TextBox)gvshoping0.Rows[i].FindControl("textbox0")).Text;
                try
                {
                    Convert.ToInt16(num);
                }
                catch
                {
                    num = "0";
                }
                //Response.Write("  " + num + "<br />");
                ht[id] = num;
            }
            Session["shopcar_base"] = ht;
            gvband0();

        }
        protected void textbox1_TextChanged(object sender, EventArgs e)
        {

            Hashtable ht = (Hashtable)Session["shopcar"];
            if (ht == null) return;
            for (int i = 0; i < gvshoping1.Rows.Count; i++)
            {
                string id = gvshoping1.Rows[i].Cells[0].Text.ToString();
                //Response.Write(id);
                string num = ((TextBox)gvshoping1.Rows[i].FindControl("textbox1")).Text;
                try
                {
                    Convert.ToInt16(num);
                }
                catch
                {
                    num = "0";
                }
                //Response.Write("  " + num + "<br />");
                ht[id] = num;
            }
            Session["shopcar"] = ht;
            gvband1();

        }
        protected void textbox2_TextChanged(object sender, EventArgs e)
        {

            Hashtable ht = (Hashtable)Session["Nshopcar"];
            if (ht == null) return;
            for (int i = 0; i < gvshoping2.Rows.Count; i++)
            {
                string id = gvshoping2.Rows[i].Cells[0].Text.ToString();
                //Response.Write(id);
                string num = ((TextBox)gvshoping2.Rows[i].FindControl("textbox2")).Text;
                try
                {
                    Convert.ToInt16(num);
                }
                catch
                {
                    num = "0";
                }
                //Response.Write("  " + num + "<br />");
                ht[id] = num;
            }
            Session["Nshopcar"] = ht;
            gvband2();

        }
        protected void button0_Click(object sender, EventArgs e)
        {
            string id = ((Button)sender).CommandArgument;
            Hashtable ht = (Hashtable)Session["shopcar_base"];
            if (ht == null) return;
            ht.Remove(id);
            gvband0();
        }
        protected void button1_Click(object sender, EventArgs e)
        {
            string id = ((Button)sender).CommandArgument;
            Hashtable ht = (Hashtable)Session["shopcar"];
            if (ht == null) return;
            ht.Remove(id);
            gvband1();
        }
        protected void button2_Click(object sender, EventArgs e)
        {
            string id = ((Button)sender).CommandArgument;
            Hashtable ht = (Hashtable)Session["Nshopcar"];
            if (ht == null) return;
            ht.Remove(id);
            gvband2();
        }

        protected void ButtonApprove_Click(object sender, EventArgs e)
        {
            DataTable dtAll = new DataTable();
            if (Session["dt0"] != null)
            {
                dtAll.Merge((DataTable)Session["dt0"]);
            }
            if (Session["dt"] != null)
            {
                dtAll.Merge((DataTable)Session["dt"]);
            }
            if (Session["dt1"] != null)
            {
                dtAll.Merge((DataTable)Session["dt1"]);
            }

            //Session.Abandon();
            Session["dtAll"] = dtAll;
            Session.Remove("dt0");
            Session.Remove("dt");
            Session.Remove("dt1");
            Session.Remove("shopcar_base");
            Session.Remove("shopcar");
            Session.Remove("Nshopcar");
            Response.Redirect("New_Request.aspx");
            //gvshoping2.DataSource = dtAll.DefaultView;
            //gvshoping2.DataBind();

        }




    }//class
}