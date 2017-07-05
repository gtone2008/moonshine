using moonshine.DAL;
using moonshine.Util;

namespace moonshine.BLL
{
    /// <summary>
    /// approver
    /// </summary>
    public partial class approver
    {
        private static readonly moonshine.DAL.approver dal = new moonshine.DAL.approver();

        public approver()
        { }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static bool Add(moonshine.Model.approver model)
        {
            return dal.Add(model);
        }

        public static string get()
        {
            string sqlStr = @"select * from approver";
            return JsonHelper.ToJson(MysqlHelper.ExecuteReader(sqlStr));
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static bool Update(moonshine.Model.approver model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static bool Delete(string costID)
        {
            return dal.Delete(costID);
        }
    }
}