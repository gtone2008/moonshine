using System;
using moonshine.EnumAll;
using moonshine.Model;
using moonshine.Request;
using moonshine.Util;

namespace moonshine
{
    public partial class Requests : System.Web.UI.Page
    {
        private RequestInfo RequestInfo = null;
        private FlowInfo flow = null;
        private TaskInfo task = null;
        private string reqID = null;
        private AdUser currentUser = Common.GetADUserEntity(Common.GetCurrentNTID());

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetNoStore();
            reqID = Request.QueryString["reqid"].ToString() ?? "0";
            if (!string.IsNullOrEmpty(reqID))
            {
                try { Convert.ToInt16(reqID); } catch { reqID = "0"; }//{ throw new Exception("传入的reqid不正确!"); }
                RequestInfo = RequestMgr.GetRequest(reqID);
                flow = RequestMgr.GetTopFlow(reqID);
                if (flow.Status != FlowStatus.Closed && flow.Status != FlowStatus.Canceled && flow.Status != FlowStatus.Rejected)
                {
                    task = RequestMgr.GetOpenTask(reqID);
                    if (RequestMgr.IsTaskOwner(currentUser.UID, task))
                    {
                        remarkDiv.Visible = true;
                        btnApprove.Visible = true;
                        btnReject.Visible = true;
                    }
                    if (RequestMgr.IsRequestOwner(currentUser.UID, RequestInfo))
                    {
                        remarkDiv.Visible = true;
                        btnCancel.Visible = true;
                    }
                }
                gvBand(reqID);
            }
            else
            {
                return;
            }
        }

        protected void gvBand(string reqID)
        {
            gvTask.DataSource = RequestMgr.GetAllTask(reqID);
            gvTask.DataBind();
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            string comments = string.IsNullOrEmpty(Request.Form["txtRemark"]) ? string.Empty : Request.Form["txtRemark"].ToString();
            WorkflowHandler.HandleTask(currentUser, task.SNo, flow, RequestInfo, ActionType.Approve, comments);
            Response.Write("<script>alert('Thanks for your approval')</script>");
            Response.Redirect("Requests.aspx?reqid=" + reqID);
            Response.Write("<script>document.location=document.location;</script>");//放在有write的下一句防止字体变大
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            WorkflowHandler.HandleTask(currentUser, task.SNo, flow, RequestInfo, ActionType.Reject, Request.Form["txtRemark"].ToString());
            Response.Redirect("Requests.aspx?reqid=" + reqID);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //System.Threading.Thread.Sleep(2000);
            WorkflowHandler.HandleTask(currentUser, task.SNo, flow, RequestInfo, ActionType.Cancel, Request.Form["txtRemark"].ToString());
            Response.Redirect("Requests.aspx?reqid=" + reqID);
        }
    }//class
}