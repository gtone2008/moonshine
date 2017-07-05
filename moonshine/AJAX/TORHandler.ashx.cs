using System.Web;
using moonshine.BLL;

namespace moonshine.AJAX
{
    /// <summary>
    /// Summary description for TORHandler
    /// </summary>
    public class TORHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            switch (context.Request.QueryString["action"])
            {
                case "getDataTOR": context.Response.Write(getDataTOR(context)); break;
                default: context.Response.Write("No Data"); break;
            }
        }

        private string getDataTOR(HttpContext context)
        {
            string sdate = context.Request.QueryString["sdate"], edate = context.Request.QueryString["edate"];
            return bll_TOR.getBData(sdate, edate);
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