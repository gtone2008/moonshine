using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using moonshine.Model;
using moonshine.DAL;
using MySql.Data.MySqlClient;
using moonshine.EnumAll;
using System.Data;
using moonshine.Util;
using System.Web.UI;

namespace moonshine.Request
{
    public class RequestMgr
    {
        public static AdUser GetApprover(string costID)
        {
            string cmdText = "select approverNTID from approver where costID='{0}'";
            cmdText=string.Format(cmdText, costID);
            return (Common.GetADUserEntity(MysqlHelper.ExecuteScalar(cmdText).ToString()));
        }


        public static bool IsRequestOwner(string userId, RequestInfo reqInfo)
        {
            return string.Compare(reqInfo.User.UID, userId, true) == 0;
        }


        public static bool IsTaskOwner(string userId, TaskInfo TaskInfo)
        {
            return string.Compare(TaskInfo.UID, userId, true) == 0;
        }


        public static bool IsLastApprover(string uid, FlowInfo flow)
        {
            string cmdText = "select 1 from Task where   flowId={0} and uid='{1}' and logTime is null;";
            cmdText = string.Format(cmdText, flow.FlowId, uid);
            return MysqlHelper.ExecuteScalar(cmdText) ==null;
        }

        public static FlowInfo GetTopFlow(string reqId)
        {
            Model.FlowInfo flow = new FlowInfo();
            string cmdText = "select * from flow where reqID ={0}";
            cmdText = string.Format(cmdText, reqId);
            using (MySqlDataReader reader = MysqlHelper.ExecuteReader(cmdText))
            {
                if (reader.Read())
                {

                    flow.FlowId = Convert.ToInt32(reader["flowId"]);
                    flow.ParentId = Convert.ToInt32(reader["parentId"]);
                    flow.Status = (FlowStatus)int.Parse(reader["Status"].ToString());
                    flow.ReqId = Convert.ToInt32(reqId);

                }
            }
            return flow;
        }
        public static RequestInfo GetRequest(string reqId)
        {
            RequestInfo request = new RequestInfo();
            string cmdText = "select uid,reqInfo,reqCost from request inner join request_top on request.reqID=request_top.reqID  where request.reqID ={0}";
            cmdText = string.Format(cmdText,reqId);
            using (MySqlDataReader reader = MysqlHelper.ExecuteReader(cmdText))
            {
                if (reader.Read())
                {
                    request.ReqID = Convert.ToInt16(reqId);
                    request.User= Common.GetADUserEntity((string)reader["uid"]);
                    request.ReqInfo = (string)reader["reqInfo"];
                    request.CostCenter = (string)reader["reqCost"];
                }
            }
            return request;
        }

        public static TaskInfo GetOpenTask(string reqid)
        {
            string cmdText = "select * from task where reqID={0} order by sno desc limit 1";
            cmdText = string.Format(cmdText, reqid);
            TaskInfo ts = new TaskInfo();
            using (MySqlDataReader reader = MysqlHelper.ExecuteReader(cmdText))
            {
                if (reader.Read())
                {
                    if (reader["uname"] != DBNull.Value && reader["uname"] != null)
                    {
                        ts.UserName = (string)reader["uname"];
                    }
                    if (reader["uid"] != DBNull.Value && reader["uid"] != null)
                    {
                        ts.UID = reader["uid"].ToString();
                    }
                    if (reader["sno"] != DBNull.Value && reader["sno"] != null)
                    {
                        ts.SNo = (int)reader["sno"];
                    }
                }
            }
            return ts;
        }
        public static DataTable GetAllTask(string reqid)
        {
            string cmdText = "select logTime,roleName as role,uname as approver,actionName as action,comment from task left join task_role on task_role.roleID=task.role left join task_action on task_action.actionID=task.act   where reqID={0}";
            cmdText=string.Format(cmdText, reqid);
            using (DataTable dt = MysqlHelper.ExecuteDataTable(cmdText)) 
            return dt;
        }

       

