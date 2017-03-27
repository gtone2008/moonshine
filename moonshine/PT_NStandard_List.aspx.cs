﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using moonshine.DAL;
using System.Collections;

namespace moonshine
{
    public partial class PT_NStandard_List : Page
    {
        string str1 = "SELECT ps_id, ps_name, ps_standard, ps_pic, ps_useArea ,v_amount FROM product_no_standard join v_no_product_standard on ps_id=v_bs_name; ";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dlband();
                if (Session["Nshopcar"] != null)
                {
                    Hashtable ht= (Hashtable)Session["Nshopcar"];
                    lab1.Text = "[" + ht.Count.ToString() + "]";
                }
            }
        }

        private void dlband()
        {
            dlProducts.DataSource = MysqlHelper.ExecuteDataTable(str1);
            dlProducts.DataBind();
        }

        //创建hashtable key ps_id  value 是数量
        protected void dlProducts_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "addcart")
            {
                //Response.Write("<script>alert('成功！')"+e.CommandArgument+"</script>");
                string item_id = e.CommandArgument.ToString();
                Hashtable ht;
                if (Session["Nshopcar"] == null)
                {
                    ht = new Hashtable();
                    ht.Add(item_id, 1);
                    Session["Nshopcar"] = ht;
                }
                else
                {
                    ht = (Hashtable)Session["Nshopcar"];
                    if (ht.Contains(item_id))
                    {
                        int count = int.Parse(ht[item_id].ToString());
                        ht[item_id] = count + 1;
                        Session["Nshopcar"] = ht;
                    }
                    else
                    {
                        ht.Add(item_id, 1);
                        Session["Nshopcar"] = ht;
                    }
                }
                lab1.Text ="["+ ht.Count.ToString()+"]";

            }

        }









    }
}