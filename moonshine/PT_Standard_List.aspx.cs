using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using moonshine.DAL;

namespace moonshine
{
    public partial class PT_Standard_List : Page
    {
        private string str1 = "SELECT ps_id, ps_name, ps_standard, ps_pic, ps_useArea ,v_amount FROM product_standard join v_product_standard on ps_id=v_bs_name where 1=1 ";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dlband(str1);
                if (Session["shopcar"] != null)
                {
                    Hashtable ht = (Hashtable)Session["shopcar"];
                    lab1.Text = "[" + ht.Count.ToString() + "]";
                }
            }
        }

        private void dlband(string sql)
        {
            dlProducts.DataSource = MysqlHelper.ExecuteDataTable(sql);
            dlProducts.DataBind();
        }

        /// <summary>
        /// 根据文本框输入拼接查询字符串
        /// </summary>
        /// <param name="sql1"></param>
        protected void txtInput(string sql1)
        {
            if (Request.Form["txtbase"] != "")
            {
                sql1 = sql1 + " and ps_id ='" + Request.Form["txtbase"] + "'";
            }
            if (Request.Form["txtdesc"] != "")
            {
                sql1 = sql1 + " and ps_name like '%" + Request.Form["txtdesc"] + "%'";
            }

            dlband(sql1);
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            this.txtInput(str1);
        }

        //创建hashtable key ps_id  value 是数量
        protected void dlProducts_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "addcart")
            {
                //Response.Write("<script>alert('成功！')"+e.CommandArgument+"</script>");
                string item_id = e.CommandArgument.ToString();
                Hashtable ht;
                if (Session["shopcar"] == null)
                {
                    ht = new Hashtable();
                    ht.Add(item_id, 1);
                    Session["shopcar"] = ht;
                }
                else
                {
                    ht = (Hashtable)Session["shopcar"];
                    if (ht.Contains(item_id))
                    {
                        int count = int.Parse(ht[item_id].ToString());
                        ht[item_id] = count + 1;
                        Session["shopcar"] = ht;
                    }
                    else
                    {
                        ht.Add(item_id, 1);
                        Session["shopcar"] = ht;
                    }
                }
                lab1.Text = "[" + ht.Count.ToString() + "]";
            }
        }
    }
}