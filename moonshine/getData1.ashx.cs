using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using moonshine.DAL;
using MySql.Data;
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
            if(context.Request.QueryString["action"]=="getPN")
            {
                string sql1 = "select basic_id from basic_data where basic_id ='{0}'";
                sql1 = string.Format(sql1, context.Request.QueryString["pn1"]);
                //str3 = string.Format(str3, "ME003335");
                // Response.Write(str3);
                context.Response.Write((MysqlHelper.Exists(sql1)).ToString());//存在
            }
            if (context.Request.QueryString["action"] == "getPNS")
            {
                string sql1 = "select basic_id,description,spec,current_qty from basic_data";
                string jsonPN = JsonHelper.ToJson(MysqlHelper.ExecuteReader(sql1));
                context.Response.Write(jsonPN);

            }
            if (context.Request.QueryString["action"] == "getCCS")
            {
                string sql1 = "select * from approver where costID not in(0,1) ";
                string jsonPN = JsonHelper.ToJson(MysqlHelper.ExecuteReader(sql1));
                context.Response.Write(jsonPN);

            }

            if (context.Request.QueryString["action"] == "getRequestByReqID")
            {
                string sql1 = "SELECT * FROM request inner join request_top on request_top.reqID=request.reqID  where request_top.reqID={0}";
                sql1 = string.Format(sql1, context.Request.QueryString["reqid"]);
                string jsonPN = JsonHelper.ToJson(MysqlHelper.ExecuteReader(sql1));
                context.Response.Write(jsonPN);

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