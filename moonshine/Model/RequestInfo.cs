using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace moonshine.Model
{
    public class RequestInfo
    {
        public int ReqID { get; set; }
        public AdUser User { get; set; }
        public string ReqInfo { get; set; }
        public string CostCenter { get; set; }
    }
}