        public static IList<ActionType> GetRequestPermission(int taskId, FlowInfo flow, RequestInfo reqInfo, string userId)
        {
            List<ActionType> actions = new List<ActionType>();

            TaskInfo ts = RequestMgr.GetOpenTask(reqInfo.ReqID.ToString());

            #region taskowners' permissions
            // ReqID<0 if for new creation request, no need check open task.
            if (ts != null && ts.UID.ToLower() == userId.ToLower())
            {
                switch (flow.Status)
                {
                    case FlowStatus.Created:
                        actions.Add(ActionType.Submit);
                        break;
                    case FlowStatus.PendingForMs:
                    case FlowStatus.PendingForCost:
                    case FlowStatus.PendingForIE:
                        actions.Add(ActionType.Approve);
                        actions.Add(ActionType.Reject);
                        break;
                }
            }

            #endregion

            #region requstor's permissions

            if (RequestMgr.IsRequestOwner(userId, reqInfo))
            {
                switch (flow.Status)
                {
                    case FlowStatus.Created:
                        actions.Add(ActionType.Submit);
                        break;
                    case FlowStatus.PendingForMs:
                        actions.Add(ActionType.Cancel);
                        break;
                    default:
                        break;
                }
            }
            #endregion

            return actions;
        }


        public static TaskInfo AddTask(string uid, FlowInfo flow, RoleType roleType,string uname)
        {
            string cmdText = "insert into task(reqID, flowId, uid,role,uname) value ({0},{1},'{2}',{3},'{4}');select max(sno) from task;";
            TaskInfo task = new TaskInfo();
            task.ReqId = flow.ReqId;
            task.FlowId = flow.FlowId;
            task.UID = uid;
            task.Role = roleType;
            cmdText = string.Format(cmdText, task.ReqId, task.FlowId,task.UID,(int)task.Role, uname);
            task.SNo = Convert.ToInt32(MysqlHelper.ExecuteScalar(cmdText));
            return task;
        }
        public static void AddTasks(Dictionary<AdUser, RoleType> nextUser, FlowInfo flow)
        {
            if (nextUser.Count == 0) return;
            // add task
            foreach (var u in nextUser)
            {
                AddTask(u.Key.UID, flow, u.Value,u.Key.DisplayName);
            }
        }

        public static void UpdateTask(int taskId, ActionType action, string comments)
        {
            Page currentPage = System.Web.HttpContext.Current.Handler as Page;
            TaskInfo task = new TaskInfo
            {
                SNo = taskId,
                Act = action,
                Comments = comments,
                HostName = currentPage.Request.UserHostName
        };
            string cmdText = "update task set hostName='{0}',logTime=now(),act={1},comment='{2}' where sno={3}";
            cmdText = string.Format(cmdText, task.HostName, (int)task.Act, task.Comments,task.SNo);
            MysqlHelper.ExecuteNonQuery(cmdText);
        }
      

        public static void CancelAllOtherTasks(string uid, FlowInfo flow, ActionType action,string comment)
        {
            string cmdText = "update task set act={0},comment='{1}',logTime=NOW() where flowId={2} and uid='{3}' and logTime is null";
            cmdText = string.Format(cmdText,(int)action, comment,flow.FlowId,uid);
            MysqlHelper.ExecuteNonQuery(cmdText);
        }

        public static void UpdateStatus(int flowId, FlowStatus status)
        {
            string cmdText = "update flow set Status={0} where flowId={1}";
            cmdText = string.Format(cmdText, (int)status,flowId);
            MysqlHelper.ExecuteNonQuery(cmdText);
        }
        //public static FlowStatus GetStatus(int reqID)
        //{
        //    string cmdText = "select Status from flow where  reqID={1}";
        //     cmdText=string.Format(cmdText, (int)status, flowId);
        //    MysqlHelper.ExecuteNonQuery(cmdText);
        //    return (FlowStatus)111;
        //}
        //public static AdUser GetMs(int departId)
        //{
        //    return _org.GetFM(departId);
        //}

        //public Model.UserInfo GetFM(int departId)
        //{
        //    string cmdText = "select u.userid,u.dept,u.displayName,u.mail,u.site 
        //                    from department d left join WinADUser u on d.deptMgr=u.userid
        //                    where d.deptid=@deptId";
        //    SqlParameter param = new SqlParameter("@deptId", departId);
        //    return GetUser(cmdText, param);
        //}

    }//class
}