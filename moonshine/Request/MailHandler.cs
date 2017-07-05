using System.Collections.Generic;
using moonshine.EnumAll;
using moonshine.Model;

namespace moonshine.Request
{
    public class MailHandler
    {
        private static readonly string EMAILBOTTOM =
                "<p><i><font color=\"red\">Please do not reply this mail.</font></i></p>"
                + "<p>Moonshine Management  System</p>";

        /// <summary>
        /// email reminder
        /// </summary>
        /// <param name="currentUser">current user</param>
        /// <param name="request">request info</param>
        /// <param name="nextUser">next action users</param>
        /// <param name="comments">remark</param>
        public static void SendMail(AdUser currentUser, RequestInfo request, FlowInfo flow, List<AdUser> nextUser, string comments)
        {
            switch (flow.Status)
            {
                case FlowStatus.PendingForMs:
                    SendMailToMs(request, currentUser, nextUser);
                    break;

                case FlowStatus.PendingForCost:
                    SendMailToCost(request, currentUser, nextUser);
                    break;

                case FlowStatus.PendingForIE:
                    SendMailToIE(request, currentUser, nextUser);
                    break;

                case FlowStatus.Returned:
                    break;

                case FlowStatus.Closed:
                    SendMailForClose(request, currentUser);
                    break;

                case FlowStatus.Rejected:
                    SendMailForReject(request, currentUser, comments);
                    break;

                case FlowStatus.Canceled:
                    SendMailForCancel(request, currentUser);
                    break;

                default:
                    break;
            }
        }

        #region Email function

        private static void SendMailForCancel(RequestInfo request, AdUser currentUser)
        {
            string subject = string.Format("Moonshine [{0}] [{1}] Request Cancel", request.ReqID.ToString(), request.User.DisplayName);
            string body = "Dear " + request.User.DisplayName + ","
                        + "<p>Moonshine Request had been Canceled.</p></br>"
                        + "<p>" + request.ReqInfo + "</p>"
                        + "<p>Approve link: <a href=\"" + Util.Common.GetCurrentUrl() + "Requests.aspx?reqid="
                        + request.ReqID.ToString() + "\">[" + request.ReqID.ToString() + "] </a></p><br/><br/>"
                        + EMAILBOTTOM;
            Util.Common.ClientSendMail(request.User.Email, RequestMgr.GetApprover("0").Email, subject, body);
        }

        private static void SendMailForReject(RequestInfo request, AdUser currentUser, string comments)
        {
            string subject = string.Format("Moonshine [{0}] [{1}] Request Cancel", request.ReqID.ToString("0"), request.User.DisplayName);
            string body = "Dear " + request.User.DisplayName + ","
                        + "<p>Moonshine Request had been rejected.</p></br>"
                        + "<p>" + request.ReqInfo + "</p>"
                        + "<p>Remark:<i>" + comments + "</i></p>"
                        + "<p>Approve link: <a href=\"" + Util.Common.GetCurrentUrl() + "Requests.aspx?reqid="
                         + request.ReqID.ToString() + "\">[" + request.ReqID.ToString() + "] </a></p><br/><br/>"
                        + EMAILBOTTOM;
            Util.Common.ClientSendMail(request.User.Email, RequestMgr.GetApprover("0").Email, subject, body);
        }

        private static void SendMailForClose(RequestInfo request, AdUser currentUser)
        {
            string subject = string.Format("Moonshine [{0}] [{1}] Request Close", request.ReqID.ToString(), request.User.DisplayName);
            string body = "Dear " + request.User.DisplayName + ","
                        + "<p>Moonshine Request had been closed.</p>"
                        + "<p>Approve link: <a href=\"" + Util.Common.GetCurrentUrl() + "Requests.aspx?reqid="
                        + request.ReqID.ToString() + "\">[" + request.ReqID.ToString() + "] </a></p><br/><br/>"
                        + EMAILBOTTOM;
            Util.Common.ClientSendMail(request.User.Email, RequestMgr.GetApprover("0").Email, subject, body);
        }

        private static void SendMailToMs(RequestInfo request, AdUser currentUser, List<AdUser> nextUser)
        {
            if (nextUser == null) return;
            if (nextUser.Count == 0) return;

            string subject = string.Format("Moonshine [{0}] [{1}] Request Need Your Approval", request.ReqID.ToString(), request.User.DisplayName);
            string to = GetUserMails(nextUser);
            string body = "Dear " + nextUser[0].DisplayName
                        + "<p>Moonshine Request had been submitted for your approval.</p>"
                        + "<p>" + request.ReqInfo + "</p>"
                        + "<p>Approve link: <a href=\"" + Util.Common.GetCurrentUrl() + "Requests.aspx?reqid="
                        + request.ReqID.ToString() + "\">[" + request.ReqID.ToString() + "] </a></p><br/><br/>"
                        + EMAILBOTTOM;
            Util.Common.ClientSendMail(to, currentUser.Email, subject, body);
        }

        private static void SendMailToCost(RequestInfo request, AdUser currentUser, List<AdUser> nextUser)
        {
            if (nextUser == null) return;
            if (nextUser.Count == 0) return;

            string subject = string.Format("Moonshine [{0}] [{1}] Request Need Your Approval", request.ReqID.ToString(), request.User.DisplayName);
            string to = GetUserMails(nextUser);
            string body = "Dear " + nextUser[0].DisplayName
                        + "<p>Moonshine Request had been submitted for your approval.</p>"
                        + "<p>" + request.ReqInfo + "</p>"
                        + "<p>Approve link: <a href=\"" + Util.Common.GetCurrentUrl() + "Requests.aspx?reqid="
                        + request.ReqID.ToString() + "\">[" + request.ReqID.ToString() + "] </a></p><br/><br/>"
                        + EMAILBOTTOM;
            Util.Common.ClientSendMail(to, request.User.Email, subject, body);
        }

        private static void SendMailToIE(RequestInfo request, AdUser currentUser, List<AdUser> nextUser)
        {
            if (nextUser == null) return;
            if (nextUser.Count == 0) return;

            string subject = string.Format("Moonshine [{0}] [{1}] Request Need Your Approval", request.ReqID.ToString(), request.User.DisplayName);
            string to = GetUserMails(nextUser);
            string body = "Dear " + nextUser[0].DisplayName
                       + "<p>Moonshine Request had been submitted for your approval.</p>"
                        + "<p>" + request.ReqInfo + "</p>"
                        + "<p>Approve link: <a href=\"" + Util.Common.GetCurrentUrl() + "Requests.aspx?reqid="
                        + request.ReqID.ToString() + "\">[" + request.ReqID.ToString() + "] </a></p><br/><br/>"
                        + EMAILBOTTOM;
            Util.Common.ClientSendMail(to, request.User.Email, subject, body);
        }

        private static string GetUserMails(List<AdUser> users)
        {
            string mails = string.Empty;

            foreach (AdUser u in users)
            {
                if (!string.IsNullOrEmpty(u.Email))
                {
                    mails += u.Email + ",";
                }
            }

            return mails.TrimEnd(',');
        }

        #endregion Email function
    }
}