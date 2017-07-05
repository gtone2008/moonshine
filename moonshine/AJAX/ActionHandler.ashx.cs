using System;
using System.Web;

namespace moonshine.AJAX
{
    /// <summary>
    /// Summary description for ActionHandler
    /// </summary>
    public class ActionHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            var res = context.Response;
            var action = context.Request.QueryString["action"];
            res.ContentType = "text/plain";
            try
            {
                var admin = context.Request.Cookies["admin"].Value;
                if (admin == "1")
                {
                    switch (action)
                    {
                        case "getDataPM": res.Write(BLL.bll_PM.getData()); break;
                        case "editDataPM": res.Write(BLL.bll_PM.editData(getmodelPM(context))); break;
                        case "addDataPM": res.Write(BLL.bll_PM.AddData(getmodelPM(context))); break;
                        case "delDataPM": res.Write(BLL.bll_PM.DelData(context.Request.QueryString["ID"])); break;

                        case "getDataApprover": res.Write(BLL.approver.get()); break;
                        case "editDataApprover": res.Write(BLL.approver.Update(getmodelApprover(context))); break;
                        case "addDataApprover": res.Write(BLL.approver.Add(getmodelApprover(context))); break;
                        case "delDataApprover": res.Write(BLL.approver.Delete(context.Request.QueryString["costID"])); break;

                        case "getDataUser": res.Write(BLL.bll_roles_group.getData()); break;
                        case "editDataUser": res.Write(BLL.bll_roles_group.Update(getmodelUser(context))); break;
                        case "addDataUser": res.Write(BLL.bll_roles_group.Add(getmodelUser(context))); break;
                        case "delDataUser": res.Write(BLL.bll_roles_group.Delete(context.Request.QueryString["NTID"])); break;
                    }
                }
                else if (admin=="0")
                {
                    switch (action)
                    {
                        case "getDataPM": res.Write(BLL.bll_PM.getData()); break;


                        case "getDataApprover": res.Write(BLL.approver.get()); break;


                        case "getDataUser": res.Write(BLL.bll_roles_group.getData()); break;

                    }
                }
            }
            catch (Exception ex)
            {
                res.Write("非法操作！");
            }            
            
        }

        public Model.pm_history getmodelPM(HttpContext context)
        {
            var req = context.Request.QueryString;
            Model.pm_history model = new Model.pm_history
            {
                ID = Convert.ToInt32(req["ID"]),
                occur_date = req["occur_date"],
                location = req["location"],
                machine_type = req["machine_type"],
                machine_ID = req["machine_ID"],
                costCenter = req["costCenter"],
                type = req["type"],
                rootCause = req["rootCause"],
                solution = req["solution"],
                cost = req["cost"],
                operator1 = req["operator"],
                solve_date = req["solve_date"],
                condition = req["condition"]
            };
            return model;
        }

        public Model.approver getmodelApprover(HttpContext context)
        {
            var req = context.Request;
            Model.approver model = new Model.approver
            {
                costID = req.QueryString["costID"],
                costName = req.QueryString["costName"],
                approverNTID = req.QueryString["approverNTID"],
                approverName = req.QueryString["approverName"]
            };
            return model;
        }

        public Model.roles_group getmodelUser(HttpContext context)
        {
            var req = context.Request;
            Model.roles_group model = new Model.roles_group
            {
                NTID = req.QueryString["NTID"],
                power = req.QueryString["power"]
            };
            return model;
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