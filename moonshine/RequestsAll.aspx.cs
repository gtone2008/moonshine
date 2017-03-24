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
    public partial class RequestsAll : System.Web.UI.Page
    {
        string sql1 = "select case newold when 1 then '新产品报价' when 0 then '作回收报价' end as newold,request_top.reqID,request_top.reqUser,reqDate,reqNeedDate,reqCost,reqCER,reqDesc,case Status when  0 then 'Created' when  555 then 'Returned' when  999 then 'Closed' when  444 then 'Rejected' when  777 then 'Canceled' when  900 then 'PendingForMs' when  901 then 'PendingForCost' when  902 then 'PendingForIE' end as status from request inner join request_top on request.reqID=request_top.reqID inner join flow on request.reqID= flow.reqID where 1=1";
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (Session["userName"] != null)
                { reqUser.Text = (string)Session["userName"]; }

                gvBand(sql1);
            }
        }
        protected void txtInput(string sql1)
        {
            if (reqID.Text != "")
            {
                sql1 = sql1 + " and request_top.reqID=" + reqID.Text;
            }
            if (reqUser.Text != "")
            {
                sql1 = sql1 + " and request_top.reqUser='" + reqUser.Text+"'";
            }
            gvBand(sql1);
        }
        protected void gvBand(string sql1)
        {
            sql1 = sql1 + " order by request_top.reqID desc";
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
                    HyperLink h1 = e.Row.Cells[0].FindControl("linkReqID") as HyperLink;
                    h1.Attributes.Add("href", "Requests.aspx?reqid="+h1.Text);
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