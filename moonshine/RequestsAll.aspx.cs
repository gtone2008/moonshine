using System;
using System.Web.UI.WebControls;
using moonshine.DAL;
using moonshine.Util;

namespace moonshine
{
    public partial class RequestsAll : System.Web.UI.Page
    {
        private string sql1 = @"select (select logTime from task  where reqID=request.reqID order by sno desc limit 1) as lastApproval,taskStatus,taskRemark,confirms,case newold when 1 then '新产品报价' when 0 then '作回收报价' end as newold,request_top.reqID,request_top.reqUser,reqDate,reqNeedDate,reqCost,reqCER,reqDesc,case Status when  0 then 'Created' when  555 then 'Returned' when  999 then 'Closed' when  444 then 'Rejected' when  777 then 'Canceled' when  900 then 'PendingForMs' when  901 then 'PendingForCost' when  902 then 'PendingForIE' end as status from request inner join request_top on request.reqID=request_top.reqID inner join flow on request.reqID= flow.reqID  where 1=1";

        private string sql2 = @"UPDATE request_top set taskStatus='{0}',taskRemark='{1}',confirms='{2}' where reqID={3}";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (Session["userName"] != null)
                //{ reqUser.Text = (string)Session["userName"]; }
                gvBand(sql1);
            }
        }

        /// <summary>
        /// 文本框查询条件
        /// </summary>
        /// <param name="sql1"></param>
        protected void txtInput(string sql1)
        {
            if (reqID.Text != "")
            {
                sql1 = sql1 + " and request_top.reqID=" + reqID.Text;
            }
            if (reqUser.Text != "")
            {
                sql1 = sql1 + " and request_top.reqUser like '%" + reqUser.Text + "%'";
            }
            if (ckMyReq.Checked == true)
            {
                sql1 = sql1 + " and request.uid = '" + Common.GetCurrentNTID() + "'";
            }
            if (coCter.Text != "")
            {
                sql1 = sql1 + " and request_top.reqCost = '" + coCter.Text + "'";
            }

            if (ckMyAppr.Checked == true)
            {
                sql1 = sql1 + @" and request.reqID = (select  reqID from task   where  request.reqID=task.reqID and task.uid='" + Common.GetCurrentNTID() + "'  and sno=(select max(sno) from task where request.reqID=task.reqID) ) and Status  in ('900','901','902') ";
            }
            if (ckClose.Checked == true)
            {
                sql1 = sql1 + @" and flow.Status ='999' ";
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
                if (e.Row.RowState == DataControlRowState.Edit || e.Row.RowState == (DataControlRowState.Alternate | DataControlRowState.Edit))
                {
                    DropDownList ddl = e.Row.FindControl("ddlTaskStatus") as DropDownList;
                    DropDownList ddlApp = e.Row.FindControl("ddlApp") as DropDownList;
                    TextBox tb1 = e.Row.FindControl("tbRemark") as TextBox;

                    #region 完成审批

                    if (e.Row.Cells[8].Text == "Closed")
                    {
                        string sql1 = "select taskStatus from request_top where reqID='{0}'";
                        sql1 = string.Format(sql1, gvAll.DataKeys[e.Row.RowIndex].Value.ToString());
                        ddl.DataSource = DAL.MysqlHelper.ExecuteDataTable(sql1);
                        ddl.DataTextField = "taskStatus";
                        ddl.DataBind();

                        string sql2 = "select confirms from request_top where reqID='{0}'";
                        sql1 = string.Format(sql2, gvAll.DataKeys[e.Row.RowIndex].Value.ToString());
                        ddlApp.DataSource = DAL.MysqlHelper.ExecuteDataTable(sql1);
                        ddlApp.DataTextField = "confirms";
                        ddlApp.DataBind();

                        if (role.role_checkAdmin())
                        {
                            if (ddl.Text == "ON-GOING")
                            {
                                ddl.Items.Add("CLOSED");
                                ddl.Enabled = true;
                                tb1.Enabled = true;
                            }
                            if (ddl.Text == "")
                            {
                                ddl.Items.Add("ON-GOING");
                                ddl.Items.Add("CLOSED");
                                ddl.Enabled = true;
                                tb1.Enabled = true;
                            }
                        }
                        if (ddl.Text == "CLOSED")
                        {
                            ddl.Enabled = false;
                            tb1.Enabled = false;

                            if (Session["userName"].ToString() == e.Row.Cells[1].Text)
                            {
                                if (ddlApp.Text == "")
                                {
                                    ddlApp.Items.Add("已确认接收");
                                    ddlApp.Enabled = true;
                                }
                            }
                        }
                        //Response.Write("<script>alert('" + gvAll.DataKeys[e.Row.RowIndex].Value + "')</script>");
                        //Response.Write("<script>alert('" + sql2 + "')</script>");
                    }

                    #endregion 完成审批
                }
                if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
                {
                    HyperLink h1 = e.Row.Cells[0].FindControl("linkReqID") as HyperLink;
                    h1.Attributes.Add("href", "Requests.aspx?reqid=" + h1.Text);
                    Label lb1 = e.Row.Cells[10].Controls[1] as Label;//TaskStatus
                    Label lb2 = e.Row.Cells[12].Controls[1] as Label;
                    for (int i = 0; i < gvAll.Rows.Count; i++)
                    {
                        for (int j = 0; j < gvAll.Rows[i].Cells.Count; j++)
                        {

                            gvAll.Rows[i].Cells[j].ToolTip = gvAll.Rows[i].Cells[j].Text;
         
                        }
                    }
                    if (e.Row.Cells[8].Text == "Closed")
                    {
                        if (lb1.Text == "")
                        {
                            lb1.Text = "OPEN";
                            e.Row.Cells[10].BackColor = System.Drawing.Color.Red;
                        }
                        if (lb1.Text == "CLOSED")
                        {
                            e.Row.Cells[10].BackColor = System.Drawing.Color.Green;

                            if (lb2.Text == "已确认接收")
                            {
                                e.Row.Cells[12].BackColor = System.Drawing.Color.Green;
                                e.Row.Cells[13].Visible = false;
                            }
                            if (lb2.Text == "")
                            {
                                lb2.Text = "待确认接收";
                                e.Row.Cells[12].BackColor = System.Drawing.Color.Yellow;
                            }
                        }
                        if (lb1.Text == "ON-GOING")
                        {
                            e.Row.Cells[10].BackColor = System.Drawing.Color.Yellow;
                            if (lb2.Text == "")
                            {
                                lb2.Text = "待确认接收";
                                e.Row.Cells[12].BackColor = System.Drawing.Color.Yellow;
                            }
                        }
                    }
                    else
                    {
                        e.Row.Cells[13].Visible = false;
                    }
                }
            }
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            txtInput(sql1);
        }

        protected void gvAll_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvAll.EditIndex = e.NewEditIndex;
            txtInput(sql1);
        }

        protected void gvAll_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvAll.EditIndex = -1;
            txtInput(sql1);
        }

        protected void gvAll_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            DropDownList ddl = gvAll.Rows[e.RowIndex].FindControl("ddlTaskStatus") as DropDownList;
            TextBox tb1 = gvAll.Rows[e.RowIndex].FindControl("tbRemark") as TextBox;
            DropDownList ddlApp = gvAll.Rows[e.RowIndex].FindControl("ddlApp") as DropDownList;
            if (gvAll.Rows[e.RowIndex].Cells[8].Text == "Closed")
            {
                sql2 = string.Format(sql2, ddl.Text, tb1.Text, ddlApp.Text, e.Keys[0].ToString());
                if (MysqlHelper.ExecuteNonQuery(sql2) > 0)
                {
                    Response.Write("<script>alert('成功！')</script>");
                    Response.Write("<script>document.location=document.location;</script>");
                }
            }
        }

        protected void gvAll_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAll.PageIndex = e.NewPageIndex;
            txtInput(sql1);
        }

        protected void ckMyReq_CheckedChanged(object sender, EventArgs e)
        {
            txtInput(sql1);
        }

        protected void ckMyAppr_CheckedChanged(object sender, EventArgs e)
        {
            txtInput(sql1);
        }
    }//class
}