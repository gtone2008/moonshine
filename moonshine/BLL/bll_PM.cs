using moonshine.DAL;
using moonshine.Util;

namespace moonshine.BLL
{
    public class bll_PM
    {
        private static readonly DAL.pm_history dal = new DAL.pm_history();

        public static string getData()
        {
            string sqlStr = @"select * from pm_history order by id desc ";
            return JsonHelper.ToJson(MysqlHelper.ExecuteReader(sqlStr));
        }

        public static bool editData(Model.pm_history model)
        {
            return dal.Update(model);
        }

        public static bool AddData(moonshine.Model.pm_history model)
        {
            return dal.Add(model);
        }

        public static bool DelData(string ID)
        {
            return dal.Delete(ID);
        }
    }
}