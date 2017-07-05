using System.Web;
using moonshine.Util;

namespace moonshine.AJAX
{
    /// <summary>
    /// Summary description for getPower
    /// </summary>
    public class getPower : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string power = role.power();
            if (power == "")
            {
                context.Response.Write("guest");
            }
            else
            {
                context.Response.Write(power);
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