using System;
using System.Collections.Generic;
using moonshine.EnumAll;
using moonshine.Model;

namespace moonshine.Request
{
    public class WorkflowHandler
    {
        public static void HandleTask(AdUser currentUser, int taskId, FlowInfo flow, RequestInfo request, ActionType action, string comments)
        {
            FlowStatus oldStatus = flow.Status;
            RequestMgr.UpdateTask(taskId, action, comments);
            SF_MoveAfterTaskDone(currentUser, flow, request, action, comments);
            if (oldStatus == flow.Status) return;
        }

        private static void SF_MoveAfterTaskDone(AdUser currentUser, FlowInfo flow, RequestInfo request, ActionType action, string comments)
        {
            FlowStatus nextStatus = flow.Status;
            switch (flow.Status)
            {
                #region case RequestStatus.Created

                case FlowStatus.Created:
                    switch (action)
                    {
                        case ActionType.Submit:
                            nextStatus = FlowStatus.PendingForMs;
                            break;

                        default:
                            throw new Exception("Wrong Action");
                    }
                    break;

                #endregion case RequestStatus.Created

                #region case RequestStatus.PendingForMs

                case FlowStatus.PendingForMs:
                    switch (action)
                    {
                        case ActionType.Approve:
                            nextStatus = FlowStatus.PendingForCost;
                            break;

                        case ActionType.Reject:
                            nextStatus = FlowStatus.Rejected;
                            RequestMgr.CancelAllOtherTasks(currentUser.UID, flow, action, comments);
                            break;

                        case ActionType.Cancel:
                            RequestMgr.CancelAllOtherTasks(currentUser.UID, flow, action, comments);     // if cancel, then not need manager approval any more.
                            nextStatus = FlowStatus.Canceled;
                            break;

                        default:
                            throw new Exception("Wrong Action");
                    }
                    break;

                #endregion case RequestStatus.PendingForMs

                #region PendingForCost

                case FlowStatus.PendingForCost:
                    switch (action)
                    {
                        case ActionType.Approve:
                            nextStatus = FlowStatus.PendingForIE;
                            break;

                        case ActionType.Reject:
                            nextStatus = FlowStatus.Rejected;
                            RequestMgr.CancelAllOtherTasks(currentUser.UID, flow, action, comments);
                            break;

                        case ActionType.Cancel:
                            RequestMgr.CancelAllOtherTasks(currentUser.UID, flow, action, comments);     // if cancel, then not need manager approval any more.
                            nextStatus = FlowStatus.Canceled;
                            break;

                        default:
                            throw new Exception("Wrong Action");
                    }
                    break;

                #endregion PendingForCost

                #region PendingForIE

                case FlowStatus.PendingForIE:
                    switch (action)
                    {
                        case ActionType.Approve:
                            nextStatus = FlowStatus.Closed;
                            break;

                        case ActionType.Reject:
                            nextStatus = FlowStatus.Rejected;
                            break;

                        default:
                            throw new Exception("Wrong Action");
                    }
                    break;

                    #endregion PendingForIE
            }

            if (nextStatus != flow.Status)
            {
                SF_MoveTo(currentUser, flow, nextStatus, request, comments);
            }
        }

        private static void SF_MoveTo(AdUser currentUser, FlowInfo flow, FlowStatus nextStatus, RequestInfo request, string comments)
        {
            Dictionary<AdUser, RoleType> nextUser = new Dictionary<AdUser, RoleType>();

            switch (nextStatus)
            {
                #region PendingForMs

                case FlowStatus.PendingForMs:
                    nextUser.Add(RequestMgr.GetApprover("0"), RoleType.MS);
                    break;

                #endregion PendingForMs

                #region PendingForCost

                case FlowStatus.PendingForCost:
                    nextUser.Add(RequestMgr.GetApprover(request.CostCenter), RoleType.Cost);
                    break;

                #endregion PendingForCost

                #region PendingForIE

                case FlowStatus.PendingForIE:
                    nextUser.Add(RequestMgr.GetApprover("1"), RoleType.IE);
                    break;

                    #endregion PendingForIE
            }
            if (flow.Status != nextStatus)
            {
                RequestMgr.UpdateStatus(flow.FlowId, nextStatus);
            }
            flow.Status = nextStatus;
            RequestMgr.AddTasks(nextUser, flow);

            SendMail(currentUser, request, flow, nextUser, comments);
        }

        private static void SendMail(AdUser currentUser, RequestInfo request, FlowInfo flow, Dictionary<AdUser, RoleType> nextUser, string comments)
        {
            // send mail
            List<AdUser> taskUsers = new List<AdUser>();
            taskUsers.AddRange(nextUser.Keys);
            MailHandler.SendMail(currentUser, request, flow, taskUsers, comments);
        }
    }//class
}