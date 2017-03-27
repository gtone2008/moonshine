using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using moonshine.DAL;
using moonshine.Util;
using moonshine.Model;
using moonshine.Request;
using moonshine.EnumAll;

namespace moonshine
{
    public partial class Cost_Summary : System.Web.UI.Page
    {
        string sql1 = @"select rtt.reqCost,SUM( CASE newold WHEN 1 THEN rt.reqSum ELSE 0 END)  AS 'new',
                         SUM(CASE newold WHEN 0 THEN rt.reqSum ELSE 0 END)  AS 'old'
                         from request_top rtt
                         inner join request rt on rtt.reqID=rt.reqID
                         inner join flow on rtt.reqID =flow.reqID where flow.Status= '999' ";
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                gvBand(sql1);
            }
        }
        protected void txtInput(string sql1)
        {
            if (Request.Form["ckCer"] == "0")
            {
                sql1 = sql1 + " and rtt.reqCER ='' ";
            }
            
            sql1 = sql1 + " and rtt.reqDate  between '{0}' and '{1}' ";
            sql1 = string.Format(sql1, Request.Form["sDate"], Request.Form["eDate"]);
            gvBand(sql1);
        }
        protected void gvBand(string sql1)
        {
            sql1 = sql1 + " group by rtt.reqCost;";
            gvAll.DataSource = MysqlHelper.ExecuteDataTable(sql1);
            gvAll.DataBind();

            // WorkflowHandler.HandleTask(AdUser currentUser, int taskId, FlowInfo flow, RequestInfo request, ActionType action, string comments);
        }

        protected void gvAll_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
                {
                    //HyperLink h1 = e.Row.Cells[0].FindControl("linkReqID") as HyperLink;
                    //h1.Attributes.Add("href", "Requests.aspx?reqid="+h1.Text);
                }
            }
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {

            txtInput(sql1);
        }
        //Response.Write("<script>document.location=document.location;</script>");//放在有write的下一句防止字体变大
    }//class
}