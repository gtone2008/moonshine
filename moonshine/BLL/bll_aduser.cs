using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using moonshine.DAL;
using moonshine.Util;

namespace moonshine.BLL
{
    public class bll_aduser
    {
        private static readonly dal_aduser _Nozzles = new dal_aduser();

        public static Model.AdUser GetCurrentAdUser()
        {
            Model.AdUser user = null;
            if (HttpContext.Current.Session["ADUser"] == null)
            {
                string uid = Util.Common.GetCurrentNTID();

                user = GetUser(uid);

                if (user == null)
                {
                    user = Common.GetADUserEntity(uid);
                    SaveUser(user);
                }
                HttpContext.Current.Session["ADUser"] = user;
            }
            else
            {
                user = (Model.AdUser)HttpContext.Current.Session["ADUser"];
            }
            return user;
        }

        private static void SaveUser(Model.AdUser user)
        {
            _Nozzles.SaveUser(user);
        }

        public static Model.AdUser GetUser(string uid)
        {
            return _Nozzles.GetUser(uid);
        }

    }
}