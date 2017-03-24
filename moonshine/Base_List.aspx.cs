using System;
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
    public partial class Base_List : Page
    {
        string str1 = "SELECT basic_id, description, spec, photo, price,current_qty FROM basic_data where 1=1  ";

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            // {           
            dlband(str1);
                if (Session["shopcar_base"] != null)
                {
                    Hashtable ht= (Hashtable)Session["shopcar_base"];
                    lab1.Text = "[" + ht.Count.ToString() + "]";
                }
            //}
        }

        private void dlband(string str)
        {
            str = str + " order by description desc";
            dlbase.DataSource = MysqlHelper.ExecuteDataTable(str1);
            dlbase.DataBind();
        }

        protected void checklow_CheckedChanged(object sender, EventArgs e)
        {
            if (checklow.Checked)
            {
                str1 = str1 + " and LowLimit>=current_qty ";

                dlband(str1);
            }
            else
            {
                dlband(str1);
            }
        }

        //创建hashtable key= basic_id  value =数量
        protected void dlProducts_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "addcart")
            {
                //Response.Write("<script>alert('成功！')"+e.CommandArgument+"</script>");
                string item_id = e.CommandArgument.ToString();
                Hashtable ht;
                if (Session["shopcar_base"] == null)
                {
                    ht = new Hashtable();
                    ht.Add(item_id, 1);
                    Session["shopcar_base"] = ht;
                }
                else
                {
                    ht = (Hashtable)Session["shopcar_base"];
                    if (ht.Contains(item_id))
                    {
                        int count = int.Parse(ht[item_id].ToString());
                        ht[item_id] = count + 1;
                        Session["shopcar_base"] = ht;
                    }
                    else
                    {
                        ht.Add(item_id, 1);
                        Session["shopcar_base"] = ht;
                    }
                }
                lab1.Text ="["+ ht.Count.ToString()+"]";

            }

        }









    }
}