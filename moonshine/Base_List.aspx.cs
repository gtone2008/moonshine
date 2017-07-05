using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using moonshine.DAL;

namespace moonshine
{
    public partial class Base_List : Page
    {
        private string str1 = "SELECT basic_id, description, spec, photo, price,current_qty FROM basic_data where 1=1  ";

        protected void Page_Load(object sender, EventArgs e)
        {
            txtInput(str1);

            if (!IsPostBack)
            {
                gvBand(str1);
                if (Session["shopcar_base"] != null)
                {
                    Hashtable ht = (Hashtable)Session["shopcar_base"];
                    lab1.Text = "[" + ht.Count.ToString() + "]";
                }
            }
        }

        private void gvBand(string str)
        {
            str = str + " order by basic_id";
            dlbase.DataSource = MysqlHelper.ExecuteDataTable(str);
            dlbase.DataBind();
        }

        /// <summary>
        /// 根据文本框输入拼接查询字符串
        /// </summary>
        /// <param name="sql1"></param>
        protected void txtInput(string sql1)
        {
            if (Request.Form["txtbase"] != "")
            {
                sql1 = sql1 + " and basic_id ='" + Request.Form["txtbase"] + "'";
            }
            if (Request.Form["txtdesc"] != "")
            {
                sql1 = sql1 + " and description like '%" + Request.Form["txtdesc"] + "%'";
            }

            gvBand(sql1);
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            txtInput(str1);
        }

        protected void checklow_CheckedChanged(object sender, EventArgs e)
        {
            str1 = str1 + " and LowLimit>=current_qty ";
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
                lab1.Text = "[" + ht.Count.ToString() + "]";
            }
        }
    }
}