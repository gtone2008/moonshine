using System;
using System.Web.UI;

namespace moonshine
{
    public partial class About : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write(Request.QueryString["id"]);
        }
    }
}