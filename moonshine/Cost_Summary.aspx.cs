using System;
using System.Web.UI.WebControls;
using moonshine.DAL;

namespace moonshine
{
    public partial class Cost_Summary : System.Web.UI.Page
    {
        private string sql1 = @"select rtt.reqCost,SUM( CASE newold WHEN 1 THEN rt.reqSum ELSE 0 END)  AS 'new',
                         SUM(CASE newold WHEN 0 THEN rt.reqSum ELSE 0 END)  AS 'old',
                         (select sum(cost) from pm_history ) as cost
                         from request_top rtt
                         inner join request rt on rtt.reqID=rt.reqID
                         inner join flow on rtt.reqID =flow.reqID
                         left join (select reqID ,max(logTime) lt from task   group by reqID ) as aa on aa.reqID=rtt.reqID
                         where flow.Status= '999'  ";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
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
            sql1 = sql1 + " and aa.lt  between '{0}' and '{1}' ";
            sql1 = string.Format(sql1, Request.Form["sDate"], Request.Form["eDate"]);
            sql1 = sql1.Replace("pm_history", $"pm_history where date(solve_date) between '{Request.Form["sDate"]}' and '{Request.Form["eDate"]}'");
            gvBand(sql1);
        }

        protected void gvBand(string sql1)
        {
            sql1 = sql1 + " group by rtt.reqCost;";
            gvAll.DataSource = MysqlHelper.ExecuteDataTable(sql1);
            gvAll.DataBind();
        }

        protected void gvAll_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
                {
                    Label lb1 = e.Row.Cells[4].FindControl("Total") as Label;
                    var troCost=0.00;
                    try
                    {
                         troCost = Convert.ToDouble(e.Row.Cells[3].Text);
                    } catch (Exception)
                    {
                        e.Row.Cells[3].Text = "0.00";
                    }
                    if (troCost>0)
                    {
                        switch (e.Row.Cells[0].Text)
                        {
                            case "6311020": e.Row.Cells[3].Text = (troCost * 0.2463).ToString(); break;
                            case "6311022": e.Row.Cells[3].Text = (troCost * 0.4429).ToString(); break;
                            case "6311030": e.Row.Cells[3].Text = (troCost * 0.027).ToString(); break;
                            case "6311032": e.Row.Cells[3].Text = (troCost * 0.0166).ToString(); break;
                            case "6311033": e.Row.Cells[3].Text = (troCost * 0.2672).ToString(); break;                          
                            default: e.Row.Cells[3].Text = "0.00"; break;
                        }
                    }
                    decimal dcTotal = Convert.ToDecimal(e.Row.Cells[1].Text) - Convert.ToDecimal(e.Row.Cells[2].Text)+ Convert.ToDecimal(e.Row.Cells[3].Text);
                    lb1.Text = dcTotal.ToString();
                    if (dcTotal < 0)
                    {
                        lb1.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            txtInput(sql1);
        }
    }//class
}