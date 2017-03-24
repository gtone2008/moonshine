using moonshine.EnumAll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace moonshine.Model
{
    public class TaskInfo
    {
        public int SNo { get; set; }
        public int ReqId { get; set; }
        public int FlowId { get; set; }
        public string UID { get; set; }

        public string UserName { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? LogTime { get; set; }
        public string HostName { get; set; }
        public ActionType? Act { get; set; }
        public string Comments { get; set; }
        public RoleType? Role { get; set; }
    }
}