using System.Web;
using moonshine.DAL;
using moonshine.Util;

namespace moonshine
{
    /// <summary>
    /// Summary description for getData1
    /// </summary>
    public class getData1 : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            switch (context.Request.QueryString["action"])
            {
                case "getPN":

                    string sql1 = "select basic_id from basic_data where basic_id ='{0}'";
                    sql1 = string.Format(sql1, context.Request.QueryString["pn1"]);
                    //str3 = string.Format(str3, "ME003335");
                    // Response.Write(str3);
                    context.Response.Write((MysqlHelper.Exists(sql1)).ToString());//存在
                    break;

                case "getPNS":

                    string sql2 = "select basic_id,description,spec,current_qty from basic_data";
                    context.Response.Write(JsonHelper.ToJson(MysqlHelper.ExecuteReader(sql2)));

                    break;

                case "getCCS":

                    string sql3 = "select * from approver where costID not in(0,1) ";
                    context.Response.Write(JsonHelper.ToJson(MysqlHelper.ExecuteReader(sql3)));

                    break;

                case "getRequestByReqID":

                    string sql4 = @"SELECT * FROM request inner join request_top on request_top.reqID=request.reqID  where request_top.reqID={0}";
                    sql4 = string.Format(sql4, context.Request.QueryString["reqid"]);
                    context.Response.Write(JsonHelper.ToJson(MysqlHelper.ExecuteReader(sql4)));
                    break;

                default:
                    break;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}