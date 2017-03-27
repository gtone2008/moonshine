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
using moonshine.EnumAll;

namespace moonshine
{
    public partial class New_Request : System.Web.UI.Page
    {
        decimal sum = 0;        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["dtAll"] != null) { dvband(); }
            }
        }

        //gv取数据
        private void dvband()
        {
            DataTable tb = (DataTable)Session["dtAll"];
            gvAll.DataSource = tb;
            gvAll.DataBind();
        }






        //金额汇总
        protected void gvbom1_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                sum += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "total"));

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[4].Text = "合计：";
                e.Row.Cells[5].Text = sum.ToString();
            }


        }

        protected void ButtonApprove_Click(object sender, EventArgs e)
        {
            if (Session["dtAll"] != null)
            {
                Session.Remove("dtAll");//可以预防多此提交

                RequestInfo requestInfo = new RequestInfo();
                requestInfo.ReqInfo = Request.Form["txtAll"];
                requestInfo.User = Common.GetADUserEntity(Common.GetCurrentNTID());
                requestInfo.CostCenter = Request.Form["reqCost"];
                string reqCER = string.IsNullOrEmpty(Request.Form["reqCER"])?string.Empty:Request.Form["reqCER"].ToString();
                string reqDesc = string.IsNullOrEmpty(Request.Form["reqDesc"]) ? string.Empty : Request.Form["reqDesc"].ToString();
                //add request
                string sql1 = "insert into request(uid,reqInfo,reqSum) values ('{0}','{1}',{2});select max(reqID) from request";
                sql1 = string.Format(sql1, Common.GetCurrentNTID(), requestInfo.ReqInfo, sum);
                int reqID = Convert.ToInt32(MysqlHelper.ExecuteScalar(sql1));

                //add request_top
                string sql2 = "insert into request_top(reqID, reqUser,reqPhone, reqDep, reqWC, reqCost, reqNeedDate, reqCER, reqDesc,newold) values ({0},'{1}',{2},'{3}','{4}','{5}','{6}','{7}','{8}','{9}')";
                sql2 = string.Format(sql2, reqID, Request.Form["reqUser"], Request.Form["reqPhone"], Request.Form["reqDep"], Request.Form["reqWC"], 
                    Request.Form["reqCost"], Request.Form["reqNeedDate"], reqCER, reqDesc, Request.Form["newold"]); 
                MysqlHelper.ExecuteNonQuery(sql2);

                //add flow
                string sql3= "insert into flow(reqID, parentId, Status) values ({0},{1},{2}); select max(flowId) from flow;";
                sql3 = string.Format(sql3, reqID,0, (int)EnumAll.FlowStatus.Created);
                int flowId= Convert.ToInt32(MysqlHelper.ExecuteScalar(sql3));
               
                //add task
                string sql4 = "insert into task(reqID, flowId, uid,hostName, role,uname) value ({0},{1},'{2}','{3}',{4},'{5}');select max(sno) from task";
                sql4 = string.Format(sql4, reqID, flowId, Common.GetCurrentNTID(),Page.Request.UserHostName,(int)EnumAll.RoleType.Requestor, requestInfo.User.DisplayName);
                int sno= Convert.ToInt32(MysqlHelper.ExecuteScalar(sql4));
                requestInfo.ReqID = reqID;
                FlowInfo mainflow = new FlowInfo();
                mainflow.ParentId = 0;
                mainflow.ReqId = reqID;
                mainflow.FlowId = flowId;
                mainflow.Status= FlowStatus.Created;

                
                moonshine.Request.WorkflowHandler.HandleTask(requestInfo.User, sno, mainflow, requestInfo, EnumAll.ActionType.Submit, "new created.");
                Response.Redirect("RequestsAll.aspx");
            }


        }



        //Response.Write("<script>document.location=document.location;</script>");//放在有write的下一句防止字体变大
    }//class
}